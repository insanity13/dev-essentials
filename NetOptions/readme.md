# NetOptions Library

## Overview

NetOptions is a simple yet powerful library that provides automatic registration of configuration classes in .NET applications. By using the `[Options]` attribute, you can easily mark your configuration classes and have them automatically registered with the dependency injection system.

## Features

- Automatic registration of options classes
- Configuration binding with validation
- Support for DataAnnotations validation
- Option to validate configurations on application start
- Clean and minimal API


## Usage

### 1. Decorate your options class

Simply mark your configuration class with the `[Options]` attribute:

```csharp
[Options("TodoSettings:Integration")]
internal class TodoIntegrationSettings
{
    [Required(ErrorMessage = $"{nameof(Url)} required")]
    public string Url { get; set; }
}
```

### 2. Register all options

In your dependency injection setup, call the extension method:

```csharp
services.AddAllOptions();
```

This will generate and register all your options classes automatically.

## Attribute Parameters

The `OptionsAttribute` accepts the following parameters:

1. `section` (required): The configuration section name to bind to
2. `validateOnStart` (optional, default: true): Whether to validate the configuration on application startup

## Generated Code Example

The source generator will produce code similar to this:

```csharp
namespace NetOptions.Core
{
    public static partial class OptionsRegistrationExtensions
    {
        public static IServiceCollection AddAllOptions(this IServiceCollection services)
        {
            services.AddOptions<Infrastructure.Settings.TodoIntegrationSettings>()
                   .BindConfiguration("TodoSettings:Integration")
                   .ValidateDataAnnotations()
                   .ValidateOnStart();
                   
            return services;
        }
    }
}
```

## Requirements
- .NET 6.0 or later
- Microsoft.Extensions.Options.ConfigurationExtensions
- Microsoft.Extensions.DependencyInjection

## Benefits
- Reduces boilerplate code for options registration
- Ensures consistent registration pattern across all options
- Automatic validation support
- Compile-time generation for better performance
