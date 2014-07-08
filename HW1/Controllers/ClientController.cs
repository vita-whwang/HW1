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
    public class ClientController : Controller
    {
        //private 資料庫Entities db = new 資料庫Entities();
        客戶資料Repository ClientRepository = RepositoryHelper.Get客戶資料Repository();
        客戶聯絡人Repository ContactRepository = RepositoryHelper.Get客戶聯絡人Repository();
        客戶銀行資訊Repository BankRepository = RepositoryHelper.Get客戶銀行資訊Repository();
        
        // GET: /Client/
        public ActionResult Index()
        {

            return View(ClientRepository.GetAll().ToList());
        }

        // GET: /Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 =ClientRepository.FindClientById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ClientRepository.Add(客戶資料);
                ClientRepository.UnitOfWork.Commit();
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: /Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = ClientRepository.FindClientById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: /Client/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ClientRepository.UnitOfWork.Context.Entry(客戶資料).State = System.Data.Entity.EntityState.Modified;
                ClientRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: /Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = ClientRepository.FindClientById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = ClientRepository.FindClientById(id);
            foreach (var item in 客戶資料.客戶聯絡人)
            {
                item.isDelete = true;
            }

            foreach (var item in 客戶資料.客戶銀行資訊)
            {
                item.isDelete = true;
            }

            客戶資料.isDelete = true;
            ClientRepository.UnitOfWork.Context.Entry(客戶資料).State = System.Data.Entity.EntityState.Modified;
            ClientRepository.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

    }
}
