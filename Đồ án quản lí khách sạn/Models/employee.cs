namespace Đồ_án_quản_lí_khách_sạn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("employee")]
    public partial class employee
    {
        [Key]
        public int eid { get; set; }

        [Required]
        [StringLength(250)]
        public string ename { get; set; }

        public long mobile { get; set; }

        [Required]
        [StringLength(50)]
        public string gender { get; set; }

        [Required]
        [StringLength(120)]
        public string emailid { get; set; }

        [Required]
        [StringLength(150)]
        public string username { get; set; }

        [Required]
        [StringLength(150)]
        public string pass { get; set; }
    }
}
