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

namespace AdoNet_NoProcedure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Listing()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Companies", conn);

            DataTable dTable = new DataTable();
            adapter.Fill(dTable);
            DataGridView1.DataSource = dTable;
        }

        public void Cleaning()
        {
            TxtCompanyName.Clear();
            TxtDescription.Clear();
            TxtEmployeeNumber.Clear();
            DateTimeFoundationDate.Value = DateTime.Now;
        }

        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Holding;integrated security=true");

        //"Server=.\\SQLEXPRESS;Database=Holding;uid=userName;pwd=password;"
        // Trusted_Connection=True;

        private void BtnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Companies(CompanyName,Description,EmployeeNumber,FoundationDate) values (@companyName,@description,@employeeNumber,@foundationDate)", conn);
            cmd.Parameters.AddWithValue("@companyName", TxtCompanyName.Text);
            cmd.Parameters.AddWithValue("@description", TxtDescription.Text);
            cmd.Parameters.AddWithValue("@employeeNumber", Convert.ToInt32(TxtEmployeeNumber.Text));
            cmd.Parameters.AddWithValue("@foundationDate", Convert.ToDateTime(DateTimeFoundationDate.Text));

            cmd.ExecuteNonQuery();
            conn.Close();
            Listing();
            Cleaning();


        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            Listing();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DataGridView1.CurrentRow;
            TxtCompanyName.Tag = row.Cells["Id"].Value.ToString();
            TxtCompanyName.Text = row.Cells["CompanyName"].Value.ToString();
            TxtDescription.Text = row.Cells["Description"].Value.ToString();
            TxtEmployeeNumber.Text = row.Cells["EmployeeNumber"].Value.ToString();
            DateTimeFoundationDate.Text = row.Cells["FoundationDate"].Value.ToString();
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("Update Companies Set CompanyName=@companyName,Description =@description,EmployeeNumber=@employeeNumber,FoundationDate=@foundationDate where Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", TxtCompanyName.Tag);
            cmd.Parameters.AddWithValue("@companyName", TxtCompanyName.Text);
            cmd.Parameters.AddWithValue("@description", TxtDescription.Text);
            cmd.Parameters.AddWithValue("@employeeNumber", Convert.ToInt32(TxtEmployeeNumber.Text));
            cmd.Parameters.AddWithValue("@foundationDate", Convert.ToDateTime(DateTimeFoundationDate.Value));
            cmd.ExecuteNonQuery();
            conn.Close();
            Listing();
            Cleaning();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("Delete from Companies where Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", TxtCompanyName.Tag);
            cmd.ExecuteNonQuery();

            conn.Close();
            Listing();
            Cleaning();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter adapter = new SqlDataAdapter("Select * from Companies where CompanyName like '%" + TxtSearch.Text + "%'", conn);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //DataGridView1.DataSource = table;

            Search("Select * from Companies where CompanyName like '%" + TxtSearch.Text + "%'");
        }

        // Search Method

        public void Search(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView1.DataSource = table;
        }

        private void BtnEmployeeNumber_Click(object sender, EventArgs e)
        {
            Search("Select * from Companies where EmployeeNumber>" + TxtSearch.Text + "");
        }

        private void BtnDescription_Click(object sender, EventArgs e)
        {
            Search("Select * from Companies where Description like '%" + TxtSearch.Text + "%'");
        }
    }
}
