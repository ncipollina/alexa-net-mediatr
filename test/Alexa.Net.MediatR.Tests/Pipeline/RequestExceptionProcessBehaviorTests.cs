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

public class RequestExceptionProcessBehaviorTests
{
    public class RequestHandler : IRequestHandler<IntentRequest>
    {
        public Task<bool> CanHandle(IHandlerInput input, CancellationToken cancellationToken = default) => Task.FromResult(true);
        
        public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            var intent = (input.RequestEnvelope.Request as IntentRequest);
            if (intent!.Intent.Name == "Invalid")
            {
                throw new InvalidOperationException("Invalid");
            }
            else
            {
                throw new ArgumentException("Argument");
            }
        }
    }
    
    public class ExceptionHandler : IExceptionHandler
    {
        public Task<bool> CanHandle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ex is InvalidOperationException);
        }

        public Task<SkillResponse> Handle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ResponseBuilder.Empty());
        }
    }

    [Fact]
    public async Task Send_WithHandleableException_ShouldBeHandled()
    {
        var request = CreateDefaultSkillRequest("Invalid");
        var mediator = CreateDefaultSkillMediator(request);

        var response = await mediator.Send(request);

        response.Should().NotBeNull();
        response.Should().BeOfType<SkillResponse>();
    }

    [Fact]
    public void SendWithUnhandleableException_ShouldThrowException()
    {
        var request = CreateDefaultSkillRequest();
        var mediator = CreateDefaultSkillMediator(request);

        Func<Task> act = async () => await mediator.Send(request);

        act.Should().ThrowAsync<ArgumentException>();
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
                }
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
        serviceCollection.AddTransient<IExceptionHandler, ExceptionHandler>();
        serviceCollection.AddTransient(typeof(IPipelineBehavior), typeof(RequestExceptionProcessBehavior));
        serviceCollection.TryAdd(new ServiceDescriptor(typeof(ISkillMediator), typeof(SkillMediator),
            ServiceLifetime.Transient));
        serviceCollection.AddSingleton(new AlexaSkillOptions { SkillId = "0" });
        serviceCollection.AddTransient<IHandlerInput>(p => mockInputHandler.Object);

        var services = serviceCollection.BuildServiceProvider();
        return services.GetRequiredService<ISkillMediator>();
    }
}