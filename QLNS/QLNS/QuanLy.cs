using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    public partial class QuanLy : Form
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QLNS"].ConnectionString;
        public QuanLy()
        {
            InitializeComponent();
        }

        private void QuanLy_Load(object sender, EventArgs e)
        {
            lbDN1.Text = dl.TenDN;
            lbID.Text = dl.TenDN;
            HienThi();
        }
        private void HienThi()
        {
            string sqlView = "SELECT STT, IDDangNhap, TenNV, QLXa, ThongTin, Sua, Xoa FROM QLNV";
            SqlDataAdapter da = new SqlDataAdapter(sqlView, connectionString);
            DataSet ds = new DataSet();
            da.Fill(ds, "QLNV");
            dataGrid.DataSource = ds.Tables["QLNV"];
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string TenNV = txtTim.Text;
            string CMND = txtTim.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string sqlTim = "SELECT STT, IDDangNhap, TenNV, QLXa, ThongTin, Sua, Xoa FROM QLNV WHERE TenNV like '%" + txtTim.Text.Trim() + "%' OR CMND like '%" + txtTim.Text.Trim() + "%'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlTim, conn);
                cmd.ExecuteNonQuery(); // Cho nay code thua.
                DataTable tb = new DataTable();
                SqlDataAdapter apd = new SqlDataAdapter(cmd);
                apd.Fill(tb);
                dataGrid.DataSource = tb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm." + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void btnDX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    xemNV(e.RowIndex); 
                    return;
                    string ID = txtID.Text;
                    string TenNV = txtNV.Text;
                    string DiaChi = txtDC.Text;
                    string SDT = txtSDT.Text;
                    string CMND = txtCMND.Text;
                    string Email = txtEmail.Text;

                    SqlConnection conn = new SqlConnection(connectionString);

                    //string STT = Convert.ToString(dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());

                    string sqlThongTin = "SELECT TenNV, DiaChi, SDT, CMND, Email FROM QLNV ";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sqlThongTin, conn);
                        cmd.ExecuteNonQuery();
                        DataTable tb = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tb);

                        txtNV.Text = tb.Rows[e.RowIndex][0].ToString();
                        txtDC.Text = tb.Rows[e.RowIndex][1].ToString();
                        txtSDT.Text = tb.Rows[e.RowIndex][2].ToString();
                        txtCMND.Text = tb.Rows[e.RowIndex][3].ToString();
                        txtEmail.Text = tb.Rows[e.RowIndex][4].ToString();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Thông báo lỗi");
                    }
                    finally
                    {
                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }

                }
                if (e.ColumnIndex == 5)
                {
                    FormSua frs = new FormSua();
                    frs.Message = txtNV.Text;
                    frs.Message1 = txtDC.Text;
                    frs.Message2 = txtSDT.Text;
                    frs.Message3 = txtCMND.Text;
                    frs.Message4 = txtEmail.Text;
                    frs.ShowDialog();
                    this.Show();
                }
                if (e.ColumnIndex == 6)
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    string STT = Convert.ToString(dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string sqlXoa = "Delete From QLNV Where STT = '" + STT + "'";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sqlXoa, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Delete successfully");
                        HienThi();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xemNV(int rowInndex)
        {
            DataTable table = (DataTable) dataGrid.DataSource;
            String idDangNhap = (String)table.Rows[rowInndex]["IDDangNhap"];

            //string STT = Convert.ToString(dataGrid.Rows[e.RowIndex].Cells[0].Value.ToString());

            string sqlThongTin = "SELECT TenNV, DiaChi, SDT, CMND, Email FROM QLNV where IDDangNhap =N'"+idDangNhap+"' ";
            try
            {
                DataTable tb = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlThongTin,connectionString);
                da.Fill(tb);

                txtNV.Text = tb.Rows[0][0].ToString();
                txtDC.Text = tb.Rows[0][1].ToString();
                txtSDT.Text = tb.Rows[0][2].ToString();
                txtCMND.Text = tb.Rows[0][3].ToString();
                txtEmail.Text = tb.Rows[0][4].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Thông báo lỗi");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string iddn = txtID.Text;
            string tenNV = txtNV1.Text;
            string mk = txtPass.Text;
            string remk = txtRePass.Text;
            string qlx = cmboxQLXa.Text;
            string dc = txtDC1.Text;
            string sdt = txtSDT1.Text;
            string cmnd = txtND.Text;
            string email = txtMail.Text;

            if (txtID.Text == "" || txtNV1.Text == "" || txtPass.Text == "" || txtRePass.Text == "" || txtDC1.Text == "" || txtSDT1.Text == "" || txtND.Text == "" || txtMail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin.", "Thông báo");
            }
            else
            {
                if (txtPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("Mật khẩu không khớp.Vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    
                          themNV(iddn, tenNV, mk, remk, qlx, dc, sdt, cmnd, email);
                          HienThi();
                    
                }
            }
        }
        private void themNV(string iddn, string tenNV, string mk, string remk, string qlx, string dc, string sdt, string cmnd, string email)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            
            string insert = string.Format("Insert into QLNV (IDDangNhap, TenNV, MatKhau, ReMatKhau, QLXa, DiaChi, SDT, CMND, Email) values (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}')", iddn, tenNV, mk, remk, qlx, dc, sdt, cmnd, email );

            try
            {
                connect.Open();
                SqlCommand com = new SqlCommand(insert, connect);
                com.ExecuteNonQuery();
                MessageBox.Show("Bạn đã thêm nhân viên thành công.");
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(SqlException))
                {
                    if (ex.Message.Contains("PRIMARY KEY"))
                    {
                       MessageBox.Show("ID Đăng nhập đã tồn tại.Vui lòng nhập ID Đăng nhập khác");
                    }
                    else
                        throw ex;
                }
            }
        }

        

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.ResetText();
            txtNV1.ResetText();
            txtPass.ResetText();
            txtRePass.ResetText();
            cmboxQLXa.Text = "Thị trấn Phù Mỹ";
            txtDC1.ResetText();
            txtSDT1.ResetText();
            txtND.ResetText();
            txtMail.ResetText();
        }

        private void btnDX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCMND_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        

        
    }
}
