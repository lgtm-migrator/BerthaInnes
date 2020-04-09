﻿using System.Collections.Generic;

namespace BerthaInnes.DomainEvents
{
    public struct MarchandisePartiallyReceived : IDomainEvent
    {
        public List<Colis> ColisList { get; }
        public int NumberColisRemaining { get; }

        public MarchandisePartiallyReceived(List<Colis> colisList, int numberColisRemaining)
        {
            ColisList = colisList;
            NumberColisRemaining = numberColisRemaining;
        }
    }
}