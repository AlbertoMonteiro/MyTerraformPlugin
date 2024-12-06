using MessagePack;
using MyTerraformPlugin.Resources;
using MyTerraformPlugin.Serialization;
using System.ComponentModel;

namespace MyTerraformPlugin;

[SchemaVersion(1)]
[MessagePackObject]
public class SampleDataSource
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
}
