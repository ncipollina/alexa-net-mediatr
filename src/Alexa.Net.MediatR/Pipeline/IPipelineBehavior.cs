using Alexa.NET.Response;

namespace Alexa.Net.MediatR.Pipeline;

public delegate Task<SkillResponse> RequestHandlerDelegate();

public interface IPipelineBehavior
{
    Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken, RequestHandlerDelegate next);
}