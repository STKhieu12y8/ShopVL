using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Models.EntityFramework
{
    public partial class AplicationDBContext : DbContext
    {
        public AplicationDBContext()
            : base("name=AplicationDBContext")
        {
        }

        public virtual DbSet<ACOUNT> ACOUNTs { get; set; }
        public virtual DbSet<BAI_VIET> BAI_VIET { get; set; }
        
        public virtual DbSet<LOAI_NGANH_NGHE> LOAI_NGANH_NGHE { get; set; }
        public virtual DbSet<VIEC_LAM> VIEC_LAM { get; set; }
        public virtual DbSet<LEVEL> LEVELs { get; set; }
        public virtual DbSet<SLIDE> SLIDEs { get; set; }        
        public virtual DbSet<NHAN_VIEN> NHAN_VIEN { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACOUNT>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<ACOUNT>()
                .Property(e => e.MAT_KHAU)
                .IsUnicode(false);

            modelBuilder.Entity<ACOUNT>()
                .Property(e => e.LINK_ANH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ACOUNT>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BAI_VIET>()
                .Property(e => e.SLUG)
                .IsUnicode(false);

            modelBuilder.Entity<BAI_VIET>()
                .Property(e => e.IMAGES)
                .IsUnicode(false);

            
            modelBuilder.Entity<LOAI_NGANH_NGHE>()
                .Property(e => e.SLUG)
                .IsUnicode(false);

           
            modelBuilder.Entity<VIEC_LAM>()
                .Property(e => e.MA_VL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VIEC_LAM>()
                .Property(e => e.LINK_ANH_CHINH)
                .IsUnicode(false);

            modelBuilder.Entity<VIEC_LAM>()
                .Property(e => e.LIST_ANH_KEM)
                .IsUnicode(false);

            modelBuilder.Entity<VIEC_LAM>()
                .Property(e => e.MUC_LUONG)
                .HasPrecision(15, 4);            

            modelBuilder.Entity<VIEC_LAM_CHI_TIET>()
                .Property(e => e.MA_SP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LEVEL>()
                .Property(e => e.TEN_LEVEL)
                .IsUnicode(false);

            modelBuilder.Entity<LEVEL>()
                .Property(e => e.SLUG)
                .IsUnicode(false);

            modelBuilder.Entity<SLIDE>()
                .Property(e => e.LINK)
                .IsUnicode(false);

            modelBuilder.Entity<SLIDE>()
                .Property(e => e.IMAGES)
                .IsUnicode(false);
            modelBuilder.Entity<NHAN_VIEN>()
                .Property(e => e.TEN_NHAN_VIEN)
                .IsUnicode(false);
        }
    }
}
