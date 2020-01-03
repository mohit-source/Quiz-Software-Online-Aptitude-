using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pinnacle
{
    public partial class Instruction : Form
    {
        string connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        public Instruction()
        {
            InitializeComponent();
        }

        public string txtUsername;
        public string txtRollNO;

        public string passingvalue
        {

            get { return txtUsername; }
            set { txtUsername = value; }
        }


        public string passingvalue1
        {

            get { return txtRollNO; }
            set { txtRollNO = value; }


        }  

        private void Instruction_Load(object sender, EventArgs e)
        {
          
        }


        private void Instruction_Load_1(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            SqlConnection con;
            SqlDataReader dr;

            lbluser.Text = txtUsername;

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM settings;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lbltime.Text = dr["exam_time"].ToString()+" Min";
                lblque.Text = dr["total_que"].ToString();
                lblMark.Text = dr["carry_mark"].ToString()+" Mark.";
            }
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Exam fm = new Exam();
            fm.passingvalue = txtUsername;
            fm.passingvalue1 = txtRollNO;
            fm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lbluser_Click(object sender, EventArgs e)
        {

        }

        private void lbMark_Click(object sender, EventArgs e)
        {

        }

      
        
        

    

       
        
      
      
    }
}
