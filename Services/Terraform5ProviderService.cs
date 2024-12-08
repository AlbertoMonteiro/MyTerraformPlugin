using Grpc.Core;
using MyTerraformPlugin.ProviderConfig;
using MyTerraformPlugin.ResourceProvider;

namespace MyTerraformPlugin.Services;

class Terraform5ProviderService : Provider.ProviderBase
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IProviderConfiguration _providerConfiguration;
    private readonly IDataSourceFinder _dataSourceFinder;
    private readonly IEnumerable<ITerraformDataSource> _dataSources;

    public Terraform5ProviderService(IHostApplicationLifetime lifetime, IProviderConfiguration providerConfiguration1, IEnumerable<ITerraformDataSource> dataSources, IDataSourceFinder dataSourceFinder)
    {
        _lifetime = lifetime;
        _providerConfiguration = providerConfiguration1;
        _dataSources = dataSources;
        _dataSourceFinder = dataSourceFinder;
    }

    public override async Task<Configure.Types.Response> Configure(Configure.Types.Request request, ServerCallContext context)
    {
        if (_providerConfiguration is null)
        {
            return new Configure.Types.Response { };
        }

        await _providerConfiguration.ConfigureAsync(request);
        return new Configure.Types.Response { };
    }

    public override Task<GetProviderSchema.Types.Response> GetSchema(GetProviderSchema.Types.Request request, ServerCallContext context)
    {
        var res = new GetProviderSchema.Types.Response
        {
            Provider = _providerConfiguration.GetConfigurationSchema() ?? new Schema { Block = new Schema.Types.Block { } },
        };

        //foreach (var schema in _resourceRegistry.Schemas)
        //{
        //    res.ResourceSchemas.Add(schema.Key, schema.Value);
        //}

        foreach (var schema in _dataSources)
        {
            res.DataSourceSchemas.Add($"dotnetsample_{schema.GetName()}", schema.GetSchema());
        }

        return Task.FromResult(res);
    }

    public override Task<PlanResourceChange.Types.Response> PlanResourceChange(PlanResourceChange.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new PlanResourceChange.Types.Response
        {
            Diagnostics =
                    {
                        new Diagnostic { Detail = "Unknown type name." },
                    },
        });
    }

    public override Task<ApplyResourceChange.Types.Response> ApplyResourceChange(ApplyResourceChange.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new ApplyResourceChange.Types.Response
        {
            Diagnostics =
                    {
                        new Diagnostic { Detail = "Unknown type name." },
                    },
        });
    }

    public override Task<UpgradeResourceState.Types.Response> UpgradeResourceState(UpgradeResourceState.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new UpgradeResourceState.Types.Response
        {
            Diagnostics =
                    {
                        new Diagnostic { Detail = "Unknown type name." },
                    },
        });
    }

    public override Task<ReadResource.Types.Response> ReadResource(ReadResource.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new ReadResource.Types.Response
        {
            Diagnostics =
                    {
                        new Diagnostic { Detail = "Unknown type name." },
                    },
        });
    }

    public override Task<ImportResourceState.Types.Response> ImportResourceState(ImportResourceState.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new ImportResourceState.Types.Response
        {
            Diagnostics =
                    {
                        new Diagnostic { Detail = "Unknown type name." },
                    },
        });
    }

    public override Task<PrepareProviderConfig.Types.Response> PrepareProviderConfig(PrepareProviderConfig.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new PrepareProviderConfig.Types.Response());
    }

    public override Task<Stop.Types.Response> Stop(Stop.Types.Request request, ServerCallContext context)
    {
        _lifetime.StopApplication();
        _lifetime.ApplicationStopped.WaitHandle.WaitOne();
        return Task.FromResult(new Stop.Types.Response());
    }

    public override Task<ValidateDataSourceConfig.Types.Response> ValidateDataSourceConfig(ValidateDataSourceConfig.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new ValidateDataSourceConfig.Types.Response());
    }

    public override Task<ValidateResourceTypeConfig.Types.Response> ValidateResourceTypeConfig(ValidateResourceTypeConfig.Types.Request request, ServerCallContext context)
    {
        return Task.FromResult(new ValidateResourceTypeConfig.Types.Response());
    }

    public override async Task<ReadDataSource.Types.Response> ReadDataSource(ReadDataSource.Types.Request request, ServerCallContext context)
    {
        try
        {
            var ds = _dataSourceFinder.GetDataSourceProvider(request.TypeName);

            var response = await ds.ReadDataSource(request);
            return response;
        }
        catch (Exception ex)
        {
            return new ReadDataSource.Types.Response
            {
                Diagnostics =
                        {
                            new Diagnostic { Detail = ex.ToString() },
                        },
            };
        }
    }
}
