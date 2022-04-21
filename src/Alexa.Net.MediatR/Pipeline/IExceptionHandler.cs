using Alexa.NET.Response;

namespace Alexa.Net.MediatR.Pipeline;

public interface IExceptionHandler
{
    Task<bool> CanHandle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = default);

    Task<SkillResponse> Handle(IHandlerInput handlerInput, Exception ex, CancellationToken cancellationToken = default);
}