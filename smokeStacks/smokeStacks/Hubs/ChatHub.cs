using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using smokeStacks.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
namespace smokeStacks
{
    public class ChatHub : Hub
    {
        private smokeStacksEntities1 db = new smokeStacksEntities1();
 
        public void Send(string name, string message, string room)
        {
            // Get the current logged in User and look up the user in ASP.NET Identity
            string username = Context.User.Identity.Name;
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(Context.User.Identity.GetUserId());


            // Call the addNewMessageToPage method to update clients... 
            Clients.All.addNewMessageToPage(currentUser.FirstName + " " + currentUser.LastName , message, room);
            saveMessage(message, room);
        }


        public override Task OnConnected()
        {
            // Add your own code here.
            // For example: in a chat application, record the association between
            // the current connection ID and user name, and mark the user as online.
            // After the code in this method completes, the client is informed that
            // the connection is established; for example, in a JavaScript client,
            // the start().done callback is executed.
            return base.OnConnected();
        }


        private void saveMessage(string message, string room)
        {
            MessageLog msg = new MessageLog();
            msg.CreatorID = Context.User.Identity.GetUserId();
            msg.Message = message;
            msg.RoomID = int.Parse(room);
            msg.Recived = DateTime.UtcNow;
            db.MessageLogs.Add(msg);
            db.SaveChangesAsync();
        }
    }
}