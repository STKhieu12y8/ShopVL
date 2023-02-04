using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shop.Data
{
    public class QuanLyJobDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public QuanLyJobDbContext() : base("name=ShopQuanAoContext")
        {
        }

        public System.Data.Entity.DbSet<Models.EntityFramework.BAI_VIET> BAI_VIET { get; set; }

        public System.Data.Entity.DbSet<Models.EntityFramework.VIEC_LAM> SAN_PHAM { get; set; }

        public System.Data.Entity.DbSet<Models.EntityFramework.SLIDE> SLIDEs { get; set; }

        public System.Data.Entity.DbSet<Models.EntityFramework.ACOUNT> ACOUNTs { get; set; }

        
    }
}
