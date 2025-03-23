namespace MyTerraformPlugin.ProviderConfig;

public interface IProviderConfiguration
{
    ValueTask ConfigureAsync(ConfigureProvider.Types.Request request);

    Schema GetConfigurationSchema();
}
