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
    public partial class patMan : Form
    {
        public patMan()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        dbOperations qs = new dbOperations();

        private void PatMan_Load(object sender, EventArgs e)
        {
            //Disable patient id an treatment history
            txtpid.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
        }

        //Clear Fields
        public void clearAll()
        {
            txtpid.Text = "";
            txtpname.Text = "";
            txtsearch.Text = "";
            loadDataGridView();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM patient";
            populateDataGridView(selectQuery);

        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            patdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Patdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtpid.Text = patdgv.CurrentRow.Cells[0].Value.ToString();
            txtpname.Text = patdgv.CurrentRow.Cells[1].Value.ToString();
        }

        //Insert
        private void Btninsert_Click_1(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO patient(p_name) " +
                "VALUES('" + txtpname.Text + "')";
            if(qs.executeQuery(insertQuery) == "T")
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
        private void Btnupdate_Click_1(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE patient SET p_name = '" + txtpname.Text + "' WHERE p_id = '" + int.Parse(txtpid.Text) + "'";
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
        private void Btndelete_Click_1(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM patient WHERE p_id = '" + int.Parse(txtpid.Text) + "'";
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
        private void Btnsearch_Click_1(object sender, EventArgs e)
        {
            string search = "SELECT * FROM patient WHERE p_name LIKE '%" + txtsearch.Text + "%' OR p_id LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Txtpname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txtpid_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void Patdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            treat tr = new treat();
            tr.Show();
        }
    }
}
