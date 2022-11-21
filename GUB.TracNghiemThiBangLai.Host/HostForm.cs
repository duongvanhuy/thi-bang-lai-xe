using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using User = GUB.TracNghiemThiBangLai.Entities.User;

namespace GUB.TracNghiemThiBangLai.Host
{
    public partial class HostForm : Form
    {
        UserRepository userRepository;
        //Task<List<User>> users ;

        public HostForm()
        {
            InitializeComponent();
            userRepository = new UserRepository();
            Initial();

        }

        public async void Initial()
        {

            var listUsers = await userRepository.GetUsers();
            renderDataUser(listUsers);

        }
        private void renderDataUser(List<User> listUsers)
        {
            userBindingSource1.DataSource = listUsers;
            dgvUser.DataSource = userBindingSource1;
            txtCountUser.Text = listUsers == null ? "0" : listUsers.Count.ToString();
            renderUserInfo();
        }

<<<<<<< HEAD

=======
     //search
>>>>>>> e0544621da671134faa06fd4b5fbfac65569f8cd
        private async void txtSearchName(object sender, KeyPressEventArgs e)
        {
          
            if (e.KeyChar == 13)
            {
<<<<<<< HEAD

                var userSearchs = await userRepository.SearchUser(txtCountUser.Text);
                renderDataUser(userSearchs);
                txtCountUser.Text = userSearchs == null ? "0" : userSearchs.Count.ToString();
=======
               
                var userSearchs = await userRepository.SearchUser(txtSearch.Text);
                if (userSearchs != null)
                {
                    renderDataUser(userSearchs);
                }
               
                txtCountUser.Text = userSearchs == null? "0" : userSearchs.Count.ToString();
>>>>>>> e0544621da671134faa06fd4b5fbfac65569f8cd
            }
        }

        //render User info
        private void renderUserInfo()
        {
            var userRow = dgvUser.SelectedRows[0].DataBoundItem as User;
            txtName.Text = userRow.FullName;
            txtCCCD.Text = userRow.CCCD;
            txtAddress.Text = userRow.Address;
            txtPhone.Text = userRow.Phone;
            dtpBirthday.Value = userRow.Birthday;
        }





        //render data when click
        private void cellClick(object sender, DataGridViewCellEventArgs e)
        {
            renderUserInfo();
            clearMessageError();
        }

        //Xóa user
        //private async void button2_Click(object sender, EventArgs e)
        //{
        //    if (dgvUser.SelectedRows.Count > 0)
        //    {
        //        var confirm = MessageBox.Show("Bạn có chắc là muốn xoá user này không?", "Chú ý",
        //            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (confirm == DialogResult.OK)
        //        {
        //            var userRow = dgvUser.SelectedRows[0].DataBoundItem as User;
        //            if (userRow != null)
        //            {
        //                    //User userDeleted = new User();
        //                User userDeleted = await userRepository.DeleteUser(userRow.Id);


        //                //Xóa thành công
        //                if (userDeleted != null)
        //                {
        //                    MessageBox.Show("Xoá user thành công", "Thông báo",
        //                     MessageBoxButtons.OK);
        //                    Initial();
        //                }
        //                else
        //                    MessageBox.Show("Xóa không thành công", "Thông báo",
        //                         MessageBoxButtons.OK, MessageBoxIcon.Error);



        //            }
        //            else
        //            {
        //                MessageBox.Show("Vui lòng chọn đúng dữ liệu", "Thông báo",
        //                            MessageBoxButtons.OK);
        //            }
        //        }

        //    }
        //}

        //click add user
        private void button1_Click(object sender, EventArgs e)
        {
            dgvUser.ClearSelection();
            txtName.Text = "";
            txtName.Focus();
            txtCCCD.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            clearMessageError();

        }

        //reset mk
        //private async void button3_Click(object sender, EventArgs e)
        //{
        //    if (dgvUser.SelectedRows.Count > 0)
        //    {
        //        var confirm = MessageBox.Show("Bạn có chắc là muốn reset mật khẩu user này không?", "Chú ý",
        //            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (confirm == DialogResult.OK)
        //        {
        //            var userRow = dgvUser.SelectedRows[0].DataBoundItem as User;
        //            if (userRow != null)
        //            {

        //                var userReset = await userRepository.ResetPassword(userRow.Id);
        //                //Reset thành công
        //                if (userReset != null)
        //                {
        //                    MessageBox.Show("reser mật khẩu thành công", "Thông báo",
        //                     MessageBoxButtons.OK);
        //                    Initial();
        //                }
        //                else {
        //                    MessageBox.Show("Reset mật khẩu không thành công", "Thông báo",
        //                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }




        //            }
        //            else
        //            {
        //                MessageBox.Show("Vui lòng chọn đúng dữ liệu", "Thông báo",
        //                            MessageBoxButtons.OK);
        //            }
        //        }

        //    }
        //}

        //save
        private async void button4_Click(object sender, EventArgs e)
        {
            //edit user
            if (dgvUser.SelectedRows.Count > 0)
            {

                var confirm = MessageBox.Show("Bạn có chắc là muốn sửa thông tin user này không?", "Chú ý",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirm == DialogResult.OK)
                {
                    var userRow = dgvUser.SelectedRows[0].DataBoundItem as User;
                    if (userRow != null)
                    {

                        if (validateForm())
                        {
                            userRow.FullName = txtName.Text;
                            userRow.CCCD = txtCCCD.Text;
                            userRow.Phone = txtPhone.Text;
                            userRow.Address = txtAddress.Text;
                            userRow.Birthday = dtpBirthday.Value;

                            var userUpdate = await userRepository.UpdateUser(userRow);
                            //update thành công
                            if (userUpdate != null && userUpdate.Id != null)
                            {
                                MessageBox.Show("update user thành công", "Thông báo",
                                 MessageBoxButtons.OK);
                                Initial();
                            }
                            else
                            {
                                MessageBox.Show("Sửa thông tin không thành công", "Thông báo",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn đúng dữ liệu", "Thông báo",
                                    MessageBoxButtons.OK);
                    }
                }

                //thêm mới user
            }
            else
            {
                if (validateForm())
                {
                    User newUser = new User
                    {
                        FullName = txtName.Text,
                        CCCD = txtCCCD.Text,
                        Phone = txtPhone.Text,
                        Address = txtAddress.Text,
                        Birthday = dtpBirthday.Value
                    };

                    var userAdd = await userRepository.AddUser(newUser);
                    if (userAdd != null)
                    {
                        MessageBox.Show("Thêm mới user thành công", "Thông báo",
                         MessageBoxButtons.OK);
                        Initial();
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới không thành công", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }

            }
        }

        //cancel
        private void button5_Click(object sender, EventArgs e)
        {
            dgvUser.Rows[0].Selected = true;
            renderUserInfo();
            clearMessageError();
        }

        private Boolean validateForm()
        {
            Boolean result = true;
            if (txtName.Text == "" || txtName.Text.Trim() == "")
            {
                result = false;
                txtMessName.Text = "Tên không được để trống!!";
            }
            if (txtCCCD.Text == "" || txtCCCD.Text.Trim() == "")
            {
                result = false;
                txtMessCCCD.Text = "CCCD không được để trống!!";
            }
            if (txtAddress.Text == "" || txtAddress.Text.Trim() == "")
            {
                result = false;
                txtMessAddress.Text = "Địa chỉ không được để trống!!";
            }
            if (txtPhone.Text == "" || txtPhone.Text.Trim() == "")
            {
                result = false;
                txtMessPhone.Text = "Số điện thoại không được để trống!!";
            }

            return result;
        }

        private void clearMessageError()
        {
            txtMessName.Text = "";
            txtMessCCCD.Text = "";
            txtMessAddress.Text = "";
            txtMessPhone.Text = "";
        }

        private void clickName(object sender, EventArgs e)
        {
            clearMessageError();
        }


    }
}
