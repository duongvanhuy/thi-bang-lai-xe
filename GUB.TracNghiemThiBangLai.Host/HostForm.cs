using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User = GUB.TracNghiemThiBangLai.Entities.User;

namespace GUB.TracNghiemThiBangLai.Host
{
    public partial class HostForm : Form
    {
        UserRepository userRepository ;
        Task<List<User>> users ;
        public HostForm()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            Initial();
          
        }

        public async void Initial()
        {
           
           var otherUsers = await userRepository.GetUsers();
            renderDataUser(otherUsers);

        }
        private void renderDataUser(List<User> users)
        {
           userBindingSource.DataSource = users;
            dgvUser.DataSource = users;
            txtCountUser.Text = users.Count.ToString();
        }

     
        private async void txtSearchName(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
               var userSearchs = await userRepository.SearchUser(txtCountUser.Text);
                renderDataUser(userSearchs);
                txtCountUser.Text = userSearchs.Count.ToString();
            }
        }
    }
}
