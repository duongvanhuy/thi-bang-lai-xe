using ExcelDataReader;
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
        public HomeForm()
        {
            InitializeComponent();
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

                    string pathName = openFileDialog.FileName;
                    fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    DataTable tbContainer = new DataTable();
                    string strConn = string.Empty;
                    string sheetName = fileName;

                    FileInfo file = new FileInfo(pathName);
                    if (!file.Exists) { throw new Exception("Error, file doesn't exists!"); }
                    string extension = file.Extension;
                    switch (extension)
                    {
                        case ".xls":
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                        case ".xlsx":
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                            break;
                        default:
                            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                            break;
                    }
                    OleDbConnection cnnxls = new OleDbConnection(strConn);
                    OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [KiThi$]", sheetName), cnnxls);
                    oda.Fill(tbContainer);

                    dataTable.DataSource = tbContainer;
                }

            }
        
        }
    }
}
