﻿using System.Collections.Generic;
using BerthaInnes.Domain.CommandSide.DomainEvents;

namespace BerthaInnes.Domain.QuerySide
{
    public class PendingOrderEventHandler : IEventHandler
    {
        private readonly List<WaitingOrder> _repository;

        public PendingOrderEventHandler(List<WaitingOrder> repository)
        {
            _repository = repository;
        }

        public void Handle(EventWrapper evt)
        {
            switch (evt.DomainEvent)
            {
                case OrderStarted orderStarted:
                    {

                        var waitingOrder = new WaitingOrder(evt.OrderId, orderStarted.NumberColis);
                        _repository.Add(waitingOrder);
                        break;
                    }
                case MarchandiseReceived _:
                    {
                        _repository.RemoveAll(w => w.Id == evt.OrderId);
                        break;
                    }
            }
        }
    }
}