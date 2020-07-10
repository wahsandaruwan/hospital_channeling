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
    public partial class account : Form
    {
        public account()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd;
        dbOperations qs = new dbOperations();

        //For Update
        string pass, ust;
        int eid;
        private void Account_Load(object sender, EventArgs e)
        {
            //Load Data Grid View
            loadDataGridView();
            //Populate Combo Boxes
            popCombo();
        }

        //Clear Fields
        public void clearAll()
        {
            txteid.Text = "";
            txtpass.Text = "";
            cmbetype.SelectedItem = "None";
            txtsearch.Text = "";
            loadDataGridView();
            popCombo();
        }

        private void Accdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txteid.Text = accdgv.CurrentRow.Cells[0].Value.ToString();
            eid = int.Parse(txteid.Text);
            txtpass.Text = accdgv.CurrentRow.Cells[1].Value.ToString();
            pass = txtpass.Text;
            cmbetype.SelectedItem = accdgv.CurrentRow.Cells[2].Value.ToString();
            ust = cmbetype.Text;
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM useracc";
            populateDataGridView(selectQuery);
            popCombo();
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            accdgv.DataSource = dbo.forPopulateTable(query);
        }

        //Populate Employee ID 
        public void popCombo()
        {
            cmbetype.Items.Clear();
            string qur = "SELECT DISTINCT etype FROM employee";
            cmd = new MySqlCommand(qur, db.getConnection());
            MySqlDataReader myR;
            try
            {
                cmbetype.Items.Add("None");
                db.openCon();
                myR = cmd.ExecuteReader();
                while (myR.Read())
                {
                    string pt = myR.GetString("etype");
                    cmbetype.Items.Add(pt);
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

        private void Btninsert_Click(object sender, EventArgs e)
        {
            if (cmbetype.Text != "None")
            {
                string insertQuery = "INSERT INTO useracc(e_id,pass,ustype) " +
                "VALUES('" + txteid.Text + "','" + txtpass.Text + "','" + cmbetype.SelectedItem + "')";
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
                MessageBox.Show("Please Set a User Type!");
            }
        }

        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if (cmbetype.Text != "None")
            {
                string updateQuery = "UPDATE useracc SET e_id = '" + txteid.Text + "', pass = '" + txtpass.Text + "', ustype = '" + cmbetype.SelectedItem + "' " +
                " WHERE e_id = '" + eid + "' AND pass = '" + pass + "' AND ustype = '" + ust + "'";
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
                MessageBox.Show("Please Set a User Type!");
            }
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM useracc WHERE e_id = '" + eid + "' AND pass = '" + pass + "' AND ustype = '" + ust + "'";
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

        private void Btnsearch_Click(object sender, EventArgs e)
        {
            string search = "SELECT * FROM useracc WHERE e_id LIKE '%" + txtsearch.Text + "%' OR ustype LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
