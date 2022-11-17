using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
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
        Task<List<Question>> questionList;
        
        public ExamForm()
        {
            InitializeComponent();
            questionRepository = new QuestionRepository();
            Initial();
            
        }

        public async void Initial()
        {
            var listQuestion = await questionRepository.GetQuestions();
            addListFlpDapAn(listQuestion);
            Question question = await questionRepository.GetByQuestion(2);
            renderQuestion(question);

        }

        //Render 1 list flow đáp án
        public void addListFlpDapAn(List<Question> listQuestion  )
        {
            foreach (Question question in listQuestion)
            {
                flpDapAn.Controls.Add(createPanelInFlowPanel(question));
            }
        }
        

        //Tạo ra một flow đáp án
        private FlowLayoutPanel createPanelInFlowPanel(Question question)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            Label label = new Label();
            label.AutoSize = true;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Name = "label" + question.Id.ToString();
            label.Size = new System.Drawing.Size(25, 46);
            label.Tag = question.Id;
            label.Text = question.Id.ToString(); //Số câu hỏi
            label.Click += new EventHandler(labelQuestion_Clicked);
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            flowLayoutPanel.Controls.Add(label);
            //for (var i = 1; i <= 4; i ++)
            //{
                
            //}
            for (int i = 1; i <= 4; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.AutoSize = true;
                radioButton.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
                radioButton.Name = "radioButton" + i.ToString();
                radioButton.Size = new System.Drawing.Size(21, 40);
                radioButton.TabIndex = i;
                radioButton.TabStop = true;
                radioButton.Text = i.ToString();
                radioButton.Click += new EventHandler(radioButton_CheckedChanged);
                radioButton.UseVisualStyleBackColor = true;

                flowLayoutPanel.Controls.Add(radioButton);

            }
            flowLayoutPanel.Name = "flowLayoutPanel" + question.Id.ToString();//Đặt tên theo số câu hỏi
            flowLayoutPanel.Tag = question.Id;  
            flowLayoutPanel.Size = new System.Drawing.Size(144, 60);
            flowLayoutPanel.Click += new EventHandler(flowLayoutPanel_Clicked);
            return flowLayoutPanel;
        }


        //Đỗ dữ liệu câu hỏi
        private void renderQuestion(Question question)
        {
            lblCauHoi.Text= question.Content;
            //ptbHinhAnhCauHoi.Image = LoadImage(question.Image);
            
            lblCauTraLoi.Text= string.Format("1. {0} \n2. {1} \n3. {2} \n4. {3}", question.AnswerA, question.AnswerB, question.AnswerC, question.AnswerD);
        }

        //Convert Base64 to Image 
        public Image LoadImage(string base64)
        {
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(base64);

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
                radioButton.Parent.BackColor = Color.Yellow;
            }
        }
    }
}
