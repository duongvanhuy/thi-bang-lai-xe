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

namespace GUB.TracNghiemThiBangLai.Host
{
    public partial class LoginForm : Form
    {
         AccountRepository accountRepository = new AccountRepository();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            var username = inpUserName.Text;
            var password = inpPassword.Text;

            var account =  new  Account() {
                UserName = username,
                Password = password
            };

           var Accountreponse = await accountRepository.Login(account);
            
            if(Accountreponse != null && Accountreponse.Id != 0)
            {
                var f = new HomeForm();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại");
            }
        }
    }
}
