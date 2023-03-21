using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace CustomerForm
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            con= new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }
        private DataSet GetAllProducts()
        {
            da = new SqlDataAdapter("Select * from book1", con);
            da.MissingSchemaAction=MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds= new DataSet();
            da.Fill(ds, "book1");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book1"].NewRow();
                row["name"]=txtName.Text;
                row["author"]=txtAuthor.Text;
                row["price"]=txtPrice.Text;
                // add new row in the dataset table

                ds.Tables["book1"].Rows.Add(row);
                int res = da.Update(ds.Tables["book1"]);
                if (res >= 1)
                {
                    MessageBox.Show("Record Inserted..");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                dataGridView1.DataSource = ds.Tables["book1"];

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book1"].Rows.Find(txtId.Text);
                if(row!=null)
                {
                    txtName.Text = row["name"].ToString();
                    txtAuthor.Text = row["author"].ToString();
                    txtPrice.Text = row["price"].ToString();

                }
                else
                {
                    MessageBox.Show("Record not found.");
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book1"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["author"] = txtAuthor.Text;
                    row["price"] = txtPrice.Text;
                    int res = da.Update(ds.Tables["book1"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record updated..");
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book1"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["book1"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record Deleted..");
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
