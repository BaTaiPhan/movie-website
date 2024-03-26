using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Home()
        {
            int demnd = data.Users.ToList().Count;
            var doanhthu = 0.0;
            var tk = data.Tickets.OrderByDescending(x => x.TicketID).ToList();
            foreach (var i in tk)
            {
                doanhthu += i.Money;
            }
            ViewData["doanhthu"] = doanhthu;
            int phimsapchieu = data.Movies.Where(x => x.ComingSoon == 1).ToList().Count;
            int phimdangchieu = data.Movies.Where(x => x.ComingSoon == 0).ToList().Count;
            ViewData["NguoiDung"] = demnd;
            ViewData["phimsapchieu"] = phimsapchieu;
            ViewData["phimdangchieu"] = phimdangchieu;
            var DSPhimSapChieu = data.Movies.Where(x => x.ComingSoon == 1).Take(10).ToList();
            ViewData["DSPhimSapChieu"] = DSPhimSapChieu;
            var DSPhimDangChieu = data.Movies.OrderByDescending(x => x.MovieID).Take(10).ToList();
            ViewData["DSPhimDangChieu"] = DSPhimDangChieu;
            var DSDatVe = data.Tickets.OrderByDescending(a => a.TicketID).Take(10).ToList();
            ViewData["DSDatVe"] = DSDatVe;
            return View();
        }
        public ActionResult DienVien()
        {
            var ac = data.Actors.ToList();
            return View(ac);
        }
        // them dien vien
        public ActionResult ThemDV(Actor a)
        {
            return View(a);
        }
        [HttpPost]
        public ActionResult ThemDV(FormCollection collection, Actor a)
        {
            var A_name = collection["Name"];
            var A_Nationality = collection["Nationality"];
            var A_Birth = Convert.ToDateTime(collection["Birth"]);
            if (string.IsNullOrEmpty(A_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                a.Name = A_name.ToString();
                a.Nationality = A_Nationality.ToString();
                a.Birth = A_Birth;    
                data.Actors.InsertOnSubmit(a);
                data.SubmitChanges();
                return RedirectToAction("DienVien");
            }
            return this.ThemDV(a);
        }

        //sua dien vien

        public ActionResult SuaDV(int id)
        {
            var E_sach = data.Actors.First(m => m.ActorID == id);

            return View(E_sach);
        }
        [HttpPost]



        public ActionResult SuaDV(int id, FormCollection collection)
        {
            var tl = data.Actors.First(n => n.ActorID == id);
            var E_name = collection["Name"];
            var E_Nationality = collection["Nationality"];
            var E_Birth = Convert.ToDateTime(collection["Birth"]);


            tl.ActorID = id;
            if (string.IsNullOrEmpty(E_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.Name = E_name;
                tl.Nationality = E_Nationality;
                tl.Birth = E_Birth;

                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("DienVien");
            }
            return this.SuaDaoDien(id);
        }
        //Xoa DienVien
        public ActionResult XoaDienVien(int id)
        {
            var D_DienVien = data.Actors.First(m => m.ActorID == id);
            return View(D_DienVien);
        }
        [HttpPost]
        public ActionResult XoaDienVien(int id, FormCollection collection)
        {
            var D_DienVien = data.Actors.Where(m => m.ActorID == id).First();
            data.Actors.DeleteOnSubmit(D_DienVien);
            data.SubmitChanges();
            return RedirectToAction("DienVien");

        }


        public ActionResult DaoDien()
        {
            var ac = data.Directors.ToList();
            return View(ac);
        }
        //code ddi cop cai file cshtml á rename lại ok
        //m adđ dì làm di chạy view có đc đâu ??? k dc đc mỗi cái diễn viên




        //Them dao dien
        public ActionResult ThemDD(Director d)
        {
            return View(d);
        }
        [HttpPost]
        public ActionResult ThemDD(FormCollection collection, Director d)
        {
            var E_name = collection["Name"];
            var E_Nationality = collection["Nationality"];

            if (string.IsNullOrEmpty(E_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                d.Name = E_name.ToString();
                d.Nationality = E_Nationality.ToString();
                //d.Birth = E_Birth;    
                data.Directors.InsertOnSubmit(d);
                data.SubmitChanges();
                return RedirectToAction("DaoDien");
            }
            return this.ThemDD(d);
        }



        //Xoa dao dien
        public ActionResult XoaDaoDien(int id)
        {
            var D_daodien = data.Directors.First(m => m.DirectorID == id);
            return View(D_daodien);
        }
        [HttpPost]
        public ActionResult XoaDaoDien(int id, FormCollection collection)
        {
            var D_daodien = data.Directors.Where(m => m.DirectorID == id).First();
            data.Directors.DeleteOnSubmit(D_daodien);
            data.SubmitChanges();
            return RedirectToAction("DaoDien");

        }

        //Sua dao dien
        public ActionResult SuaDaoDien(int id)
        {
            var E_sach = data.Directors.First(m => m.DirectorID == id);

            return View(E_sach);
        }
        [HttpPost]



        public ActionResult SuaDaoDien(int id, FormCollection collection)
        {
            var tl = data.Directors.First(n => n.DirectorID == id);
            var E_name = collection["Name"];
            var E_Nationality = collection["Nationality"];
            var E_Birth = Convert.ToDateTime(collection["Birth"]);


            tl.DirectorID = id;
            if (string.IsNullOrEmpty(E_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.Name = E_name;
                tl.Nationality = E_Nationality;
                tl.Birth = E_Birth;

                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("DaoDien");
            }
            return this.SuaDaoDien(id);
        }


        public ActionResult TL()
        {
            var ac = data.Genres.ToList();
            return View(ac);
        }

        //Thêm thể loai
        public ActionResult ThemTheLoai(Genre g)
        {
            return View(g);
        }
        [HttpPost]
        public ActionResult ThemTheLoai(FormCollection collection, Genre g)
        {
            var g_name = collection["GenreName"];
            
            if (string.IsNullOrEmpty(g_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                g.GenreName = g_name.ToString();
               
                data.Genres.InsertOnSubmit(g);
                data.SubmitChanges();
                return RedirectToAction("TL");
            }
            return this.ThemTheLoai(g);
        }

        //Xóa thể loai
        public ActionResult XoaTheLoai(int id)
        {
            var D_TheLoai = data.Genres.First(m => m.GenreID == id);
            return View(D_TheLoai);
        }
        [HttpPost]
        public ActionResult XoaTheLoai(int id, FormCollection collection)
        {
            var D_TheLoai = data.Genres.Where(m => m.GenreID == id).First();
            data.Genres.DeleteOnSubmit(D_TheLoai);
            data.SubmitChanges();
            return RedirectToAction("TL");

        }

        //Sửa thể loại
        public ActionResult SuaTheLoai(int id)
        {
            var E_sach = data.Genres.First(m => m.GenreID == id);

            return View(E_sach);
        }
        [HttpPost]



        public ActionResult SuaTheLoai(int id, FormCollection collection)
        {
            var tl = data.Genres.First(n => n.GenreID == id);
            var E_name = collection["GenreName"];

            tl.GenreID = id;
            if (string.IsNullOrEmpty(E_name))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.GenreName = E_name;

                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("TL");
            }
            return this.SuaTheLoai(id);
        }

        public ActionResult VeXemPhim()
        {
            var ac = data.Tickets.ToList();
            return View(ac);
        }
        //Thêm vexemphim
        public ActionResult Themvexemphim(Ticket t)
        {
            return View(t);
        }
        [HttpPost]
        public ActionResult Themvexemphim(FormCollection collection, Ticket g)
        {
            var g_money = Convert.ToInt32(collection["Money"]);
            var g_Seat = collection["Seat"];
            var g_Vip = Convert.ToInt32(collection["Vip"]);
            var g_Normal = Convert.ToInt32(collection["Normal"]);
            var g_AmountSeats = Convert.ToInt32(collection["AmountSeats"]);


            if (string.IsNullOrEmpty(g_Seat))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                g.Money = g_money;
                g.Seat = g_Seat.ToString();
                g.Vip = g_Vip;
                g.Normal = g_Normal;
                g.AmountSeats = g_AmountSeats;
                data.Tickets.InsertOnSubmit(g);
                data.SubmitChanges();
                return RedirectToAction("VeXemPhim");
            }
            return this.Themvexemphim(g);
        }
        //Xóa vexemphim
        public ActionResult Xoavexemphim(int id)
        {
            var D_vexemphim = data.Tickets.First(m => m.TicketID == id);
            return View(D_vexemphim);
        }
        [HttpPost]
        public ActionResult Xoavexemphim(int id, FormCollection collection)
        {
            var D_vexemphim = data.Tickets.Where(m => m.TicketID == id).First();
            data.Tickets.DeleteOnSubmit(D_vexemphim);
            data.SubmitChanges();
            return RedirectToAction("VeXemPhim");

        }

        //Sửavexemphim
        public ActionResult Suavexemphim(int id)
        {
            var E_sach = data.Tickets.First(m => m.TicketID == id);

            return View(E_sach);
        }
        [HttpPost]



        public ActionResult Suavexemphim(int id, FormCollection collection)
        {
            var tl = data.Tickets.First(n => n.TicketID == id);
            var E_Seat = collection["Seat"];
            var E_Money = Convert.ToInt32(collection["Money"]);
            var E_Vip = Convert.ToInt32(collection["Vip"]);
            var E_Normal = Convert.ToInt32(collection["Normal"]);
            var E_AmountSeats = Convert.ToInt32(collection["AmountSeats"]);

            tl.TicketID = id;
            if (string.IsNullOrEmpty(E_Seat))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.Money = E_Money;
                tl.Seat = E_Seat.ToString();
                tl.Vip = E_Vip;
                tl.Normal = E_Normal;
                tl.AmountSeats = E_AmountSeats;

                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("VeXemPhim");
            }
            return this.Suavexemphim(id);
        }
        //chi vexemphim
        public ActionResult Detailvexemphim(int id,  Ticket f)
        {
            
            var D_vexemphim = data.Tickets.Where(m => m.TicketID == id).First();
            
            return View(D_vexemphim);
        }

        public ActionResult veban()
        {
            var lst = from l in data.MovieSeats select l;
            return View(lst);
        }






        //Thêm tài khoản

        public ActionResult add(User s)
        {
            return View(s);
        }
        [HttpPost]

        public ActionResult add(User tk, FormCollection coll)
        {
            var tendn = coll["TenDN"];
            var mk = coll["MatKhau"];
            var mknhaplai = coll["MatKhauNhapLai"];
            var fn = coll["TenDau"];
            var ln = coll["TenCuoi"];
            var em = coll["email"];
            var ph = coll["sodienthoai"];
            //var taikhoan = from t in data.TaiKhoans where t.TenDN.Equals(tendn) select t.TenDN;
            var taikhoan = data.Users.ToList();
            int kt = 0;
            int kt2 = 0;
            var ktem = data.Users.Where(a => a.Email == em);
            foreach (var item in taikhoan)
            {
                if (item.Username == tendn)
                    kt = 1;
                if (item.Phone == ph)
                    kt2 = 1;
            }
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi"] = "Tên đăng nhập không được để chống";
            else if (String.IsNullOrEmpty(mk))
                ViewData["Loi1"] = "Mật khẩu không được để chống";
            else if (String.IsNullOrEmpty(em))
                ViewData["Loi2"] = "Vui lòng nhập Email";
            else if (String.IsNullOrEmpty(ph))
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại";
            else if (kt == 1)
            {
                ViewData["Loi2"] = "Đã có tài khoản này";
            }
            else if (ktem.Count() != 0)
            {
                ViewData["Loi2e"] = "Email đã được sử dụng";
            }
            else if (kt2 == 1)
            {
                ViewData["Loi3e"] = "Số điện thoại đã được sử dụng";
            }
            else if (mk != mknhaplai)
            {
                ViewData["Loi12"] = "Mật khẩu nhập lại không đúng";
            }
            else
            {
                tk.Username = tendn;
                tk.Password = mk;
                tk.FirstName = fn;
                tk.LastName = ln;
                tk.Email = em;
                tk.Phone = ph;
                data.Users.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("users");
            }
            return View();
        }
        //Sửa
        public ActionResult edit(int id)
        {
            var E_sach = data.Users.First(m => m.UserID == id);

            return View(E_sach);
        }

        [HttpPost]

        public ActionResult edit(int id, FormCollection collection)
        {
            var tk = data.Users.First(n => n.UserID == id);
            var taikhoan = data.Users.ToList();
            var fn = collection["FirstName"];
            var ln = collection["LastName"];
            var em = collection["Email"];
            var ph = collection["Phone"];
            var matkhau = collection["Password"];
            int kt2 = 0;
            var ktem = data.Users.Where(a => a.Email == em && tk.Email != em);
            var ktph = data.Users.Where(a => a.Phone == em && tk.Phone != em);
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(em))
                ViewData["Loi2"] = "Vui lòng nhập Email";
            else if (String.IsNullOrEmpty(ph))
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại";
            else if (ktem.Count() != 0)
            {
                ViewData["Loi2e"] = "Email đã được sử dụng";
            }
            else if (ktph.Count() != 0)
            {
                ViewData["Loi3e"] = "Số điện thoại đã được sử dụng";
            }
            else
            {
                tk.Password = matkhau;
                tk.Phone = ph;
                tk.Email = em;
                tk.FirstName = fn;
                tk.LastName = ln;
                UpdateModel(tk);
                data.SubmitChanges();
                return RedirectToAction("users");
            }
            return this.edit(id);
        }

       
       
      


    }
}
