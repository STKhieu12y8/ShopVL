using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;
namespace Models.DAL
{
    public class DAL_Level
    {
        private AplicationDBContext context = null;
        public DAL_Level()
        {
            this.context = new AplicationDBContext();
        }
        public List<LEVEL> getDanhSach()
        {
            return this.context.LEVELs.Select(a => a).ToList().Where(a => a.IS_REMOVE == false).ToList();
        }

        public bool insertSize(string ten, string slug, string mota)
        {
            try
            {
                var level = new LEVEL();
                level.TEN_LEVEL = ten;
                level.SLUG = slug;
                level.MO_TA = mota;
                level.TRANG_THAI = true;
                level.IS_REMOVE = false;
                this.context.LEVELs.Add(level);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool editSize(int id, string ten, string slug, string mota)
        {
            try
            {
                var level = this.context.LEVELs.Find(id);
                level.MO_TA = mota;
                level.TEN_LEVEL = ten;
                level.SLUG = slug;
                this.context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public LEVEL returnSize(int id)
        {
            return this.context.LEVELs.Find(id);
        }

        public bool ChangeStatus(int id)
        {
            
                var level = this.context.LEVELs.Find(id);
                level.TRANG_THAI = !level.TRANG_THAI;
                this.context.SaveChanges();
                return (bool)!level.TRANG_THAI;
            
        }

        public bool ValidateInsertLevel(string key, string value)
        {

            var sql = "SELECT * FROM Level WHERE " + key + " = '" + value + "';";
                var size = this.context.LEVELs.SqlQuery(sql).FirstOrDefault();
                if (size != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }

        public bool ValidateUpdateLevel(int id, string key, string value)
        {
        
                var size = this.context.LEVELs.SqlQuery("SELECT * FROM Level WHERE ID != " + id + " AND " + key + " = '" + value + "';").FirstOrDefault();
                if (size != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }

        public bool deleteLevel(int id)
        {
            try
            {
                
                    var level = this.context.LEVELs.Find(id);
                    level.IS_REMOVE = true;
                    this.context.SaveChanges();
                    return true;
               
            }
            catch
            {
                return false;
            }
        }
    }
}
