using MessagePack;
using MyTerraformPlugin.Resources;
using MyTerraformPlugin.Schemas;
using MyTerraformPlugin.Schemas.Types;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

[SchemaVersion(1)]
[MessagePackObject]
public class Configuration : ITerraformSchema
{
    [Key("dummy_data")]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Data { get; set; }

    public Schema GetSchema()
        => new()
        {
            Version = 1,
            Block = new Schema.Types.Block()
            {
                Attributes =
                {
                    new Schema.Types.Attribute() { Name = "dummy_data", Type = TfTypes.String, Description = "Dummy data returned by the data source provider.", Required = false, Optional = true, Computed = false, Sensitive = false }
                }
            },
        };
}