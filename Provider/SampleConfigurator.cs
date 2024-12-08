using MyTerraformPlugin.ProviderConfig;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

public class SampleConfigurator(IDynamicValueSerializer serializer, Configuration config) : IProviderConfiguration
{
    private readonly IDynamicValueSerializer _serializer = serializer;

    public Configuration Config { get; private set; } = config;

    public ValueTask ConfigureAsync(Configure.Types.Request request)
    {
        var cfg = _serializer.DeserializeDynamicValue<Configuration>(request.Config);
        Config.Data = cfg.Data;
        return ValueTask.CompletedTask;
    }

    public Schema GetConfigurationSchema()
        => Config.GetSchema();
}
