using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.Net.MediatR.Pipeline;
using Alexa.Net.MediatR.Wrappers;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests.Wrappers;

public class RequestHandlerWrapperImplTests
{
    public class RequestHandler : IRequestHandler<IntentRequest>
    {
        public Task<bool> CanHandle(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(input.RequestEnvelope.Request is IntentRequest intent && intent.Intent.Name == "Handled");
        }
        public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ResponseBuilder.Tell("From Handler"));
        }
    }

    public class DefaultRequestHandler : IDefaultRequestHandler
    {
        public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ResponseBuilder.Tell("From Default"));
        }
    }
    
    public class EmptyPipelineBehavior : IPipelineBehavior
    {
        public Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken, RequestHandlerDelegate next)
        {
            return next();
       }
    }

    [Fact]
    public async Task Handle_WithFoundHandler_ReturnsResponse()
    {
        var skillRequest = CreateDefaultSkillRequest();
        var handlerWrapper = new RequestHandlerWrapperImpl<IntentRequest>();

        var response = await handlerWrapper.Handle(skillRequest, CancellationToken.None,
            CreateDefaultServiceFactory(skillRequest));

        response.Should().NotBeNull().And.BeOfType<SkillResponse>();
        response.Response.OutputSpeech.Should().BeOfType<PlainTextOutputSpeech>();
        var outputSpeech = response.Response.OutputSpeech as PlainTextOutputSpeech;
        outputSpeech!.Text.Should().Be("From Handler");
    }

    [Fact]
    public async Task Handle_WithoutFoundHandler_ReturnsResponse()
    {
        var skillRequest = CreateDefaultSkillRequest("Unhandled");
        var handlerWrapper = new RequestHandlerWrapperImpl<IntentRequest>();

        var response = await handlerWrapper.Handle(skillRequest, CancellationToken.None,
            CreateDefaultServiceFactory(skillRequest));

        response.Should().NotBeNull().And.BeOfType<SkillResponse>();
        response.Response.OutputSpeech.Should().BeOfType<PlainTextOutputSpeech>();
        var outputSpeech = response.Response.OutputSpeech as PlainTextOutputSpeech;
        outputSpeech!.Text.Should().Be("From Default");
    }

    [Fact]
    public void Handle_WithoutDefaultHandler_ThrowsException()
    {
        var skillRequest = CreateDefaultSkillRequest("Unhandled");
        var handlerWrapper = new RequestHandlerWrapperImpl<IntentRequest>();

        Func<Task> act = async () =>
            await handlerWrapper.Handle(skillRequest, CancellationToken.None, CreateDefaultServiceFactory(skillRequest, false));

        act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public void Handle_WithGetInstancesException_ThrowsException()
    {
        var skillRequest = CreateDefaultSkillRequest();
        ServiceFactory serviceFactory = (type) =>
        {
            object returnObj = type switch
            {
                Type t when t == typeof(IHandlerInput) => Mock.Of<IHandlerInput>(),
                Type t when t == typeof(IEnumerable<IPipelineBehavior>) => new List<IPipelineBehavior>{new EmptyPipelineBehavior()},
                _ => throw new NotSupportedException()
            };
            return returnObj;
        };
        var handlerWrapper = new RequestHandlerWrapperImpl<IntentRequest>();

        Func<Task> act = async () => await handlerWrapper.Handle(skillRequest, CancellationToken.None, serviceFactory);

        act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public void Handle_WithNullGetInstances_ThrowsException()
    {
        var skillRequest = CreateDefaultSkillRequest();
        ServiceFactory serviceFactory = (type) =>
        {
            object returnObj = type switch
            {
                Type t when t == typeof(IHandlerInput) => Mock.Of<IHandlerInput>(),
                Type t when t == typeof(IEnumerable<IPipelineBehavior>) => new List<IPipelineBehavior>{new EmptyPipelineBehavior()},
                _ => null
            };
            return returnObj!;
        };
        var handlerWrapper = new RequestHandlerWrapperImpl<IntentRequest>();

        Func<Task> act = async () => await handlerWrapper.Handle(skillRequest, CancellationToken.None, serviceFactory);

        act.Should().ThrowAsync<InvalidOperationException>();
    }

    private static SkillRequest CreateDefaultSkillRequest(string intentName = "Handled")
    {
        return new SkillRequest
        {
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

    private static ServiceFactory CreateDefaultServiceFactory(SkillRequest skillRequest, bool includeDefaultHandler = true)
    {
        var mockHandlerInput = new Mock<IHandlerInput>();
        mockHandlerInput.SetupGet(input => input.RequestEnvelope).Returns(skillRequest);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IRequestHandler<IntentRequest>, RequestHandler>();
        if (includeDefaultHandler)
            serviceCollection.AddTransient<IDefaultRequestHandler, DefaultRequestHandler>();
        serviceCollection.AddTransient<IHandlerInput>(p => mockHandlerInput.Object);

        var services = serviceCollection.BuildServiceProvider();

        return services.GetRequiredService;
    }
}