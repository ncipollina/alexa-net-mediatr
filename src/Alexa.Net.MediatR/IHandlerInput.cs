using Alexa.Net.MediatR.Attributes;
using Alexa.Net.MediatR.Response;
using Alexa.NET.Request;

namespace Alexa.Net.MediatR;

public interface IHandlerInput
{
    SkillRequest RequestEnvelope { get; }
    
    IAttributesManager AttributesManager { get; }
    
    IResponseBuilder ResponseBuilder { get; }
}