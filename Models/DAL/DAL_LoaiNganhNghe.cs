using Models.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAL
{
    public class DAL_LoaiNganhNghe
    {
        private AplicationDBContext context = null;

        public DAL_LoaiNganhNghe()
        {
            this.context = new AplicationDBContext();
        }

        public List<LOAI_NGANH_NGHE> getDanhSach()
        {
            return this.context.LOAI_NGANH_NGHE.ToList();
        }

        public IEnumerable<LOAI_NGANH_NGHE> ListAllPaging(int page, int pageSize , string search)
        {
            if(search != null)
            {
                return this.context.LOAI_NGANH_NGHE.Where(x =>x.TEN_LOAI_VL.Contains(search) && x.IS_REMOVE == false ).OrderBy(x => x.ID_CHA).ToPagedList(page, pageSize);
            }
            return this.context.LOAI_NGANH_NGHE.Where(a=> a.IS_REMOVE == false).OrderBy(x => x.ID_CHA).ToPagedList(page, pageSize);
        }
        

        public bool insertLoaiViecLam(LOAI_NGANH_NGHE vl)
        {
            try
            {
                vl.IS_REMOVE = false;
                vl.NGAY_TAO = DateTime.Now;
                vl.TRANG_THAI = false;
                this.context.LOAI_NGANH_NGHE.Add(vl);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool updateLoaiViecLam( int id , LOAI_NGANH_NGHE lvl)
        {
            try
            {
               
                var lspEdit = this.context.LOAI_NGANH_NGHE.Find(id);
                lspEdit.SLUG = lvl.SLUG;
                lspEdit.ID_CHA = lvl.ID_CHA;
                lspEdit.TEN_LOAI_VL = lvl.TEN_LOAI_VL;
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

       

        public bool Check_isset_danh_muc_insert(string slug)
        {
           
                var lsp = this.context.LOAI_NGANH_NGHE.Where(f => String.Compare(f.SLUG, slug, true) == 0).FirstOrDefault();
                if (lsp == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
           
        }

        public bool Check_isset_danh_muc_update(int id, string slug)
        {
           
                var lsp = this.context.LOAI_NGANH_NGHE.Where(c => String.Compare(c.SLUG, slug, true) == 0 && c.ID_LOAI_VL != id).FirstOrDefault();
                if (lsp == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
           
        }

        public bool ChangeStatus(int id)
        {
           
            var lsp = this.context.LOAI_NGANH_NGHE.Find(id);
            lsp.TRANG_THAI = !lsp.TRANG_THAI;
            this.context.SaveChanges();
            return (bool)!lsp.TRANG_THAI;
            
        }

        public bool DeleteLoaiViecLam(int id)
        {
            try
            {
                var lsp = this.context.LOAI_NGANH_NGHE.Find(id);
                lsp.IS_REMOVE = true;
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public LOAI_NGANH_NGHE returnLoaiViecLam(int id)
        {
            return this.context.LOAI_NGANH_NGHE.Find(id);
        }

    }
}
