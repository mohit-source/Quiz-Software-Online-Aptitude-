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
    public partial class Addcollege : Form
    {
        string connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";

        public Addcollege()
        {
            InitializeComponent();
        }



        private void Addcollege_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


         
            SqlConnection con;
            SqlDataReader dr;
     


            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM College;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                collegelistselect.Items.Add(dr["clg_name"]);
            }
            con.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

         
            SqlConnection con;
         
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("insert into College(clg_name) values('" + addclgtxt.Text +"')", con);

        cmd.CommandType = CommandType.Text;

        try

        {
            if (addclgtxt.Text == "")
            {
                MessageBox.Show("Please insert College Name!!!");
            }
            else
            {
                con.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("College Name Inserted successfully");

                this.Hide();
                Addcollege mf = new Addcollege();
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
        public void refress()
        {
             addclgtxt.Text = "";

        }
        public void validate()
        {
            
        }

       
     

        private void updateclg_Click(object sender, EventArgs e)
        {
             SqlConnection con;
         
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("update College set clg_name ='" +selectedclg.Text+ "' where clg_name = '"+slct+"' ;", con);

        cmd.CommandType = CommandType.Text;

        try

        {
            if (selectedclg.Text == "")
            {
                MessageBox.Show("Please Insert College Name!!!");
            }
            else
            {
                con.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("College Name Updated successfully");
                this.Hide();
                Addcollege mf = new Addcollege();
                mf.Show();
  
                con.Close();
                selectedclg.Text = " ";
               
            }
           

        }

        catch (Exception ex)

        {
            MessageBox.Show(ex.Message);

        }

    
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
        public String slct;
        private void collegelistselect_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            slct = collegelistselect.SelectedItem.ToString();

            selectedclg.Text = slct;
        }

        private void rmvclg_Click(object sender, EventArgs e)
        {
            SqlConnection con;

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("delete from College where clg_name = '" + slct + "' ;", con);

            cmd.CommandType = CommandType.Text;

            try
            {
                if (selectedclg.Text == "")
                {
                    MessageBox.Show("Please Insert College Name for Remove !!!");
                }
                else
                {
                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("College Name Removed successfully");
                    this.Hide();
                    Addcollege mf = new Addcollege();
                    mf.Show();

                    con.Close();
                    selectedclg.Text = " ";

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        }
    }

