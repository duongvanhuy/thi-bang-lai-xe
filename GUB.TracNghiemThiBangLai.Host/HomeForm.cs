using ExcelDataReader;
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.WinFormsUtilities;
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
        public HomeForm()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            InitializeComponent();
            cbTrangThai.SelectedIndex = 0;
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
    }
}
