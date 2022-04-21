using Alexa.Net.MediatR.Attributes;
using Alexa.Net.MediatR.Response;
using Alexa.NET.Request;

namespace Alexa.Net.MediatR;

public class DefaultHandlerInput : IHandlerInput
{
    public DefaultHandlerInput(SkillRequestFactory skillRequestFactory, IAttributesManager attributesManager, IResponseBuilder responseBuilder)
    {
        if (skillRequestFactory is null)
            throw new ArgumentNullException(nameof(skillRequestFactory));
        RequestEnvelope = skillRequestFactory() ?? throw new ArgumentNullException(nameof(RequestEnvelope));
        AttributesManager = attributesManager ?? throw new ArgumentNullException(nameof(attributesManager));
        ResponseBuilder = responseBuilder ?? throw new ArgumentNullException(nameof(responseBuilder));
    }
    public SkillRequest RequestEnvelope { get; }
    public IAttributesManager AttributesManager { get; }
    public IResponseBuilder ResponseBuilder { get; }
}