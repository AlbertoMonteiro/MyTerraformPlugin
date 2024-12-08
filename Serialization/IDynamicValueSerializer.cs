
namespace MyTerraformPlugin.Serialization;

public interface IDynamicValueSerializer
{
    T DeserializeDynamicValue<T>(DynamicValue value);
    T DeserializeJson<T>(ReadOnlyMemory<byte> value);
    DynamicValue SerializeDynamicValue<T>(T value);
}
