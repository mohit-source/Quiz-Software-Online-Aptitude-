using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pinnacle
{
    public partial class Addque : Form
    {
        String connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        public Addque()
        {
            InitializeComponent();
        }

        private void Addque_Load(object sender, EventArgs e)
        {

        }

        private void Addque_Load_1(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void score_Click(object sender, EventArgs e)
        {
             this.Hide();
            Adminpage mf = new Adminpage();
            mf.Show();
        
        }

        private void addQ_Click(object sender, EventArgs e)
        {

            this.Hide();
            Addque mf = new Addque();
            mf.Show();
          
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

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

        public int roll;
        private void save_Click(object sender, EventArgs e)
        {


       
          
            SqlConnection con;
           
            con = new SqlConnection(connetionString);

            SqlCommand cmd = new SqlCommand("insert into question(Question,Option1,Option2,Option3,Option4,Answer) values ('" +addquetxt.Text+ "','" +option1.Text+ "','" +option2.Text+ "','" +option3.Text+ "','" +option4.Text+ "','" +answer.Text+ "')", con);
        cmd.CommandType = CommandType.Text;

        try

        {
            if ( addquetxt.Text=="" & option1.Text=="" & option2.Text=="" & option3.Text=="" & option4.Text=="" & answer.Text=="")
            {
                MessageBox.Show("Please Insert Question and Answer!!!");
            }
            else
            {

                con.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data inserted successfully");


                con.Close();

                refress();
            
            }

        }

        catch (Exception ex)

        {
            MessageBox.Show(ex.Message);

        }

    
        }
        public void refress()
        {

            qno.Text = "";

            addquetxt.Text = "";

            option1.Text = "";

            option2.Text = "";

            option3.Text = "";

            option4.Text = "";

            answer.Text = "";

           

        }

        private void updateque_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateQuestions mf = new UpdateQuestions();
            mf.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logout fm = new Logout();
            fm.Show();
        }
    }
}
