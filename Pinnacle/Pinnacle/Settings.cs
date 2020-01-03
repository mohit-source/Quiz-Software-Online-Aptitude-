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
    public partial class Settings : Form
    {
        string connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM category;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                categoryBox.Items.Add(dr["categories"]);
            }
            con.Close();


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

        private void addclg_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addcollege mf = new Addcollege();
            mf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            float neg_mark = float.Parse(txtNegMarks.Text);
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("update settings set total_que ='" + txtTQ.Text + "',carry_mark ='" + txtMarks.Text + "',exam_time ='" + txtExamTime.Text + "',neg_mark ='" + neg_mark + "';", con);

            cmd.CommandType = CommandType.Text;

            try
            {
                if (txtTQ.Text == "" & txtMarks.Text =="" & txtNegMarks.Text == "" & txtExamTime.Text == "")
                {
                    MessageBox.Show("Please Set Settings!!!");
                }
                else
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Setting Updated successfully");


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
           txtTQ.Text = "" ;
            txtMarks.Text ="" ;
            txtNegMarks.Text = "" ;
            txtExamTime.Text = "";

        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logout fm = new Logout();
            fm.Show();
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void addcat_Click(object sender, EventArgs e)
        {
            SqlConnection con;

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("insert into category(categories) values('" + categoryBox.Text + "')", con);

            cmd.CommandType = CommandType.Text;

            try
            {
                if (categoryBox.Text == "")
                {
                    MessageBox.Show("Please insert Category !!!");
                }
                else
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Category Inserted successfully");

                    this.Hide();
                    Settings mf = new Settings();
                    mf.Show();
                    con.Close();

                    refress();
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void rmvcat_Click(object sender, EventArgs e)
        {
            SqlConnection con;

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("delete from category where categories = '" + categoryBox.Text + "' ;", con);

            cmd.CommandType = CommandType.Text;

            try
            {
                if (categoryBox.Text == "")
                {
                    MessageBox.Show("Please Select Category for Remove !!!");
                }
                else
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Category Removed successfully");
                    this.Hide();
                    Settings mf = new Settings();
                    mf.Show();

                    con.Close();
                    categoryBox.Text = " ";

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void AddAppName_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            string appname = txtAppname.Text;
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("update AppName set appname ='" + txtAppname.Text + "';", con);

            cmd.CommandType = CommandType.Text;

            try
            {
                if (txtAppname.Text == "")
                {
                    MessageBox.Show("Please Insert Application Name!!!");
                }
                else
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Application Name Updated successfully");


                    con.Close();

                    refress();
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        
    }
}
