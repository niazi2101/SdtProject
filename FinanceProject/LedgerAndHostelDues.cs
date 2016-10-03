using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceProject
{
    public partial class LedgerAndHostelDues : Form
    {
        //string RegNum = "2101";

        public LedgerAndHostelDues()
        {
            InitializeComponent();
        }

        private void LedgerAndHostelDues_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.HostelDues' table. You can move, or remove it, as needed.
           // this.HostelDuesTableAdapter.Fill(this.DataSet1.HostelDues);

            this.reportViewer1.RefreshReport();
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void reportLoaded(object sender, EventArgs e)
        {
            
        }

       
    }
}
