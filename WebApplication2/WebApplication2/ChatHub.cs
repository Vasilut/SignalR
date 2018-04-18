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
            Clients.Caller.newMessage(message); //just me send message
            Clients.Client(Context.ConnectionId).newMessage(message); //just me send message


            Clients.All.newMessage(message);
        }

        //public void SendMessageToGroup(string message)
        //{
        //    var msg = $"{Context.ConnectionId} + ': ' {message}";
        //}

        public void SendMessageToRoom(string room, string message)
        {
            var msg = $"{Context.ConnectionId} + ': ' {message}";
            Clients.Group(room).newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            //this is not persisted...
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }

        //public Task<int> SendDataAsync()
        //{
        //    //async ... work ...


        //}

        public override Task OnConnected()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            SendMonitoringData("Disconected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connectionId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connectionId);
        }
    }
}