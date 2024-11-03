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

namespace Đồ_án_quản_lí_khách_sạn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            if (txtten.Text != "" && txtmatkhau.Text != "")
            {
                string username = txtten.Text;
                string password = txtmatkhau.Text;

                using (var context = new Hotels())
                {
                    var employee = context.employees.SingleOrDefault(emp => emp.username == username && emp.pass == password);

                    if (employee != null)
                    {
                        labelError.Visible = false;
                        Dashboard dash = new Dashboard();
                        this.Hide();
                        dash.Show();
                    }
                    else
                    {
                        labelError.Visible = true;
                        txtmatkhau.Clear();
                    }
                }
            }

        }


    }
}