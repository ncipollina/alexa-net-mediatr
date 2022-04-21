using Alexa.NET;
using Alexa.Net.MediatR.Attributes;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Ssml;

namespace Alexa.Net.MediatR.Response;

public class DefaultResponseBuilder : IResponseBuilder
{
    private readonly IAttributesManager _attributesManager;

    public DefaultResponseBuilder(IAttributesManager attributesManager)
    {
        _attributesManager = attributesManager ?? throw new ArgumentNullException(nameof(attributesManager));
    }

    public async Task<SkillResponse> Tell(IOutputSpeech speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Tell(speechResponse, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> Tell(string speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Tell(speechResponse, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> Tell(Speech speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Tell(speechResponse, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithReprompt(IOutputSpeech speechResponse, Reprompt reprompt,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithReprompt(speechResponse, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithReprompt(string speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithReprompt(speechResponse, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithReprompt(Speech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithReprompt(speechResponse, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithCard(IOutputSpeech speechResponse, string title, string content,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithCard(speechResponse, title, content,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithCard(string speechResponse, string title, string content, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithCard(speechResponse, title, content,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithCard(Speech speechResponse, string title, string content, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithCard(speechResponse, title, content,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithLinkAccountCard(IOutputSpeech speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithLinkAccountCard(speechResponse,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithLinkAccountCard(string speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithLinkAccountCard(speechResponse,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithLinkAccountCard(Speech speechResponse, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithLinkAccountCard(speechResponse,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithAskForPermissionsConsentCard(IOutputSpeech speechResponse, IEnumerable<string> permissions,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithAskForPermissionsConsentCard(speechResponse, permissions,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithAskForPermissionsConsentCard(string speechResponse, IEnumerable<string> permissions,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithAskForPermissionConsentCard(speechResponse, permissions,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> TellWithAskForPermissionsConsentCard(Speech speechResponse, IEnumerable<string> permissions,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.TellWithAskForPermissionConsentCard(speechResponse, permissions,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> Ask(IOutputSpeech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Ask(speechResponse, reprompt, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> Ask(string speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Ask(speechResponse, reprompt, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> Ask(Speech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.Ask(speechResponse, reprompt, await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> AskWithCard(IOutputSpeech speechResponse, string title, string content, Reprompt reprompt,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.AskWithCard(speechResponse, title, content, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> AskWithCard(string speechResponse, string title, string content, Reprompt reprompt,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.AskWithCard(speechResponse, title, content, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public async Task<SkillResponse> AskWithCard(Speech speechResponse, string title, string content, Reprompt reprompt,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.AskWithCard(speechResponse, title, content, reprompt,
            await _attributesManager.GetSession(cancellationToken));
    }

    public Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.AudioPlayerPlay(playBehavior, url, token));
    }

    public Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, int offsetInMilliseconds,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.AudioPlayerPlay(playBehavior, url, token, offsetInMilliseconds));
    }

    public Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, string expectedPreviousToken,
        int offsetInMilliseconds, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.AudioPlayerPlay(playBehavior, url, token, expectedPreviousToken, offsetInMilliseconds));
    }

    public Task<SkillResponse> AudioPlayerStop(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.AudioPlayerStop());
    }

    public Task<SkillResponse> AudioPlayerClearQueue(ClearBehavior clearBehavior, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.AudioPlayerClearQueue(clearBehavior));
    }

    public async Task<SkillResponse> DialogDelegate(Intent? updatedIntent = null, CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.DialogDelegate(await _attributesManager.GetSession(cancellationToken), updatedIntent);
    }

    public async Task<SkillResponse> DialogElicitSlot(IOutputSpeech outputSpeech, string slotName, Intent? updatedIntent = null,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.DialogElicitSlot(outputSpeech, slotName,
            await _attributesManager.GetSession(cancellationToken), updatedIntent);
    }

    public async Task<SkillResponse> DialogConfirmSlot(IOutputSpeech outputSpeech, string slotName, Intent? updatedIntent = null,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.DialogConfirmSlot(outputSpeech, slotName,
            await _attributesManager.GetSession(cancellationToken), updatedIntent);
    }

    public async Task<SkillResponse> DialogConfirmIntent(IOutputSpeech outputSpeech, Intent? updatedIntent = null,
        CancellationToken cancellationToken = default)
    {
        return ResponseBuilder.DialogConfirmIntent(outputSpeech, await _attributesManager.GetSession(cancellationToken),
            updatedIntent);
    }

    public Task<SkillResponse> Empty(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResponseBuilder.Empty());
    }
}