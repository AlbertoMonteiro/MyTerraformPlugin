namespace MyTerraformPlugin.ProviderConfig;

public interface IProviderConfigurator<T>
{
    Task ConfigureAsync(T config);
}
