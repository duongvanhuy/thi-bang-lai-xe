using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using System;
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
        QuestionRepository questionRepository = new QuestionRepository();
        List<Question> questionList = new List<Question>();
        public ExamForm()
        {
            InitializeComponent();
            flpDapAn.Controls.Add(createPanelInFlowPanel());
        }

        private FlowLayoutPanel createPanelInFlowPanel()
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            Label label = new Label();
            label.AutoSize = true;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            //label.Location = new System.Drawing.Point(3, 0);
            label.Name = "label7";
            label.Size = new System.Drawing.Size(25, 46);
            label.TabIndex = 0;
            label.Text = "10"; //Số câu hỏi
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
                //radioButton.Location = new System.Drawing.Point(34, 3);
                radioButton.Name = "radioButton" + i.ToString();
                radioButton.Size = new System.Drawing.Size(21, 40);
                radioButton.TabIndex = i;
                radioButton.TabStop = true;
                radioButton.Text = i.ToString();
                radioButton.UseVisualStyleBackColor = true;

                flowLayoutPanel.Controls.Add(radioButton);

            }
            flowLayoutPanel.Location = new System.Drawing.Point(149, 3);
            flowLayoutPanel.Name = "flowLayoutPanel1"; //Đặt tên theo số câu hỏi
            flowLayoutPanel.Size = new System.Drawing.Size(340, 300);
            return flowLayoutPanel;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
