﻿using System.Collections.Generic;
using BerthaInnes.Domain.CommandSide.DomainEvents;
using BerthaInnes.Domain.QuerySide;
using BerthaInnes.Infrastructure.EventStore;
using Xunit;

namespace BerthaInnes.Tests.Infrastructure.EventStore
{
    public class EventStoreInMemoryTests
    {
        [Fact]
        public void
            Should_Return_All_Events_When_Get_All_Events_Of_Aggregate_Instance_After_Store_Events_Of_An_Aggregate_Instance()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new OrderStarted() }, 1));
            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new MarchandiseReceived() }, 2));

            var events = eventStore.GetAll("1");

            Assert.Equal(2, events.Count);
        }

        [Fact]
        public void Should_Return_only_events_of_aggregate_instance_when_get_all_events_of_aggregate_instance_after_store_events_of_several_aggregate_instances()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new OrderStarted() }, 1));
            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new MarchandiseReceived() }, 2));
            eventStore.Add(new EventsWrapper("2", new List<IDomainEvent> { new OrderStarted() }, 1));

            var events = eventStore.GetAll("1");

            Assert.Equal(2, events.Count);
        }

        [Fact]
        public void Should_throw_when_store_event_with_sequence_event_already_stored()
        {
            var eventStore = new EventStoreInMemory();

            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new OrderStarted() }, 1));
            eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new MarchandiseReceived() }, 2));

            Assert.Throws<SequenceAlreadyStoredException>(()
                => eventStore.Add(new EventsWrapper("1", new List<IDomainEvent> { new MarchandiseReceived() }, 2)));
        }
    }
}