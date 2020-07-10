using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Channelling
{
    public partial class Main : Form
    {
        //Get User Type
        string utt;
        public Main()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        //Exit
        private void Btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Employee Management
        private void Btnemp_Click(object sender, EventArgs e)
        {
            empManage em = new empManage();
            em.Show();
        }

        //Patient Management
        private void Btnpat_Click(object sender, EventArgs e)
        {
            patMan pt = new patMan();
            pt.Show();
        }

        //Appointments Management
        private void Btnapt_Click(object sender, EventArgs e)
        {
            appt ap = new appt();
            ap.Show();
        }

        //Payment Management
        private void Btnpay_Click(object sender, EventArgs e)
        {
            pay p = new pay();
            p.Show();
        }

        //Department Management
        private void Btndep_Click(object sender, EventArgs e)
        {
            depMan dp = new depMan();
            dp.Show();
        }

        //Room Management
        private void Btnroom_Click(object sender, EventArgs e)
        {
            roomMan rm = new roomMan();
            rm.Show();
        }

        //User Account Management
        private void Btnuser_Click(object sender, EventArgs e)
        {
            account ac = new account();
            ac.Show();
        }

        //Log Out
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        //Minimize
        private void Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //User Restriction
        public void restrict(string u)
        {
            utt = u.ToString();
            if(utt == "Doctor" || utt == "Nurse")
            {
                btnapt.Enabled = true;
                btnpat.Enabled = true;
                btnemp.Enabled = false;
                btndep.Enabled = false;
                btnpay.Enabled = false;
                btnroom.Enabled = true;
                btnuser.Enabled = false;
            }
            else if(utt == "Receptionist")
            {
                btnapt.Enabled = true;
                btnpat.Enabled = true;
                btnemp.Enabled = true;
                btndep.Enabled = false;
                btnpay.Enabled = true;
                btnroom.Enabled = true;
                btnuser.Enabled = false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        { 
            
        }
    }
}
