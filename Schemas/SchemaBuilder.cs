﻿#pragma warning disable
using Google.Protobuf;
using MyTerraformPlugin.Resources;
using MyTerraformPlugin.Schemas.Types;
using System.ComponentModel;
using System.Reflection;
using KeyAttribute = MessagePack.KeyAttribute;

namespace MyTerraformPlugin.Schemas;

class SchemaBuilder : ISchemaBuilder
{
    private readonly ILogger<SchemaBuilder> _logger;
    private readonly ITerraformTypeBuilder _typeBuilder;

    public SchemaBuilder(ILogger<SchemaBuilder> logger, ITerraformTypeBuilder typeBuilder)
    {
        _logger = logger;
        _typeBuilder = typeBuilder;
    }

    public Schema BuildSchema<T>()
        where T : ITerraformSchema
    {
        

        return T.GetSchema();

        var block = new Schema.Types.Block();

        //var properties = type.GetProperties();
        //foreach (var property in properties)
        //{
        //    var key = property.GetCustomAttribute<KeyAttribute>() ?? throw new InvalidOperationException($"Missing {nameof(KeyAttribute)} on {property.Name} in {type.Name}.");

        //    var description = property.GetCustomAttribute<DescriptionAttribute>();
        //    var required = TerraformTypeBuilder.IsRequiredAttribute(property);
        //    var computed = property.GetCustomAttribute<ComputedAttribute>() != null;
        //    var terraformType = _typeBuilder.GetTerraformType(property.PropertyType);

        //    if (terraformType is TerraformType.TfObject _ && !required)
        //    {
        //        throw new InvalidOperationException("Optional object types are not supported.");
        //    }

        //    block.Attributes.Add(new Schema.Types.Attribute
        //    {
        //        Name = key.StringKey,
        //        Type = ByteString.CopyFromUtf8(terraformType.ToJson()),
        //        Description = description?.Description,
        //        Optional = !required,
        //        Required = required,
        //        Computed = computed,
        //    });
        //}

        return new Schema
        {
            Version = 1,
            Block = block,
        };
    }
}
