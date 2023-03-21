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
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form3()
        {
            InitializeComponent();
            con=new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }
        private DataSet GetAllResults()
        {
            da = new SqlDataAdapter("Select * from Studentss",con);
            da.MissingSchemaAction =  MissingSchemaAction.AddWithKey;
            scb=new SqlCommandBuilder(da);
            ds= new DataSet();
            da.Fill(ds, "Studentss");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllResults();
                DataRow row = ds.Tables["Studentss"].NewRow();
                row["name"] = txtName.Text;
                row["percentage"] = txtPercentage.Text;

                ds.Tables["Studentss"].Rows.Add(row);
                int res = da.Update(ds.Tables["Studentss"]);
                if (res>=1)
                {
                    MessageBox.Show("Record Inserted..");
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
                DataSet ds = GetAllResults();
                DataRow row = ds.Tables["Studentss"].Rows.Find(txtRollno.Text);
                if(row != null)
                {
                    row["name"] = txtName.Text;
                    row["percentage"]=txtPercentage.Text;
                    int res = da.Update(ds.Tables["Studentss"]);
                    if(res>=1)
                    {
                        MessageBox.Show("Record Updated..");
                    }
                    else
                    {
                        MessageBox.Show("Record not found.");
                    }
                }

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
                DataSet ds = GetAllResults();
                DataRow row = ds.Tables["Studentss"].Rows.Find(txtRollno.Text);
                if(row != null)
                {
                   txtRollno.Text = row["roll_no"].ToString();
                    txtName.Text = row["name"].ToString();
                    txtPercentage.Text= row["percentage"].ToString();
                }
                else
                {
                    MessageBox.Show("Record Not Found");
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
                DataSet ds = GetAllResults();
                DataRow row = ds.Tables["Studentss"].Rows.Find(txtRollno.Text);
                if (row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["Studentss"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record Deleted..");
                    }
                    else
                    {
                        MessageBox.Show("Record not found");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetAllStudents_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllResults();
                dataGridView1.DataSource = ds.Tables["Studentss"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
