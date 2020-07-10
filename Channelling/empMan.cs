using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Channelling
{
    public partial class empManage : Form
    {
        public empManage()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        dbOperations qs = new dbOperations();

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void EmpManage_Load(object sender, EventArgs e)
        {
            //Disable employee id
            txtempid.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
            //Default Search Combo
            cmbsearch.SelectedItem = "ALL";
        }

        //Clear Fields
        public void clearAll()
        {
            txtempid.Text = "";
            txtempname.Text = "";
            txtempsal.Text = "";
            txtdepno.Text = "";
            txtsearch.Text = "";
            txtmajor.Text = "";
            cmbemptype.SelectedItem = "";
            cmbsearch.SelectedItem = "ALL";
            loadDataGridView();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM employee";
            populateDataGridView(selectQuery);
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            empdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Empdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtempid.Text = empdgv.CurrentRow.Cells[0].Value.ToString();
            txtempname.Text = empdgv.CurrentRow.Cells[1].Value.ToString();
            txtempsal.Text = empdgv.CurrentRow.Cells[2].Value.ToString();
            cmbemptype.SelectedItem = empdgv.CurrentRow.Cells[3].Value.ToString();
            txtdepno.Text = empdgv.CurrentRow.Cells[4].Value.ToString();
            txtmajor.Text = empdgv.CurrentRow.Cells[5].Value.ToString();
        }

        //Insert
        private void Btninsert_Click(object sender, EventArgs e)
        {
            if(cmbemptype.Text != "None")
            {
                string insertQuery = "INSERT INTO employee(e_name,salary,etype,dep_no,major) " +
                "VALUES('" + txtempname.Text + "','" + txtempsal.Text + "', '" + cmbemptype.SelectedItem + "', '" + txtdepno.Text + "', '" + txtmajor.Text + "')";
                if (qs.executeQuery(insertQuery) == "T")
                {
                    MessageBox.Show("Successfully Inserted!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Insert!");
                    MessageBox.Show(qs.executeQuery(insertQuery));
                }
            }
            else
            {
                MessageBox.Show("Please select an Employee Type!");
            }
        }

        //Update
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if (cmbemptype.Text != "None")
            {
                string updateQuery = "UPDATE employee SET e_name = '" + txtempname.Text + "', salary = '" + txtempsal.Text + "', " +
                "etype = '" + cmbemptype.SelectedItem + "', dep_no = '" + txtdepno.Text + "', major = '" + txtmajor.Text + "' WHERE e_id = '" + int.Parse(txtempid.Text) + "'";
                if (qs.executeQuery(updateQuery) == "T")
                {
                    MessageBox.Show("Successfully Updated!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Update!");
                    MessageBox.Show(qs.executeQuery(updateQuery));
                }
            }
            else
            {
                MessageBox.Show("Please select an Employee Type!");
            }

        }

        //Delete
        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM employee WHERE e_id = '" + int.Parse(txtempid.Text) + "'";
            if (qs.executeQuery(deleteQuery) == "T")
            {
                MessageBox.Show("Successfully Deleted!");
                clearAll();
            }
            else
            {
                MessageBox.Show("Failed to Delete!");
                MessageBox.Show(qs.executeQuery(deleteQuery));
            }
        }

        //Custom Search
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            //Load All Data
            if(cmbsearch.Text == "ALL" && txtsearch.Text == "")
            {
                loadDataGridView();
            }

            //Search Data According to Search Box
            else if(cmbsearch.Text == "ALL" && txtsearch.Text != "")
            {
                string search1 = "SELECT * FROM employee WHERE e_name LIKE '%" + txtsearch.Text + "%' OR e_id LIKE '%" + txtsearch.Text + "%' OR salary LIKE '%" + txtsearch.Text + "%' OR dep_no LIKE '%" + txtsearch.Text + "%' OR major LIKE '%" + txtsearch.Text + "%'";
                populateDataGridView(search1);
            }

            //Search Data using Employee Type and Any Other Column
            else
            {
                string search2 = "SELECT * FROM employee WHERE (e_name LIKE '%" + txtsearch.Text + "%' OR e_id LIKE '%" + txtsearch.Text + "%' OR salary LIKE '%" + txtsearch.Text + "%' OR dep_no LIKE '%" + txtsearch.Text + "%' OR major LIKE '%" + txtsearch.Text + "%') AND etype LIKE '%" + cmbsearch.SelectedItem + "%'";
                populateDataGridView(search2);
            }
                      
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
