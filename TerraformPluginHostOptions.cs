﻿using MyTerraformPlugin.Resources;

namespace MyTerraformPlugin;

public class TerraformPluginHostOptions
{
    /// <summary>
    /// The full provider name. For example, `example.com/example/dotnetsample`.
    /// </summary>
    [Required]
    public string FullProviderName { get; set; } = null!;

    /// <summary>
    /// Configures debug mode which listens on H2C instead of TLS.
    /// </summary>
    public bool DebugMode { get; set; }
}