using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ADO;
using Models.EntityFramework;
namespace Models.DAL
{
    public class DAL_ViecLam
    {
        private AplicationDBContext context = null;

        public DAL_ViecLam()
        {
            this.context = new AplicationDBContext();
        }

        /// insert dữ liệu vào bảng Sản Phẩm ;
        public bool insertSanPham(VIEC_LAM sp)
        {
            try
            {
                sp.IS_REMOVE = false;
                this.context.VIEC_LAM.Add(sp);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //phân trang
        public List<ModelsSanPham> GetPageSanPham(int page, int itemsPerPage, out int totalCount , string search)
        {
            var list = new List<ModelsSanPham>();
            var arrSanPham = new List<VIEC_LAM>();
            if (search!= null)
            {
                arrSanPham = this.context.VIEC_LAM.Where(a => a.TEN_VL.Contains(search) && a.IS_REMOVE == false).OrderBy(c => c.NGAY_TAO).Skip(itemsPerPage * page).Take(itemsPerPage).ToList();
                totalCount = this.context.VIEC_LAM.Where(a => a.TEN_VL.Contains(search) && a.IS_REMOVE == false).Count();
            }
            else
            {//skip: loại bỏ số phần tử từ đầu
                //take: lấy số phần tử từ đầu
                arrSanPham = this.context.VIEC_LAM.Where(a => a.IS_REMOVE == false).OrderBy(c => c.NGAY_TAO).Skip(itemsPerPage * page).Take(itemsPerPage)
                     .ToList();
                totalCount = this.context.VIEC_LAM.Where(a => a.IS_REMOVE == false).Count();
            }
            foreach ( var item in arrSanPham)
            {
                list.Add(convertSanPham(item));
            }
            return list;
        }
        //chuyển sản phẩm để lấy cả sản phẩm, bảng size và màu
        public ModelsSanPham convertSanPham(VIEC_LAM item)
        {           
            var DAL_LSP = new DAL_LoaiNganhNghe();
            var sp = new ModelsSanPham();
            if (item == null)
            {
                return sp;
            }
            sp.ID_LVL = item.ID_LVL;
            sp.TEN_VL = item.TEN_VL;
            sp.MO_TA = item.MO_TA;
            sp.MO_TA_CHI_TIET = item.MO_TA_CHI_TIET;
            sp.SLUG = item.SLUG;
            sp.NOI_BAT = item.NOI_BAT;
            sp.LINK_ANH_CHINH = item.LINK_ANH_CHINH;
            sp.LIST_ANH_KEM = item.LIST_ANH_KEM;
            sp.NGAY_TAO = item.NGAY_TAO;
            sp.SO_LUONG_TONG = item.SO_LUONG_TONG;


            sp.ID_LVL = item.ID_LVL;

            sp.MUC_LUONG = item.MUC_LUONG;
            sp.TenLoaiSP = DAL_LSP.returnLoaiViecLam(sp.ID_LVL).TEN_LOAI_VL;

            return sp;
        }

        public ModelsSanPham getSanPham(string ma)
        {
            var sp = this.context.VIEC_LAM.Find(ma);
            return this.convertSanPham(sp);
        }

        public VIEC_LAM ReturnSAN_PHAM(string ma)
        {
            return this.context.VIEC_LAM.Find(ma);
        }


        public bool checkQueryFirstOrDefault(string sql)
        {
            var sp = this.context.VIEC_LAM.SqlQuery(sql).FirstOrDefault();
            if (sp != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateSanPham( string masp ,  VIEC_LAM sp)
        {
            try
            {
                var sanpham = this.context.VIEC_LAM.Find(masp);
                if(sanpham != null)
                {
                    sanpham.ID_LVL = sp.ID_LVL;
                    sanpham.MUC_LUONG = sp.MUC_LUONG;
                    
                    if (sp.LINK_ANH_CHINH != "")
                    {
                        sanpham.LINK_ANH_CHINH = sp.LINK_ANH_CHINH;
                    }
                    if (sp.NOI_BAT != null)
                    {
                        sanpham.NOI_BAT = sp.NOI_BAT;
                    }                    
                    if (sp.LIST_ANH_KEM != "")
                    {
                        sanpham.LIST_ANH_KEM = sp.LIST_ANH_KEM;
                    }
                    sanpham.MO_TA = sp.MO_TA;
                    sanpham.MO_TA_CHI_TIET = sp.MO_TA_CHI_TIET;                    
                    sanpham.ID_LVL = sp.ID_LVL;
                    sanpham.SLUG = sp.SLUG;
                    this.context.SaveChanges();
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteSP(string id)
        {
            var sp = this.context.VIEC_LAM.SingleOrDefault(a => a.MA_VL == id);
            if(sp!= null)
            {
                sp.IS_REMOVE = true;
                this.context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    

}
