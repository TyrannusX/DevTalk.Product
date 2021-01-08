using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class PriceUpdatedEvent : INotification
    {
        public Product Product { get; private set; }

        public PriceUpdatedEvent(Product product)
        {
            Product = product;
        }
    }
}
