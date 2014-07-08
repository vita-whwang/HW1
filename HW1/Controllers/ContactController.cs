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
    public class ContactController : Controller
    {
        //private 資料庫Entities db = new 資料庫Entities();
        客戶聯絡人Repository ContactRepository = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository ClientRepository = RepositoryHelper.Get客戶資料Repository();
        
        // GET: /Contact/
        public ActionResult Index()
        {
            var 客戶聯絡人 = ContactRepository.GetAll().Include(客 => 客.客戶資料);
            return View(客戶聯絡人.ToList());
        }

        // GET: /Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepository.FindContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: /Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱");
            return View();
        }

        // POST: /Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ContactRepository.Add(客戶聯絡人);
                ContactRepository.UnitOfWork.Commit();
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepository.FindContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: /Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ContactRepository.UnitOfWork.Context.Entry(客戶聯絡人).State = System.Data.Entity.EntityState.Modified;
                ContactRepository.UnitOfWork.Commit();

                //db.Entry(客戶聯絡人).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(ClientRepository.GetAll(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepository.FindContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = ContactRepository.FindContactById(id);
            客戶聯絡人.isDelete = true;
            ContactRepository.UnitOfWork.Context.Entry(客戶聯絡人).State = System.Data.Entity.EntityState.Modified;
            ContactRepository.UnitOfWork.Commit();
            //db.客戶聯絡人.Remove(客戶聯絡人);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
