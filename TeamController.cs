using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FTMS.DAL;
using FTMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace FTMS.Controllers
{
    [HandleError]
    [Authorize]
    [RoutePrefix("Team")]
    public class TeamController : Controller
    {
        DataService dal = new DataService();
        //// GET: Team
        //[Route("Team/ViewTeam")]
        //public ActionResult GetTeam()
        //{

        //    ViewBag.Employees= dal.GetTeam();
        //    return View("TeamDetails");
        //}



        //public JsonResult Delete(int empId)
        //{
        //    return Json(dal.Delete(empId),JsonRequestBehavior.AllowGet);
        //    //return View("TeamDetails");
        //}


        //[Route("Team/AddMember")]
        //public ActionResult AddMember()
        //{ 
        //        return View();
        //    }

        //[HttpPost]
        //public ActionResult Create(Employee emp)
        //{
        //    if (ModelState.IsValid == false)
        //        return View("AddMember");
        //    else

        //        if (dal.AddEmployee(emp))
        //        TempData["Success"] = "Employee added successfully";
        //        else
        //        TempData["Failure"] = "Error occured in adding Employee";
        //        ViewBag.Employees = dal.GetTeam();
        //        return View("TeamDetails");
        //}


        [Authorize]
        [Route("TeamDetails")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTeam()
        {
            return Json(dal.GetTeam(),JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Add(Employee emp)
        {
            if (ModelState.IsValid)
            {
                return Json(dal.AddEmployee(emp), JsonRequestBehavior.AllowGet);
            }
            else

                return View("Index");
            
            
        }   
        
        public JsonResult Delete(Employee emp)
        {
            return Json(dal.Delete(emp),JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Employee emp)
        {
            return Json(dal.Update(emp),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMember(int empID)
        {
            var employee = dal.GetTeam().Find(x => x.empId.Equals(empID));
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        }
    }
