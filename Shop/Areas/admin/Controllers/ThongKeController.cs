using Models.DAL;
using Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;



namespace Shop.Areas.admin.Controllers
{
    public class ThongKeController : Controller
    {
        private AplicationDBContext db = new AplicationDBContext();

        // GET: admin/ThongKe       

        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }


    }
}