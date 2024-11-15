﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreWebMvc.Models;
using PagedList;
using PagedList.Mvc;

namespace BookStoreWebMvc.Controllers
{
    public class BookStoreController : Controller
    {
        QLBansachEntities qLBansachEntities = new QLBansachEntities();

        private List<SACH> Laysachmoi(int count)
        {
            return qLBansachEntities.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        private List<CHUDE> Laychude()
        {
            return qLBansachEntities.CHUDEs.ToList();
        }

        private List<NHAXUATBAN>  Laynhaxuatban()
        {
            return qLBansachEntities.NHAXUATBANs.ToList();
        }
        // GET: BookStore
        public ActionResult Index(int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var sachmoi = qLBansachEntities.SACHes.OrderByDescending(a => a.Ngaycapnhat).ToPagedList(pageNum, pageSize);
            var chudeList = Laychude();
            var nhaxuatbanList = Laynhaxuatban();

            var viewModel = new BookStoreVM
            {
                saches = sachmoi,
                chudes = chudeList,
                nhaxuatbans = nhaxuatbanList,
            };

            return View(viewModel);
        }

        // Phương thức lọc sách theo danh mục chủ đề
        public ActionResult FilterByCategory(int id, int? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var sachTheoDanhMuc = qLBansachEntities.SACHes.Where(s => s.MaCD == id).ToPagedList(pageNum, pageSize);
            var viewModel = new BookStoreVM
            {
                saches = sachTheoDanhMuc,
                chudes = Laychude(),
                nhaxuatbans = Laynhaxuatban()
            };

            return View("Index", viewModel); // Render lại view Index với kết quả lọc
        }

        // Phương thức lọc sách theo nhà xuất bản
        public ActionResult FilterByPublisher(int id, int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);

            var sachTheoNhaXuatBan = qLBansachEntities.SACHes.Where(s => s.MaNXB == id).ToPagedList(pageNum, pageSize);
            var viewModel = new BookStoreVM
            {
                saches = sachTheoNhaXuatBan,
                chudes = Laychude(),
                nhaxuatbans = Laynhaxuatban()
            };

            return View("Index", viewModel); // Render lại view Index với kết quả lọc
        }

        public ActionResult Details(int id)
        {
            var sach = qLBansachEntities.SACHes.FirstOrDefault(s => s.Masach == id);

            if (sach == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy sách
            }

            return View(sach); // Truyền đối tượng sách vào view chi tiết
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}