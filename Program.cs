using MyTerraformPlugin;
using MyTerraformPlugin.ResourceProvider;
using MyTerraformPlugin.Services;

var builder = WebApplication.CreateSlimBuilder(args);

var certificate = CertificateGenerator.GenerateSelfSignedCertificate("CN=127.0.0.1", "CN=root ca");

builder.WebHost.ConfigureKestrel(x =>
    x.ListenLocalhost(5344, x => x.UseHttps(x =>
    {
        x.ServerCertificate = certificate;

        x.AllowAnyClientCertificate();
    })));

builder.Logging.ClearProviders();

var services = builder.Services;
services.AddGrpc();
services.AddTerraformPluginCore();

services.AddSingleton(new Configuration());
services.AddTerraformProviderConfigurator<SampleConfigurator>();
services.AddSingleton<ITerraformDataSource, SampleDataSource>();
services.AddSingleton<IDataSourceProvider, SampleDataSourceProvider>();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => Console.WriteLine($"1|5|tcp|127.0.0.1:5344|grpc|{Convert.ToBase64String(certificate.RawData)}"));

app.MapGrpcService<Terraform5ProviderService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
