using Đồ_án_quản_lí_khách_sạn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Đồ_án_quản_lí_khách_sạn.User_control
{
    public partial class Uc_Nhan_vien : UserControl
    {
        public Uc_Nhan_vien()
        {
            InitializeComponent();
        }

        private void Uc_Nhan_vien_Load(object sender, EventArgs e)
        {
            getMaxID();
        }

        public void getMaxID()
        {
            using (var context = new Hotels())
            {
                var maxID = context.employees.Select(e => (int?)e.eid).Max();
                labelToSet.Text = ((maxID ?? 0) + 1).ToString();
            }
        }


        private void btndangki_Click(object sender, EventArgs e)
        {

            if (txttennhanvien.Text != "" && txtsdt.Text != "" && cbbgioitinh.Text != "" && txtemail.Text != "" && txttennguoidung.Text != "" && txtmatkhau.Text != "")
            {
                string name = txttennhanvien.Text;
                Int64 mobile = Int64.Parse(txtsdt.Text);
                string email = txtemail.Text;
                string gender = cbbgioitinh.Text;
                string username = txttennguoidung.Text;
                string pass = txtmatkhau.Text;

                using (var context = new Hotels())
                {
                    var employee = new employee
                    {
                        ename = name,
                        mobile = mobile,
                        gender = gender,
                        emailid = email,
                        username = username,
                        pass = pass
                    };

                    context.employees.Add(employee);
                    context.SaveChanges();
                }

                clearAll();
                getMaxID();
            }
        }
        public void clearAll()
        {
            txttennhanvien.Clear();
            txtsdt.Clear();
            cbbgioitinh.SelectedIndex = -1; 
            txttennguoidung.Clear();
            txtemail.Clear();
            txtmatkhau.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                setEmployee(guna2DataGridView2);
            }

        }
        public void setEmployee(DataGridView dgv)
        {
            using (var context = new Hotels())
            {
                var employees = context.employees.ToList();
                dgv.DataSource = employees;
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {

            if (txtidnhanvien.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = int.Parse(txtidnhanvien.Text);

                    using (var context = new Hotels())
                    {
                        var employee = context.employees.SingleOrDefault(emp => emp.eid == id);

                        if (employee != null)
                        {
                            context.employees.Remove(employee);
                            context.SaveChanges();
                            MessageBox.Show("Thông Tin Nhân Viên Đã Được Xóa!");
                        }
                        else
                        {
                            MessageBox.Show("Nhân Viên Không Tồn Tại!");
                        }
                    }

                    tabControl1_SelectedIndexChanged(this, null);
                }
            }
        }

        private void Uc_Nhan_vien_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}

