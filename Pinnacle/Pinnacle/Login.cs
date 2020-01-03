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
    public partial class Login : Form
    {

     

        string connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";

        public string rollno;
        string appname,year;
         
        public Login()
        {
            InitializeComponent();
        }

        public string passingvalue
        {

            get { return appname; }
            set { appname = value; }
        }

        private void Login_Load(object sender, EventArgs e)
        {

            user.Checked = true;
            txtRollno.Enabled = true;
            catlist.Enabled = true;
            clglist.Enabled = true;
            txtPassword.Enabled = false;
            txtUsername.Enabled = true;

           year = DateTime.Now.Year.ToString();

           lblAppName.Text = appname + " " + year;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            SqlConnection con;
            SqlDataReader dr;

            //clg list

            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM College;", con);
          

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
            
                clglist.Items.Add(dr["clg_name"]);
            }
            con.Close();


           


            //catogory

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
        }

 
       

    
      

        public void refress()
        {
            txtUsername.Text = "";

        }
      

      

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void admin_CheckedChanged_1(object sender, EventArgs e)
        {
           
        }

        private void user_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void catlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (admin.Checked == true)
            {

                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please provide UserName and Password");
                    return;
                }
                try
                {
                    //Create SqlConnection
                    SqlConnection con = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("Select * from Admin where admin_name=@username and admin_pass=@password", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    con.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    con.Close();
                    int count = ds.Tables[0].Rows.Count;
                    //If count is equal to 1, than show frmMain form
                    if (count == 1)
                    {
                        this.Hide();
                        Adminpage fm = new Adminpage();
                        fm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            if (user.Checked == true)
            {
                //existing login validation

                loginvalidation();

                if (txtRollno.Text == rollno)
                {
                    MessageBox.Show("You have already Attempted Exam.");
                }
              
                else
                {
                    string clg = clglist.SelectedItem.ToString();
                    string cat = catlist.SelectedItem.ToString();


                    SqlConnection con;

                    con = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("insert into result (roll_no,username,clg_name,category,score,mob) values ('" + txtRollno.Text + "','" + txtUsername.Text + "','" + clg + "','" + cat + "','" + 0 + "','" + txtmob.Text + "')", con);

                    cmd.CommandType = CommandType.Text;

                    try
                    {
                        string mob = txtmob.Text ;
                        if (txtUsername.Text == "" || txtRollno.Text == "")
                        {
                            MessageBox.Show("Please insert fields!!!");
                        }

                        else if (mob.Length != 10)
                        {
                            MessageBox.Show("Please insert Proper Mobile 10 No.only!!!");
                        }
                      
                        else
                        {
                            con.Open();

                            cmd.ExecuteNonQuery();


                            this.Hide();
                            Instruction fm = new Instruction();
                            fm.passingvalue = txtUsername.Text;
                            fm.passingvalue1 = txtRollno.Text;
                            fm.Show();

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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void admin_CheckedChanged(object sender, EventArgs e)
        {
            txtRollno.Enabled = false;
            catlist.Enabled = false;
            clglist.Enabled = false;
            txtPassword.Enabled = true;
            txtmob.Enabled = false;
            txtUsername.Enabled = true;
            user.Font = new Font(user.Font, FontStyle.Regular);
            admin.Font = new Font(admin.Font, FontStyle.Bold);
        }

        private void user_CheckedChanged(object sender, EventArgs e)
        {
            txtRollno.Enabled = true;
            catlist.Enabled = true;
            clglist.Enabled = true;
            txtmob.Enabled = true;
            txtPassword.Enabled = false;
            txtUsername.Enabled = true;
            user.Font = new Font(user.Font, FontStyle.Bold);
            admin.Font = new Font(admin.Font, FontStyle.Regular);

        }



        void loginvalidation()
        {
            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM result where roll_no ='"+txtRollno.Text+"';", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                rollno = dr["roll_no"].ToString();
            }
            con.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
    }
}
