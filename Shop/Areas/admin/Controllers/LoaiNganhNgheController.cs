using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models.DAL;
using Models.EntityFramework;
using Shop.Areas.admin.Data;


namespace Shop.Areas.admin.Controllers
{
    public class LoaiNganhNgheController : BaseController
    {
        private AplicationDBContext db = new AplicationDBContext();

        // GET: admin/lOAI_NGANH_NGHE
        public ActionResult Index(int page = 1, string search = null)
        {
            var dalLSP = new DAL_LoaiNganhNghe();
            ViewBag.Search = search;
            var model = dalLSP.ListAllPaging(page, 5, search);
            return View(model);
        }

        // GET: admin/lOAI_NGANH_NGHE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI_NGANH_NGHE lOAI_NGANH_NGHE = db.LOAI_NGANH_NGHE.Find(id);
            if (lOAI_NGANH_NGHE == null)
            {
                return HttpNotFound();
            }
            return View(lOAI_NGANH_NGHE);
        }

        // GET: admin/lOAI_NGANH_NGHE/Create
        public ActionResult Create()
        {

            this.SetSelectOption();
            return View();
        }

        public void SetSelectOption(int? selected = null)
        {
            var ListSelectOption = new DeQuySelectOption().ListSelectOption().Where(c => c.IS_REMOVE == false).ToList();
            var lsp = new SelectOptionLoaiViecLam();
            lsp.ID_CHA = 0;
            lsp.TEN_LOAI_VL = " --- Không thuộc loại nào ---";
            ListSelectOption.Insert(0, lsp);
            ViewBag.MovieType = new SelectList(ListSelectOption, "ID_LOAI_VL", "TEN_LOAI_VL", selected);
        }

        // POST: admin/lOAI_NGANH_NGHE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LOAI_NGANH_NGHE lOAI_NGANH_NGHE)
        {
            var DB = new DAL_LoaiNganhNghe();
            if (ModelState.IsValid)
            {
                var check = DB.Check_isset_danh_muc_insert(lOAI_NGANH_NGHE.SLUG);
                if (check)
                {
                    lOAI_NGANH_NGHE.IS_REMOVE = false;
                    bool insert = DB.insertLoaiViecLam(lOAI_NGANH_NGHE);
                    if (check)
                    {
                        ViewBag.Success = "Thêm Thành Công !!! ";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Err = "Không chèn được ";
                        SetSelectOption();
                        return View(lOAI_NGANH_NGHE);
                    }
                }
                else
                {
                    ViewBag.Err = "Loại ngành nghề đã tồn tại";
                    SetSelectOption();
                    return View(lOAI_NGANH_NGHE);
                }

            }
            SetSelectOption();
            return View(lOAI_NGANH_NGHE);
        }

        // GET: admin/lOAI_NGANH_NGHE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.SetSelectOption(id);

            LOAI_NGANH_NGHE lOAI_NGANH_NGHE = db.LOAI_NGANH_NGHE.Find(id);

            if (lOAI_NGANH_NGHE == null)
            {
                return HttpNotFound();
            }
            return View(lOAI_NGANH_NGHE);
        }

        // POST: admin/lOAI_NGANH_NGHE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LOAI_NGANH_NGHE lOAI_NGANH_NGHE)
        {
            if (ModelState.IsValid)
            {
                var DB = new DAL_LoaiNganhNghe();
                var check = DB.Check_isset_danh_muc_update(lOAI_NGANH_NGHE.ID_LOAI_VL, lOAI_NGANH_NGHE.SLUG);
                if (check)
                {
                    bool insert = DB.updateLoaiViecLam(lOAI_NGANH_NGHE.ID_LOAI_VL, lOAI_NGANH_NGHE);
                    if (insert)
                    {
                        ViewBag.Success = "Sửa Thành Công !!! ";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Err = "Không sửa được ";
                        SetSelectOption((int)lOAI_NGANH_NGHE.ID_CHA);
                        return View(lOAI_NGANH_NGHE);
                    }
                }
                else
                {
                    ViewBag.Err = "Loại sản phẩm đã tồn tại";
                    SetSelectOption((int)lOAI_NGANH_NGHE.ID_CHA);
                    return View(lOAI_NGANH_NGHE);
                }

                return RedirectToAction("Index");
            }
            SetSelectOption(lOAI_NGANH_NGHE.ID_CHA);
            return View(lOAI_NGANH_NGHE);
        }

        public JsonResult Delete(int id)
        {
            var result = new DAL_LoaiNganhNghe().DeleteLoaiViecLam(id);

            return Json(new
            {
                status = (result) ? "success" : "error",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id)
        {
            var result = new DAL_LoaiNganhNghe().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


        // POST: admin/lOAI_NGANH_NGHE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOAI_NGANH_NGHE lOAI_NGANH_NGHE = db.LOAI_NGANH_NGHE.Find(id);
            db.LOAI_NGANH_NGHE.Remove(lOAI_NGANH_NGHE);
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
