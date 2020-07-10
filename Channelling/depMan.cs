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
    public partial class depMan : Form
    {
        public depMan()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        dbOperations qs = new dbOperations();

        private void DepMan_Load(object sender, EventArgs e)
        {
            //Disable department no
            txtdno.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
        }

        //Clear Fields
        public void clearAll()
        {
            txtdno.Text = "";
            txtdname.Text = "";
            txtsearch.Text = "";
            loadDataGridView();
        }


        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM department";
            populateDataGridView(selectQuery);
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            depdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Depdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtdno.Text = depdgv.CurrentRow.Cells[0].Value.ToString();
            txtdname.Text = depdgv.CurrentRow.Cells[1].Value.ToString();
        }

        //Insert
        private void Btninsert_Click(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO department(d_name) " +
                "VALUES('" + txtdname.Text + "')";
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

        //Update
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE department SET d_name = '" + txtdname.Text + "' WHERE d_no = '" + int.Parse(txtdno.Text) + "'";
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

        //Delete
        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM department WHERE d_no = '" + int.Parse(txtdno.Text) + "'";
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
            string search = "SELECT * FROM department WHERE d_name LIKE '%" + txtsearch.Text + "%' OR d_no LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
