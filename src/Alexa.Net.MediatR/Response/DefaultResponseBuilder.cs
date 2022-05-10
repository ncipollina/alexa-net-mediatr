using Alexa.NET;
using Alexa.Net.MediatR.Attributes;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Response.Ssml;
using Microsoft.VisualBasic;

namespace Alexa.Net.MediatR.Response;

public class DefaultResponseBuilder : IResponseBuilder
{
    private readonly IAttributesManager _attributesManager;
    private readonly ResponseBody _response;

    public DefaultResponseBuilder(IAttributesManager attributesManager)
    {
        _attributesManager = attributesManager ?? throw new ArgumentNullException(nameof(attributesManager));
        _response = new();
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
        return GetResponse(cancellationToken);
    }

    public IResponseBuilder Speak(string? speechOutput)
    {
        speechOutput = TrimOutputSpeech(speechOutput);
        return Speak(new PlainText(speechOutput));
    }

    public IResponseBuilder Speak(params ISsml[] elements)
    {
        _response.OutputSpeech = new SsmlOutputSpeech
        {
            Ssml = new Speech(elements).ToXml()
        };

        return this;
    }

    public IResponseBuilder SpeakAudio(string? audioUrl)
    {
        audioUrl = TrimOutputSpeech(audioUrl);
        return Speak(new Audio(audioUrl));
    }

    public IResponseBuilder Reprompt(string? repromptSpeechOutput)
    {
        repromptSpeechOutput = TrimOutputSpeech(repromptSpeechOutput);
        return Reprompt(new PlainText(repromptSpeechOutput));
    }

    public IResponseBuilder Reprompt(params ISsml[] elements)
    {
        _response.Reprompt = new Reprompt
        {
            OutputSpeech = new SsmlOutputSpeech
            {
                Ssml = new Speech(elements).ToXml()
            }
        };

        if (!IsVideoAppLaunchDirectivePresent())
        {
            _response.ShouldEndSession = false;
        }

        return this;
    }

    public IResponseBuilder WithSimpleCard(string cardTitle, string cardContent)
    {
        _response.Card = new SimpleCard
        {
            Title = cardTitle,
            Content = cardContent
        };

        return this;
    }

    public IResponseBuilder WithStandardCard(string cardTitle, string cardContent, string? smallImageUrl = null,
        string? largeImageUrl = null)
    {
        var card = new StandardCard
        {
            Title = cardTitle,
            Content = cardContent
        };

        if (!string.IsNullOrWhiteSpace(smallImageUrl) || !string.IsNullOrWhiteSpace(largeImageUrl))
        {
            card.Image = new CardImage
            {
                SmallImageUrl = smallImageUrl,
                LargeImageUrl = largeImageUrl
            };
        }

        _response.Card = card;

        return this;
    }

    public IResponseBuilder WithLinkAccountCard()
    {
        _response.Card = new LinkAccountCard();

        return this;
    }

    public IResponseBuilder WithAskForPermissionsConsentCard(List<string> permissionArray)
    {
        _response.Card = new AskForPermissionsConsentCard
        {
            Permissions = permissionArray
        };

        return this;
    }

    public IResponseBuilder AddAudioPlayerPlayDirective(PlayBehavior playBehavior, string url, string token,
        int offsetInMilliseconds, string? expectedPreviousToken = null, AudioItemMetadata? audioItemMetadata = null,
        CancellationToken cancellationToken = default)
    {
        var stream = new AudioItemStream
        {
            Url = url,
            Token = token,
            OffsetInMilliseconds = offsetInMilliseconds
        };

        if (!string.IsNullOrWhiteSpace(expectedPreviousToken))
            stream.ExpectedPreviousToken = expectedPreviousToken;

        var audioItem = new AudioItem
        {
            Stream = stream
        };

        if (audioItemMetadata != null)
        {
            audioItem.Metadata = audioItemMetadata;
        }

        var playDirective = new AudioPlayerPlayDirective
        {
            PlayBehavior = playBehavior,
            AudioItem = audioItem
        };

        return AddDirective(playDirective);
    }

    public IResponseBuilder AddAudioPlayerStopDirective()
    {
        return AddDirective(new StopDirective());
    }

    public IResponseBuilder AddAudioPlayerClearQueueDirective(ClearBehavior clearBehavior)
    {
        return AddDirective(new ClearQueueDirective
        {
            ClearBehavior = clearBehavior
        });
    }

    public IResponseBuilder AddConfirmIntentDirective(Intent? updatedIntent = null)
    {
        var confirmIntentDirective = new DialogConfirmIntent();
        if (updatedIntent != null)
        {
            confirmIntentDirective.UpdatedIntent = updatedIntent;
        }

        return AddDirective(confirmIntentDirective);
    }

    public IResponseBuilder AddConfirmSlotDirective(string slotToConfirm, Intent? updatedIntent = null)
    {
        var confirmSlotDirective = new DialogConfirmSlot(slotToConfirm);
        if (updatedIntent != null)
        {
            confirmSlotDirective.UpdatedIntent = updatedIntent;
        }

        return AddDirective(confirmSlotDirective);
    }

    public IResponseBuilder AddDelegateDirective(Intent? updatedIntent = null)
    {
        var delegateDirective = new DialogDelegate();

        if (updatedIntent != null)
        {
            delegateDirective.UpdatedIntent = updatedIntent;
        }

        return AddDirective(delegateDirective);
    }

    public IResponseBuilder AddDirective(IDirective directive)
    {
        if (_response.Directives == null)
            _response.Directives = new List<IDirective>();

        _response.Directives.Add(directive);

        return this;
    }

    public IResponseBuilder AddElicitSlotDirective(string slotToElicit, Intent? updatedIntent = null)
    {
        var elicitSlotDirective = new DialogElicitSlot(slotToElicit);

        if (updatedIntent != null)
        {
            elicitSlotDirective.UpdatedIntent = updatedIntent;
        }

        return AddDirective(elicitSlotDirective);
    }

    public IResponseBuilder AddHintDirective(string text)
    {
        return AddDirective(new HintDirective
        {
            Hint = new Hint
            {
                Type = "PlainText",
                Text = text
            }
        });
    }

    public IResponseBuilder AddVideoAppLaunchDirective(string source, string? title = null, string? subtitle = null)
    {
        var videoItem = new VideoItem(source);

        if (!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(subtitle))
        {
            videoItem.Metadata = new VideoItemMetadata
            {
                Subtitle = subtitle,
                Title = title
            };
        }

        _response.ShouldEndSession = null;

        return AddDirective(new VideoAppDirective
        {
            VideoItem = videoItem
        });
    }

    public IResponseBuilder WithShouldEndSession(bool val)
    {
        if (!IsVideoAppLaunchDirectivePresent())
            _response.ShouldEndSession = val;

        return this;
    }

    public async Task<SkillResponse> GetResponse(CancellationToken cancellationToken = default)
    {
        var response = new SkillResponse
        {
            Version = "1.0",
            SessionAttributes = (await _attributesManager.GetSession(cancellationToken)).Attributes,
            Response = _response
        };
        return response;
    }

    private bool IsVideoAppLaunchDirectivePresent()
    {
        return _response.Directives?.Any(d => d is VideoAppDirective) ?? false;
    }

    private string TrimOutputSpeech(string? outputSpeech)
    {
        if (string.IsNullOrWhiteSpace(outputSpeech))
            return string.Empty;
        
        var speechSpan = outputSpeech.AsSpan();
        speechSpan = speechSpan.Trim();
        var start = "<speak>".AsSpan();
        var end = "</speak>".AsSpan();
        if (speechSpan.StartsWith(start) && speechSpan.EndsWith(end))
        {
            return speechSpan.Slice(start.Length, speechSpan.Length - start.Length - end.Length).Trim().ToString();
        }

        return speechSpan.ToString();
    }
}