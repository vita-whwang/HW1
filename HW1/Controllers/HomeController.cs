using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW1.Models;

namespace HW1.Controllers
{
    public class HomeController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        客戶資訊Repository ReportRepository = RepositoryHelper.Get客戶資訊Repository();
        public ActionResult Index()
        {
            return View(ReportRepository.All());
        }

    }
}