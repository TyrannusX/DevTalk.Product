using Common.Application.Events.PriceUpdated;
using Domain.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Events.PriceUpdated
{
    public class PriceUpdatedEventHandler : INotificationHandler<PriceUpdatedEvent>
    {
        private readonly ISendEndpointProvider _sendEndpoint;

        public PriceUpdatedEventHandler(ISendEndpointProvider publishEndpoint)
        {
            _sendEndpoint = publishEndpoint;
        }

        public async Task Handle(PriceUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var priceUpdatedIntegrationEvent = new PriceUpdatedIntegrationEvent(notification.Product.ProductId, notification.Product.Price);
            var sender = await _sendEndpoint.GetSendEndpoint(new Uri("sb://reyes-devtalk.servicebus.windows.net/priceupdated"));
            await sender.Send(priceUpdatedIntegrationEvent).ConfigureAwait(false);
        }
    }
}
