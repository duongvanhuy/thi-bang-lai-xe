using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    FileStream fileStream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
                   // excelReader.IsFirstRow
                    string filePath = openFileDialog.FileName;
                    ImportFileExcel(filePath);
                }
            }
        }
    }
}
