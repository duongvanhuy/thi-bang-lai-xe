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
    }
}
