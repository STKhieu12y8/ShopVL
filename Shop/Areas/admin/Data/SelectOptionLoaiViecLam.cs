using Models.DAL;
using Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Areas.admin.Data
{
    public class SelectOptionLoaiViecLam
    {
      
        public int ID_LOAI_VL { get; set; }

        public string TEN_LOAI_VL { get; set; }

        public string SLUG { get; set; }     
      
        public bool? IS_REMOVE { get; set; }
        public int? ID_CHA { get; set; }

        public DateTime? NGAY_TAO { get; set; }
    }

    public class DeQuySelectOption
    {
        public List<SelectOptionLoaiViecLam> listDanhSach;
        public DeQuySelectOption()
        {
            listDanhSach = new List<SelectOptionLoaiViecLam>();
            this.setListDanhSach();
        }
        public void setListDanhSach()
        {
            var listLSP = new DAL_LoaiNganhNghe().getDanhSach();
            foreach(var item in listLSP)
            {
                var option = new SelectOptionLoaiViecLam();
                option.ID_LOAI_VL = item.ID_LOAI_VL;
                option.ID_CHA= item.ID_CHA;
                option.TEN_LOAI_VL = item.TEN_LOAI_VL;
                option.SLUG = item.SLUG;                
                option.IS_REMOVE = item.IS_REMOVE;
                this.listDanhSach.Add(option);
            }
        }

        public List<SelectOptionLoaiViecLam> ListSelectOption()
        {
            var list_dm = this.listDanhSach;
            var list_option = new List<SelectOptionLoaiViecLam>();
            list_option = this.deQuyOptionDanhMuc(this.listDanhSach, list_option, 0);
            return list_option;

        }
        public List<SelectOptionLoaiViecLam> deQuyOptionDanhMuc(List<SelectOptionLoaiViecLam> list, List<SelectOptionLoaiViecLam> list_option, int parentID = 0, string str = "")
        {

            foreach (var item in list)
            {
                if (item.ID_LOAI_VL == parentID)
                {

                    item.TEN_LOAI_VL = str + " " + item.TEN_LOAI_VL;
                    list_option.Add(item);
                    this.deQuyOptionDanhMuc(list, list_option, item.ID_LOAI_VL, string.Concat(str, "--"));
                }
            }
            return list_option;
        }

    }

    public class DeQuyCategory
    {
        public List<LOAI_NGANH_NGHE> listDanhSach;
        public DeQuyCategory()
        {
            listDanhSach = new DAL_LoaiNganhNghe().getDanhSach();
        }
        public List<LOAI_NGANH_NGHE> getChildenLoaiSanPham(List<LOAI_NGANH_NGHE> listLSP, int id = 0)
        {
            foreach (var item in this.listDanhSach)
            {
                
                if (item.ID_CHA == id)
                { 
                    listLSP.Add(item);
                    this.getChildenLoaiSanPham(listLSP, item.ID_LOAI_VL);
                }
            }
            return listLSP;
        }

        public List<LOAI_NGANH_NGHE> getParentLoaiSanPham(List<LOAI_NGANH_NGHE> listLSP , int parent_id)
        {
            foreach (var item in this.listDanhSach)
            {
                if (item.ID_LOAI_VL == parent_id)
                {
                    listLSP.Add(item);
                    
                    if(item.ID_CHA == 0)
                    {
                        return listLSP;
                    }
                    this.getParentLoaiSanPham(listLSP, item.ID_CHA??0);
                }
            }
            return listLSP;
        }
    }
}

