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
    public partial class UpdateQuestions : Form
    {
        String connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        public UpdateQuestions()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            que = comboBox1.SelectedItem.ToString();

            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM question where Question = '" + que + "';", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Text = dr["Question"].ToString();
                qno.Text = dr["Qno"].ToString();
                option1.Text = dr["Option1"].ToString();
                option2.Text = dr["Option2"].ToString();
                option3.Text = dr["Option3"].ToString();
                option4.Text = dr["Option4"].ToString();
                answer.Text = dr["Answer"].ToString();

            }
            con.Close();
        }

       
        private void UpdateQuestions_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM question;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();
           
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Question"]);
            }
            con.Close();
        }

        private void updateque_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateQuestions mf = new UpdateQuestions();
            mf.Show();
        }
        public string que;
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            SqlConnection con;

            con = new SqlConnection(connetionString);

            SqlCommand cmd = new SqlCommand("update question set Question ='" + comboBox1.Text + "' ,Option1 ='" + option1.Text + "' ,Option2 = '" + option2.Text + "',Option3 ='" + option3.Text + "' ,Option4 ='" + option4.Text + "',Answer ='" + answer.Text + "' where Question = '" + que + "';", con);
            cmd.CommandType = CommandType.Text;

            try
            {
                if (comboBox1.Text == "" & option1.Text == "" & option2.Text == "" & option3.Text == "" & option4.Text == "" & answer.Text == "")
                {
                    MessageBox.Show("Please Insert Question and Answer!!!");
                }
                else
                {

                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Updated successfully");


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

            comboBox1.Text = "";

            option1.Text = "";

            option2.Text = "";

            option3.Text = "";

            option4.Text = "";

            answer.Text = "";

           

        }

        private void addQ_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addque mf = new Addque();
            mf.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logout mf = new Logout();
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
    }
}
