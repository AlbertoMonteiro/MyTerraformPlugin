namespace MyTerraformPlugin.ResourceProvider;

public interface IDataSourceFinder
{
    IDataSourceProvider GetDataSourceProvider(string name);
}
