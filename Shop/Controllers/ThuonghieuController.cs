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
    public class ThuonghieuController : Controller
    {
        dbQLSNDataContext data = new dbQLSNDataContext();
        // GET: Thuonghieu
        public ActionResult Thuonghieu(int? page)
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
                return View(data.NSXes.ToList().OrderBy(n => n.MANSX).ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nsx = from nxb in data.NSXes where nxb.MANSX == id select nxb;
                return View(nsx.SingleOrDefault());
            }
        }
        //3. Thêm mới Nhà xuất bản
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(NSX nhaxuatban)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.NSXes.InsertOnSubmit(nhaxuatban);
                data.SubmitChanges();

                return RedirectToAction("Thuonghieu", "Thuonghieu");
            }
        }
        //4. Xóa 1 Nhà xuất bản gồm 2 trang: xác nhận xóa và xử lý xóa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nsx = from nxb in data.NSXes where nxb.MANSX == id select nxb;
                return View(nsx.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NSX nsx = data.NSXes.SingleOrDefault(n => n.MANSX == id);
                data.NSXes.DeleteOnSubmit(nsx);
                data.SubmitChanges();

                return RedirectToAction("Thuonghieu", "Thuonghieu");
            }
        }
        //5. Điều chỉnh thông tin 1  Nhà xuất bản gồm 2 trang: Xem và điều chỉnh và cập nhật lưu lại
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nsx = from nxb in data.NSXes where nxb.MANSX == id select nxb;
                return View(nsx.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NSX nsx = data.NSXes.SingleOrDefault(n => n.MANSX == id);

                UpdateModel(nsx);
                data.SubmitChanges();
                return RedirectToAction("Thuonghieu", "Thuonghieu");
            }
        }
    }
}