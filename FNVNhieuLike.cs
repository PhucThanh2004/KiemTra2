using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FNVNhieuLike : Form
    {

        string sqlStr;
      //  string Email = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        public FNVNhieuLike()
        {
            InitializeComponent();
        }

        public Form activeForm = null;
        public void MoFormCon(Form fCon, Panel pl)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = fCon;
            fCon.TopLevel = false;
            fCon.FormBorderStyle = FormBorderStyle.None;
            fCon.Dock = DockStyle.Fill;
            pl.Controls.Clear();
            pl.Controls.Add(fCon);
            pl.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }
        private void themUCHoSoYeuThich(string tenUV, string gioiTinh, string emailUV, string soluong)
        {
            UCCVYeuThich uc = new UCCVYeuThich();
            uc.btnBoYeuThich.Visible = false;
            uc.Dock = DockStyle.Top;
            uc.lblTenUV.Text = tenUV;
            uc.lblGioiTinh.Text = gioiTinh;
            uc.lblEmailUV.Text = emailUV;
            plFormCha.Controls.Add(uc);
            uc.label4.Text = soluong;
            uc.btnXemChiTiet.Visible = false;

            plFormCha.Controls.Add(uc);
        }
        private void QuayLai(object sender, EventArgs e)
        {
            dao.MoFormCon(new FNVNhieuLike(), plFormCha);
        }

        private void FNVNhieuLike_Load(object sender, EventArgs e)
        {
            sqlStr = @"SELECT a.*, uv.*
            FROM (SELECT COUNT(EmailUngVien) AS Soluong, EmailUngVien
            FROM HoSoYeuThich
            GROUP BY EmailUngVien) AS a
            INNER JOIN UngVien uv
            ON a.EmailUngVien = uv.EmailDangNhap 
            Order by Soluong";


            try
            {
                // Kết nối cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();

                    // Truy vấn dữ liệu từ bảng HoSoYeuThich và kết hợp với bảng UngVien để lấy thông tin tên ứng viên

                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Lấy thông tin từ bảng HoSoYeuThich
                                string tenUngVien = reader["hoTen"].ToString();
                                string gioiTinh = reader["gioiTinh"].ToString();
                                string emailUV = reader["EmailUngVien"].ToString();
                                string soluong = reader["SoLuong"].ToString() ;
                                
                               

                                // Tạo và thêm UserControl vào Form
                                themUCHoSoYeuThich(tenUngVien, gioiTinh, emailUV, soluong);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi truy xuất dữ liệu
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void plFormCha_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
