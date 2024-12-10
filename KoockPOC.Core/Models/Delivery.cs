using KoockPOC.Core.Enums;

namespace KoockPOC.Core.Models;

public class Delivery
{
    public Guid DeliveryID { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}
