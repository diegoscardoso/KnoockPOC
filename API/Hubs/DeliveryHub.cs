using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class DeliveryHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var affiliateID = Context.GetHttpContext()?.Request.Query["affiliateID"].ToString();

            if (!string.IsNullOrWhiteSpace(affiliateID)) 
            {
                // do something with the affiliatedID if necessary
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
