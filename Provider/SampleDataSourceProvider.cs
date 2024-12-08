using MyTerraformPlugin.ResourceProvider;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

public class SampleDataSourceProvider(IDynamicValueSerializer serializer, Configuration configuration) : IDataSourceProvider
{
    private readonly IDynamicValueSerializer _serializer = serializer;

    public string Name { get; } = "sampledata";

    public Task<SampleDataSource> ReadAsync(SampleDataSource request)
    {
        return Task.FromResult(new SampleDataSource
        {
            Id = $"{request.Id} from .NET",
            Data = configuration.Data ?? "No dummy data configured",
        });
    }

    public async Task<ReadDataSource.Types.Response> ReadDataSource(ReadDataSource.Types.Request request)
    {
        var current = _serializer.DeserializeDynamicValue<SampleDataSource>(request.Config);

        var read = await ReadAsync(current);
        var readSerialized = _serializer.SerializeDynamicValue(read);

        return new ReadDataSource.Types.Response
        {
            State = readSerialized,
        };
    }
}
