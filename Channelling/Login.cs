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
    public partial class Login : Form
    {
        //Set User Type
        string ut;
        public Login()
        {
            InitializeComponent();
            //Populate Combo Box
            popCombo();
        }

        //Database Connection
        MySQLConnect db = new MySQLConnect();
        MySqlCommand cmd = new MySqlCommand();

        //Populate Employee ID 
        public void popCombo()
        {
            cmbutype.Items.Clear();
            string qur = "SELECT DISTINCT etype FROM employee";
            cmd = new MySqlCommand(qur, db.getConnection());
            MySqlDataReader myR;
            try
            {
                cmbutype.Items.Add("None");
                db.openCon();
                myR = cmd.ExecuteReader();
                while (myR.Read())
                {
                    string pt = myR.GetString("etype");
                    cmbutype.Items.Add(pt);
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

        //Login Process
        private void Btnlogin_Click(object sender, EventArgs e)
        {
            int i = 0;
            db.openCon();
            string query = "SELECT * from useracc WHERE e_id='" + txtuid.Text + "' AND pass='" + txtpass.Text + "' AND ustype='" + cmbutype.SelectedItem + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(query, db.getConnection());
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if(i == 0)
            {
                MessageBox.Show("Invalid User");
            }
            else
            {
                //For Main Form
                Main m = new Main();

                //Get user type
                ut = cmbutype.Text;
                m.restrict(ut.ToString());

                //Display Main Form
                this.Hide();
                m.Show();
            }
        }

        //Exit
        private void Btncancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
