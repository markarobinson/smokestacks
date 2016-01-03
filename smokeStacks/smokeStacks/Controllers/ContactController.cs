using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace smokeStacks.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send()
        {

            string name = Convert.ToString(Request["Name"].ToString());
            string email = Convert.ToString(Request["Email"].ToString());
            string postMessage = Convert.ToString(Request["Message"].ToString());

            
            MailAddress from = new MailAddress(email, name);
            MailAddress to = new MailAddress("me@home.com", "Admin");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Contact us";
      
            

            return Content(name +email + message );
        }
    }
}