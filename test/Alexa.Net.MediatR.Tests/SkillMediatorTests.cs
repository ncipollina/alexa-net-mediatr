using System;
using System.Threading;
using System.Threading.Tasks;
using Alexa.Net.MediatR.Options;
using Alexa.NET.Request;
using Alexa.NET.Response;
using FluentAssertions;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests;

public class SkillMediatorTests
{
    [Fact]
    public void Ctor_NullServiceFactory_ThrowsException()
    {
        Action act = () => new SkillMediator(null, Mock.Of<AlexaSkillOptions>());

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_NullOptions_ThrowsException()
    {
        Action act = () => new SkillMediator(Mock.Of<ServiceFactory>(), null);

        act.Should().Throw<ArgumentNullException>();
    }


    [Theory]
    [InlineData("", "0")]
    [InlineData("  ", "0")]
    [InlineData(null, "0")]
    [InlineData("0", "1")]
    [InlineData("0", "")]
    [InlineData("0", "  ")]
    [InlineData("0", null)]
    public void Send_InvalidSkillId_ThrowsException(string skillId, string applicationId)
    {
        var skillRequest = CreateDefaultSkillRequest(applicationId);
        var skillMediator = CreateDefaultSkillMediator(CreateDefaultSkillOptions(skillId));

        Func<Task> act = async () => await skillMediator.Send(skillRequest);

        act.Should().ThrowAsync<ArgumentException>();
    }

    private static AlexaSkillOptions CreateDefaultSkillOptions(string skillId = "0")
    {
        return new AlexaSkillOptions
        {
            SkillId = skillId
        };
    }

    private static SkillRequest CreateDefaultSkillRequest(string applicationId = "0")
    {
        return new SkillRequest
        {
            Context = new Context
            {
                System = new AlexaSystem
                {
                    Application = new Application
                    {
                        ApplicationId = applicationId
                    }
                }
            }
        };
    }

    private static SkillMediator CreateDefaultSkillMediator(AlexaSkillOptions skillOptions)
    {
        var mockHandlerInput = new Mock<IHandlerInput>();
        var mockServiceFactory = new Mock<ServiceFactory>();
        mockServiceFactory.Setup(factory => factory(It.IsAny<Type>())).Returns(mockHandlerInput.Object);
        var skillMediator = new SkillMediator(mockServiceFactory.Object, skillOptions);
        return skillMediator;
    }

}