namespace MyTerraformPlugin;

public interface ITerraformDataSource : ITerraformSchema
{
    string GetName();
}
