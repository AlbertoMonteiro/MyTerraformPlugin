using Google.Protobuf;

namespace MyTerraformPlugin;

public static class TfTypes
{
    public static ByteString String { get; } = ByteString.CopyFromUtf8("\"string\"");
}