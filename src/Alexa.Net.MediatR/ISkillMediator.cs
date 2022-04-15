using Alexa.NET.Request;
using Alexa.NET.Response;

namespace Alexa.Net.MediatR;

public interface ISkillMediator
{
    Task<SkillResponse> Send(SkillRequest request, CancellationToken cancellationToken = default);
}