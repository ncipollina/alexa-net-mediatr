# Alexa Skills Mediator

## Package Version
| Build Status                                                                                                                                                                                      | Nuget                                                                                                                |
|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------|
| [![.Net Build and Package](https://github.com/ncipollina/alexa-net-mediatr/actions/workflows/build.yaml/badge.svg)](https://github.com/ncipollina/alexa-net-mediatr/actions/workflows/build.yaml) | [![NuGet Badge](https://buildstats.info/nuget/alexa.net.mediatr)](https://www.nuget.org/packages/Alexa.Net.MediatR/) |

Simple mediator implementation for Alexa Skill development  in .NET

## Installation

Install the package via nuget:

```powershell

Install-Package Alexa.Net.MediatR

```

Or via the .Net command line interface

```bash

dotnet add package Alexa.Net.MediatR

```

## Setup

After installing the package, there are two delegates that need to be configured, a service factory delegate, which is used to instantiate all the handlers, the pipelines, the request and response interceptors, the exception handlers, the attributes manager, the handler input, and the default handler.

The other delegate that must be configured is the skill request factory, which is used to inject the incoming `SkillRequest` into the `IAttributesManager` for request and session attributes.

## .Net

The easistet way to set up your skill mediator is to use the [Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/Alexa.Net.MediatR.Extensions.Microsoft.DependencyInjection/) package which includes several `IServiceCollection` extension methods, which allow you to register all of your custom handlers and configuration from a given assemly or set of assemblies. This package also has a depenency on the [Alexa.Net.Lambda](https://www.nuget.org/packages/Alexa.Net.Lambda/) package which provides the ability to host your Skill via AWS Lambda with Dependency Injection. An example from the `Init` method:

```c#

protected override void Init(IHostBuilder builder){
    builder.ConfigureServices((context, services) => {
        services.AddSkillMediatorFromAssemblies(context.Configuration, nameof(AlexaSkillOptions), typeof(Function).GetTypeInfo().Assembly);
    })
}

```

## Basics

The Skill Mediator provides the ability to handler request messages for a given `Request` types. It also provides the ability to intercept those messages and apply changes to the incoming request and the outgoing response.

### Request Handling

The base `IRequestHandler` interface contains two methods, `CanHandle` and `Handle`. `CanHandle` includes the logic to determine if an incoming request can be handled by the handler.

The `IRequestHandler<TRequestType>` interface is the interface that should be implemented for request handling. The skill mediator will inspect incoming requests to determine the request type, and try and find the appropriate Handler of that type.

```c#

public class LaunchRequestHandler : IRequestHandler<LaunchRequest>
{
    public Task<bool> CanHandler(IHandlerInput input, CancellationToken cancellationToken) => Task.FromResult(true);
    
    public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken)
    {
        return input.ResponseBuilder.Tell($"Response from {nameof(LaunchRequestHandler)}", cancellationToken);
    }
}

```

The above class will handle requests of `LaunchRequest` type. The `IHandlerInput` contains references to information that will be needed to process requests, the incoming `SkillRequest`, the instance of `IAttributesManager`, and the `IResponseBuilder` to help generate responses Alexa can understand.

> *** Note: unless you add the handlers to the underlying DI container manually, there is no guarantee as to what order they will be registered. As the developer, you are responsible for ensuring that there is no overlap in the handlers `CanHandle` methods.