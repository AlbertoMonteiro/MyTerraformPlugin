using System.Text.Json;
using MessagePack;

namespace MyTerraformPlugin.Serialization;

public class DefaultDynamicValueSerializer : IDynamicValueSerializer
{
    static readonly MessagePackSerializerOptions _messagePackOptions = MessagePackSerializerOptions.Standard.WithResolver(MessagePackAotResolver.Instance);

    public T DeserializeJson<T>(ReadOnlyMemory<byte> value)
    {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
        return JsonSerializer.Deserialize<T>(value.Span, SourceGenerationContext.Default.Options)
             ?? throw new InvalidOperationException("Invalid Json provided");
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
    }

    public DynamicValue SerializeDynamicValue<T>(T value)
        => new() { Msgpack = Google.Protobuf.ByteString.CopyFrom(MessagePackSerializer.Serialize(value, _messagePackOptions)) };

    public T DeserializeDynamicValue<T>(DynamicValue value)
        => value switch
        {
            { Msgpack.IsEmpty: false } => MessagePackSerializer.Deserialize<T>(value.Msgpack.Memory, _messagePackOptions),
            { Json.IsEmpty: false } => DeserializeJson<T>(value.Json.Memory),
            _ => throw new ArgumentException("Either MessagePack or Json must be non-empty.", nameof(value))
        };
}
