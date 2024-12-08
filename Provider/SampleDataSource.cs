using MessagePack;
using MyTerraformPlugin.Resources;
using MyTerraformPlugin.Serialization;

namespace MyTerraformPlugin;

[SchemaVersion(1)]
[MessagePackObject]
public class SampleDataSource : ITerraformDataSource
{
    [Key("id")]
    [Required]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Id { get; set; }

    [Key("data")]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Data { get; set; }

    public string GetName() => "sampledata";

    public Schema GetSchema()
        => new()
        {
            Version = 1,
            Block = new Schema.Types.Block()
            {
                Attributes =
                {
                    new Schema.Types.Attribute() { Name = "id", Type = TfTypes.String, Description = "Id", Required = true, Optional = false, Computed = false, Sensitive = false },
                    new Schema.Types.Attribute() { Name = "data", Type = TfTypes.String, Description = "Dummy data", Required = true, Optional = false, Computed = false, Sensitive = false }
                }
            },
        };
}