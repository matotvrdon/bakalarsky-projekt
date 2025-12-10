using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using Web.Services.DTOs.Conference;

namespace Web.Services.Abstractions;

public interface IEventPublisherService
{
	void PublishConferenceAdded(ConferenceDto newConference);
	IAsyncEnumerable<SseItem<ConferenceDto>> SubscribeToConferenceEvents(CancellationToken cancellationToken);
}