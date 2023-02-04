using Models.ADO;
using Models.DAL;
using Models.EntityFramework;
using Newtonsoft.Json;
using PagedList;
using Shop.Areas.admin.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop.Areas.admin.Controllers
{
    public class ViecLamController : BaseController
    {
        // GET: admin/SanPham
        protected AplicationDBContext db = new AplicationDBContext();
        protected DAL_LoaiNganhNghe DalLVL { get; set; }
        protected DAL_ViecLam DalVL { get; set; }
        protected DAL_Level DalLevel { get; set; }


        public ViecLamController()
        {
            this.DalLevel = new DAL_Level();
            this.DalVL = new DAL_ViecLam();

            this.DalLVL = new DAL_LoaiNganhNghe();
        }

        public ActionResult Index(int? page, string search = null)
        {
            int pagenumber = (page ?? 1) - 1;
            int totalCount = 0;
            ViewBag.Search = search;
            var listViecLam = this.DalVL.GetPageSanPham(pagenumber, 5, out totalCount, search);
            IPagedList<ModelsSanPham> pageOrders = new StaticPagedList<ModelsSanPham>(listViecLam, pagenumber + 1, 5, totalCount);
            return View(pageOrders);
        }

        public void setSelectOptionSize(int? selected = null)
        {
            var listSize = this.DalLevel.getDanhSach();
            ViewBag.ListSize = new SelectList(listSize, "ID", "TEN_LEVEL", selected);
        }

        //Ajax form create detail Sản phẩm :
        public JsonResult ajaxSelectOptionSize()
        {
            var listSize = this.DalLevel.getDanhSach();
            return Json(new
            {
                arrSize = listSize
            }, JsonRequestBehavior.AllowGet);
        }

        //Ajax form create Sản phẩm :
        public JsonResult ValidateInsertSlug(FormCollection collection)
        {
            var key = collection["key"];
            var value = collection["value"];
            var sql = "SELECT * FROM SAN_PHAM WHERE " + key + " = '" + value + "';";
            var result = this.DalVL.checkQueryFirstOrDefault(sql);
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: admin/SanPham/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: admin/SanPham/Create
        public ActionResult Create()
        {
            this.SetSelectOptionLSP();
            this.setSelectOptionSize();
            return View();
        }

        public void SetSelectOptionLSP(int? selected = null)
        {
            var ListSelectOption = new DeQuySelectOption().ListSelectOption().Where(c => c.IS_REMOVE == false).ToList();
            ViewBag.MovieType = new SelectList(ListSelectOption, "ID_LOAI_VL", "TEN_LOAI_VL", selected);
        }


        // POST: admin/SanPham/Create  => Sử dụng Ajax
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var sanPham = new VIEC_LAM();
                sanPham.MA_VL = collection["ma-sp"];
                sanPham.ID_LVL = int.Parse(collection["loai-san-pham"]);
                sanPham.TEN_VL = collection["ten-sp"];
                sanPham.SLUG = collection["slug"];
                sanPham.MO_TA = collection["mo-ta"];
                sanPham.MO_TA_CHI_TIET = collection["mo-ta-chi-tiet"];
                sanPham.MUC_LUONG = decimal.Parse(collection["gia-ban"]);
                sanPham.LINK_ANH_CHINH = this.SaveUploadImage(Request.Files["anh-chinh"]);
                sanPham.NOI_BAT = Boolean.Parse(collection["noi-bat"]);
                var str_list_anh = "";
                foreach (var file in Request.Files.GetMultiple("list-anh"))
                {
                    str_list_anh += SaveUploadImage(file) + ",";
                }
                sanPham.LIST_ANH_KEM = str_list_anh.Trim(',');
                sanPham.NGAY_TAO = DateTime.Now;
                sanPham.SO_LUONG_TONG = 0;

                if (this.DalVL.insertSanPham(sanPham))
                {
                    var jsonDetails = JsonConvert.DeserializeObject<List<JsonDetails>>(collection["details"]);
                    var imageColor = Request.Files.GetMultiple("anh-mau");

                    for (var i = 0; i < jsonDetails.Count; i++)
                    {
                        foreach (var item in jsonDetails[i].sizeQuantity)
                        {

                            var spct = new VIEC_LAM_CHI_TIET();
                            spct.SO_LUONG = int.Parse(item.quantity);
                            spct.ID_SIZE = int.Parse(item.size);
                            spct.NGAY_TAO = DateTime.Now;
                            spct.TRANG_THAI = false;
                            spct.MA_SP = sanPham.MA_VL;

                        }

                    }

                    return Json(new
                    {
                        status = "success"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    this.DropFileImages(sanPham.LINK_ANH_CHINH);
                    foreach (var i in str_list_anh.Split(','))
                    {
                        this.DropFileImages(i);
                    }
                    return Json(new
                    {
                        status = "error"
                    }, JsonRequestBehavior.AllowGet);
                }
                /// thông tin chi tiết từng màu - size : 
            }
            catch
            {
                return Json(new
                {
                    status = "error"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private void DropFileImages(string strFile)
        {
            var file = Server.MapPath(strFile);
            System.IO.File.Delete(file);
        }

        private string SaveUploadImage(HttpPostedFileBase file)
        {
            var str_file = "";
            Random random = new Random();
            if (file != null && file.ContentLength > 0)
            {
                //Định nghĩa đường dẫn lưu file trên server
                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadFileImage/images/") + file.FileName);
                string newFileName = file.FileName;
                //lấy đường dẫn lưu file sau khi kiểm tra tên file trên server có tồn tại hay không
                while (System.IO.File.Exists(ServerSavePath) == true)
                {
                    string filename = Path.GetFileNameWithoutExtension(newFileName);
                    string extension = Path.GetExtension(newFileName);
                    newFileName = filename + "_" + random.Next(1, 1000) + extension;
                    ServerSavePath = Path.Combine(Server.MapPath("~/UploadFileImage/images/") + newFileName);
                }
                //Lưu hình ảnh Resize từ file sử dụng file.InputStream
                file.SaveAs(ServerSavePath);

                str_file = "/UploadFileImage/images/" + newFileName;
            }
            return str_file;// trả về đường dẫn file trong thư mục
        }


        [HttpGet]

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIEC_LAM sAN_PHAM = this.DalVL.ReturnSAN_PHAM(id.Trim());

            if (sAN_PHAM == null)
            {
                return HttpNotFound();
            }
            this.setSelectOptionSize();
            this.SetSelectOptionLSP(sAN_PHAM.ID_LVL);

            ViewBag.Message = TempData["status-edit"];
            ViewBag.StatusCreate = TempData["status-create"];
            return View(sAN_PHAM);
        }


        // POST: admin/SanPham/Edit/5


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_VL,ID_LVL,TEN_VL,SLUG,MO_TA,MO_TA_CHI_TIET,LINK_ANH_CHINH,MUC_LUONG,DON_VI_TINH")] VIEC_LAM vIEC_LAM)
        {
            this.setSelectOptionSize();
            this.SetSelectOptionLSP(vIEC_LAM.ID_LVL);
            vIEC_LAM.LIST_ANH_KEM = "";
            vIEC_LAM.LINK_ANH_CHINH = "";
            vIEC_LAM.NOI_BAT = Request.Form["NoiBat"].Contains("true") ? true : false;
            if (ModelState.IsValid)
            {
                var Sql = "Select * from VIEC_LAM where MA_VL!='" + vIEC_LAM.MA_VL + "' AND SLUG = '" + vIEC_LAM.SLUG + "'";
                if (!this.DalVL.checkQueryFirstOrDefault(Sql))
                {
                    vIEC_LAM.LINK_ANH_CHINH = (Request.Files.Get("product-images").ContentLength > 0) ? SaveUploadImage(Request.Files.Get("product-images")) : this.DalVL.getSanPham(vIEC_LAM.MA_VL).LINK_ANH_CHINH;
                    string str_list_anh = "";
                    foreach (var file in Request.Files.GetMultiple("product-list-images"))
                    {
                        str_list_anh += SaveUploadImage(file) + ",";
                    }
                    vIEC_LAM.LIST_ANH_KEM = (str_list_anh.Trim(',') != "") ? str_list_anh.Trim(',') : this.DalVL.getSanPham(vIEC_LAM.MA_VL).LIST_ANH_KEM;

                    if (this.DalVL.UpdateSanPham(vIEC_LAM.MA_VL, vIEC_LAM))
                    {

                        ViewBag.Status = "Success";
                        return View(this.DalVL.ReturnSAN_PHAM(vIEC_LAM.MA_VL));
                    }
                    else
                    {
                        ViewBag.Status = "Error";
                        return View(vIEC_LAM);
                    }


                }
                else
                {
                    ViewBag.Status = "Sản Phẩm đã tồn tại";
                    return View(vIEC_LAM);
                }

            }
            return View(vIEC_LAM);
        }


        /// hàm method post sửa màu và hình ảnh , thông tin sản phẩm theo màu        


        //  hàm method post tạo mới 1 màu - hình ảnh - size - trong form (edit)
        /*[HttpPost]
        public ActionResult CreateDetails(string id, FormCollection collection)
        {
            var check = this.DalColor.checkIssetInsertColorMasp(id, collection["slug-create"]);
            if (check)
            {
                TempData["status-create"] = "Màu sản phẩm đã tồn tại !!!";
                return RedirectToAction("Edit", "ViecLam", new { id = id });
            }
            else
            {
                var arrSize = collection["size-create"].Split(',');
                var arrSL = collection["quantity-create"].Split(',');
                var color = new COLOR();
                color.IMAGES = SaveUploadImage(Request.Files.Get("image-color-create"));
                color.MA_MAU = collection["ma-mau-create"];
                color.TEN_MAU = collection["ten-mau-create"];
                color.TRANG_THAI = true;
                color.SLUG = collection["slug-create"];
                color.MA_SP = id;
                var idColor = this.DalColor.insertColor(color);
                try
                {
                    for (var i = 0; i < arrSize.Length; i++)
                    {
                        var spct = new VIEC_LAM_CHI_TIET();
                        spct.ID_COLOR = idColor;
                        spct.MA_SP = id;
                        spct.ID_SIZE = int.Parse(arrSize[i]);
                        spct.SO_LUONG = int.Parse(arrSL[i]);
                        spct.SLUG = this.DalVL.ReturnSAN_PHAM(id).SLUG;
                        spct.TRANG_THAI = true;
                        spct.NGAY_TAO = DateTime.Now;
                        this.DalSPCT.insertSanPhamChiTiet(spct);
                    }
                }
                catch (Exception ex)
                {
                    db.COLORs.Remove(db.COLORs.Find(idColor));
                }

                TempData["status-create"] = "success";
                return RedirectToAction("Edit", "SanPham", new { id = id });
            }

        }
        */
        // GET: admin/SanPham/Delete/5
        public ActionResult Delete(string id)
        {
            var result = new DAL_ViecLam().deleteSP(id);

            return Json(new
            {
                status = (result) ? "success" : "error",
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
