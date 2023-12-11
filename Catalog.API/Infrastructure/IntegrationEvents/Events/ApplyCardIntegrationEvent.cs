using Catalog.API.Models;

namespace Catalog.API.Infrastructure.IntegrationEvents.Events
{
    public class ApplyCardIntegrationEvent
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ApplyCardIntegrationEvent(ApplicationForm item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            Email = item.Email;
        }
    }
}
