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
    public partial class assignRoom : Form
    {
        public assignRoom()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd;
        dbOperations qs = new dbOperations();

        private void AssignRoom_Load(object sender, EventArgs e)
        {
            //Load Data Grid View
            loadDataGridView();
            //Load rtypes from department
            popCombo();
        }

        //Populate room number
        public void popCombo()
        {
            cmbrn.Items.Clear();
            cmbrn1.Items.Clear();
            string qur = "SELECT * FROM room";
            cmd = new MySqlCommand(qur, db.getConnection());
            MySqlDataReader myR;
            try
            {
                cmbrn.Items.Add("None");
                cmbrn1.Items.Add("None");
                db.openCon();
                myR = cmd.ExecuteReader();
                while (myR.Read())
                {
                    string ro = myR.GetString("room_no");
                    cmbrn.Items.Add(ro);
                    cmbrn1.Items.Add(ro);
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
            txtdo.Text = "";
            txtnu.Text = "";
            cmbrn.SelectedItem = "None";
            cmbrn1.SelectedItem = "None";
            loadDataGridView();
        }

        private void Asrdgv_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery1 = "SELECT * FROM doctorroom";
            string selectQuery2 = "SELECT * FROM nurseroom";
            populateDataGridView1(selectQuery1);
            populateDataGridView2(selectQuery2);
            popCombo();
        }

        //Populate the data grid view of Doctorroom
        public void populateDataGridView1(string query)
        {
            dbOperations dbo = new dbOperations();
            asrdgvs.DataSource = dbo.forPopulateTable(query);
        }

        //Populate the data grid view of Nurseroom
        public void populateDataGridView2(string query)
        {
            dbOperations dbo = new dbOperations();
            asrdgvz.DataSource = dbo.forPopulateTable(query);
        }

        //Insert Doctorrrom
        private void Btninsert_Click(object sender, EventArgs e)
        {
            if (cmbrn.Text != "None")
            {
                string insertQuery1 = "INSERT INTO doctorroom(e_id,room_no) " +
                "VALUES('" + txtdo.Text + "', '" + cmbrn.SelectedItem + "')";
                if (qs.executeQuery(insertQuery1) == "T")
                {
                    MessageBox.Show("Successfully Inserted!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Insert!");
                    MessageBox.Show(qs.executeQuery(insertQuery1));
                }
            }
            else
            {
                MessageBox.Show("Please Select a Room Number");
            }
        }

        //Update Doctorrrom
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if (cmbrn.Text != "None")
            {
                string updateQuery1 = "UPDATE doctorroom SET e_id = '" + txtdo.Text + "' WHERE room_no = '" + int.Parse(cmbrn.Text) + "'";               
                if (qs.executeQuery(updateQuery1) == "T")
                {
                    MessageBox.Show("Successfully Updated!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Update!");
                    MessageBox.Show(qs.executeQuery(updateQuery1));
                }
            }
            else
            {
                MessageBox.Show("Please Select a Room Type");
            }
        }

        //Delete Doctorrrom
        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery1 = "DELETE FROM doctorroom WHERE room_no = '" + int.Parse(cmbrn.Text) + "'";
            if (qs.executeQuery(deleteQuery1) == "T")
            {
                MessageBox.Show("Successfully Deleted!");
                clearAll();
            }
            else
            {
                MessageBox.Show("Failed to Delete!");
                MessageBox.Show(qs.executeQuery(deleteQuery1));
            }
        }

        //Search Doctorrrom
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            string search = "SELECT * FROM doctorroom WHERE room_no LIKE '%" + txtsearch.Text + "%' OR e_id LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView1(search);
        }

        private void Asrdgv_MouseClick_1(object sender, MouseEventArgs e)
        {
            
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Cmbrn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void Asrdgvs_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtdo.Text = asrdgvs.CurrentRow.Cells[0].Value.ToString();
            cmbrn.Text = asrdgvs.CurrentRow.Cells[1].Value.ToString();
        }

        //Insert Nurseroom
        private void Btninsert1_Click(object sender, EventArgs e)
        {
            if (cmbrn1.Text != "None")
            {
                string insertQuery1 = "INSERT INTO nurseroom(e_id,room_no) " +
                "VALUES('" + txtnu.Text + "', '" + cmbrn1.SelectedItem + "')";
                if (qs.executeQuery(insertQuery1) == "T")
                {
                    MessageBox.Show("Successfully Inserted!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Insert!");
                    MessageBox.Show(qs.executeQuery(insertQuery1));
                }
            }
            else
            {
                MessageBox.Show("Please Select a Room Number");
            }
        }

        //Update Nurseroom
        private void Btnupdate1_Click(object sender, EventArgs e)
        {
            if (cmbrn.Text != "None")
            {
                string updateQuery1 = "UPDATE nurseroom SET e_id = '" + txtnu.Text + "' WHERE room_no = '" + int.Parse(cmbrn1.Text) + "'";
                if (qs.executeQuery(updateQuery1) == "T")
                {
                    MessageBox.Show("Successfully Updated!");
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Failed to Update!");
                    MessageBox.Show(qs.executeQuery(updateQuery1));
                }
            }
            else
            {
                MessageBox.Show("Please Select a Room Type");
            }
        }

        //Delete Nurseroom
        private void Btndelete1_Click(object sender, EventArgs e)
        {
            string deleteQuery1 = "DELETE FROM nurseroom WHERE room_no = '" + int.Parse(cmbrn1.Text) + "'";
            if (qs.executeQuery(deleteQuery1) == "T")
            {
                MessageBox.Show("Successfully Deleted!");
                clearAll();
            }
            else
            {
                MessageBox.Show("Failed to Delete!");
                MessageBox.Show(qs.executeQuery(deleteQuery1));
            }
        }

        //Search Nurseroom
        private void Button1_Click(object sender, EventArgs e)
        {
            string search = "SELECT * FROM nurseroom WHERE room_no LIKE '%" + txtsearch1.Text + "%' OR e_id LIKE '%" + txtsearch1.Text + "%'";
            populateDataGridView2(search);
        }

        private void Asrdgvz_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtnu.Text = asrdgvz.CurrentRow.Cells[0].Value.ToString();
            cmbrn1.Text = asrdgvz.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
