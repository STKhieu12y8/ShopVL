using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EntityFramework;
namespace Models.DAL
{
    public class DAL_DetailsBill
    {
        private AplicationDBContext context { get; set; }

        public DAL_DetailsBill()
        {
            this.context = new AplicationDBContext();
        }

        public List<CHI_TIET_HOA_DON> getListDetailsBill(int id)
        {
            return this.context.CHI_TIET_HOA_DON.Where(a => a.ID_HD == id).ToList();
        }


    }
}
