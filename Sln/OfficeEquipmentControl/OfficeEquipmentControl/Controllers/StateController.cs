using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeEquipmentControl.Models;

namespace OfficeEquipmentControl.Controllers
{
    public class StateController : Controller
    {
        private OfficeEquipmentControlEntities db = new OfficeEquipmentControlEntities();

        // GET: State
        public ActionResult Index()
        {
            return View(db.T_状態.ToList());
        }

        // GET: State/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_状態 t_状態 = db.T_状態.Find(id);
            if (t_状態 == null)
            {
                return HttpNotFound();
            }
            return View(t_状態);
        }

        // GET: State/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: State/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "状態ID,状態,TS,更新日,更新者")] T_状態 t_状態)
        {
            if (ModelState.IsValid)
            {
                db.T_状態.Add(t_状態);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_状態);
        }

        // GET: State/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_状態 t_状態 = db.T_状態.Find(id);
            if (t_状態 == null)
            {
                return HttpNotFound();
            }
            return View(t_状態);
        }

        // POST: State/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "状態ID,状態,TS,更新日,更新者")] T_状態 t_状態)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_状態).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_状態);
        }

        // GET: State/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_状態 t_状態 = db.T_状態.Find(id);
            if (t_状態 == null)
            {
                return HttpNotFound();
            }
            return View(t_状態);
        }

        // POST: State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_状態 t_状態 = db.T_状態.Find(id);
            db.T_状態.Remove(t_状態);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
