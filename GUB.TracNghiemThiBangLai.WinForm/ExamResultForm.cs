using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using GUB.TracNghiemThiBangLai.Share.Model;
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
    public partial class ExamResultForm : Form
    {

        ResultExamRepository resultExamRepository;

        public ExamResultForm(List<Answer> listAnswer, int correctAnswer, int totalQuestion, User user, int idComputer)
        {
            InitializeComponent();
            resultExamRepository = new ResultExamRepository();
            halderResult(listAnswer, correctAnswer, totalQuestion, user, idComputer);
            handerUser(user);
        }

        public void handerUser(User user)
        {
            lblCCCD.Text = user.CCCD;
            lblHoVaTen.Text = user.FullName;
            lblDiaChi.Text = user.Address;
            lblNgaySinh.Text = user.Birthday.ToString("dd/MM/yyyy");
        }

        public void halderResult(List<Answer> listAnswer, int correctAnswer, int totalQuestion, User user, int idComputer)
        {
            var noAnswer = totalQuestion - listAnswer.Count;
            var wrongAnswer = totalQuestion - noAnswer - correctAnswer;

            lblSai.Text = wrongAnswer.ToString();
            lblDung.Text = correctAnswer.ToString();
            lblChua.Text = noAnswer.ToString();
            
            if(correctAnswer >= 21)
            {
                lblKetQua.Text = "Đạt";
            } else
            {
                lblKetQua.Text = "Không Đạt";
            }

            //Create resultExam
            ResultExam resultExam = new ResultExam() {
                CMND = user.CCCD,
                ComputerNum = idComputer,
                Status = lblKetQua.Text,
                NumberOfCorrect = correctAnswer
            };
            resultExamRepository.CreateResultExam(resultExam);

        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            
        }
    }
}
