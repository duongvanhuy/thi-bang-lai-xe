using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using GUB.TracNghiemThiBangLai.Share.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUB.TracNghiemThiBangLai.WinForm
{
    public partial class ExamForm : Form
    {
        QuestionRepository questionRepository;
        ComputerRepository computerRepository;
        UserRepository userRepository;
        List<Question> questionList;
        Computer computer;
        User user;
        int count;
        int idComputer = 202;


        public ExamForm()
        {
            InitializeComponent();
            questionRepository = new QuestionRepository();
            computerRepository = new ComputerRepository();
            userRepository = new UserRepository();
            Initial();
            
        }

        public async void Initial()
        {
            
            questionList = await questionRepository.GetQuestions() ;
            addListFlpDapAn(questionList);

            //Hiển thị câu hỏi đầu tiên của list
            Question question = await questionRepository.GetByQuestion(questionList[0].Id);
            renderQuestion(question);

            //Lấy ra User từ mã máy tính
            computer = await computerRepository.GetComputerByNumber(idComputer);
            user = await userRepository.GetUserByCCCD(computer.CCCD);

            renderUser();

            //Chạy thời gian
            runTime();

        }

        public void renderUser()
        {
            lblHoVaTen.Text = user.FullName;
            lblCCCD.Text = user.CCCD;
            lblNgaySinh.Text = user.Birthday.ToString("dd/MM/yyyy");
            lblDiaChi.Text = user.Address;
        }

        

        //Render 1 list flow đáp án
        public void addListFlpDapAn(List<Question> listQuestion  )
        {
            //foreach (Question question in listQuestion)
            //{
            //    flpDapAn.Controls.Add(createPanelInFlowPanel(question));
            //}
            for(var i = 0; i < listQuestion.Count; i ++)
            {
                flpDapAn.Controls.Add(createPanelInFlowPanel(listQuestion[i], i + 1)); // Số thứ tự câu hỏi
            }
        }
        

        //Tạo ra một flow đáp án
        private FlowLayoutPanel createPanelInFlowPanel(Question question, int sttQuestion)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            Label label = new Label();
            label.AutoSize = true;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Name = "label" + sttQuestion;
            label.Size = new System.Drawing.Size(25, 46);
            label.Tag = question.Id;
            label.Text = sttQuestion.ToString(); //Số câu hỏi
            label.Click += new EventHandler(labelQuestion_Clicked);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            flowLayoutPanel.Controls.Add(label);
            //for (var i = 1; i <= 4; i ++)
            //{
                
            //}
            for (int i = 1; i <= 4; i++)
            {
                var answer = "";
                if(i == 1)
                {
                    answer = "A";
                } else if(i == 2)
                {
                   answer = "B";
                } else if(i == 3)
                {
                    answer = "C";
                } else if(i == 4)
                {
                    answer = "D";
                }
                RadioButton radioButton = new RadioButton();
                radioButton.AutoSize = true;
                radioButton.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
                radioButton.Name = "radioButton" + i.ToString();
                radioButton.Size = new System.Drawing.Size(21, 40);
                radioButton.TabIndex = i;
                radioButton.TabStop = true;
                radioButton.Text = answer;
                radioButton.Tag = question.Id;
                radioButton.Click += new EventHandler(radioButton_CheckedChanged);
                radioButton.UseVisualStyleBackColor = true;

                flowLayoutPanel.Controls.Add(radioButton);

            }
            flowLayoutPanel.Name = "flowLayoutPanel" + question.Id.ToString();//Đặt tên theo số câu hỏi
            flowLayoutPanel.Tag = question.Id;  
            flowLayoutPanel.Size = new System.Drawing.Size(150, 60);
            flowLayoutPanel.Click += new EventHandler(flowLayoutPanel_Clicked);
            return flowLayoutPanel;
        }


        //Đỗ dữ liệu câu hỏi
        private void renderQuestion(Question question)
        {
            lblCauHoi.Text= question.Content;

            if(question.Image.Length >0 )
            {
                ptbHinhAnhCauHoi.Image = LoadImage(question.Image);
                lblCauTraLoi.Dock = DockStyle.Bottom;
            }
            else
            {
                ptbHinhAnhCauHoi.Image = null;
                lblCauTraLoi.Dock = DockStyle.Fill;
            }
            
            if (question.AnswerD.Length == 0)
            {
                lblCauTraLoi.Text = string.Format("A. {0} \nB. {1} \nC. {2} ", question.AnswerA, question.AnswerB, question.AnswerC);
            }
            else
            {
                lblCauTraLoi.Text= string.Format("A. {0} \nB. {1} \nC. {2} \nD. {3}", question.AnswerA, question.AnswerB, question.AnswerC, question.AnswerD);
            }
            
            
        }

        //Convert Base64 to Image 
        public Image LoadImage(string base64)
        {
            
            var stringBase64 = base64.Split("data:image/jpeg;base64,")[1];
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(stringBase64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void flpDapAn_Paint(object sender, PaintEventArgs e)
        {

        }

        //Sự kiện click chuyển câu hỏi cho flowlayoutpanel
        private async void flowLayoutPanel_Clicked(object sender, EventArgs e)
        {
            var flowQuestion = (FlowLayoutPanel)sender;
            var idQuestion = Int32.Parse(flowQuestion.Tag.ToString());
            Question question = await questionRepository.GetByQuestion(idQuestion);
            renderQuestion(question);
        }

        //Sự kiện click chuyển câu hỏi cho label 
        private async void labelQuestion_Clicked(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var idQuestion = Int32.Parse(label.Tag.ToString());
            Question question = await questionRepository.GetByQuestion(idQuestion);
            renderQuestion(question);
        }

        //Sự kiện click chọn đáp án rồi thì flow đổi màu
        private async void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton)sender;
            if(radioButton.Checked)
            {
                Question question = await questionRepository.GetByQuestion(Convert.ToInt32( radioButton.Tag));
                renderQuestion(question);
                radioButton.Parent.BackColor = Color.Yellow;
            }
        }

        private async void btnKetThuc_Click(object sender, EventArgs e)
        {
            List<Answer> listAnswer = new List<Answer>();
            var confirm = new DialogResult();
            if (count != 0) // Thời gian hết thì phải cần show confirm
            {
                confirm = MessageBox.Show("Bạn có chắc là muốn kết thúc không?", "Chú ý",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            } else // Nếu thời gian đã hết thì confirm mặc định là OK 
            {
                confirm = DialogResult.OK;
            }
            
            if (confirm == DialogResult.OK)
            {
                for (var i = 0; i < flpDapAn.Controls.Count; i++)
                {
                    var flow = flpDapAn.Controls[i] as FlowLayoutPanel;
                    for (var j = 1; j < flow.Controls.Count; j++)
                    {
                        var radioButton = flow.Controls[j] as RadioButton;
                        if (radioButton.Checked)
                        {
                            listAnswer.Add(new Answer()
                            {
                                Id = i + 1,
                                UserAnswer = radioButton.Text
                            });
                        }
                    }
                }
                if (count != 0 && listAnswer.Count < questionList.Count) //Làm chưa đủ 25 câu
                {
                    MessageBox.Show("Bạn chưa hoàn thành tất cả các câu hỏi");
                }
                else // Làm đủ 25 câu và đã hết thời gian
                {
                    timer1.Stop(); // Nộp bài rồi thì dừng thời gian
                    var correctAnswer = 0;
                    foreach (var answer in listAnswer)
                    {
                        //var listQuestion = await questionRepository.GetQuestions();
                        Question question = questionList[answer.Id - 1];
                        if (question.CorrectAnswer.Equals(answer.UserAnswer))
                        {
                            correctAnswer++;
                        }
                    }
                    var totalQuestion = questionList.Count; // 
                    var examResultForm = new ExamResultForm(listAnswer, correctAnswer, totalQuestion, user, idComputer);
                    if (examResultForm.ShowDialog() == DialogResult.Cancel)
                    {
                        examResultForm.Close();
                        this.Close();
                    }
                }
            }
        }

        // Hàm khởi chạy timer
        public void runTime()
        {
            count = 20; //Set up thời gian chạy
            timer1.Start();
        }

        public string convert(int count)
        {
            int m = count / 60;
            int s = count % 60;

            return string.Format($"{m:0#}:{s:0#}");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count--;
            if (count == 0) // Hết giờ thì làm gì
            {
                timer1.Stop();
                MessageBox.Show("Thời gian làm bài đã hết!");
                btnKetThuc_Click(sender, e);
            } else if (count == 10) // Gần hết giờ thì làm gì
            {
                lblTimer.ForeColor = Color.Red;
            }
            lblTimer.Text = convert(count);
        }
    }
}
