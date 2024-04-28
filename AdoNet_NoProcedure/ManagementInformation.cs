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
    public partial class ManagementInformation : Form
    {
        public ManagementInformation()
        {
            InitializeComponent();
        }

        public void Listing()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from ManagementInformation", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DataGridView.DataSource = dt;

        }

        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Holding;integrated security=true");

        private void BtnImage_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFile1 = new OpenFileDialog();
            //openFile1.ShowDialog();
            OpenFileImage.ShowDialog();
            TxtBxImage.Text = OpenFileImage.FileName;
            PictureBx.ImageLocation = OpenFileImage.FileName;

        }

        private void BtnCV_Click(object sender, EventArgs e)
        {
            OpenFileCV.ShowDialog();
            TxtBxCV.Text = OpenFileCV.FileName;
        }

        private void LnkLblCV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(OpenFileCV.FileName);
        }

        private void ManagementInformation_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Companies", conn);
            DataTable Dtable = new DataTable();
            adapter.Fill(Dtable);
            CmbBxCompanies.DataSource = Dtable;
            CmbBxCompanies.DisplayMember = "CompanyName";
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            Listing();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into ManagementInformation (NameSurname,Tc,Image,Cv,Companies) values (@name,@tc,@image,@cv,@companies)", conn);
            cmd.Parameters.AddWithValue("@name", TxtBxNameSurname.Text);
            cmd.Parameters.AddWithValue("@tc", TxtBxTC.Text);
            cmd.Parameters.AddWithValue("@image", TxtBxImage.Text);
            cmd.Parameters.AddWithValue("@cv", TxtBxCV.Text);
            cmd.Parameters.AddWithValue("@companies", CmbBxCompanies.Text);
            cmd.ExecuteNonQuery();
            Listing();

            conn.Close();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DataGridView.CurrentRow;
            TxtBxNameSurname.Tag = row.Cells["Id"].Value.ToString();
            TxtBxNameSurname.Text = row.Cells["NameSurname"].Value.ToString();
            TxtBxTC.Text = row.Cells["Tc"].Value.ToString();
            TxtBxImage.Text = row.Cells["Image"].Value.ToString();
            TxtBxCV.Text = row.Cells["Cv"].Value.ToString();
            CmbBxCompanies.Text = row.Cells["Companies"].Value.ToString();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("update ManagementInformation set NameSurname=@name,Tc=@tc,Image=@image,Cv=@cv,Companies=@companies where Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", TxtBxNameSurname.Tag);
            cmd.Parameters.AddWithValue("@name", TxtBxNameSurname.Text);
            cmd.Parameters.AddWithValue("@tc", TxtBxTC.Text);
            cmd.Parameters.AddWithValue("@image", TxtBxImage.Text);
            cmd.Parameters.AddWithValue("@cv", TxtBxCV.Text);
            cmd.Parameters.AddWithValue("@companies", CmbBxCompanies.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from ManagementInformation where Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", TxtBxNameSurname.Tag);
            cmd.ExecuteNonQuery();
            Listing();
            conn.Close();
        }
    }
}
