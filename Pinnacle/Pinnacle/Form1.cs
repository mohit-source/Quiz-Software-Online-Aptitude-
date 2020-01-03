using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pinnacle
{
    public partial class Splash : Form
    {
        String connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        SqlConnection con;
        SqlDataReader dr;
        string appname;
        string year;

        public Splash()
        {
            InitializeComponent();
        }
        Timer tmr;
        private void Splash_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            year = DateTime.Now.Year.ToString();


            tmr = new Timer();

            //set time interval 3 sec

            tmr.Interval = 8000;

            //starts the timer

            tmr.Start();

            tmr.Tick += tmr_Tick;

         

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM AppName ;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                appname = dr["appname"].ToString();

            }
            con.Close();

            lblAppname.Text = appname + " " + year;

        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void tmr_Tick(object sender, EventArgs e)
        {

            //after 3 sec stop the timer

            tmr.Stop();

            //display mainform

            Login mf = new Login();
            mf.passingvalue = appname;
            mf.Show();

            //hide this form

            this.Hide();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
