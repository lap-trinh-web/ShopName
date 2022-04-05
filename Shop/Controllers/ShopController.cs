using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        dbQLSNDataContext data = new dbQLSNDataContext();
        private List<SANPHAM> layhangmoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NGAYCAPNHAT).Take(count).ToList();
        }
        
        public ActionResult Index(int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);

            var spmoi = layhangmoi(12);
            return View(spmoi.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Loai()
        {
            var loai = from l in data.LOAISANPHAMs select l;
            return PartialView(loai);

        }
        public ActionResult Chitiet(int id)
        {
            var sp = from s in data.SANPHAMs where s.MASP == id select s;
            return View(sp.Single());
        }
        public ActionResult Sptheoloai(int id)
        {
            var sp = from h in data.SANPHAMs where h.MALOAI == id select h;
            return View(sp);
        }
        public ActionResult nhasanxuat()
        {
            var nhasanxuat = from nsx in data.NSXes select nsx;
            return PartialView(nhasanxuat);
        }
        public ActionResult SPTheoNSX(int id)
        {
            var nhasanxuat = from s in data.SANPHAMs where s.MANSX == id select s;
            return View(nhasanxuat);
        }

    }
}