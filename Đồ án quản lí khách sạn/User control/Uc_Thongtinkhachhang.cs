using Đồ_án_quản_lí_khách_sạn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Đồ_án_quản_lí_khách_sạn.User_control
{
    public partial class Uc_Thongtinkhachhang : UserControl
    {
        public Uc_Thongtinkhachhang()
        {
            InitializeComponent();
        }

        private void txttimkiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new Hotels())
            {
                IQueryable<customer> query = context.customers.Include(c => c.room);

              
                switch (txttimkiem.SelectedItem.ToString())
                {
                    case "Tất cả thông tin chi tiết khách hàng":
                        
                        break;

                    case "Khách hàng đã thanh toán":
                        
                        query = query.Where(c => c.checkout == "YES");
                        break;

                    default:
                        MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                
                var data = query.Select(c => new
                {
                    c.cid,
                    c.cname,
                    c.mobile,
                    c.nationality,
                    c.gender,
                    c.dob,
                    c.idproof,
                    c.address,
                    c.checkin,
                    RoomNo = c.room.RoomNo,
                    RoomType = c.room.RoomType,
                    Bed = c.room.Bed,
                    Price = c.room.Price
                }).ToList();

                guna2DataGridView1.DataSource = data;
            }
        }

    }

}
