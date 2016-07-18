﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeEquipmentControl.Models;
using PagedList; // ページリスト

/// <summary>
/// OfficeEquipmentControl.Controllers
/// </summary>
namespace OfficeEquipmentControl.Controllers
{
    /// <summary>
    /// 備品管理コントローラーです。
    /// </summary>
    [Authorize]
    public class BihinKanriController : Controller
    {
        #region エンティティ
        /// <summary>
        /// OfficeEquipmentControlEntities
        /// </summary>
        private OfficeEquipmentControlEntities db = new OfficeEquipmentControlEntities();
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "admin,user")]
        public ActionResult Index()
        {
            var v_備品管理_MAX = db.V_備品管理_MAX.Where(r => r.利用者名 == User.Identity.Name);
            return View(v_備品管理_MAX.ToList());
        }
        #endregion

        #region Search
        [Authorize(Roles = "admin")]
        public ActionResult Search(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.V_備品管理_MAX
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.品名.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "備品ID":
                    students = students.OrderByDescending(s => s.備品ID);
                    break;
                case "品名":
                    students = students.OrderByDescending(s => s.品名);
                    break;
                case "型番":
                    students = students.OrderBy(s => s.型番);
                    break;
                case "備考":
                    students = students.OrderByDescending(s => s.備考);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.備品ID);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        // GET: BihinKanri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_備品管理 t_備品管理 = db.T_備品管理.Find(id);
            if (t_備品管理 == null)
            {
                return HttpNotFound();
            }
            return View(t_備品管理);
        }

        public ActionResult ItemDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_備品 t_備品 = db.T_備品.Find(id);
            if (t_備品 == null)
            {
                return HttpNotFound();
            }
            return View(t_備品);
        }

        
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            T_備品 t_備品 = new T_備品();
            t_備品.更新日 = DateTime.Now;
            t_備品.更新者 = User.Identity.Name;
            return View(t_備品);
        }

        // POST: Bihin/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "備品ID,品名,型番,備考,TS,更新日,更新者")] T_備品 t_備品)
        {
            if (ModelState.IsValid)
            {
                db.T_備品.Add(t_備品);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_備品);
        }


        // GET: BihinKanri/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_備品管理 t_備品管理 = db.T_備品管理.Find(id);
            if (t_備品管理 == null)
            {
                return HttpNotFound();
            }
            ViewBag.状態ID = new SelectList(db.T_状態, "状態ID", "状態", t_備品管理.状態ID);
            ViewBag.備品ID = new SelectList(db.T_備品, "備品ID", "品名", t_備品管理.備品ID);
            return View(t_備品管理);
        }

        // POST: BihinKanri/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "備品管理ID,備品ID,状態ID,利用者名,貸出返却日,TS,更新日,更新者")] T_備品管理 t_備品管理)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_備品管理).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.状態ID = new SelectList(db.T_状態, "状態ID", "状態", t_備品管理.状態ID);
            ViewBag.備品ID = new SelectList(db.T_備品, "備品ID", "品名", t_備品管理.備品ID);
            return View(t_備品管理);
        }

        // GET: BihinKanri/OneEdit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OneEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 備品IDの履歴で最新の履歴を取得
            V_備品管理_MAX v_備品管理_MAX = db.V_備品管理_MAX.Where(r => r.備品ID == id).First();
            if (v_備品管理_MAX == null)
            {
                return HttpNotFound();
            }


            T_備品管理 t_備品管理 = db.T_備品管理.Find(v_備品管理_MAX.備品管理ID);

            if (t_備品管理 == null)
            {
                return HttpNotFound();
            }

            switch (t_備品管理.状態ID)
            {
                case 1: // 
                    ViewBag.BtnState = "借りる";
                    ViewBag.MsgStr = "借り";
                    break;
                case 2: // 
                    ViewBag.BtnState = "返す";
                    ViewBag.MsgStr = "借し";
                    break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.状態ID = new SelectList(db.T_状態, "状態ID", "状態", t_備品管理.状態ID);
            ViewBag.備品ID = new SelectList(db.T_備品, "備品ID", "品名", t_備品管理.備品ID);

            return View(t_備品管理);
        }

        // POST: BihinKanri/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_備品管理"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OneEdit([Bind(Include = "備品管理ID,備品ID,状態ID,利用者名,貸出返却日,TS,更新日,更新者")] T_備品管理 t_備品管理)
        {
            if (ModelState.IsValid)
            {
                //トランザクション開始 
                using (var tx = db.Database.BeginTransaction())
                {

                    // 既に更新されていないか確認する。
                    if (t_備品管理.TS.SequenceEqual(db.T_備品管理.Find(t_備品管理.備品管理ID).TS))
                    {

                        switch (t_備品管理.状態ID)
                        {
                            case 1: // 借りる場合
                                t_備品管理.状態ID = 2; // 貸出中にする
                                break;
                            case 2: // 借りる場合
                                t_備品管理.状態ID = 3; // 返却済にする
                                // 貸出可能にするのはSQL Server内のトリガーで行う
                                break;
                            default:
                                // それ以外はエラーとする。
                                break;
                        }

                        DateTime dt = DateTime.Now; // 時間取得

                        t_備品管理.利用者名 = User.Identity.Name;
                        t_備品管理.貸出返却日 = dt;
                        t_備品管理.更新日 = dt;

                        db.Entry(t_備品管理).State = EntityState.Added;
                        db.SaveChanges();
                        tx.Commit();
                    }
                    else
                    {
                        // 既に更新されている場合。

                        tx.Rollback(); // ロールバック
                    }

                }

                return RedirectToAction("Index");
            }

            ViewBag.状態ID = new SelectList(db.T_状態, "状態ID", "状態", t_備品管理.状態ID);
            ViewBag.備品ID = new SelectList(db.T_備品, "備品ID", "品名", t_備品管理.備品ID);
            return View(t_備品管理);
        }


        // GET: BihinKanri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            T_備品 t_備品 = db.T_備品.Find(id);
            if (t_備品 == null)
            {
                return HttpNotFound();
            }

            return View(t_備品);
        }

        // POST: BihinKanri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_備品管理 t_備品管理 = db.T_備品管理.Find(id);
            db.T_備品管理.Remove(t_備品管理);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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