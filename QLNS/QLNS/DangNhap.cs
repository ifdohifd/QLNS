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
    public partial class DangNhap : Form
    {

        const String keyRememberId = "Remember.ID";
        const String keyRememberPassword = "Remember.Password";

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QLNS"].ConnectionString;
        public DangNhap()
        {
            InitializeComponent();
            string idDaLuu = IdDaLuu();
            if (idDaLuu != null)
            {
                txtID.Text = idDaLuu;
                txtPass.Text = matKhauDaLuu();

            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            bool DN = false;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataReader rdr = null;
            string IDDangNhap = txtID.Text.Trim();
            string MatKhau = txtPass.Text.Trim();
            try
            {
                conn.Open();
                string sqlDN = "SELECT * FROM QLNV"; 
                SqlCommand cmd = new SqlCommand(sqlDN, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if ((IDDangNhap == rdr["IDDangNhap"].ToString().Trim()) && (MatKhau == rdr["MatKhau"].ToString().Trim()))
                    {
                        DN = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL" + ex.ToString());
                return;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            if (DN == false)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            else
            {
                if (checkBox1.Checked)
                {
                    NhoMatKhau(IDDangNhap, MatKhau);
                }
                dl.TenDN = txtID.Text;
                QuanLy QL = new QuanLy();
                this.Hide();
                QL.ShowDialog();
                this.Show();
            }
        }
        private void NhoMatKhau(string id, string pas)
        {
            saveTmp(keyRememberId, id);
            saveTmp(keyRememberPassword, pas);
        }

        private string matKhauDaLuu()
        {
            return readTmp(keyRememberPassword);
        }
        private string IdDaLuu()
        {
            return readTmp(keyRememberId);
        }

        private void saveTmp(String key, String value)
        {
            SqlCommand command = new SqlCommand("delete from Tmp where [key] = N'"+key+"'; insert into Tmp values(N'"+key+"',N'"+value+"')");
            SqlConnection connection =new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private string readTmp(String key){
            SqlCommand command = new SqlCommand("select value from Tmp where [key] = N'" + key + "'");
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                return (string)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

    }
}
