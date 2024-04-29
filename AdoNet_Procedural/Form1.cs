using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdoNet_Procedural
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Listing()
        {
            SqlCommand cmd = new SqlCommand("CustomerList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView1.DataSource = dt;
        }

        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=SalesCompany;integrated security=true");

        private void BtnList_Click(object sender, EventArgs e)
        {
            Listing();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "CustomerSave";
            //cmd.Connection = conn;

            SqlCommand cmd = new SqlCommand("CustomerSave", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FullName", TxtBxFullName.Text);
            cmd.Parameters.AddWithValue("Tc", TxtBxTc.Text);
            cmd.Parameters.AddWithValue("Address", TxtBxAddress.Text);
            cmd.Parameters.AddWithValue("City", TxtBxCity.Text);
            cmd.Parameters.AddWithValue("Phone", TxtBxPhone.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            Listing();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("CustomerUpdate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", TxtBxFullName.Tag);
            cmd.Parameters.AddWithValue("FullName", TxtBxFullName.Text);
            cmd.Parameters.AddWithValue("Tc", TxtBxTc.Text);
            cmd.Parameters.AddWithValue("Address", TxtBxAddress.Text);
            cmd.Parameters.AddWithValue("City", TxtBxCity.Text);
            cmd.Parameters.AddWithValue("Phone", TxtBxPhone.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            Listing();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("CustomerDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", TxtBxFullName.Tag);
            cmd.ExecuteNonQuery();
            conn.Close();
            Listing();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DataGridView1.CurrentRow;
            TxtBxFullName.Tag = row.Cells["Id"].Value.ToString();
            TxtBxFullName.Text = row.Cells["FullName"].Value.ToString();
            TxtBxTc.Text = row.Cells["Tc"].Value.ToString();
            TxtBxAddress.Text = row.Cells["Address"].Value.ToString();
            TxtBxCity.Text = row.Cells["City"].Value.ToString();
            TxtBxPhone.Text = row.Cells["Phone"].Value.ToString();
        }

        Sales salesForm = new Sales();
        private void BtnSaleTransition_Click(object sender, EventArgs e)
        {
            salesForm.Show();
        }
    }
}
