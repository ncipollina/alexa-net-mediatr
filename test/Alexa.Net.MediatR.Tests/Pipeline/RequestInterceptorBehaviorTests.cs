using System;
using System.Threading;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.Net.MediatR.Options;
using Alexa.Net.MediatR.Pipeline;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests.Pipeline;

public class RequestInterceptorBehaviorTests
{
    public class RequestHandler : IRequestHandler<IntentRequest>
    {
        public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ResponseBuilder.Tell(input.RequestEnvelope.Request.Locale));
        }
    }

    public class RequestInterceptor : IRequestInterceptor
    {
        public Task Process(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            input.RequestEnvelope.Request.Locale = "fr-fr";
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task Send_WithInterceptor_ShouldChangeLocal()
    {
        var request = CreateDefaultSkillRequest();
        var mediator = CreateDefaultSkillMediator(request);

        var response = await mediator.Send(request);

        var outputSpeech = response.Response.OutputSpeech as PlainTextOutputSpeech;
        outputSpeech.Should().NotBeNull();
        outputSpeech!.Text.Should().Be("fr-fr");
    }

    private static SkillRequest CreateDefaultSkillRequest(string intentName = "")
    {
        return new SkillRequest
        {
            Context = new Context
            {
                System = new AlexaSystem
                {
                    Application = new Application
                    {
                        ApplicationId = "0"
                    }
                }
            },
            Request = new IntentRequest
            {
                Intent = new Intent
                {
                    Name = intentName
                },
                Locale = "en-US"
            }
        };
    }

    private static ISkillMediator CreateDefaultSkillMediator(SkillRequest request)
    {
        var mockSkillRequestFactory = new Mock<SkillRequestFactory>();
        mockSkillRequestFactory.Setup(factory => factory()).Returns(request);
        var mockInputHandler = new Mock<IHandlerInput>();
        mockInputHandler.SetupGet(input => input.RequestEnvelope).Returns(request);
        var serviceCollection = new ServiceCollection();
        serviceCollection.TryAddTransient<ServiceFactory>(p => p.GetRequiredService);
        serviceCollection.TryAddScoped(p => mockSkillRequestFactory.Object);
        serviceCollection.AddTransient<IRequestHandler<IntentRequest>, RequestHandler>();
        serviceCollection.AddTransient<IRequestInterceptor, RequestInterceptor>();
        serviceCollection.AddTransient(typeof(IPipelineBehavior), typeof(RequestInterceptorBehavior));
        serviceCollection.TryAdd(new ServiceDescriptor(typeof(ISkillMediator), typeof(SkillMediator),
            ServiceLifetime.Transient));
        serviceCollection.AddSingleton(new AlexaSkillOptions { SkillId = "0" });
        serviceCollection.AddTransient<IHandlerInput>(p => mockInputHandler.Object);

        var services = serviceCollection.BuildServiceProvider();
        return services.GetRequiredService<ISkillMediator>();
    }
}