using MyTerraformPlugin.ProviderConfig;
using MyTerraformPlugin.ResourceProvider;
using MyTerraformPlugin.Schemas;
using MyTerraformPlugin.Schemas.Types;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTerraformPluginCore(this IServiceCollection services)
    {
        services.AddTransient<ITerraformTypeBuilder, TerraformTypeBuilder>();
        services.AddTransient<ISchemaBuilder, SchemaBuilder>();
        services.AddTransient(typeof(ProviderConfigurationHost<>));
        services.AddTransient(typeof(ResourceProviderHost<>));
        services.AddTransient(typeof(DataSourceProviderHost<>));
        services.AddTransient(typeof(IResourceUpgrader<>), typeof(DefaultResourceUpgrader<>));
        services.AddTransient<IDynamicValueSerializer, DefaultDynamicValueSerializer>();
        return services;
    }

    public static IResourceRegistryContext AddTerraformResourceRegistry(this IServiceCollection services)
    {
        services.AddOptions<TerraformPluginHostOptions>().ValidateDataAnnotations();
        services.AddSingleton<ResourceRegistry>();

        var registryContext = new ServiceCollectionResourceRegistryContext(services);
        return registryContext;
    }

    /// <summary>
    /// Adds a configurator that will be called when configuring this terraform plugin.
    /// </summary>
    public static IServiceCollection AddTerraformProviderConfigurator<TConfig, TProviderConfigurator>(this IServiceCollection services)
        where TProviderConfigurator : IProviderConfigurator<TConfig>
        where TConfig : ITerraformSchema
    {
        services.AddSingleton(s => new ProviderConfigurationRegistry(
            ConfigurationSchema: s.GetRequiredService<ISchemaBuilder>().BuildSchema<TConfig>(),
            ConfigurationType: typeof(TConfig)));

        services.AddTransient<IProviderConfigurator<TConfig>>(s => s.GetRequiredService<TProviderConfigurator>());
        return services;
    }
}