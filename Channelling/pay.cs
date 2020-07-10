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
    public partial class pay : Form
    {
        public pay()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd;
        dbOperations qs = new dbOperations();
        private void Pay_Load(object sender, EventArgs e)
        {
            //Disable employee id
            txtbill.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
            //Populate Combo Boxes
            popCombo();
        }

        //Clear Fields
        public void clearAll()
        {
            txtbill.Text = "";
            txtamt.Text = "";
            cmbpatient.SelectedItem = "None";
            txtsearch.Text = "";
            loadDataGridView();
            popCombo();
        }

        private void Paydgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtbill.Text = paydgv.CurrentRow.Cells[0].Value.ToString();
            txtamt.Text = paydgv.CurrentRow.Cells[1].Value.ToString();
            cmbpatient.SelectedItem = paydgv.CurrentRow.Cells[2].Value.ToString();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM payment";
            populateDataGridView(selectQuery);
            popCombo();
        }

        //Insert
        private void Btninsert_Click(object sender, EventArgs e)
        {
            if (cmbpatient.Text != "None")
            {
                string insertQuery = "INSERT INTO payment(amount,p_id) " +
                "VALUES('" + txtamt.Text + "','" + cmbpatient.SelectedItem + "')";
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
                MessageBox.Show("Please Set a Patient ID!");
            }
        }

        //Update
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if (cmbpatient.Text != "None")
            {
                string updateQuery = "UPDATE payment SET amount = '" + txtamt.Text + "', p_id = '" + cmbpatient.SelectedItem + "' " +
                " WHERE bill_no = '" + int.Parse(txtbill.Text) + "'";
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
                MessageBox.Show("Please Set a Patient ID!");
            }
        }

        //Delete
        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM payment WHERE bill_no = '" + int.Parse(txtbill.Text) + "'";
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
            string search = "SELECT * FROM payment WHERE bill_no LIKE '%" + txtsearch.Text + "%' OR amount LIKE '%" + txtsearch.Text + "%' OR p_id LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            paydgv.DataSource = dbo.forPopulateTable(query);
        }

        //Populate Patient ID 
        public void popCombo()
        {
            cmbpatient.Items.Clear();
            string qur = "SELECT * FROM patient";
            cmd = new MySqlCommand(qur, db.getConnection());
            MySqlDataReader myR;
            try
            {
                cmbpatient.Items.Add("None");
                db.openCon();
                myR = cmd.ExecuteReader();
                while (myR.Read())
                {
                    string pt = myR.GetString("p_id");
                    cmbpatient.Items.Add(pt);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.closeCon();
            }
        }

        private void Txtamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cmbpatient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Txtbill_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
