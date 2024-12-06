using MyTerraformPlugin.ResourceProvider;

namespace MyTerraformPlugin;

public class SampleDataSourceProvider : IDataSourceProvider<SampleDataSource>
{
    private readonly SampleConfigurator _configurator;

    public SampleDataSourceProvider(SampleConfigurator configurator)
    {
        _configurator = configurator;
    }

    public Task<SampleDataSource> ReadAsync(SampleDataSource request)
    {
        return Task.FromResult(new SampleDataSource
        {
            Id = request.Id,
            Data = _configurator.Config?.Data ?? "No dummy data configured",
        });
    }
}
