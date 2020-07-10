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
    public partial class treat : Form
    {
        public treat()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd1, cmd2;
        dbOperations qs = new dbOperations();

        private void Btninsert_Click(object sender, EventArgs e)
        {
            if(cmbpatient.Text != "None" && cmbdoc.Text != "None")
            {
                string insertQuery = "INSERT INTO doctorpatient(e_id,p_id,treat) " +
                "VALUES('" + cmbdoc.Text + "', '" + cmbpatient.Text + "', '" + txttreat.Text + "')";
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
                MessageBox.Show("Please Select a Patient Number and Doctor Number");
            }
        }

        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if(cmbpatient.Text != "None" && cmbdoc.Text != "None")
            {
                string updateQuery = "UPDATE doctorpatient SET p_id = '" + cmbpatient.Text + "', treat = '" + txttreat.Text + "' WHERE e_id = '" + int.Parse(cmbdoc.Text) + "'";
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
                MessageBox.Show("Please Select a Patient Number and Doctor Number");
            }
        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM doctorpatient WHERE e_id = '" + int.Parse(cmbdoc.Text) + "' AND p_id = '" + int.Parse(cmbpatient.Text) + "' AND treat = '" + int.Parse(txttreat.Text) + "'";
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
            string search = "SELECT * FROM doctorpatient WHERE e_id LIKE '%" + txtsearch.Text + "%' OR p_id LIKE '%" + txtsearch.Text + "%' OR treat = '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void Treat_Load(object sender, EventArgs e)
        {
            //Load Data Grid View
            loadDataGridView();
            //Load rtypes from department
            popCombo();
        }

        //Populate Patient ID an Doctor ID
        public void popCombo()
        {
            cmbpatient.Items.Clear();
            cmbdoc.Items.Clear();
            string qur1 = "SELECT * FROM patient";
            string dr = "Doctor";
            string qur2 = "SELECT * FROM employee WHERE etype='"+dr+"'";
            cmd1 = new MySqlCommand(qur1, db.getConnection());
            cmd2 = new MySqlCommand(qur2, db.getConnection());
            MySqlDataReader myR1, myR2;
            try
            {
                cmbpatient.Items.Add("None");
                db.openCon();
                myR1 = cmd1.ExecuteReader();
                while (myR1.Read())
                {
                    string pt = myR1.GetString("p_id");
                    cmbpatient.Items.Add(pt);
                }
                db.closeCon();


                db.openCon();
                cmbdoc.Items.Add("None");
                myR2 = cmd2.ExecuteReader();
                while (myR2.Read())
                {
                    string doc = myR2.GetString("e_id");
                    cmbdoc.Items.Add(doc);
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

        //Clear Fields
        public void clearAll()
        {
            txtsearch.Text = "";
            txttreat.Text = "";
            cmbpatient.SelectedItem = "None";
            cmbdoc.SelectedItem = "None";
            loadDataGridView();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM doctorpatient";
            populateDataGridView(selectQuery);
            popCombo();
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            trdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Trdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            cmbpatient.Text = trdgv.CurrentRow.Cells[0].Value.ToString();
            cmbdoc.SelectedItem = trdgv.CurrentRow.Cells[1].Value.ToString();
            txttreat.Text = trdgv.CurrentRow.Cells[2].Value.ToString();
        }

        private void Patdgv_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
