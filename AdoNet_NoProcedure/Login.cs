using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdoNet_NoProcedure
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Holding;integrated security=true");

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand comm = new SqlCommand("Select * from Users where UserName=@user and Password=@pass", conn);
            comm.Parameters.AddWithValue("@user", TxtUser.Text);
            comm.Parameters.AddWithValue("pass", TxtPass.Text);

            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı!");
            }

            conn.Close();
        }
    }
}
