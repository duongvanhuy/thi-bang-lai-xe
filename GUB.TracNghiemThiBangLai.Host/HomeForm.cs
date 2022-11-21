using ExcelDataReader;
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;
using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Controller;
using GUB.TracNghiemThiBangLai.Share.Resources;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace GUB.TracNghiemThiBangLai.Host
{
    public partial class HomeForm : Form
    {
        String pathFileExcel = "";
        ComputerRepository computerRepository = new ComputerRepository();
        ResultExamRepository resultExamRepository = new ResultExamRepository();
        UserRepository userRepository = new UserRepository();
        AppSettingRepository appSettingRepository = new AppSettingRepository();
        int ExamTime = 0;


        // lưu tạm danh sách 5 người đang thi
        List<string> listCCCDMemory = new List<string>();


        bool isHeader = false; // biến kiểm tra header hoạt động máy thi


        public HomeForm()
        {

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            InitializeComponent();

            ExamTime = AppSetting.ExamTime;
            setTimelable();


            cbTrangThai.SelectedIndex = 0;

            // set cứng số máy trong phòng thi
            lblSoMayHoatDong.Text = "5";
        }

        private void ImportFileExcel(string pathFile)
        {

        }

        private string InitialFile(string fileName)
        {
            string pathFile = Path.Combine(Application.StartupPath, fileName);
            if (!File.Exists(pathFile))
            {
                File.Create(pathFile);
            }
            return pathFile;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                string fileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    /* FileStream fileStream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                     IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fileStream);

                     string filePath = openFileDialog.FileName;
                     ImportFileExcel(filePath);*/

                    pathFileExcel = openFileDialog.FileName;
                    fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    DataTable tbContainer = new DataTable();
                    string strConn = string.Empty;
                    string sheetName = fileName;

                    FileInfo file = new FileInfo(pathFileExcel);
                    if (!file.Exists) { throw new Exception("Error, file doesn't exists!"); }
                    string extension = file.Extension;
                    switch (extension)
                    {
                        case ".xls":
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathFileExcel + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                        case ".xlsx":
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathFileExcel + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                            break;
                        default:
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathFileExcel + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                    }
                    OleDbConnection cnnxls = new OleDbConnection(strConn);
                    OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [Sheet1$]", sheetName), cnnxls);
                    oda.Fill(tbContainer);


                    dataTable.DataSource = tbContainer;
                    saveUserFordatabase();
                }

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // lấy ra giá trị cột cuối cùng của hàng
            string value = dataTable.Rows[e.RowIndex].Cells[dataTable.Columns.Count - 1].Value.ToString();

            switch (value)
            {
                case "Có mặt":
                    cbTrangThai.SelectedIndex = 1;
                    break;
                case "Vắng mặt":
                    cbTrangThai.SelectedIndex = 2;
                    break;
                default:
                    cbTrangThai.SelectedIndex = 1;
                    break;
            }
            // int lastRow = dataTable.Rows.Count - 1;
        }

        /// <summary>
        /// kiểm tra hoạt động máy thi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button4_Click(object sender, EventArgs e)
        {
            // lấy ra danh sach máy thi
            List<Computer> computers = new List<Computer>();
            computers = await computerRepository.GetComputers();
            var count = 0;


            // Lấy ra HeaderText cột cuối cùng
            string headerText = dataTable.Columns[dataTable.Columns.Count - 1].HeaderText;

            // Kiểm tra cột "Số máy" đã tồn tại trong file excel chưa
            if (!isHeader)
            {
                // thêm 1 cột "Số máy" vào cuối cùng
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = "Số máy";
                col.Name = "SoMay";
                dataTable.Columns.Add(col);



                // kiểm tra giá trị của từng ô trong  cột "Chú thích"
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (count < computers.Count)
                    {
                        var a = dataTable.Rows[i].Cells[0].Value;
                        // kiểm tra hàng hiện tại có khác null không
                        if (dataTable.Rows[i].Cells[0].Value != null)
                        {
                            string chuThich = dataTable.Rows[i].Cells[dataTable.Columns.Count - 2].Value.ToString();
                            string soLanThi = dataTable.Rows[i].Cells[dataTable.Columns.Count - 4].Value.ToString();


                            if (chuThich.Equals("Có mặt") && soLanThi.Equals("0"))
                            {

                                dataTable.Rows[i].Cells[dataTable.Columns.Count - 1].Value = computers[count].NumberCom.ToString();
                                /* dataTable.Rows[i].Cells[dataTable.Columns.Count - 4].Value = 1;*/
                                listCCCDMemory.Add(dataTable.Rows[i].Cells[3].Value.ToString());

                                // lưu tạm danh sách người thi tương ứng với máy thi vào CSDL
                                await computerRepository.UpdateComputer(computers[count].Id, dataTable.Rows[i].Cells[3].Value.ToString());
                                count++;
                            }
                            //else if (chuThich.Equals("Có mặt") && soLanThi.Equals("1"))
                            //{
                            //    dataTable.Rows[i].Cells[dataTable.Columns.Count - 1].Value = "0";

                            //}
                            //else if (chuThich.Equals("Vắng mặt"))
                            //{
                            //    dataTable.Rows[i].Cells[dataTable.Columns.Count - 1].Value = "0";

                            //}
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                isHeader = true;
            }
            // kết thúc bài thi lần 1 - update danh sách người thi lần 2
            else
            {
                // kiểm tra giá trị của từng ô trong  cột "Chú thích"
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (count < computers.Count)
                    {

                        // kiểm tra hàng hiện tại có khác null không
                        if (dataTable.Rows[i].Cells[0].Value != null)
                        {
                            string chuThich = dataTable.Rows[i].Cells[dataTable.Columns.Count - 3].Value.ToString();
                            string soLanThi = dataTable.Rows[i].Cells[dataTable.Columns.Count - 5].Value.ToString();


                            if (chuThich.Equals("Có mặt") && soLanThi.Equals("0"))
                            {


                                dataTable.Rows[i].Cells[dataTable.Columns.Count - 2].Value = computers[count].NumberCom.ToString();
                                //dataTable.Rows[i].Cells[dataTable.Columns.Count - 5].Value = 1;
                                listCCCDMemory.Add(dataTable.Rows[i].Cells[3].Value.ToString());

                                // lưu tạm danh sách người thi tương ứng với máy thi vào CSDL
                                await computerRepository.UpdateComputer(computers[count].Id, dataTable.Rows[i].Cells[3].Value.ToString());
                                count++;
                            }
                            //else if (chuThich.Equals("Có mặt") && soLanThi.Equals("1"))
                            //{
                            //    dataTable.Rows[i].Cells[dataTable.Columns.Count - 1].Value = "0";

                            //}
                            //else if (chuThich.Equals("Vắng mặt"))
                            //{
                            //    dataTable.Rows[i].Cells[dataTable.Columns.Count - 1].Value = "0";

                            //}
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            //  importDefault = 1;

        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // thay đổi trạng thái của người dùng
        private void button1_Click_1(object sender, EventArgs e)
        {
            // kiểm tra coi có hàng nào trong datatable được chọn hay không
            if (dataTable.SelectedRows.Count > 0)
            {
                var status = cbTrangThai.SelectedItem.ToString();
                // hiện thị confirm trước khi thay đổi
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thay đổi trạng thái của học viên này không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // thay đổi trạng thái của học viên
                    dataTable.SelectedRows[0].Cells[dataTable.Columns.Count - 1].Value = status;
                    // ghi file excel
                    WriteExcelFile();

                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn dòng nào trong bảng");
            }
        }

        private void WriteExcelFile()
        {
            // var saveFileDialog = new SaveFileDialog();
            // saveFileDialog.Filter =
            //     "XLS (*.xls)|*.xls|" +
            //     "XLT (*.xlt)|*.xlt|" +
            //     "XLSX (*.xlsx)|*.xlsx|" +
            //     "XLSM (*.xlsm)|*.xlsm|" +
            //     "XLTX (*.xltx)|*.xltx|" +
            //     "XLTM (*.xltm)|*.xltm|" +
            //     "ODS (*.ods)|*.ods|" +
            //     "OTS (*.ots)|*.ots|" +
            //     "CSV (*.csv)|*.csv|" +
            //     "TSV (*.tsv)|*.tsv|" +
            //     "HTML (*.html)|*.html|" +
            //     "MHTML (.mhtml)|*.mhtml|" +
            //     "PDF (*.pdf)|*.pdf|" +
            //     "XPS (*.xps)|*.xps|" +
            //     "BMP (*.bmp)|*.bmp|" +
            //     "GIF (*.gif)|*.gif|" +
            //     "JPEG (*.jpg)|*.jpg|" +
            //     "PNG (*.png)|*.png|" +
            //     "TIFF (*.tif)|*.tif|" +
            //     "WMP (*.wdp)|*.wdp";

            // saveFileDialog.FilterIndex = 3;

            // if (saveFileDialog.ShowDialog() == DialogResult.OK)
            // {
            //     var workbook = new ExcelFile();
            //     var worksheet = workbook.Worksheets.Add("Sheet1");

            //     // From DataGridView to ExcelFile.
            //     DataGridViewConverter.ImportFromDataGridView(
            //         worksheet,
            //         this.dataTable,
            //         new ImportFromDataGridViewOptions() { ColumnHeaders = true });

            //     workbook.Save(saveFileDialog.FileName);
            // }

            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add("Sheet1");
            DataGridViewConverter.ImportFromDataGridView(
                worksheet,
             this.dataTable,
             new ImportFromDataGridViewOptions() { ColumnHeaders = true });
            workbook.Save(pathFileExcel);
        }

        // bắt đầu bài thi
        private async void button3_Click(object sender, EventArgs e)
        {
            ExamTime = AppSetting.ExamTime;
            timer1.Start();
            AppSettingEntities appSettingEntities = new AppSettingEntities()
            {
                Key = 1,
                valueKey = 1
            };
            await appSettingRepository.UpdateAppSettingKey(appSettingEntities);
            lblSoNguoiDangThi.Text = listCCCDMemory.Count.ToString();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            --ExamTime;
            setTimelable();

            // hết thời gian
            if (ExamTime == 0)
            {
                timer1.Stop();
                updateResultInDataTable();
            }
        }

        private void setTimelable()
        {
            int minute = ExamTime / 60;
            int second = ExamTime % 60;
            string time = minute.ToString() + ":" + second.ToString();
            lbTime.Text = time;
        }

        private void lbTime_Click(object sender, EventArgs e)
        {

        }

        //kết thúc bài thi
        private async void button5_Click(object sender, EventArgs e)
        {
            // hiện thị confirm trước khi thay đổi
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn kết thúc tất cả bài thi không?", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                updateResultInDataTable();
                ExamTime = 0;
                lblSoNguoiThiXong.Text = listCCCDMemory.Count.ToString();
                lblSoNguoiDangThi.Text = "0";



            }

        }

        private async void updateResultInDataTable()
        {
            // danh sách kết quả thi
            var listResultExam = await resultExamRepository.GetResultExams();

            // thêm vào cột cuối cùng phần trạng thái "Đạt" và "Không đạt"
            // Lấy ra HeaderText cột cuối cùng
            string headerText = dataTable.Columns[dataTable.Columns.Count - 1].HeaderText;
            // Kiểm tra cột "Số máy" đã tồn tại trong file excel chưa
            if (!headerText.Equals("Trạng thái"))
            {
                // thêm 1 cột "Số máy" vào cuối cùng
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = "Trạng thái";
                col.Name = "trangThai";
                dataTable.Columns.Add(col);

                // kiểm tra listResultExam có CCCD nào trùng với listCCCDMemory không
                // nếu có cập nhật lại điểm ở dataTable
                for (int i = 0; i < listResultExam.Count; i++)
                {
                    for (int j = 0; j < listCCCDMemory.Count; j++)
                    {
                        if (listResultExam[i].CMND.Equals(listCCCDMemory[j]))
                        {

                            dataTable.Rows[j].Cells[dataTable.Columns.Count - 4].Value = listResultExam[i].NumberOfCorrect;
                            dataTable.Rows[i].Cells[dataTable.Columns.Count - 5].Value = 1;
                            //if (listResultExam[i].NumberOfCorrect >= 16)
                            //{

                            //    dataTable.Rows[j].Cells[dataTable.Columns.Count - 1].Value = "Đạt";

                            //}
                            //else
                            //{
                            //    dataTable.Rows[j].Cells[dataTable.Columns.Count - 1].Value = "Không đạt";

                            //}
                            dataTable.Rows[j].Cells[dataTable.Columns.Count - 1].Value = listResultExam[i].Status;
                        }
                    }
                }
            }
        }
        private void SapXepVaoMayThi()
        {

        }

        // quá trình thi
        private async void button6_Click(object sender, EventArgs e)
        {
            // kiểm tra người thi đã thi xong chưa

            // danh sách kết quả thi
            var listResultExam = await resultExamRepository.GetResultExams();
            string headerText = dataTable.Columns[dataTable.Columns.Count - 1].HeaderText;
            int count = 0; // số người thi xong
            bool isHeader = false; // kiểm tra coi có cột trạng thái 

            // kiểm tra listResultExam có CCCD nào trùng với listCCCDMemory không
            // nếu có cập nhật lại điểm ở dataTable và thêm 1 ô trạng thái
            for (int i = 0; i < listResultExam.Count; i++)
            {
                for (int j = 0; j < listCCCDMemory.Count; j++)
                {
                    if (listResultExam[i].CMND.Equals(listCCCDMemory[j]))
                    {
                        if (!isHeader)
                        {
                            // thêm 1 cột "Số máy" vào cuối cùng
                            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                            col.HeaderText = "Trạng thái";
                            col.Name = "trangThai";
                            dataTable.Columns.Add(col);
                        }

                        int index = dataTable.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[3].Value.ToString().Equals(listCCCDMemory[j])).First().Index;
                        dataTable.Rows[index].Cells[dataTable.Columns.Count - 4].Value = listResultExam[i].NumberOfCorrect;
                        dataTable.Rows[index].Cells[dataTable.Columns.Count - 5].Value = 1;
                        //if (listResultExam[i].NumberOfCorrect >= 16)
                        //{

                        //    dataTable.Rows[j].Cells[dataTable.Columns.Count - 1].Value = "Đạt";


                        //}
                        //else
                        //{
                        //    dataTable.Rows[j].Cells[dataTable.Columns.Count - 1].Value = "Không đạt";

                        //}
                        dataTable.Rows[index].Cells[dataTable.Columns.Count - 1].Value = listResultExam[i].Status;
                        count++;
                        isHeader = true;

                    }
                }

                //foreach(var data in dataTable.Rows)
                //{

                //    if(data.ca)
                //}
            }

            // cập nhật lại số người đang thi và đã thi xong
            lblSoNguoiDangThi.Text = (listCCCDMemory.Count - count).ToString();
            lblSoNguoiThiXong.Text = count.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblSoMayHoatDong_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // lấy giá trị của user sau khi import file excel
        private User getUser()
        {
            try
            {
                var user = new User();
                user.FullName = dataTable.Rows[dataTable.CurrentRow.Index].Cells[1].Value.ToString();
                user.Phone = dataTable.Rows[dataTable.CurrentRow.Index].Cells[2].Value.ToString();
                user.CCCD = dataTable.Rows[dataTable.CurrentRow.Index].Cells[3].Value.ToString();
                user.Birthday = DateTime.Parse(dataTable.Rows[dataTable.CurrentRow.Index].Cells[4].Value.ToString());
                user.Address = dataTable.Rows[dataTable.CurrentRow.Index].Cells[5].Value.ToString();
                return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // lấy giá trị của user sau khi import file excel
        private async void saveUserFordatabase()
        {
            try
            {
                // duyệt qua các hàng trong bảng
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    // lấy giá trị của user
                    var user = new User();
                    user.FullName = dataTable.Rows[i].Cells[1].Value.ToString();
                    user.Phone = dataTable.Rows[i].Cells[2].Value.ToString();
                    user.CCCD = dataTable.Rows[i].Cells[3].Value.ToString();
                    user.Birthday = DateTime.Parse(dataTable.Rows[i].Cells[4].Value.ToString());
                    user.Address = dataTable.Rows[i].Cells[5].Value.ToString();
                    // thêm user vào database
                    await userRepository.AddUser(user);
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);

            }
        }
    }
}
