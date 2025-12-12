using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.NewFolder1.siniflar
{
    public class Kargo
    {
        [Key]
        public int KargoId { get; set; }

        [Required(ErrorMessage = "Takip numarası gereklidir")]
        [StringLength(50)]
        public string TakipNo { get; set; }

        [Required(ErrorMessage = "Alıcı adı gereklidir")]
        [StringLength(100)]
        public string AliciAd { get; set; }

        [Required(ErrorMessage = "Alıcı adresi gereklidir")]
        [StringLength(300)]
        public string AliciAdres { get; set; }

        [StringLength(50)]
        public string AliciTelefon { get; set; }

        [StringLength(50)]
        public string AliciSehir { get; set; }

        public DateTime GonderimTarihi { get; set; }

        public DateTime? TeslimTarihi { get; set; }

        // Durum: Hazırlanıyor, Kargoya Verildi, Yolda, Teslim Edildi
        [StringLength(50)]
        public string Durum { get; set; }

        // İlişkiler
        public int? SatisId { get; set; }
        public virtual SatisHareket SatisHareket { get; set; }
    }
}
