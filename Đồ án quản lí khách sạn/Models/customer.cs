namespace Đồ_án_quản_lí_khách_sạn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customer")]
    public partial class customer
    {
        [Key]
        public int cid { get; set; }

        [Required]
        [StringLength(250)]
        public string cname { get; set; }

        public long mobile { get; set; }

        [Required]
        [StringLength(250)]
        public string nationality { get; set; }

        [Required]
        [StringLength(50)]
        public string gender { get; set; }

        [Required]
        [StringLength(50)]
        public string dob { get; set; }

        [Required]
        [StringLength(250)]
        public string idproof { get; set; }

        [Required]
        [StringLength(350)]
        public string address { get; set; }

        [Required]
        [StringLength(250)]
        public string checkin { get; set; }

        [StringLength(250)]
        public string checkout { get; set; }
        [StringLength(250)]
        public string chekout { get; set; }

        public int roomid { get; set; }
       
        public virtual Room room { get; set; }
    }
}
