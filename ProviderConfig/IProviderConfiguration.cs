namespace MyTerraformPlugin.ProviderConfig;

public interface IProviderConfiguration
{
    ValueTask ConfigureAsync(Configure.Types.Request request);

    Schema GetConfigurationSchema();
}
