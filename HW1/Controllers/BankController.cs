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
    public class BankController : Controller
    {
        //private 資料庫Entities db = new 資料庫Entities();
        客戶資料Repository ClientRepository = RepositoryHelper.Get客戶資料Repository();
        客戶銀行資訊Repository BankRepository = RepositoryHelper.Get客戶銀行資訊Repository();
        

        // GET: /Bank/
        public ActionResult Index()
        {
            var 客戶銀行資訊 = BankRepository.GetAll().Include(客 => 客.客戶資料);
            return View(客戶銀行資訊.ToList());
        }

        // GET: /Bank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 =BankRepository.FindBankById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: /Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱");
            return View();
        }

        // POST: /Bank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                BankRepository.Add(客戶銀行資訊);
                BankRepository.UnitOfWork.Commit();
                //db.客戶銀行資訊.Add(客戶銀行資訊);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: /Bank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = BankRepository.FindBankById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: /Bank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                BankRepository.UnitOfWork.Context.Entry(客戶銀行資訊).State = System.Data.Entity.EntityState.Modified;
                BankRepository.UnitOfWork.Commit();
                //db.Entry(客戶銀行資訊).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: /Bank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = BankRepository.FindBankById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: /Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = BankRepository.FindBankById(id);
            客戶銀行資訊.isDelete = true;
            BankRepository.UnitOfWork.Context.Entry(客戶銀行資訊).State = System.Data.Entity.EntityState.Modified;
            BankRepository.UnitOfWork.Commit();
            //db.客戶銀行資訊.Remove(客戶銀行資訊);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
