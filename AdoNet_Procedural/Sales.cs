using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AdoNet_Procedural
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
        }

        public void Listing(string procedure)
        {
            SqlCommand cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DGridView.DataSource = dt;
        }

        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=SalesCompany;integrated security=true");

        private void BtnList_Click(object sender, EventArgs e)
        {
            Listing("SalesList");
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            Listing("SalesList");
            SqlCommand cmd = new SqlCommand("ProductList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            CmbBxProduct.DataSource = dt;
            CmbBxProduct.DisplayMember = "ProductName";
            CmbBxProduct.ValueMember = "Id";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SalesSave", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("customerid", TxtBxCustomer.Text);
            cmd.Parameters.AddWithValue("productid", CmbBxProduct.SelectedValue);
            cmd.Parameters.AddWithValue("price", TxtBxPrice.Text);
            cmd.Parameters.AddWithValue("piece", TxtBxPiece.Text);
            cmd.Parameters.AddWithValue("payment", TxtBxPayment.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            Listing("SalesList");
        }
    }
}
