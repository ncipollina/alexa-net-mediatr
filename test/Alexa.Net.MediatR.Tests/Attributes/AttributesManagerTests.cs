using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alexa.Net.MediatR.Attributes;
using Alexa.Net.MediatR.Attributes.Persistence;
using Alexa.NET.Request;
using FluentAssertions;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests.Attributes;

public class AttributesManagerTests
{
    [Fact]
    public void Ctor_NullSkillRequestFactory_ThrowsException()
    {
        Action act = () => new AttributesManager(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_NullEventRequest_ThrowsException()
    {
        var mockSkillRequestFactory = new Mock<SkillRequestFactory>();
        mockSkillRequestFactory.Setup(factory => factory()).Returns((SkillRequest)null!);

        Action act = () => new AttributesManager(mockSkillRequestFactory.Object);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task GetRequestAttributes_ReturnsAttributes()
    {
        var attributesManager = CreateDefaultAttributesManager(CreateDefaultSkillRequest());

        var attributes = await attributesManager.GetRequestAttributes();

        attributes.Should().NotBeNull().And.BeOfType<Dictionary<string, object>>();
    }

    [Fact]
    public void GetSessionAttributes_NullSession_ThrowsException()
    {
        var request = CreateDefaultSkillRequest(false);
        var attributesManager = CreateDefaultAttributesManager(request);

        Func<Task> act = async () => await attributesManager.GetSessionAttributes();

        act.Should().ThrowAsync<MissingMemberException>();
    }

    [Fact]
    public async Task GetSessionAttributes_WithSession_ReturnsAttributes()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);

        var attributes = await attributesManager.GetSessionAttributes();
        
        attributes.Should().NotBeNull().And.BeOfType<Dictionary<string, object>>();
    }

    [Fact]
    public void GetPersistentAttributes_WithNullAdapter_ThrowsException()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request, false);

        Func<Task> act = async () => await attributesManager.GetPersistentAttributes();

        act.Should().ThrowAsync<MissingMemberException>();
    }

    [Fact]
    public async Task GetPersistentAttributes_WithSession_ReturnsAttributes()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);

        var attributes = await attributesManager.GetPersistentAttributes();
        
        attributes.Should().NotBeNull().And.BeOfType<Dictionary<string, object>>();
    }

    [Fact]
    public async Task SetPersistentAttributes_WithAttributes_ReturnsAttributes()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);
        var attributes = new Dictionary<string, object>();

        await attributesManager.SetRequestAttributes(attributes);
        var newAttributes = await attributesManager.GetRequestAttributes();

        newAttributes.Should().NotBeNull().And.BeSameAs(attributes);
    }
    
    [Fact]
    public void SetSessionAttributes_NullSession_ThrowsException()
    {
        var request = CreateDefaultSkillRequest(false);
        var attributesManager = CreateDefaultAttributesManager(request);

        Func<Task> act = async () => await attributesManager.SetSessionAttributes(new Dictionary<string, object>());

        act.Should().ThrowAsync<MissingMemberException>();
    }

    [Fact]
    public async Task SetSessionAttributes_WithSession_ReturnsAttributes()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);
        var attributes = new Dictionary<string, object>();

        await attributesManager.SetSessionAttributes(attributes);
        var newAttributes = await attributesManager.GetSessionAttributes();

        newAttributes.Should().NotBeNull().And.BeSameAs(attributes);
    }
    
    [Fact]
    public void SetPersistentAttributes_WithNullAdapter_ThrowsException()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request, false);

        Func<Task> act = async () => await attributesManager.SetPersistentAttributes(new Dictionary<string, object>());

        act.Should().ThrowAsync<MissingMemberException>();
    }

    [Fact]
    public void SetPersistentAttributes_WithNullAdapter_DoesNotThrowsException()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);

        Func<Task> act = async () => await attributesManager.SetPersistentAttributes(new Dictionary<string, object>());

        act.Should().NotThrowAsync();
    }

    [Fact]
    public void SavePersistentAttributes_WithNullAdapter_ThrowsException()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request, false);

        Func<Task> act = async () => await attributesManager.SavePersistentAttributes();

        act.Should().ThrowAsync<MissingMemberException>();
    }

    [Fact]
    public void SavePersistentAttributes_WithNullAdapter_DoesNotThrowsException()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);

        Func<Task> act = async () =>
        {
            await attributesManager.SetPersistentAttributes(new Dictionary<string, object>());
            await attributesManager.SavePersistentAttributes();
        };

        act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task GetSession_WithSession_ReturnsSession()
    {
        var request = CreateDefaultSkillRequest();
        var attributesManager = CreateDefaultAttributesManager(request);

        var session = await attributesManager.GetSession();

        session.Should().NotBeNull().And.BeOfType<Session>();
    }

    private static SkillRequest CreateDefaultSkillRequest(bool sessionNotNull = true)
    {
        return new SkillRequest
        {
            Session = sessionNotNull
                ? new Session { Attributes = new Dictionary<string, object> { { "0", "1" } } }
                : null
        };
    }

    private static AttributesManager CreateDefaultAttributesManager(SkillRequest request, bool persistenceNotNull = true)
    {
        var mockPersistencAdapter = new Mock<IPersistenceAdapter>();
        mockPersistencAdapter
            .Setup(adapter => adapter.GetAttributes(It.IsAny<SkillRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<string, object> { { "0", "1" } });
        var mockSkillRequestFactory = new Mock<SkillRequestFactory>();
        mockSkillRequestFactory.Setup(factory => factory()).Returns(request);
        var attributesManager = new AttributesManager(mockSkillRequestFactory.Object,
            persistenceNotNull ? mockPersistencAdapter.Object : null);
        return attributesManager;
    }
}