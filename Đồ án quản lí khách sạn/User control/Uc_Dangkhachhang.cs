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
using Đồ_án_quản_lí_khách_sạn.Models;
using System.Xml.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Đồ_án_quản_lí_khách_sạn.User_control
{
    public partial class Uc_Dangkhachhang : UserControl
    {
        public Uc_Dangkhachhang()
        {
            InitializeComponent();
        }

        public void setComboBox(IEnumerable<string> items, ComboBox combo)
        {
            combo.Items.Clear();
            foreach (var item in items)
            {
                combo.Items.Add(item);
            }
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void Uc_Dangkhachhang_Load(object sender, EventArgs e)
        {
            // Mã để load dữ liệu ban đầu nếu cần
        }

        private void txtgiuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtloaiphong.SelectedIndex = -1;
            txtsophong.Items.Clear();
            txtgiatien.Clear();
        }

        private void txtloaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new Hotels())
            {
                var roomNos = context.rooms
                    .Where(r => r.Bed == txtgiuong.Text && r.RoomType == txtloaiphong.Text && r.Booked == "NO")
                    .Select(r => r.RoomNo)
                    .ToList();

                if (roomNos.Any())
                {
                    setComboBox(roomNos, txtsophong);
                    MessageBox.Show("Danh sách số phòng đã được tải thành công.");
                }

            }
        }

        int rid;
        private void txtsophong_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new Hotels())
            {
                var room = context.rooms
                    .Where(r => r.RoomNo == txtsophong.Text)
                    .Select(r => new { r.Price, r.Roomid })
                    .FirstOrDefault();

                if (room != null)
                {
                    txtgiatien.Text = room.Price.ToString();
                    rid = room.Roomid;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnthemkhachhang_Click(object sender, EventArgs e)
{
    if (!string.IsNullOrEmpty(txtten.Text) &&
        !string.IsNullOrEmpty(txtdienthoai.Text) &&
        !string.IsNullOrEmpty(txtquoctich.Text) &&
        !string.IsNullOrEmpty(txtgioitinh.Text) &&
        !string.IsNullOrEmpty(txtngaysinh.Text) &&
        !string.IsNullOrEmpty(txtID.Text) &&
        !string.IsNullOrEmpty(txtdiachi.Text) &&
        !string.IsNullOrEmpty(txtngaydk.Text) &&
        !string.IsNullOrEmpty(txtgiatien.Text))
    {
        try
        {
            using (var context = new Hotels())
            {
                
                var customer = new customer
                {
                    cname = txtten.Text,
                    mobile = Int64.Parse(txtdienthoai.Text),
                    nationality = txtquoctich.Text,
                    gender = txtgioitinh.Text,
                    dob = txtngaysinh.Text,
                    idproof = txtID.Text,
                    address = txtdiachi.Text,
                    checkin = txtngaydk.Text,
                    roomid = rid,
                    checkout = "NO"
                };
  
                context.customers.Add(customer);

                var room = context.rooms.FirstOrDefault(r => r.RoomNo == txtsophong.Text);
                if (room != null)
                {
                    room.Booked = "YES";
                    context.SaveChanges(); 
                
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MessageBox.Show("Số phòng " + txtsophong.Text + " đã đăng ký khách hàng thành công.");

              
                txtloaiphong_SelectedIndexChanged(sender, e);
                clearAll();
            }
        }
        catch (DbEntityValidationException ex)
        {
            foreach (var validationErrors in ex.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                }
            }
        }
        catch (DbUpdateException ex)
        {
            var exceptionMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
            MessageBox.Show("Lỗi: " + exceptionMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    else
    {
        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

        public void clearAll()
        {
            txtten.Clear();
            txtdienthoai.Clear();
            txtquoctich.Clear();
            txtgioitinh.SelectedIndex = -1;
            txtngaysinh.ResetText();
            txtID.Clear();
            txtdiachi.Clear();
            txtngaydk.ResetText();
            txtgiuong.SelectedIndex = -1;
            txtloaiphong.SelectedIndex = -1;
            txtsophong.Items.Clear();
            txtgiatien.Clear();
        }

        private void Uc_Dangkhachhang_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
