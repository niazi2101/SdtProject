using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

using System.Data.SqlClient;
using System.Configuration;


namespace FinanceProject
{
    public partial class HostelDuesLedger : Form
    {
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        string reg,faculty;

        public HostelDuesLedger(string faculty,string regnum)
        {
            InitializeComponent();
            this.reg = regnum;
            this.faculty = faculty;
            loadDataGridView();
        }

        private void dataGridViewHostelDues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadDataGridView()
        {
            try
            {
                if (reg != null)
                {
                    sqlCmd = new SqlCommand();
                    sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
            
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "Select * from HostelDues where regNum = '" + reg + "';";
                    SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridViewHostelDues.DataSource = dtRecord;
                    dataGridViewHostelDues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else
                {
                    MessageBox.Show("Registration number not given");
                    
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(faculty, reg);
            this.Visible = false;
            form1.Show();
            //this.Hide();
        }
    }
}
