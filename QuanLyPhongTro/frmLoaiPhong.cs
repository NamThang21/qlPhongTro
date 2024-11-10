using BT_nhom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro
{
    public partial class frmLoaiPhong : Form
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
        }
        private Database db;

        private int maLoaiPhong = 0;
        private void btnThem_Click(object sender, EventArgs e)
        {
            var tenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            var donGia = int.Parse(txtDonGia.Text);

            //ràng buộc dữ liệu
            if (string.IsNullOrEmpty(tenLoaiPhong))
            {
                MessageBox.Show("Vui lòng nhập tên loại phòng", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;//dừng chương trình ngang đây
            }

            if (donGia < 50000)
            {
                MessageBox.Show("Đơn giá tối thiểu phải là 50.000", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;//dừng chương trình ngang đây
            }
            var prList = new List<CustomParamefer>();
            prList.Add(new CustomParamefer
            {
                key = "@tenLoaiPhong",
                value = tenLoaiPhong
            });
            prList.Add(new CustomParamefer
            {
                key = "@donGia",
                value = donGia.ToString()
            });
            var rs = db.ExeCute("themLoaiPhong", prList);
            if (rs == 1)
            {
                MessageBox.Show("Thêm mới loại phòng thành công!", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDsLoaiPhong();
                txtDonGia.Text = "0";
                txtTenLoaiPhong.Text = null;
            }
        }
        private void LoadDsLoaiPhong()
        {
          
            dgvDsLoaiPhong.DataSource = db.SelectData("loadDsLoaiPhong");
        }

        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            db = new Database();
            LoadDsLoaiPhong();
            dgvDsLoaiPhong.Columns[0].Width = 100;//set bề rộng cho cột id loại phòng
            dgvDsLoaiPhong.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDsLoaiPhong.Columns[0].HeaderText = "Mã loại";

            dgvDsLoaiPhong.Columns[2].Width = 200;
            dgvDsLoaiPhong.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDsLoaiPhong.Columns[2].DefaultCellStyle.Format = "N0";
            dgvDsLoaiPhong.Columns[2].HeaderText = "Đơn giá";

            dgvDsLoaiPhong.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDsLoaiPhong.Columns[1].HeaderText = "Tên loại phòng";
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDsLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                maLoaiPhong = int.Parse(dgvDsLoaiPhong.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtTenLoaiPhong.Text = dgvDsLoaiPhong.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDonGia.Text = dgvDsLoaiPhong.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var tenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            var donGia = int.Parse(txtDonGia.Text);


            //ràng buộc dữ liệu

            if (maLoaiPhong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần cập nhật", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;//dừng chương trình ngang đây
            }

            if (string.IsNullOrEmpty(tenLoaiPhong))
            {
                MessageBox.Show("Vui lòng nhập tên loại phòng", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;//dừng chương trình ngang đây
            }

            if (donGia < 50000)
            {
                MessageBox.Show("Đơn giá tối thiểu phải là 50.000", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;//dừng chương trình ngang đây
            }


            var prList = new List<CustomParamefer>();

            prList.Add(new CustomParamefer()
            {
                key = "@idLoaiPhong",
                value = maLoaiPhong.ToString()
            });

            prList.Add(new CustomParamefer
            {
                key = "@tenLoaiPhong",
                value = tenLoaiPhong
            });
            prList.Add(new CustomParamefer
            {
                key = "@donGia",
                value = donGia.ToString()
            });



            var rs = db.ExeCute("[capNhatLoaiPhong]", prList);
            if (rs == 1)
            {
                MessageBox.Show("Cập nhật loại phòng thành công!", "Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDsLoaiPhong();
                txtDonGia.Text = "0";
                txtTenLoaiPhong.Text = null;
                maLoaiPhong = 0;
            }
        }
    }
}
