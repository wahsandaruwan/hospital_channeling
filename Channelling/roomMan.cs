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
    public partial class roomMan : Form
    {
        public roomMan()
        {
            InitializeComponent();
        }

        //My SQL Connection
        MySQLConnect db = new MySQLConnect();

        //For Queries
        MySqlCommand cmd;
        dbOperations qs = new dbOperations();

        private void RoomMan_Load(object sender, EventArgs e)
        {
            //Disable room no
            txtrno.Enabled = false;
            //Load Data Grid View
            loadDataGridView();
            //Load rtypes from department
            popCombo();
        }

        public void popCombo()
        {
            cmbrtype.Items.Clear();
            string qur = "SELECT * FROM department";
            cmd = new MySqlCommand(qur, db.getConnection());
            MySqlDataReader myR;
            try
            {
                cmbrtype.Items.Add("None");
                db.openCon();
                myR = cmd.ExecuteReader();

                while (myR.Read())
                {
                    string rt = myR.GetString("d_name");
                    cmbrtype.Items.Add(rt);
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
            txtrno.Text = "";
            cmbrtype.SelectedItem = "None";
            txtsearch.Text = "";
            loadDataGridView();
        }

        //Load Data Grid View
        public void loadDataGridView()
        {
            string selectQuery = "SELECT * FROM room";
            populateDataGridView(selectQuery);
            popCombo();
        }

        //Populate the data grid view
        public void populateDataGridView(string query)
        {
            dbOperations dbo = new dbOperations();
            roomdgv.DataSource = dbo.forPopulateTable(query);
        }

        private void Roomdgv_MouseClick(object sender, MouseEventArgs e)
        {
            //Update texboxes when click on row
            txtrno.Text = roomdgv.CurrentRow.Cells[0].Value.ToString();
            cmbrtype.Text = roomdgv.CurrentRow.Cells[1].Value.ToString();
        }

        //Insert
        private void Btninsert_Click(object sender, EventArgs e)
        {
            if(cmbrtype.Text != "None")
            {
                string insertQuery = "INSERT INTO room(rtype) " +
                "VALUES('" + cmbrtype.SelectedItem + "')";
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
                MessageBox.Show("Please Select a Room Type");
            }
        }

        //Update
        private void Btnupdate_Click(object sender, EventArgs e)
        {
            if(cmbrtype.Text != "None")
            {
                string updateQuery = "UPDATE room SET rtype = '" + cmbrtype.SelectedItem + "' WHERE room_no = '" + int.Parse(txtrno.Text) + "'";
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
                MessageBox.Show("Please Select a Room Type");
            }
        }

        //Delete
        private void Btndelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = "DELETE FROM room WHERE room_no = '" + int.Parse(txtrno.Text) + "'";
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

        private void Btnclr_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        //Custom Search
        private void Btnsearch_Click(object sender, EventArgs e)
        {
            string search = "SELECT * FROM room WHERE room_no LIKE '%" + txtsearch.Text + "%' OR rtype LIKE '%" + txtsearch.Text + "%'";
            populateDataGridView(search);
        }

        private void Btnassign_Click(object sender, EventArgs e)
        {
            assignRoom am = new assignRoom();
            am.Show();
        }
    }
}
