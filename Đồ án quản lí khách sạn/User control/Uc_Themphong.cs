using Đồ_án_quản_lí_khách_sạn.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Đồ_án_quản_lí_khách_sạn.User_control
{
    public partial class Uc_Themphong : UserControl
    {
        public Uc_Themphong()
        {
            InitializeComponent();
        }

        private void Uc_Themphong_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            try
            {
                using (var context = new Hotels())
                {
                    var rooms = context.rooms
                                       .Select(r => new { r.Roomid, r.RoomNo, r.RoomType, r.Bed, r.Price, r.Booked })
                                       .AsNoTracking()
                                       .ToList();
                    dataGridView1.DataSource = rooms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btnthemphong_Click(object sender, EventArgs e)
        {
            if (txtsophong.Text != "" && txtloaiphong.Text != "" && txtgiuong.Text != "" && txtgiatien.Text != "")
            {
                string roomno = txtsophong.Text;
                string type = txtloaiphong.Text;
                string bed = txtgiuong.Text;
                long price = long.Parse(txtgiatien.Text);

                using (var context = new Hotels())
                {
                    var room = new Room
                    {
                        RoomNo = roomno,
                        RoomType = type,
                        Bed = bed,
                        Price = price,
                        Booked = "No" 
                    };

                    context.rooms.Add(room);
                    context.SaveChanges(); 
                }

                MessageBox.Show("Đã Thêm Phòng", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataGridView(); 
                ClearAll();
            }
            else
            {
                MessageBox.Show("Xin Vui Lòng Điền Đầy Đủ Thông Tin", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public void ClearAll()
        {
            txtsophong.Clear();
            txtloaiphong.SelectedIndex = -1;
            txtgiuong.SelectedIndex = -1;
            txtgiatien.Clear();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsophong.Text))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng " + txtsophong.Text + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var context = new Hotels())
                        {
                          
                            var room = context.rooms.FirstOrDefault(r => r.RoomNo == txtsophong.Text);

                            if (room != null)
                            {
                              
                                context.rooms.Remove(room);

                              
                                context.SaveChanges();

                                MessageBox.Show("Đã xóa phòng " + txtsophong.Text + " thành công.");

                                LoadDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy phòng " + txtsophong.Text + ".");
                            }
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        
                        string errorMessage = "Có lỗi xảy ra khi cập nhật cơ sở dữ liệu:\n";
                        Exception innerException = ex.InnerException;

                        while (innerException != null)
                        {
                            errorMessage += innerException.Message + "\n";
                            innerException = innerException.InnerException;
                        }

                        MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        
                        string errorMessage = "Có lỗi xảy ra:\n" + ex.Message;

                        if (ex.InnerException != null)
                        {
                            errorMessage += "\nChi tiết lỗi:\n";
                            Exception innerException = ex.InnerException;
                            while (innerException != null)
                            {
                                errorMessage += innerException.Message + "\n";
                                innerException = innerException.InnerException;
                            }
                        }

                        MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtsophong.Text = row.Cells["RoomNo"].Value.ToString();
                txtloaiphong.SelectedItem = row.Cells["RoomType"].Value.ToString();
                string bedValue = row.Cells["Bed"].Value.ToString();
                if (txtgiuong.Items.Contains(bedValue))
                {
                    txtgiuong.SelectedItem = bedValue;
                }
                else
                {
                    txtgiuong.SelectedIndex = -1; 
                }

                txtgiatien.Text = row.Cells["Price"].Value.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtsophong.Text = row.Cells["RoomNo"].Value.ToString();
                txtloaiphong.SelectedItem = row.Cells["RoomType"].Value.ToString();

                string bedValue = row.Cells["Bed"].Value.ToString();
                if (txtgiuong.Items.Contains(bedValue))
                {
                    txtgiuong.SelectedItem = bedValue;
                }
                else
                {
                    txtgiuong.SelectedIndex = -1; 
                }

                txtgiatien.Text = row.Cells["Price"].Value.ToString();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (txtsophong.Text != "" && txtloaiphong.Text != "" && txtgiuong.Text != "" && txtgiatien.Text != "")
            {
                string roomno = txtsophong.Text;
                string type = txtloaiphong.Text;
                string bed = txtgiuong.Text;
                long price = long.Parse(txtgiatien.Text);

                using (var context = new Hotels())
                {
                    var room = context.rooms.FirstOrDefault(r => r.RoomNo == roomno);

                    if (room != null)
                    {
                      
                        room.RoomType = type;
                        room.Bed = bed;
                        room.Price = price;
                        context.SaveChanges();

                        MessageBox.Show("Đã sửa thông tin phòng " + roomno + " thành công.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();  
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng " + roomno + ".", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Xin vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
