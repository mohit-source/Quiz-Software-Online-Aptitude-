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
    public partial class Exam : Form
    {

       public  int check = 0;

        Timer timer = new Timer();
       public int times;
       public int marks;
       public int m;
       public int negMarks;
       public int totalQue;
       int pos = 0;
       int score = 0;
       public string answer;
       public string rollno;
       public string ques;

        SqlConnection con;
        SqlDataAdapter adapter;
        DataSet ds;
        DataTable table = new DataTable();
     

        string connetionString = @"Data Source=DESKTOP-13LKFD2\SQLEXPRESS;Initial Catalog=Pinnacle;Integrated Security=True";

        public Exam()
        {
            InitializeComponent();
          //  txtTime.Text = TimeSpan.FromMinutes(30).ToString();
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

        

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public int countrow;
        public int NotAttmpt = 0;
        public int Attmpt = 0;
     
        private void Exam_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


                txtTime.BackColor = Color.LimeGreen;

                var startTime = DateTime.Now;
                timer = new System.Windows.Forms.Timer() { Interval = 1000 };
                timer.Tick += (obj, args) =>
                {
                    TimeSpan ts = TimeSpan.FromMinutes(times) - (DateTime.Now - startTime);
                    txtTime.Text = ts.ToString("hh\\:mm\\:ss");
                    if (ts.Hours ==  0 &  (ts.Minutes == 0 & ts.Seconds == 0))
                    {
                        timer.Stop();
                        MessageBox.Show("Exam is Over!!!");
                        this.Hide();
                        Finish fm = new Finish();
                        fm.Show();
                    }
                    if (ts.Hours == 0 & ( ts.Minutes <= 10 & ts.Seconds == 0))
                    {
                        txtTime.BackColor = Color.Red;
                    }
                };
                timer.Start();




            //databse

            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM settings;", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               
                times = Int32.Parse(dr["exam_time"].ToString());
                marks = Int32.Parse(dr["carry_mark"].ToString());
                m = marks;
                negMarks = Int32.Parse(dr["neg_mark"].ToString());
                totalQue = Int32.Parse(dr["total_que"].ToString());
            }
            con.Close();
  
            user.Text = txtUsername;

            con = new SqlConnection(connetionString);


            adapter = new SqlDataAdapter("SELECT * FROM question where Qno <='" + totalQue + "' order by NEWID();", con);
            adapter.Fill(table);
            showData(pos);

            NotAttmpt = table.Rows.Count;
            lblNotAttmpt.Text = NotAttmpt.ToString();

            lblAttmpt.Text = Attmpt.ToString();

        }

        public string quescheck;
         
        public void showData(int index)
        {
            int no = index + 1;
         

            
            QueNo.Text = no.ToString();
            txtQue.Text = table.Rows[index][1].ToString();
            quescheck = txtQue.Text;
            rOption1.Text = table.Rows[index][2].ToString();
            rOption2.Text = table.Rows[index][3].ToString();
            rOption3.Text = table.Rows[index][4].ToString();
            rOption4.Text = table.Rows[index][5].ToString();
           
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            
            pos = 0;
            showData(pos);
            fetchQuestionandAnser();
            if (quescheck == ques & txtRollNO == rollno)
            {
             
                rOption1.Checked = false;
                rOption2.Checked = false;
                rOption3.Checked = false;
                rOption4.Checked = false;

                rOption1.Enabled = false;
                rOption2.Enabled = false;
                rOption3.Enabled = false;
                rOption4.Enabled = false;

                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnSubmit.Enabled = false;
                btnIgnore.Enabled = false;

            }
            else
            {
              
                rOption1.Checked = false;
                rOption2.Checked = false;
                rOption3.Checked = false;
                rOption4.Checked = false;

                rOption1.Enabled = true;
                rOption2.Enabled = true;
                rOption3.Enabled = true;
                rOption4.Enabled = true;

                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnSubmit.Enabled = true;
                btnIgnore.Enabled = true;

            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            pos++;
          
            if (pos < table.Rows.Count)
            {
                showData(pos);
                fetchQuestionandAnser();

                if (quescheck == ques & txtRollNO == rollno)
                {
                   
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
                else
                {
                  
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }

            }
               else if (pos >= table.Rows.Count)
               {
                   pos = 0;
                    showData(pos);
                    fetchQuestionandAnser();

                   if (quescheck == ques & txtRollNO == rollno)
                   {
                       showData(pos);
                       rOption1.Checked = false;
                       rOption2.Checked = false;
                       rOption3.Checked = false;
                       rOption4.Checked = false;

                       rOption1.Enabled = false;
                       rOption2.Enabled = false;
                       rOption3.Enabled = false;
                       rOption4.Enabled = false;

                       btnFirst.Enabled = true;
                       btnPrev.Enabled = true;
                       btnNext.Enabled = true;
                       btnLast.Enabled = true;
          
                         btnSubmit.Enabled = false;
                       btnIgnore.Enabled = false;
                   }
                   else
                   {
                      
                       rOption1.Checked = false;
                       rOption2.Checked = false;
                       rOption3.Checked = false;
                       rOption4.Checked = false;

                       rOption1.Enabled = true;
                       rOption2.Enabled = true;
                       rOption3.Enabled = true;
                       rOption4.Enabled = true;

                       btnFirst.Enabled = true;
                       btnPrev.Enabled = true;
                       btnNext.Enabled = true;
                       btnLast.Enabled = true;
                
                          btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                   }

               } 
            else
            {

                pos = table.Rows.Count - 1;
                showData(pos);
                fetchQuestionandAnser();
                if (quescheck == ques & txtRollNO == rollno)
                {
                   
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;


                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
                else
                {
                   
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;


                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;
                }

            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
           
            pos--;
            
            if (pos >= 0)
            {
                showData(pos);
                fetchQuestionandAnser();

                if (quescheck == ques & txtRollNO == rollno)
                {
                   
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
               
                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }


            }
            else if (pos < 0)
            {
                pos = table.Rows.Count - 1;
                showData(pos);
                fetchQuestionandAnser();

                if (quescheck == ques & txtRollNO == rollno)
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }

                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }
            }
            else
            {
                pos = 0;
                showData(pos);
                fetchQuestionandAnser();
                if (quescheck == ques & txtRollNO == rollno)
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            pos = table.Rows.Count - 1;
            showData(pos);
            fetchQuestionandAnser();
            if (quescheck == ques & txtRollNO == rollno)
            {
              
               
                rOption1.Checked = false;
                rOption2.Checked = false;
                rOption3.Checked = false;
                rOption4.Checked = false;

                rOption1.Enabled = false;
                rOption2.Enabled = false;
                rOption3.Enabled = false;
                rOption4.Enabled = false;

                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnSubmit.Enabled = false;
                btnIgnore.Enabled = false;
            }
            else
            {
               
               

                rOption1.Checked = false;
                rOption2.Checked = false;
                rOption3.Checked = false;
                rOption4.Checked = false;

                rOption1.Enabled = true;
                rOption2.Enabled = true;
                rOption3.Enabled = true;
                rOption4.Enabled = true;

                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnSubmit.Enabled = true;
                btnIgnore.Enabled = true;
            }

        }

        private void rOption1_CheckedChanged(object sender, EventArgs e)
        {
            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }

        private void rOption2_CheckedChanged(object sender, EventArgs e)
        {
            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }

        private void rOption3_CheckedChanged(object sender, EventArgs e)
        {
            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }

        private void rOption4_CheckedChanged(object sender, EventArgs e)
        {
             btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {


            pos++;

            if (pos < table.Rows.Count)
            {
                showData(pos);
                fetchQuestionandAnser();

                if (quescheck == ques & txtRollNO == rollno)
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }

            }
            else if (pos >= table.Rows.Count)
            {
                pos = 0;
                showData(pos);
                fetchQuestionandAnser();

                if (quescheck == ques & txtRollNO == rollno)
                {
                    showData(pos);
                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;
                }
                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;

                }

            }
            else
            {

                pos = table.Rows.Count - 1;
                showData(pos);
                fetchQuestionandAnser();
                if (quescheck == ques & txtRollNO == rollno)
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = false;
                    rOption2.Enabled = false;
                    rOption3.Enabled = false;
                    rOption4.Enabled = false;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;


                    btnSubmit.Enabled = false;
                    btnIgnore.Enabled = false;

                }
                else
                {

                    rOption1.Checked = false;
                    rOption2.Checked = false;
                    rOption3.Checked = false;
                    rOption4.Checked = false;

                    rOption1.Enabled = true;
                    rOption2.Enabled = true;
                    rOption3.Enabled = true;
                    rOption4.Enabled = true;

                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;


                    btnSubmit.Enabled = true;
                    btnIgnore.Enabled = true;
                }

            }


        }
        public string que;
        public string option1;
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            

                 if (rOption1.Checked == true)
                 {

                      option1 = rOption1.Text;

                      checkCorrectAnswer();

                 }






                 if (rOption2.Checked == true)
                 {

                     option1 = rOption2.Text;
                     checkCorrectAnswer();

                 }



                 if (rOption3.Checked == true)
                 {

                     option1 = rOption3.Text;
                     checkCorrectAnswer();

                 }





                 if (rOption4.Checked == true)
                 {

                     option1 = rOption4.Text;

                     checkCorrectAnswer();


                 }








          




            //line






            }

        private void btnFinish_Click(object sender, EventArgs e)
        {

            string message = "Do You Want to Finish your Test?";
            string title = "Finish Exam";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                timer.Stop();
                this.Close();
                Finish fm = new Finish();
                fm.Show();
            }
            else
            {
                // Do something  
            }  


          /*  DoYouWantFinish fm = new DoYouWantFinish();
            fm.Show(); */
        }


        public void checkCorrectAnswer()
        { 
        
            //start


            Attmpt = Attmpt + 1;
            lblAttmpt.Text = Attmpt.ToString();

            NotAttmpt = NotAttmpt - 1;
            lblNotAttmpt.Text = NotAttmpt.ToString();

            que = txtQue.Text;
            if (option1 == "")
            {
                MessageBox.Show("Please skip");
                return;
            }

            try
            {
                //Create SqlConnection
                SqlConnection con;
                SqlDataReader dr;



                con = new SqlConnection(connetionString);
                SqlCommand cmd = new SqlCommand("SELECT * FROM question where Question = '" + que + "' and Answer = '" + option1 + "';", con);

                cmd.CommandType = CommandType.Text;
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    answer = dr["Answer"].ToString();

                }
                con.Close();


                if (answer == option1)
                {
                    score = score + marks;
                    m = marks;
                    QuestionAnserResult();

                    SqlConnection con1;

                    con1 = new SqlConnection(connetionString);
                    SqlCommand cmd2 = new SqlCommand("update result set score ='" + score + "' where roll_No = '" + txtRollNO + "' ;", con1);

                    cmd2.CommandType = CommandType.Text;

                    try
                    {

                        con1.Open();

                        cmd2.ExecuteNonQuery();

                        con1.Close();



                        pos++;
                        
                        if (pos < table.Rows.Count)
                        {

                            showData(pos);
                            fetchQuestionandAnser();

                            if (quescheck == ques & txtRollNO == rollno)
                            {
                               
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = false;
                                rOption2.Enabled = false;
                                rOption3.Enabled = false;
                                rOption4.Enabled = false;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = false;
                                btnIgnore.Enabled = false;

                            }
                            else
                            {
                              
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = true;
                                rOption2.Enabled = true;
                                rOption3.Enabled = true;
                                rOption4.Enabled = true;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = true;
                                btnIgnore.Enabled = true;

                            }
                        }
                        else if (pos >= table.Rows.Count)
                        {
                            pos = 0;
                             showData(pos);
                            fetchQuestionandAnser();
                           if (quescheck == ques & txtRollNO == rollno)
                            {
                               
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = false;
                                rOption2.Enabled = false;
                                rOption3.Enabled = false;
                                rOption4.Enabled = false;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = false;
                                btnIgnore.Enabled = false;

                            }
                            else
                            {
                             
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = true;
                                rOption2.Enabled = true;
                                rOption3.Enabled = true;
                                rOption4.Enabled = true;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = true;
                                btnIgnore.Enabled = true;

                            }
                        } 
                        else
                        {

                            pos = table.Rows.Count - 1;
                            showData(pos);
                            fetchQuestionandAnser();
                            if (quescheck == ques & txtRollNO == rollno)
                            {
                               
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = false;
                                rOption2.Enabled = false;
                                rOption3.Enabled = false;
                                rOption4.Enabled = false;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = false;
                                btnIgnore.Enabled = false;

                            }
                            else
                            {
                                
                                rOption1.Checked = false;
                                rOption2.Checked = false;
                                rOption3.Checked = false;
                                rOption4.Checked = false;

                                rOption1.Enabled = true;
                                rOption2.Enabled = true;
                                rOption3.Enabled = true;
                                rOption4.Enabled = true;

                                btnFirst.Enabled = true;
                                btnPrev.Enabled = true;
                                btnNext.Enabled = true;
                                btnLast.Enabled = true;

                                btnSubmit.Enabled = true;
                                btnIgnore.Enabled = true;
                            }
                        }


                    }




                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }





                }
                else
                {
                    score = score + 0;
                    m = 0;
                    QuestionAnserResult();
                    pos++;

                    if (pos < table.Rows.Count)
                    {
                        showData(pos);
                        fetchQuestionandAnser();

                        if (quescheck == ques & txtRollNO == rollno)
                        {
                           
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = false;
                            rOption2.Enabled = false;
                            rOption3.Enabled = false;
                            rOption4.Enabled = false;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = false;
                            btnIgnore.Enabled = false;
                        }
                        else
                        {
                            
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = true;
                            rOption2.Enabled = true;
                            rOption3.Enabled = true;
                            rOption4.Enabled = true;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = true;
                            btnIgnore.Enabled = true;

                        }
                    }
              /*      else if (pos >= table.Rows.Count)
                    {
                        pos = 0;
                        fetchQuestionandAnser();
                        if (ques == txtQue.Text & rollno == txtRollNO)
                        {
                            showData(pos);
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = false;
                            rOption2.Enabled = false;
                            rOption3.Enabled = false;
                            rOption4.Enabled = false;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = false;
                            btnIgnore.Enabled = false;
                        }
                        else
                        {
                            showData(pos);
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = true;
                            rOption2.Enabled = true;
                            rOption3.Enabled = true;
                            rOption4.Enabled = true;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = true;
                            btnIgnore.Enabled = true;
                        }
                    }   */
                    else
                    {

                        pos = table.Rows.Count - 1;
                        showData(pos);
                        fetchQuestionandAnser();
                        if (quescheck == ques & txtRollNO == rollno)
                        {
                           
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = false;
                            rOption2.Enabled = false;
                            rOption3.Enabled = false;
                            rOption4.Enabled = false;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = false ;
                            btnIgnore.Enabled = false;

                        }
                        else
                        {
                         
                            rOption1.Checked = false;
                            rOption2.Checked = false;
                            rOption3.Checked = false;
                            rOption4.Checked = false;

                            rOption1.Enabled = true;
                            rOption2.Enabled = true;
                            rOption3.Enabled = true;
                            rOption4.Enabled = true;

                            btnFirst.Enabled = true;
                            btnPrev.Enabled = true;
                            btnNext.Enabled = true;
                            btnLast.Enabled = true;

                            btnSubmit.Enabled = true;
                            btnIgnore.Enabled = true;

                        }
                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //end

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        void QuestionAnserResult()
        {



            SqlConnection con;
        
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("insert into QAResult(roll_no,question,answer,marks) values('" + txtRollNO + "','" + que + "','" + option1 + "','" + m + "')", con);

            cmd.CommandType = CommandType.Text;

            try
            {
               
                
                    con.Open();

                    cmd.ExecuteNonQuery();
                    con.Close();

             

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


         



        }
        void fetchQuestionandAnser()
        {
            SqlConnection con;
            SqlDataReader dr;



            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM QAResult where question = '" + quescheck + "' and roll_no = '" + txtRollNO + "';", con);

            cmd.CommandType = CommandType.Text;
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                answer = dr["answer"].ToString();
                rollno = dr["roll_no"].ToString();
                ques = dr["question"].ToString();

            }
            con.Close();
        }

     }

   
       
    
}
