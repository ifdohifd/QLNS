using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS
{
    public partial class FormSua : Form
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["QLNS"].ConnectionString;
        public FormSua()
        {
            InitializeComponent();
        }
        private string message0;
        private string message1;
        private string message2;
        private string message3;
        private string message4;
        private void FormSua_Load(object sender, EventArgs e)
        {
            txtNV.Text = message0;
            txtDC.Text = message1;
            txtSDT.Text = message2;
            txtCMND.Text = message3;
            txtMail.Text = message4;
        }
        public string Message
        {
            get { return message0; }
            set { message0 = value; }
        }
        public string Message1
        {
            get { return message1; }
            set { message1 = value; }
        }
        public string Message2
        {
            get { return message2; }
            set { message2 = value; }
        }
        public string Message3
        {
            get { return message3; }
            set { message3 = value; }
        }
        public string Message4
        {
            get { return message4; }
            set { message4 = value; }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
