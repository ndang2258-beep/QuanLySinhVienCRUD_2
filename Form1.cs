
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLySinhVienCRUD.BLL;

namespace QuanLySinhVienCRUD
{
    public partial class Form1 : Form
    {
        private readonly TextBox txtHoTen = new TextBox();
        private readonly TextBox txtDiem = new TextBox();
        private readonly DataGridView dgvSinhVien = new DataGridView();
        private readonly Label lblTrangThai = new Label();

        private readonly Button btnThem = new Button();
        private readonly Button btnSua = new Button();
        private readonly Button btnXoa = new Button();
        private readonly Button btnTaiLai = new Button();
        private readonly Button btnLocDat = new Button();

        private readonly StudentBLL bll = new StudentBLL();

        private int selectedId = 0;

        public Form1()
        {
            TaoGiaoDien();
            ConnectEvents();
        }

        // KET NOI SU KIEN
        private void ConnectEvents()
        {
            Load += Form1_Load;

            dgvSinhVien.SelectionChanged +=
                dgvSinhVien_SelectionChanged;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTaiLai.Click += btnTaiLai_Click;
            btnLocDat.Click += btnLocDat_Click;
        }

        // TAO GIAO DIEN
        private void TaoGiaoDien()
        {
            Text = "Quản lý sinh viên";
            Size = new Size(850, 520);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblHoTen = new Label
            {
                Text = "Họ tên:",
                Location = new Point(20, 20),
                Size = new Size(80, 25)
            };

            txtHoTen.Location = new Point(100, 20);
            txtHoTen.Size = new Size(220, 25);

            Label lblDiem = new Label
            {
                Text = "Điểm:",
                Location = new Point(350, 20),
                Size = new Size(60, 25)
            };

            txtDiem.Location = new Point(410, 20);
            txtDiem.Size = new Size(100, 25);

            btnThem.Text = "Thêm";
            btnThem.SetBounds(540, 18, 90, 30);

            btnSua.Text = "Sửa";
            btnSua.SetBounds(640, 18, 90, 30);

            btnXoa.Text = "Xóa";
            btnXoa.SetBounds(540, 58, 90, 30);

            btnTaiLai.Text = "Tải lại";
            btnTaiLai.SetBounds(640, 58, 90, 30);

            btnLocDat.Text = "Lọc điểm đạt";
            btnLocDat.SetBounds(540, 98, 190, 30);

            dgvSinhVien.Location = new Point(20, 65);
            dgvSinhVien.Size = new Size(490, 350);
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.RowHeadersVisible = false;

            dgvSinhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã SV",
                    DataPropertyName = "Id"
                });

            dgvSinhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colHoTen",
                    HeaderText = "Họ tên",
                    DataPropertyName = "FullName"
                });

            dgvSinhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colDiem",
                    HeaderText = "Điểm",
                    DataPropertyName = "Grade"
                });

            lblTrangThai.Location = new Point(20, 435);
            lblTrangThai.Size = new Size(700, 25);
            lblTrangThai.Text = "Sẵn sàng";

            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblDiem);
            Controls.Add(txtDiem);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnTaiLai);
            Controls.Add(btnLocDat);
            Controls.Add(dgvSinhVien);
            Controls.Add(lblTrangThai);
        }

        // KHI MO FORM
        private void Form1_Load(object? sender, EventArgs e)
        {
            LoadStudents();
        }

        // TAI DANH SACH SINH VIEN
        private void LoadStudents()
        {
            try
            {
                dgvSinhVien.DataSource = null;
                dgvSinhVien.DataSource = bll.GetAll();

                lblTrangThai.Text =
                    "Tổng số sinh viên: " +
                    dgvSinhVien.Rows.Count;

                selectedId = 0;
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu!\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // CHON SINH VIEN
        private void dgvSinhVien_SelectionChanged(
            object? sender, EventArgs e)
        {
            DataGridViewRow? row = dgvSinhVien.CurrentRow;

            if (row == null || row.IsNewRow)
                return;

            object? idValue = row.Cells["colId"].Value;

            if (idValue == null ||
                !int.TryParse(Convert.ToString(idValue), out int id))
            {
                return;
            }

            selectedId = id;

            txtHoTen.Text =
                Convert.ToString(row.Cells["colHoTen"].Value)
                ?? string.Empty;

            txtDiem.Text =
                Convert.ToString(row.Cells["colDiem"].Value)
                ?? string.Empty;
        }

        // LAY THONG TIN SINH VIEN
        private Student TaoStudent()
        {
            if (!double.TryParse(txtDiem.Text.Trim(), out double diem))
            {
                throw new Exception("Điểm phải là số hợp lệ!");
            }

            return new Student
            {
                Id = selectedId,
                FullName = txtHoTen.Text.Trim(),
                Grade = diem
            };
        }

        // THEM SINH VIEN
        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                Student student = TaoStudent();
                student.Id = 0;

                bll.Add(student);

                MessageBox.Show("Thêm sinh viên thành công!");

                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // SUA SINH VIEN
        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId <= 0)
                {
                    MessageBox.Show(
                        "Vui lòng chọn sinh viên cần sửa!");
                    return;
                }

                Student student = TaoStudent();
                bll.Update(student);

                MessageBox.Show("Cập nhật sinh viên thành công!");

                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // XOA SINH VIEN
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (selectedId <= 0)
                {
                    MessageBox.Show(
                        "Vui lòng chọn sinh viên cần xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sinh viên này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bll.Delete(selectedId);

                    MessageBox.Show("Xóa sinh viên thành công!");

                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa sinh viên!\n" + ex.Message);
            }
        }

        // TAI LAI DANH SACH
        private void btnTaiLai_Click(object? sender, EventArgs e)
        {
            LoadStudents();
        }

        // LOC SINH VIEN DAT
        private void btnLocDat_Click(object? sender, EventArgs e)
        {
            try
            {
                dgvSinhVien.DataSource = null;
                dgvSinhVien.DataSource = bll.GetStudentsPassed();

                lblTrangThai.Text =
                    "Số sinh viên đạt (điểm >= 5): " +
                    dgvSinhVien.Rows.Count;

                selectedId = 0;
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lọc dữ liệu!\n" + ex.Message);
            }
        }

        // XOA NOI DUNG NHAP
        private void ClearInputs()
        {
            selectedId = 0;
            txtHoTen.Clear();
            txtDiem.Clear();
            dgvSinhVien.ClearSelection();
        }
    }
}
