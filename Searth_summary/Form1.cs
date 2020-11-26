using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searth_summary
{
    public partial class Form1 : Form
    {
       
       

        public Form1()
        {
            
            InitializeComponent();
            
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            JobSeekerBindingSource.DataSource= service.GetJob(0); 
            reportViewer1.RefreshReport();
        }
    }
}
