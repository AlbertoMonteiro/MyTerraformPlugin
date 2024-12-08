using System.Text.Json;
using MessagePack;

namespace MyTerraformPlugin.Serialization;

public class DefaultDynamicValueSerializer : IDynamicValueSerializer
{
    public T DeserializeJson<T>(ReadOnlyMemory<byte> value)
    {
        return JsonSerializer.Deserialize<T>(value.Span, SourceGenerationContext.Default.Options)
             ?? throw new InvalidOperationException("Invalid Json provided");
    }

    public DynamicValue SerializeDynamicValue<T>(T value)
        => new() { Msgpack = Google.Protobuf.ByteString.CopyFrom(SerializeMsgPack(value)) };

    public T DeserializeDynamicValue<T>(DynamicValue value)
        => value switch
        {
            { Msgpack.IsEmpty: false } => DeserializeMsgPack<T>(value.Msgpack.Memory),
            { Json.IsEmpty: false } => DeserializeJson<T>(value.Json.Memory),
            _ => throw new ArgumentException("Either MessagePack or Json must be non-empty.", nameof(value))
        };

    private T DeserializeMsgPack<T>(ReadOnlyMemory<byte> value)
    {
        return MessagePackSerializer.Deserialize<T>(value);
    }

    private byte[] SerializeMsgPack<T>(T value)
    {
        return MessagePackSerializer.Serialize(value);
    }
}
