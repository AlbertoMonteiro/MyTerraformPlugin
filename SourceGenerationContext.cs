using System.Text.Json.Serialization;

namespace MyTerraformPlugin;

[JsonSerializable(typeof(Configuration))]
[JsonSerializable(typeof(SampleDataSource))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}