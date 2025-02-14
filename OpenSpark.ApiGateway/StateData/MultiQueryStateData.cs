﻿using OpenSpark.Shared.Events.Payloads;
using System;
using System.Collections.Immutable;

namespace OpenSpark.ApiGateway.StateData
{
    public class MultiQueryStateData
    {
        public IImmutableDictionary<Guid, IPayloadEvent> Received { get; }
        public IImmutableDictionary<Guid, int> Pending { get; set; }

        public MultiQueryStateData(
            IImmutableDictionary<Guid, IPayloadEvent> received,
            IImmutableDictionary<Guid, int> pending)
        {
            Received = received;
            Pending = pending;
        }

        public MultiQueryStateData(IImmutableDictionary<Guid, int> pending)
        {
            Received = ImmutableDictionary<Guid, IPayloadEvent>.Empty;
            Pending = pending;
        }

        public MultiQueryStateData(IImmutableDictionary<Guid, IPayloadEvent> received)
        {
            Received = received;
            Pending = ImmutableDictionary<Guid, int>.Empty;
        }
    }
}