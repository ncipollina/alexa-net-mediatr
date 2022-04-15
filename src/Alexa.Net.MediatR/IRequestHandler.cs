using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Alexa.Net.MediatR;

public interface IRequestHandler
{
    public virtual Task<bool> CanHandle(IHandlerInput input, CancellationToken cancellationToken = default) =>
        Task.FromResult(true);

    Task<SkillResponse> Handle(IHandlerInput input, CancellationToken cancellationToken = default);
}

public interface IDefaultRequestHandler : IRequestHandler
{
}

public interface IRequestHandler<TRequestType> : IRequestHandler where TRequestType : NET.Request.Type.Request
{
}

public interface IIntentRequestHandler : IRequestHandler<IntentRequest>
{
    public new abstract Task<bool> CanHandle(IHandlerInput input, CancellationToken cancellationToken = default);
}

public delegate Task<bool> CanHandleFactory(IHandlerInput input, CancellationToken cancellationToken);