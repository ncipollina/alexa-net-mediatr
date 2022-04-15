using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Ssml;

namespace Alexa.Net.MediatR.Response;

public interface IResponseBuilder
{
    #region Tell Responses

    Task<SkillResponse> Tell(IOutputSpeech speechResponse, CancellationToken cancellationToken = default);

    Task<SkillResponse> Tell(string speechResponse, CancellationToken cancellationToken = default);

    Task<SkillResponse> Tell(Speech speechResponse, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithReprompt(IOutputSpeech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithReprompt(string speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> TellWithReprompt(Speech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithCard(IOutputSpeech speechResponse, string title, string content, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> TellWithCard(string speechResponse, string title, string content, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> TellWithCard(Speech speechResponse, string title, string content, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithLinkAccountCard(IOutputSpeech speechResponse, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> TellWithLinkAccountCard(string speechResponse, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> TellWithLinkAccountCard(Speech speechResponse, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithAskForPermissionsConsentCard(IOutputSpeech speechResponse, IEnumerable<string> permissions, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithAskForPermissionsConsentCard(string speechResponse, IEnumerable<string> permissions, CancellationToken cancellationToken = default);

    Task<SkillResponse> TellWithAskForPermissionsConsentCard(Speech speechResponse, IEnumerable<string> permissions, CancellationToken cancellationToken = default);

    #endregion

    #region Ask Responses

    Task<SkillResponse> Ask(IOutputSpeech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> Ask(string speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> Ask(Speech speechResponse, Reprompt reprompt, CancellationToken cancellationToken = default);

    Task<SkillResponse> AskWithCard(IOutputSpeech speechResponse, string title, string content, Reprompt reprompt, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> AskWithCard(string speechResponse, string title, string content, Reprompt reprompt, CancellationToken cancellationToken = default);
    
    Task<SkillResponse> AskWithCard(Speech speechResponse, string title, string content, Reprompt reprompt, CancellationToken cancellationToken = default);

    #endregion

    #region AudioPlayer Responses

    Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, CancellationToken cancellationToken = default);

    Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, int offsetInMilliseconds, CancellationToken cancellationToken = default);

    Task<SkillResponse> AudioPlayerPlay(PlayBehavior playBehavior, string url, string token, string expectedPreviousToken,
        int offsetInMilliseconds, CancellationToken cancellationToken = default);

    Task<SkillResponse> AudioPlayerStop(CancellationToken cancellationToken = default);

    Task<SkillResponse> AudioPlayerClearQueue(ClearBehavior clearBehavior, CancellationToken cancellationToken = default);

    #endregion

    #region Dialog Responses

    Task<SkillResponse> DialogDelegate(Intent? updatedIntent = null, CancellationToken cancellationToken = default);

    Task<SkillResponse> DialogElicitSlot(IOutputSpeech outputSpeech, string slotName, Intent? updatedIntent = null, CancellationToken cancellationToken = default);

    Task<SkillResponse> DialogConfirmSlot(IOutputSpeech outputSpeech, string slotName,
        Intent? updatedIntent = null, CancellationToken cancellationToken = default);

    Task<SkillResponse> DialogConfirmIntent(IOutputSpeech outputSpeech, Intent? updatedIntent = null, CancellationToken cancellationToken = default);

    #endregion

    Task<SkillResponse> Empty(CancellationToken cancellationToken = default);
}