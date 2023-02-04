using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ADO
{
    public class ModelsSanPham
    {

        [Display(Name = "id")]
        [StringLength(20)]
        public string MA_VL { get; set; }

        public int ID_LVL { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "name product")]
        public string TEN_VL { get; set; }

        [Required]
        [Display(Name = "slug")]
        [StringLength(70)]
        public string SLUG { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô Tả")]
        public string MO_TA { get; set; }

        [Required]
        [Display(Name = "Mô tả chi chi tiết")]
        public string MO_TA_CHI_TIET { get; set; }

       
        public string LINK_ANH_CHINH { get; set; }

        [StringLength(250)]
        public string LIST_ANH_KEM { get; set; }


        public int? SO_LUONG_TONG
        { get; set; }
                
        [Required]
        [Display(Name = "Mức Lương")]
        public decimal MUC_LUONG { get; set; }
           

        public bool? TRANG_THAI
        { get; set; }

        public bool? NOI_BAT
        { get; set; }

        public string TenLoaiSP
        {
            get;set;
        }
        public DateTime? NGAY_TAO
        { get; set; }

        public List<SanPhamDetails> listSanPhamDetails { get; set; }
        public List<ColorProduct> listSanPhamColor { get; set; }
    }
    public class SanPhamDetails
    {
        public long ID { get; set; }

        public int ID_SIZE { get; set; }

        public string TenSize { get; set; }
       
        public string Image { get; set; }

        public string MoTaSize { get; set; }

        public string TenColor { get; set; }

        public string MA_SP { get; set; }

        public string SLUG { get; set; }

        public int? SO_LUONG { get; set; }

        public bool? TRANG_THAI { get; set; }

        public DateTime? NGAY_TAO { get; set; }
    }




    /// <summary>
    /// Lấy thông tin sản phẩm chi tiết theo màu color
    /// </summary>
    public class ColorProduct
    {
        public int ID { get; set; }

        public string TEN_MAU { get; set; }

        public string MA_MAU { get; set; }

        public string MA_SP { get; set; }

        public string Slug { get; set; }

        public string IMAGES { get; set; }

        public bool? TRANG_THAI { get; set; }

    }

}
