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
        public ExamResultForm(List<Answer> listAnswer, int correctAnswer)
        {
            InitializeComponent();
            halderResult(listAnswer, correctAnswer);
        }

        public void halderResult(List<Answer> listAnswer, int correctAnswer)
        {
            var noAnswer = 25 - listAnswer.Count;
            var wrongAnswer = 25 - noAnswer - correctAnswer;

            lblSai.Text = wrongAnswer.ToString();
            lblDung.Text = correctAnswer.ToString();
            lblChua.Text = noAnswer.ToString();

            if(correctAnswer >= 22)
            {
                lblKetQua.Text = "Đạt";
            } else
            {
                lblKetQua.Text = "Không Đạt";
            }
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            
        }
    }
}
