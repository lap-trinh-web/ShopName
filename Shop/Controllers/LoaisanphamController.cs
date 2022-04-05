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
    public class LoaisanphamController : Controller
    {
        dbQLSNDataContext data = new dbQLSNDataContext();
        // GET: Loaisanpham

        public ActionResult Loaisp(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 7;
                //return View(db.SACHes.ToList());
                return View(data.LOAISANPHAMs.ToList().OrderBy(n => n.MALOAI).ToPagedList(pageNumber, pageSize));
            }

        }
        public ActionResult chitietloai(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var l = from nxb in data.LOAISANPHAMs where nxb.MALOAI == id select nxb;
                return View(l.SingleOrDefault());
            }
        }
        //3. Thêm mới Nhà xuất bản
        [HttpGet]
        public ActionResult themloai()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult themloai(LOAISANPHAM l)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.LOAISANPHAMs.InsertOnSubmit(l);
                data.SubmitChanges();

                return RedirectToAction("Loaisp", "Loaisanpham");
            }
        }
        //4. Xóa 1 Nhà xuất bản gồm 2 trang: xác nhận xóa và xử lý xóa
        [HttpGet]
        public ActionResult xoaloai(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var l = from nxb in data.LOAISANPHAMs where nxb.MALOAI == id select nxb;
                return View(l.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("xoaloai")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                LOAISANPHAM l = data.LOAISANPHAMs.SingleOrDefault(n => n.MALOAI == id);
                data.LOAISANPHAMs.DeleteOnSubmit(l);
                data.SubmitChanges();

                return RedirectToAction("Loaisp", "Loaisanpham");
            }
        }
        //5. Điều chỉnh thông tin 1  Nhà xuất bản gồm 2 trang: Xem và điều chỉnh và cập nhật lưu lại
        [HttpGet]
        public ActionResult Sualoai(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var l = from nxb in data.LOAISANPHAMs where nxb.MALOAI == id select nxb;
                return View(l.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Sualoai")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                LOAISANPHAM l = data.LOAISANPHAMs.SingleOrDefault(n => n.MALOAI == id);

                UpdateModel(l);
                data.SubmitChanges();
                return RedirectToAction("Loaisp", "Loaisanpham");
            }
        }
    }
}