namespace Alexa.Net.MediatR.Wrappers;

public abstract class HandlerBase
{
    protected static IEnumerable<THandler> GetHandlers<THandler>(ServiceFactory factory)
    {
        IEnumerable<THandler> handlers;

        try
        {
            handlers = factory.GetInstances<THandler>();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container.", e);
        }

        if (handlers is null)
        {
            throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container.");
        }
        
        return handlers;
    }
}