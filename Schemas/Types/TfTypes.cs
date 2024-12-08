using Google.Protobuf;

namespace MyTerraformPlugin.Schemas.Types;

public static class TfTypes
{
    public static ByteString String { get; } = ByteString.CopyFromUtf8("\"string\"");
}