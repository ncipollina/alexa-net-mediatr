using Alexa.NET.Response;

namespace Alexa.Net.MediatR.Pipeline;

public interface IResponseInterceptor
{
    Task Process(IHandlerInput input, SkillResponse output, CancellationToken cancellationToken = default);
}