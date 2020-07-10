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
    public partial class appt : Form
    {
        public appt()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd1, cmd2, cmd3;
        dbOperations qs = new dbOperations();
        private void Appt_Load(object sender, EventArgs e)
        {
            //Disable employee id
            txtaptno.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
            //Populate Combo Boxes
            popCombo();
            //Date Text
            txtdate.Text = "yyyy-mm-dd";
            //Set Date Format of Datagridview
            aptdgv.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";
        }

        //Clear Fields
        public void clearAll()
        {
            txtaptno.Text = "";
            txtdate.Text = "yyyy-mm-dd";
            cmbroom.SelectedItem = "None";
            cmbpatient.SelectedItem = "None";
            cmbdoc.SelectedItem = "None";
            loadDataGridView();
            popCombo();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM appointment";
            populateDataGridView(selectQuery);
            popCombo();
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            aptdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Aptdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtaptno.Text = aptdgv.CurrentRow.Cells[0].Value.ToString();
            string dt = aptdgv.CurrentRow.Cells[1].Value.ToString();
            //Set Text box date format
            txtdate.Text = DateTime.Parse(dt).ToString("yyyy-MM-dd");
            cmbroom.SelectedItem = aptdgv.CurrentRow.Cells[2].Value.ToString();
            cmbpatient.SelectedItem = aptdgv.CurrentRow.Cells[3].Value.ToString();
            cmbdoc.SelectedItem = aptdgv.CurrentRow.Cells[4].Value.ToString();
        }

        //Populate Room NO, Patient ID and Doctor ID
        public void popCombo()
        {
            cmbpatient.Items.Clear();
            cmbdoc.Items.Clear();
            cmbroom.Items.Clear();
            string qur1 = "SELECT * FROM patient";
            string dr = "Doctor";
            string qur2 = "SELECT * FROM employee WHERE etype='" + dr + "'";
            string qur3 = "SELECT * FROM room";
            cmd1 = new MySqlCommand(qur1, db.getConnection());
            cmd2 = new MySqlCommand(qur2, db.getConnection());
            cmd3 = new MySqlCommand(qur3, db.getConnection());
            MySqlDataReader myR1, myR2, myR3;
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
                db.closeCon();

                db.openCon();
                cmbroom.Items.Add("None");
                myR3 = cmd3.ExecuteReader();
                while (myR3.Read())
                {
                    string rm = myR3.GetString("room_no");
                    cmbroom.Items.Add(rm);
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

        //Insert
        private void Btninsert_Click(object sender, EventArgs e)
        {
            if(cmbroom.Text != "None" && cmbpatient.Text != "None" && cmbdoc.Text != "None")
            {
                string insertQuery = "INSERT INTO appointment(a_date,r_no,p_id,e_id) " +
                "VALUES('" + txtdate.Text + "','" + cmbroom.SelectedItem + "', '" + cmbpatient.SelectedItem + "', '" + cmbdoc.SelectedItem + "')";
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
                MessageBox.Show("Please Set a Room Number, Patient ID and Doctor ID!");
            }
        }

        //Update
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if (cmbroom.Text != "None" && cmbpatient.Text != "None" && cmbdoc.Text != "None")
            {
                string updateQuery = "UPDATE appointment SET a_date = '" + txtdate.Text + "', r_no = '" + cmbroom.SelectedItem + "', " +
                "p_id = '" + cmbpatient.SelectedItem + "', e_id = '" + cmbdoc.SelectedItem + "' WHERE app_no = '" + int.Parse(txtaptno.Text) + "'";
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
                MessageBox.Show("Please Set a Room Number, Patient ID and Doctor ID!");
            }

        }

        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM appointment WHERE app_no = '" + int.Parse(txtaptno.Text) + "'";
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
            string search = "SELECT * FROM appointment WHERE app_no LIKE '%" + txtsearch.Text + "%' OR a_date LIKE '%" + txtsearch.Text + "%' OR r_no LIKE '%" + txtsearch.Text + "%' OR p_id LIKE '%" + txtsearch.Text + "%' OR e_id LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
