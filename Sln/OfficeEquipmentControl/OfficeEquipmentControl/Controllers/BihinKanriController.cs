using System;
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
/// 備品管理
/// </summary>
namespace OfficeEquipmentControl.Controllers
{
    /// <summary>
    /// 備品管理コントローラーです。
    /// </summary>
    [Authorize]
    public class BihinKanriController : Controller
    {
        #region Entities
        /// <summary>
        /// OfficeEquipmentControlEntities
        /// </summary>
        private OfficeEquipmentControlEntities db = new OfficeEquipmentControlEntities();
        #endregion
        #region  GET :BihinKanri/Index (自分が借りている備品一覧を表示します)
        /// <summary>
        /// 自分が借りている備品一覧を表示します
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "admin,user")]
        public ActionResult Index()
        {
            var v_備品管理_MAX = db.V_備品管理_MAX.Where(r => r.利用者名 == User.Identity.Name);
            return View(v_備品管理_MAX.ToList());
        }
        #endregion
        #region GET :BihinKanri/Search (備品の検索)
        /// <summary>
        /// 備品の検索
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="状態ID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Search(string sortOrder, int? currentFilter, int? 状態ID, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (状態ID != null)
            {
                page = 1;
            }
            else
            {
                状態ID = currentFilter;
            }

            ViewBag.CurrentFilter = 状態ID;

            var bihin = from s in db.V_備品管理_MAX
                           select s;
            if (!(状態ID == null))
            {
                bihin = bihin.Where(s => s.状態ID == 状態ID);
            }
            switch (sortOrder)
            {
                case "備品ID":
                    bihin = bihin.OrderByDescending(s => s.備品ID);
                    break;
                case "品名":
                    bihin = bihin.OrderByDescending(s => s.品名);
                    break;
                case "型番":
                    bihin = bihin.OrderBy(s => s.型番);
                    break;
                case "備考":
                    bihin = bihin.OrderByDescending(s => s.備考);
                    break;
                default:  // Name ascending 
                    bihin = bihin.OrderBy(s => s.備品ID);
                    break;
            }

            ViewBag.状態ID = new SelectList(db.T_状態, "状態ID", "状態");

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(bihin.ToPagedList(pageNumber, pageSize));
        }
        #endregion
        #region GET :BihinKanri/ItemDetails (備品の詳細を表示します)
        /// <summary>
        /// 備品の詳細を表示します
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion
        #region GET :BihinKanri/Create (備品を追加します)
        /// <summary>
        /// 備品を追加します。
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            T_備品 t_備品 = new T_備品();
            t_備品.更新日 = DateTime.Now;
            t_備品.更新者 = User.Identity.Name;
            return View(t_備品);
        }
        #endregion
        #region POST:BihinKanri/Create (備品を追加します)
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
        #endregion
        #region GET :BihinKanri/Edit (備品管理状態を変更します)
        /// <summary>
        /// 備品管理状態を変更します。
        /// </summary>
        /// <param name="id">備品管理ID</param>
        /// <returns></returns>
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
        #endregion
        #region POST:BihinKanri/Edit (備品管理状態を変更します)
        /// <summary>
        /// 備品管理状態を変更します。
        /// </summary>
        /// <param name="t_備品管理">t_備品管理</param>
        /// <returns></returns>
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
        #endregion
        #region GET :BihinKanri/OneEdit (QRコードで備品管理を行います)
        /// <summary>
        /// QRコードで備品管理を行います。
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
        #endregion
        #region POST:BihinKanri/OneEdit (QRコードで備品管理を行います)
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
        #endregion
        #region GET : BihinKanri/Delete (備品を削除します)
        /// <summary>
        /// 備品を削除します
        /// </summary>
        /// <param name="id">備品id</param>
        /// <returns></returns>
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
        #endregion
        #region POST: BihinKanri/Delete (備品を削除します)
        /// <summary>
        /// 備品を削除します
        /// </summary>
        /// <param name="id">備品id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_備品 t_備品 = db.T_備品.Find(id);
            db.T_備品.Remove(t_備品);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Dispose (アンマネージ リソースを解放します)
        /// <summary>
        /// アンマネージ リソースを解放します。
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
        #endregion
    }
}
