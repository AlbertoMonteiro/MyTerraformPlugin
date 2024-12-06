using Google.Protobuf;
using MessagePack;
using MyTerraformPlugin.Resources;
using MyTerraformPlugin.Serialization;
using System.ComponentModel;

namespace MyTerraformPlugin;

[SchemaVersion(1)]
[MessagePackObject]
public class SampleDataSource : ITerraformSchema
{
    [Key("id")]
    [Description("Id")]
    [Required]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Id { get; set; }

    [Key("data")]
    [Description("Dummy data.")]
    [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
    public string? Data { get; set; }

    public static Schema GetSchema()
    {
        var block = new Schema.Types.Block();
        block.Attributes.Add(new Schema.Types.Attribute() { Name = "id", Type = ByteString.CopyFromUtf8("\"string\""), Description = "Id", Required = true, Optional = false, Computed = false, Sensitive = false });
        block.Attributes.Add(new Schema.Types.Attribute() { Name = "data", Type = ByteString.CopyFromUtf8("\"string\""), Description = "Dummy data", Required = true, Optional = false, Computed = false, Sensitive = false });

        return new Schema
        {
            Version = 1,
            Block = block,
        };
    }
}
