using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EventBus.Events
{
    public class  IntegrationEvent
    {        
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public DateTime CreationDate { get; private init; }
    }
}
