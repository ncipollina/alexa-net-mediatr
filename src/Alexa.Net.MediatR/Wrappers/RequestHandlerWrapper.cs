using Alexa.Net.MediatR.Pipeline;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Alexa.Net.MediatR.Wrappers;

public abstract class RequestHandlerWrapper : HandlerBase
{
    public abstract Task<SkillResponse> Handle(SkillRequest request, CancellationToken cancellationToken,
        ServiceFactory serviceFactory);
}

public class RequestHandlerWrapperImpl<TRequestType> : RequestHandlerWrapper where TRequestType : Request
{
    public override Task<SkillResponse> Handle(SkillRequest request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
    {
        var handlerInput = serviceFactory.GetInstance<IHandlerInput>();

        async Task<SkillResponse> Handler()
        {
            var handlers = GetHandlers<IRequestHandler<TRequestType>>(serviceFactory);
            foreach (var handler in handlers)
            {
                if (await handler.CanHandle(handlerInput, cancellationToken))
                {
                    return await handler.Handle(handlerInput, cancellationToken);
                }
            }

            var defaultHandler = serviceFactory.GetInstance<IDefaultRequestHandler>();
            if (defaultHandler is not null && await defaultHandler.CanHandle(handlerInput, cancellationToken))
            {
                return await defaultHandler.Handle(handlerInput, cancellationToken);
            }

            throw new InvalidOperationException(
                $"Handler was not found for request of type {typeof(IRequestHandler<TRequestType>)}. Register your handlers with the container.");
        }


        return serviceFactory
            .GetInstances<IPipelineBehavior>()
            .Reverse()
            .Aggregate((RequestHandlerDelegate)Handler,
                (next, pipeline) => () => pipeline.Handle(handlerInput, cancellationToken, next))();
    }
}