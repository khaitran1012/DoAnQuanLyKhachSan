using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_án_quản_lí_khách_sạn
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnthoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            uc_Themphong1.Visible = true;
            uc_Dangkhachhang1.Visible = false;
            uc_Dangkhachhang1.Visible = false;
            uc_Thanhtoan1.Visible = false;
            btndkkhachhang.PerformClick();
        }
        private void btnaddroom_Click(object sender, EventArgs e)
        {
            {
                panelmoving.Left = btnaddroom.Left + 60;
                uc_Themphong1.Visible = true;
                uc_Themphong1.BringToFront();
            }
        }
        

        private void btndkkhachhang_Click_1(object sender, EventArgs e)
        {
            {
                 panelmoving.Left = btndkkhachhang.Left + 60;
                 uc_Dangkhachhang1.Visible = true;
                 uc_Dangkhachhang1.BringToFront();
            }
        }

        private void btnnhanvien_Click(object sender, EventArgs e)
        {
            panelmoving.Left = btnnhanvien.Left + 60;
            uc_Nhan_vien1.Visible = true;
            uc_Nhan_vien1.BringToFront();
        }

        private void btnthanhtoan_Click(object sender, EventArgs e)
        {
            panelmoving.Left = btnthanhtoan.Left + 60;
            uc_Thanhtoan1.Visible = true;
            uc_Thanhtoan1.BringToFront();
        }

        private void btnttkhachhang_Click(object sender, EventArgs e)
        {
            panelmoving.Left = btnttkhachhang.Left + 60;
            uc_Thongtinkhachhang1.Visible = true;
            uc_Thongtinkhachhang1.BringToFront();
        }
    }
}
