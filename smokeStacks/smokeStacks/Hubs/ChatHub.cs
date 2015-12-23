using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace smokeStacks
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string room)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, room);
        }
    }
}