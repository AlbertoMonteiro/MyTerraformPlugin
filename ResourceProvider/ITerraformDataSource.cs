using MyTerraformPlugin.Schemas;

namespace MyTerraformPlugin.ResourceProvider;

public interface ITerraformDataSource : ITerraformSchema
{
    string GetName();
}
