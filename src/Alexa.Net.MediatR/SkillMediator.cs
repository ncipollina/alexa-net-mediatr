using System.Collections.Concurrent;
using Alexa.Net.MediatR.Options;
using Alexa.Net.MediatR.Wrappers;
using Alexa.NET.Request;
using Alexa.NET.Response;

namespace Alexa.Net.MediatR;

public class SkillMediator : ISkillMediator
{
    private readonly ServiceFactory _serviceFactory;
    private readonly AlexaSkillOptions _options;
    private static readonly ConcurrentDictionary<Type, RequestHandlerWrapper> RequestHandlers = new();

    public SkillMediator(ServiceFactory serviceFactory, AlexaSkillOptions options)
    {
        _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<SkillResponse> Send(SkillRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.SkillId) ||
            request.Context.System.Application.ApplicationId != _options.SkillId)
            throw new ArgumentException("Skill ID verification failed!");

        var requestType = request.Request.GetType();

        var handler = RequestHandlers.GetOrAdd(requestType,
            static t => (RequestHandlerWrapper)(Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<>)
                .MakeGenericType(t)) ?? throw new InvalidOperationException($"Could not create wrapper type for {t}")));

        return handler.Handle(request, cancellationToken, _serviceFactory);
    }
}