using Google.Protobuf;
using MessagePack;
using System.ComponentModel;

namespace MyTerraformPlugin;

[MessagePackObject]
public class Configuration : ITerraformSchema
{
    [Key("dummy_data")]
    [Description("Dummy data returned by the data source provider.")]
    public string? Data { get; set; }

    public static Schema GetSchema()
    {
        var block = new Schema.Types.Block();

        block.Attributes.Add(new Schema.Types.Attribute() { Name = "dummy_data", Type = ByteString.CopyFromUtf8("\"string\""), Description = "Dummy data returned by the data source provider.", Required = false, Optional = true, Computed = false, Sensitive = false });

        return new Schema
        {
            Version = 1,
            Block = block,
        };
    }
}