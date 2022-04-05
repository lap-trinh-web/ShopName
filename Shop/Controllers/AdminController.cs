using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Shop.Controllers
{
    public class AdminController : Controller
    {



        dbQLSNDataContext data = new dbQLSNDataContext();
        // GET: /Admin/
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Sanpham(int? page)
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
                return View(data.SANPHAMs.ToList().OrderBy(n => n.MASP).ToPagedList(pageNumber, pageSize));
            }

        }
        public ActionResult ThemmoiSanpham()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                ViewBag.MALOAI = new SelectList(data.LOAISANPHAMs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MANSX = new SelectList(data.NSXes.ToList().OrderBy(n => n.TENNSX), "MANSX", "TENNSX");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiSanpham(SANPHAM  sp , HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.MALOAI = new SelectList(data.LOAISANPHAMs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
            ViewBag.MANSX = new SelectList(data.NSXes.ToList().OrderBy(n => n.TENNSX), "MANSX", "TENNSX");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);        
                    var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sp.ANHBIA = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }

        //Hiển thị sản phẩm
        public ActionResult ChitietSanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                //Lay ra doi tuong sach theo ma
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
                ViewBag.Masach = sp.MASP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }

        //Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaSanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
                ViewBag.Masach = sp.MASP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }


        [HttpPost, ActionName("XoaSanpham")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
                ViewBag.MASP = sp.MASP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.SANPHAMs.DeleteOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("Sanpham");
            }
        }
        //Chinh sửa sản phẩm
        [HttpGet]
        public ActionResult Suasanpham(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                //Lay ra doi tuong sach theo ma
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
                ViewBag.MASP = sp.MASP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
            
                ViewBag.MALOAI = new SelectList(data.LOAISANPHAMs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MANSX = new SelectList(data.NSXes.ToList().OrderBy(n => n.TENNSX), "MANSX", "TENNSX");
                return View(sp);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasanpham(int id , HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            else
            {
                //Dua du lieu vao dropdownload
                ViewBag.MALOAI = new SelectList(data.LOAISANPHAMs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MANSX = new SelectList(data.NSXes.ToList().OrderBy(n => n.TENNSX), "MANSX", "TENNSX");
                //Kiem tra duong dan file
                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                //Them vao CSDL
                else
                {
                    SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MASP == id);
                    if (ModelState.IsValid)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        //Luu duong dan cua file
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileUpload.SaveAs(path);
                        }
                        sp.ANHBIA = fileName;
                        //Luu vao CSDL   
                        UpdateModel(sp);
                        data.SubmitChanges();

                    }
                    return RedirectToAction("Sanpham");
                }
            }
        }
    }
}