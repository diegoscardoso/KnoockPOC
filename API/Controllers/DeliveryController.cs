using API.Hubs;
using KoockPOC.Core.Enums;
using KoockPOC.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{

    public class DeliveryController : Controller
    {
        private readonly IHubContext<DeliveryHub> _deliveryHubContext;

        public DeliveryController(IHubContext<DeliveryHub> deliveryHubContext)
        {
            this._deliveryHubContext = deliveryHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("change-delivery-status")]
        [HttpPost]
        public IActionResult ChangeDeliveryStatus(Guid deliveryID)
        {
            // logic to change delivery Status

            return Json(new { Success = true, message = "Delivery Status Changed" });
        }

        [HttpPost]
        [Route("notify-pending-delivery")]
        public async Task<IActionResult> NotifyPendingDelivery([FromBody] Delivery pendingDelivery)
        {
            // Broadcast the message to all connected SignalR clients
            await _deliveryHubContext.Clients.All.SendAsync("ReceiveDeliveryNotification", pendingDelivery.DeliveryID, pendingDelivery.DeliveryStatus.ToString());

            return Ok(new { Success = true, message = "Pending delivery sent" });
        }

    }
}
    