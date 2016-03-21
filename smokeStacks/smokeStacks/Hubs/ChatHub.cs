using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using smokeStacks.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity.Validation;
namespace smokeStacks
{
    public class ChatHub : Hub
    {
        private smokeStacksEntities1 db = new smokeStacksEntities1();

        public void Send(string name, string message, string groupID)
        {
            // Get the current logged in User and look up the user in ASP.NET Identity
            string username = Context.User.Identity.Name;

            var currentUser = getCurrentUser();

            // Call the addNewMessageToPage method to update clients... 
            Clients.Group(groupID).addNewMessageToPage(currentUser.UserName, message, groupID);
            saveMessage(message, groupID);
        }

        public void SetOnlineMemeber(string groupID)
        {
            var currentUser = getCurrentUser();
            Clients.Group(groupID).updateUserList(currentUser.UserName);
            
        }
        public void SetOfflineMemeber(string groupID)
        {
            var currentUser = getCurrentUser();
            Clients.Group(groupID).updateUserListOffline(currentUser.UserName);
        }


        public ApplicationUser getCurrentUser()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return manager.FindById(Context.User.Identity.GetUserId());
            
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


        private void saveGroupMembership(string user, string room, string connectionID)
        {
            PrivateRoomMembership roomMember = new PrivateRoomMembership();
            roomMember.UserID = user;
            roomMember.connectionID = connectionID;
            roomMember.RoomID = int.Parse(room);
            db.PrivateRoomMemberships.Add(roomMember);
            db.SaveChangesAsync();
        }


        private void removeGroupMembership(string connectionID, string room)
        {

            var roomMember = (from roomMembers in db.PrivateRoomMemberships
                              where roomMembers.connectionID == connectionID && roomMembers.RoomID.ToString() == room
                              select roomMembers).FirstOrDefault(); 


            db.PrivateRoomMemberships.Remove(roomMember);
            db.SaveChangesAsync();
        }

        public void JoinRoom(string groupID)
        {
            string name = Context.User.Identity.Name;
            string userID = Context.User.Identity.GetUserId();

            this.Groups.Add(this.Context.ConnectionId, groupID);
            saveGroupMembership(userID, groupID, this.Context.ConnectionId);
            Clients.Group(groupID).addNewMessageToPage(name, " joined the room.", groupID);

        }

        public void LeaveRoom(string groupID)
        {
            
            string name = Context.User.Identity.Name;
            this.Groups.Remove(this.Context.ConnectionId, groupID);
            removeGroupMembership(this.Context.ConnectionId, groupID);
            Clients.Group(groupID).addNewMessageToPage(name, " left the room.", groupID);

        }

    }
}