namespace MyTerraformPlugin.ResourceProvider;

public interface IResourceRegistryContext
{
    void RegisterResource<T>(string resourceName);

    void RegisterDataSource<T>(string dataSourceName)
        where T : ITerraformSchema;
}
