#pragma warning disable
using MyTerraformPlugin.Schemas;

namespace MyTerraformPlugin.ResourceProvider;

class ResourceRegistry
{
    public ResourceRegistry(
        ISchemaBuilder schemaBulder,
        IEnumerable<ResourceRegistryRegistration> resourceRegistrations,
        IEnumerable<(string ResourceName, Schema schema, Type type)> dataSourceRegistrations)
    {
        //foreach (var registration in resourceRegistrations)
        //{
        //    Schemas.Add(registration.ResourceName, schemaBulder.BuildSchema(registration.Type));
        //    Types.Add(registration.ResourceName, registration.Type);
        //}
        foreach (var registration in dataSourceRegistrations)
        {
            DataSchemas.Add(registration.ResourceName, registration.schema);
            DataTypes.Add(registration.ResourceName, registration.type);
        }
    }

    public Dictionary<string, Schema> Schemas { get; } = new Dictionary<string, Schema>();

    public Dictionary<string, Schema> DataSchemas { get; } = new Dictionary<string, Schema>();

    public Dictionary<string, Type> Types { get; } = new Dictionary<string, Type>();

    public Dictionary<string, Type> DataTypes { get; } = new Dictionary<string, Type>();
}
