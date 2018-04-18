using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApplication2
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.newMessage(message);
        }

        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }

        //public Task<int> SendDataAsync()
        //{
        //    //async ... work ...


        //}
    }
}