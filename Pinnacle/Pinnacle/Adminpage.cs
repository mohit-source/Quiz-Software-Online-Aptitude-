using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Pinnacle
{
    public partial class Adminpage : Form
    {


        String connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        SqlConnection con;
        SqlDataReader dr;

        string totalstudent;
        public Adminpage()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Adminpage_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


           

            SqlConnection con1;
            SqlDataReader dr1;
            con1 = new SqlConnection(connetionString);
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM category;", con1);


            cmd1.CommandType = CommandType.Text;
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {
                catlist.Items.Add(dr1["categories"]);
            }
            con1.Close();

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM result ;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                totalstudent = dr["id"].ToString();
             
            }
            con.Close();


            lblStud.Text = totalstudent;

        }

        private void addQ_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addque mf = new Addque();
            mf.Show();
          
        }

        private void score_Click(object sender, EventArgs e)
        {
            this.Hide();
            Adminpage mf = new Adminpage();
            mf.Show();
          
        }

        private void addclg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addcollege mf = new Addcollege();
            mf.Show();
        }

        private void setting_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings mf = new Settings();
            mf.Show();
        }

        private void updateque_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateQuestions mf = new UpdateQuestions();
            mf.Show();
        }
        public int a;
        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {




                con = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM result;", con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                dr = cmd.ExecuteReader();

               

                if (dr.HasRows)
                {
                    
                    DataTable dt = new DataTable();

                    dt.Load(dr);
                   
                    dataGridView1.DataSource = dt;
                   
                      

                }

            }

            finally
            {

                con.Close();

            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logout fm = new Logout();
            fm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cat = catlist.SelectedItem.ToString();
            if (catlist.SelectedItem.ToString() == cat)
            {

                

                try
                {




                    con = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM result Where category='"+cat+"';", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    dr = cmd.ExecuteReader();

                  

                    if (dr.HasRows)
                    {
                       
                        DataTable dt = new DataTable();

                        dt.Load(dr);

                        dataGridView1.DataSource = dt;

                    }

                }

                finally
                {

                    con.Close();

                }



            }
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int score = Int32.Parse(txtSearch.Text);

            if (score == 0)
            {
                MessageBox.Show("Enter Minimum Marks for Searching...");
            }
            else
            {


                try
                {




                    con = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM result Where score>='" + score + "';", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    dr = cmd.ExecuteReader();


                    if (dr.HasRows)
                    {

                        DataTable dt = new DataTable();

                        dt.Load(dr);

                        dataGridView1.DataSource = dt;

                    }

                }

                finally
                {

                    con.Close();

                }


                //end


            
            }
        }

        private void lblStud_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            Adminpage mf = new Adminpage();
            mf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);



            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            e.Graphics.DrawImage(bm, 0, 0);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string a = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            int roll = Int32.Parse(a);
            try
            {




                con = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM QAResult Where roll_no='" + roll + "';", con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {

                    DataTable dt = new DataTable();

                    dt.Load(dr);

                    dataGridView2.DataSource = dt;

                }

            }

            finally
            {

                con.Close();

            }




        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


       
    }
}
