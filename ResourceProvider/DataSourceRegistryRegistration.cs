namespace MyTerraformPlugin.ResourceProvider;

record DataSourceRegistryRegistration<T>(string ResourceName)
        where T : ITerraformSchema
{
    public Schema Schema => T.GetSchema();
}
