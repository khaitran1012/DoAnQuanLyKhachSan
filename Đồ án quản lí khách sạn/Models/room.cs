namespace Đồ_án_quản_lí_khách_sạn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        public Room()
        {
            Customers = new HashSet<customer>();
        }

        public int Roomid { get; set; }
        public object RoomId { get; internal set; }
        [Required]
        [StringLength(250)]
        public string RoomNo { get; set; }

        [Required]
        [StringLength(250)]
        public string RoomType { get; set; }

        [Required]
        [StringLength(250)]
        public string Bed { get; set; }

        public long Price { get; set; }

        [StringLength(50)]
        public string Booked { get; set; }

        public virtual ICollection<customer> Customers { get; set; }
    }
}
