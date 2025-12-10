using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Conference;

namespace Web.Services.Services;

public class EventPublisherService : IEventPublisherService
{
	private readonly Channel<ConferenceDto> _channel = Channel.CreateUnbounded<ConferenceDto>();

	public void PublishConferenceAdded(ConferenceDto newConference)
	{
		_channel.Writer.TryWrite(newConference);
	}

	public async IAsyncEnumerable<SseItem<ConferenceDto>> SubscribeToConferenceEvents([EnumeratorCancellation] CancellationToken cancellationToken)
	{
		await foreach (var conference in _channel.Reader.ReadAllAsync(cancellationToken))
		{
			yield return new SseItem<ConferenceDto>(conference, "conference-added");
		}
	}
}