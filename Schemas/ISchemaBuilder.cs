namespace MyTerraformPlugin.Schemas;

public interface ISchemaBuilder
{
    Schema BuildSchema<T>()
        where T : ITerraformSchema;
}
