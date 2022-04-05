using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class DonhangController : Controller
    {
        dbQLSNDataContext data = new dbQLSNDataContext();
        // GET: Donhang
        public ActionResult Index(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 5;
                //return View(db.SACHes.ToList());
                return View(data.DONDATHANGs.ToList().OrderBy(n => n.MADONHANG).ToPagedList(pageNumber, pageSize));
            }
        }
    }
}