using Đồ_án_quản_lí_khách_sạn.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using System.Xml.Linq;

namespace Đồ_án_quản_lí_khách_sạn.User_control
{
    public partial class Uc_Thanhtoan : UserControl
    {
        public Uc_Thanhtoan()
        {
            InitializeComponent();
            this.Load += Uc_Thanhtoan_Load; 
            txtten.TextChanged += txtten_TextChanged;
        }

        private void Uc_Thanhtoan_Load(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Hotels()) 
                {
                    
                    var data = context.customers
                        .Where(c => c.checkout == "NO")
                        .Select(c => new
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
                        })
                        .ToList();

                    guna2DataGridView1.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtten_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Hotels())
                {
                    var searchText = txtten.Text;

                    
                    var data = context.customers
                        .Where(c => c.cname.StartsWith(searchText) && c.checkout == "NO")
                        .Select(c => new
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
                        })
                        .ToList();

                    guna2DataGridView1.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int id;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                using (var context = new Hotels()) 
                {
                    var customer = context.customers.Include(c => c.room).FirstOrDefault(c => c.cid == id);
                    if (customer != null)
                    {
                        txtnhapten.Text = customer.cname;
                        txtsophong.Text = customer.room.RoomNo.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                   
                }
                LoadCustomerData();

            }

        }
        private void LoadCustomerData()
        {
            using (var context = new Hotels())
            {
                var data = context.customers
                    .Where(c => c.checkout == "NO") 
                    .Select(c => new
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
                    })
                    .ToList();

                guna2DataGridView1.DataSource = data;
            }
        }

        private void btnthanhtoan_Click(object sender, EventArgs e)
        {
            if (id != 0) 
            {
                try
                {
                    using (var context = new Hotels()) 
                    {
                       
                        var customer = context.customers.Include(c => c.room).FirstOrDefault(c => c.cid == id);
                        if (customer != null)
                        {
                            
                            customer.checkout = "YES";
                            if (customer.room != null)
                            {
                                customer.room.Booked = "NO";
                            }

                            
                            context.SaveChanges();

                           
                            MessageBox.Show("Thanh toán thành công cho khách hàng: " + customer.cname, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                           
                            LoadCustomerData();
                            ClearAll();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void ClearAll()
        {
            txtnhapten.Clear();
            txtsophong.Clear();
            id = 0;
        }

        private void Uc_Thanhtoan_Leave(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
    
