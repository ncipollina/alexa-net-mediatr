using System;
using Alexa.Net.MediatR.Attributes;
using Alexa.Net.MediatR.Response;
using Alexa.NET.Request;
using FluentAssertions;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests;

public class DefaultHandlerInputTests
{
    [Fact]
    public void Ctor_NullRequestFactory_ThrowsException()
    {
        Action act = () => new DefaultHandlerInput(null, Mock.Of<IAttributesManager>(), Mock.Of<IResponseBuilder>());

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_NullRequest_ThrowsException()
    {
        var mockRequestFactory = new Mock<SkillRequestFactory>();
        mockRequestFactory.Setup(factory => factory()).Returns((SkillRequest)null!);
        Action act = () => new DefaultHandlerInput(mockRequestFactory.Object, Mock.Of<IAttributesManager>(), Mock.Of<IResponseBuilder>());

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_NullAttributesManager_ThrowsException()
    {
        var mockRequestFactory = new Mock<SkillRequestFactory>();
        mockRequestFactory.Setup(factory => factory()).Returns(new SkillRequest());
        Action act = () => new DefaultHandlerInput(mockRequestFactory.Object, null, Mock.Of<IResponseBuilder>());

        act.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void Ctor_NullResponseBuilder_ThrowsException()
    {
        var mockRequestFactory = new Mock<SkillRequestFactory>();
        mockRequestFactory.Setup(factory => factory()).Returns(new SkillRequest());
        Action act = () => new DefaultHandlerInput(mockRequestFactory.Object, Mock.Of<IAttributesManager>(), null);

        act.Should().Throw<ArgumentNullException>();
    }
}