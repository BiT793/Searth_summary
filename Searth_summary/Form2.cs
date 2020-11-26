using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searth_summary
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            VacanciBindingSource.DataSource = service.GetVacanci(0);
            reportViewer1.RefreshReport();
        }
    }
}
