using FTMS.DAL;
using FTMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FTMS.Controllers
{
    public class EventController : Controller
    {
        DataService dal = new DataService();
        // GET: Event
        [Route("Event")]
        public ActionResult Event()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEvent(Event events)
        {
         return Json(dal.AddEvent(events),JsonRequestBehavior.AllowGet);   
        }

        //GET:Retrive Eventlist

        public ActionResult GetEvents()
        {
            return Json(dal.GetEvents(), JsonRequestBehavior.AllowGet);
        }
    }
}