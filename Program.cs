using MyTerraformPlugin;
using MyTerraformPlugin.ResourceProvider;
using MyTerraformPlugin.Services;

var builder = WebApplication.CreateBuilder(args);

var certificate = CertificateGenerator.GenerateSelfSignedCertificate("CN=127.0.0.1", "CN=root ca", CertificateGenerator.GeneratePrivateKey());

builder.WebHost.ConfigureKestrel(x =>
    // Listen on localhost port 5344
    x.ListenLocalhost(5344, x => x.UseHttps(x =>
    {
        // Use self-signed certificate generated earlier
        x.ServerCertificate = certificate;

        // Don't validate the client certificate
        x.AllowAnyClientCertificate();
    })));

builder.Logging.ClearProviders();

// Add services to the container.
var services = builder.Services;
services.AddGrpc();
services.AddTerraformPluginCore();
var registry = services.AddTerraformResourceRegistry();

services.AddSingleton<SampleConfigurator>();
services.AddTerraformProviderConfigurator<Configuration, SampleConfigurator>();
services.AddSingleton<IDataSourceProvider<SampleDataSource>, SampleDataSourceProvider>();
registry.RegisterDataSource<SampleDataSource>("dotnetsample_data");

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => Console.WriteLine($"1|5|tcp|127.0.0.1:5344|grpc|{Convert.ToBase64String(certificate.RawData)}"));

// Configure the HTTP request pipeline.
app.MapGrpcService<Terraform5ProviderService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
