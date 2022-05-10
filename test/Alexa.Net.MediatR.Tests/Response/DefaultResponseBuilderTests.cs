using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alexa.Net.MediatR.Attributes;
using Alexa.Net.MediatR.Response;
using Alexa.NET.Request;
using Alexa.NET.Response;
using FluentAssertions;
using Moq;
using Xunit;

namespace Alexa.Net.MediatR.Tests.Response;

public class DefaultResponseBuilderTests
{
    [Theory]
    [InlineData("HelloWorld!", "<speak>HelloWorld!</speak>")]
    [InlineData("<speak>  HelloWorld!  </speak>", "<speak>HelloWorld!</speak>")]
    [InlineData("    <speak>  HelloWorld!  </speak>     ", "<speak>HelloWorld!</speak>")]
    [InlineData("<speak>HelloWorld!</speak>", "<speak>HelloWorld!</speak>")]
    [InlineData("", "<speak></speak>")]
    [InlineData(null, "<speak></speak>")]
    public async Task Speak_WithText_ReturnsSsmlOutputSpeech(string? speechOutput, string expectedOutput)
    {
        var responseBuilder = CreateDefaultResponseBuilder();

        var response = await responseBuilder.Speak(speechOutput).GetResponse();

        var outputSpeech = response.Response.OutputSpeech.Should().NotBeNull().And.BeOfType<SsmlOutputSpeech>().Subject;
        outputSpeech.Ssml.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("HelloWorld!", "<speak>HelloWorld!</speak>")]
    [InlineData("<speak>  HelloWorld!  </speak>", "<speak>HelloWorld!</speak>")]
    [InlineData("    <speak>  HelloWorld!  </speak>     ", "<speak>HelloWorld!</speak>")]
    [InlineData("<speak>HelloWorld!</speak>", "<speak>HelloWorld!</speak>")]
    [InlineData("", "<speak></speak>")]
    [InlineData(null, "<speak></speak>")]
    public async Task Reprompt_WithText_ReturnsSsmlOutputSpeech(string? repromptSpeechOutput, string expectedOutput)
    {
        var responseBuilder = CreateDefaultResponseBuilder();

        var response = await responseBuilder.Reprompt(repromptSpeechOutput).GetResponse();

        var outputSpeech = response.Response.Reprompt.OutputSpeech.Should().NotBeNull().And.BeOfType<SsmlOutputSpeech>().Subject;
        outputSpeech.Ssml.Should().Be(expectedOutput);
    }
    private static DefaultResponseBuilder CreateDefaultResponseBuilder()
    {
        var mockAttributesManager = new Mock<IAttributesManager>();
        mockAttributesManager.Setup(attributesManager => attributesManager.GetSession(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Session
            {
                Attributes = new Dictionary<string, object>()
            });
        var responseBuilder = new DefaultResponseBuilder(mockAttributesManager.Object);
        return responseBuilder;
    }
}