namespace MyTerraformPlugin.ResourceProvider;

public interface IDataSourceProvider
{
    Task<ReadDataSource.Types.Response> ReadDataSource(ReadDataSource.Types.Request request);

    string Name { get; }
}