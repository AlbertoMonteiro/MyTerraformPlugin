using System.Collections.Frozen;

namespace MyTerraformPlugin.ResourceProvider;

public class DataSourceFinder : IDataSourceFinder
{
    private readonly FrozenDictionary<string, IDataSourceProvider> _dataSourceProviders;

    public DataSourceFinder(IEnumerable<IDataSourceProvider> dataSourceProviders)
        => _dataSourceProviders = dataSourceProviders.ToFrozenDictionary(x => $"dotnetsample_{x.Name}", x => x);

    public IDataSourceProvider GetDataSourceProvider(string name)
        => _dataSourceProviders[name];
}