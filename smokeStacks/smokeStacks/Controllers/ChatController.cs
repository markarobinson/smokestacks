using smokeStacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace smokeStacks.Controllers
{
    public class ChatController : Controller
    {

        private smokeStacksEntities1 db = new smokeStacksEntities1();
        // GET: Chat
        public ActionResult EnterRoom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            if (room.RoomPassword != null)
            {
                string reqPassword = Convert.ToString(Request["item.roomPassword"].ToString());
                if (room.RoomPassword != reqPassword)
                    return View("NoAccess");

            }
            
            return View(room);
        }
    }
}