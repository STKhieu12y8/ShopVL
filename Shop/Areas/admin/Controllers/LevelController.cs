using Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class LevelController : BaseController
    {
        // GET: admin/Size
        
        public ActionResult Index()
        {
            var list_size = new DAL_Level().getDanhSach();
            return View(list_size);
        }




        // kiểm tra Size tồn tại trong sql khi insert
        // GET: admin/Size/ValidateInsert
        public JsonResult ValidateInsert(FormCollection collection)
        {
            var key = collection["key"];
            var value = collection["value"];

            var result = new DAL_Level().ValidateInsertLevel(key, value);
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateEdit(int id, FormCollection collection)
        {
            var key = collection["key"];
            var value = collection["value"];

            var result = new DAL_Level().ValidateUpdateLevel(id, key, value);
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }


        // POST: admin/Size/Create
        [HttpPost]
        public JsonResult Create(FormCollection collection)
        {
            var ten = collection["tenSize"];
            var slug = collection["Slug"];
            var mota = collection["MoTa"];
            var db = new DAL_Level();
            var result = db.insertSize(ten, slug, mota);
            if (result)
            {
                return Json(new
                {
                    text = "Thêm thành công !!!",
                    status = "success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = "error",
                    text = "Không thể thêm Size !!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: admin/Size/Edit/5
        public JsonResult Edit(int id)
        {
            var color = new DAL_Level().returnSize(id);
            return Json(new
            {
                value = color
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: admin/Size/Edit/5
        [HttpPost]
        public JsonResult Edit(int id, FormCollection collection)
        {
            var ten = collection["tenSize"];
            var slug = collection["Slug"];
            var mota = collection["MoTa"];
            var db = new DAL_Level();
            var result = db.editSize(id, ten, slug, mota);
            if (result)
            {
                return Json(new
                {
                    text = "Sửa thành công !!!",
                    status = "success"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = "error",
                    text = "Không thể sửa Level !!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: admin/Size/Delete/5
        public JsonResult Delete(int id)
        {
            var result = new DAL_Level().deleteLevel(id);

            return Json(new
            {
                status = (result) ? "success" : "error",
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(int id)
        {
            var result = new DAL_Level().ChangeStatus(id);
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}