using MyTerraformPlugin.ProviderConfig;
using MyTerraformPlugin.ResourceProvider;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTerraformPluginCore(this IServiceCollection services)
    {
        services.AddTransient<IDynamicValueSerializer, DefaultDynamicValueSerializer>();
        return services;
    }

    public static IServiceCollection AddTerraformProviderConfigurator<T>(this IServiceCollection services)
        where T : class, IProviderConfiguration
    {
        services.AddSingleton<IDataSourceFinder, DataSourceFinder>();

        return services.AddSingleton<IProviderConfiguration, T>();
    }
}