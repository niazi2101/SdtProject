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
    public partial class Form1 : Form
    {
        string faculty;
        /* SQL database connection 
        SqlConnection sqlCon = new SqlConnection("Data Source=NIAZI-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=testDB");
        */
        //Locla Sql database connection

        SqlConnection sqlCon;
        SqlCommand sqlCmd;

        public Form1(string fac,string regnum)
        {
            InitializeComponent();
            faculty = fac;
            labelFaculty.Text = "Showing Records of " + faculty;
            loadDataGridView();
            loadComboBoxes();
            

            
        

        }

        private void loadComboBoxes()
        {
            toolStripComboBox2.SelectedItem = "12";
            //textBoxSearch.

            //chaning combox styles
            comboBoxDegree.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBatch.DropDownStyle = ComboBoxStyle.DropDownList;

            //adding items to Department filter
            comboBoxDegree.Items.Insert(0, "Filter by Degree");
            comboBoxDegree.Items.Insert(1, "BSCS");
            comboBoxDegree.Items.Insert(2, "BSSE");
            comboBoxDegree.Items.Insert(3, "IT");

            comboBoxDegree.SelectedItem = "Select Department";
            comboBoxDegree.SelectedIndex = 0;


            //adding items to batch filter
            comboBoxBatch.Items.Insert(0, "Filter by Batch");
            comboBoxBatch.Items.Insert(1, "F14");
            comboBoxBatch.Items.Insert(2, "F13");
            comboBoxBatch.Items.Insert(3, "F12");
            comboBoxBatch.Items.Insert(4, "F11");
            comboBoxBatch.Items.Insert(5, "F10");
            comboBoxBatch.Items.Insert(6, "No Filter");
            comboBoxBatch.SelectedIndex = 0;



            }

        private void TextFieldSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
            else
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("regNum='{0}'", textBoxSearch.Text);
            }
        }

        private void textFieldSearch_Changed(object sender, EventArgs e)
        {
            //(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("regNum LIKE '{0}%'", textBoxSearch.Text);
      
        }

        private void buttonViewLedger_Click(object sender, EventArgs e)
        {
            string regNum = (textBoxSearch.Text).ToString();
            if (regNum != null)
            {
                FormLedger formLedger = new FormLedger(faculty, regNum);
                formLedger.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select or enter a Registraion Number");
            }
            

        }


        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Action listener for hostelDues button
        private void buttonHostel_Click(object sender, EventArgs e)
        {
            string regNum = (textBoxSearch.Text).ToString();

            if(regNum != null)
            { 
            //LedgerAndHostelDues hostel = new LedgerAndHostelDues();
            HostelDuesLedger hostel = new HostelDuesLedger(faculty, regNum);
            hostel.Show();
            this.Hide();
            }
            else
            {
                MessageBox.Show("Please select or enter a Registraion Number");
            }
        }

        //MouseClick on DataGrid to fill search box
        private void DataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
           textBoxSearch.Text = dr.Cells[0].Value.ToString().Substring(0,4);
            
        
        }

        

        private void comboBatchTextChanged(object sender, EventArgs e)
        {
            if (comboBoxBatch.SelectedIndex < 0)
            {
                comboBoxBatch.Text = "Filter by Batch";

            }
            else if (comboBoxBatch.Text.Equals("No Filter") || comboBoxBatch.Text.Equals("Filter by Batch"))
            {
                loadDataGridView();
                
            }
            else
            {
                string batch = comboBoxBatch.Text;
                filterByBatch(batch);
            }

        }

        private void filterByDegree(String degree)
        {
            
                try
                {
                    if (!faculty.Equals(""))
                    {
                        sqlCmd = new SqlCommand();
                        sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
           
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = "Select * from StudentRecordsTable where "
                            + " stuDegree = @stuDegree;";
                        sqlCmd.Parameters.AddWithValue("@stuDegree", degree);
                        sqlCmd.Parameters.AddWithValue("@stuFaculity", faculty);

                        SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                        DataTable dtRecord = new DataTable();
                        sqlDataAdap.Fill(dtRecord);
                        dataGridView1.DataSource = dtRecord;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    }
                }
                catch (Exception exx)
                {
                    //MessageBox.Show("Filter By Degree" + exx.Message);
                }
            //}

        }

        

        private void filterByBatch(String batch)
        {
            try
            {
                if (!faculty.Equals(""))
                {
                    sqlCmd = new SqlCommand();
                    sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
           
                    sqlCmd.Connection = sqlCon;
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "Select * from StudentRecordsTable where "
                        + " stuBatch = @stuBatch and stuFaculity = @stuFaculity;";

                    sqlCmd.Parameters.AddWithValue("@stuBatch", batch);
                    sqlCmd.Parameters.AddWithValue("@stuFaculity", faculty);

                    SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                }
            }
            catch (Exception exx)
            {
                //MessageBox.Show("Filter By Batch" + exx.Message);
            }
            //}
        }

        private void loadDataGridView()
        {
            try
            {
                sqlCmd = new SqlCommand();
                sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
           
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "Select RegNum as 'Reg#', CAST([RegNum] as VARCHAR(4)) + '-' + LTRIM(REPLACE(stuFaculity, ' ', '')) + '/' + LTRIM(REPLACE(stuDegree, ' ', '')) + '/' " +
                    "+ stuBatch as 'Full Reg#', stuName as 'Student Name', stuFatherName as 'Father Name', stuNationality as Nationality,"
                + " stuFaculity as Faculity, stuDegree as Degree, stuBatch as Batch from StudentRecordsTable where stuFaculity = '" + faculty + "';";
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                dataGridView1.DataSource = dtRecord;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }
        }

        private void comboBoxDegreeTextChanged(object sender, EventArgs e)
        {
            if (comboBoxDegree.SelectedIndex < 0)
            {
                comboBoxDegree.Text = "Filter by Degree";

            }
            else if (comboBoxDegree.Text.Equals("No Filter") || comboBoxDegree.Text.Equals("Filter by Degree"))
            {
                loadDataGridView();
                //MessageBox.Show("No Filter");

            }
            else
            {
                string Degree = comboBoxDegree.Text;
                filterByDegree(Degree);
            }

        }

        

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Font = new Font("Arial", 14);
        }

        private void toolStripMenuItemFont12_Click(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("Arial", 12);
        }

       

        private void fontSizeChanged(object sender, EventArgs e)
        {
            int font = Int32.Parse(toolStripComboBox2.Text);
            dataGridView1.Font = new Font("Arial", font);
            
        }

        private void buttonAddChallan_Click(object sender, EventArgs e)
        {
            string regNum = (textBoxSearch.Text).ToString();

            if (regNum != null)
            {
                AddChallanForm newChallan = new AddChallanForm(regNum);
               

                this.Hide();
                //newChallan.ShowDialog();
                newChallan.Show();
            }
            else
            {
                MessageBox.Show("Please select or enter a Registraion Number");
            }
        }
    }
}
