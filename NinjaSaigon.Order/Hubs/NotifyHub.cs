using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Order.Hubs
{
    public class NotifyHub: Hub
    {
        public async Task SendNewOrder(string name, string phone)
        {
            await Clients.All.SendAsync("ReceiveNewOrder", name, phone);
        }
    }
}
