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
    public partial class FormLedger : Form
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);
        
        SqlCommand sqlCmd;

        string regNum;
        string faculty;

        string name, fatherName, nation, degree, batch;

        public FormLedger(string faculty, string regNum)
        {
            InitializeComponent();
            toolStripComboBox2.SelectedItem = "12";
            this.dataGridViewLedger.DefaultCellStyle.Font = new Font("Tahoma", 12);
            
            this.faculty = faculty;
            this.regNum = regNum;
            try
            {
                string query = "select stuName, stuFatherName, stuNationality, stuDegree, stuBatch from StudentRecordsTable "
                                + "where regNum = '" + regNum + "' ; ";
                sqlCon.Open();
                SqlCommand command = new SqlCommand(query, sqlCon);
         
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //Console.WriteLine(String.Format("{0}", reader["id"]));
                        name = String.Format("{0}", reader["stuName"]);
                        fatherName = String.Format("{0}", reader["stuFatherName"]);
                        nation = String.Format("{0}", reader["stuNationality"]);
                        degree = String.Format("{0}", reader["stuDegree"]);
                        batch = String.Format("{0}", reader["stuBatch"]);
                    }
                }

                sqlCon.Close();

                textRegNum.Text = regNum + "-" + faculty.Trim() + "/" + degree.Trim() + "/" + batch.Trim() ;
                textName.Text = name;
                textFatherName.Text = fatherName;
                textNationality.Text = nation;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

            try
            {
                string query1 = "select Semester, bankChallanNum, Date,TutionFee, LabCharges, BusCharges, Security, "
                + "Other, Total, CreditHrs, Outstanding from Student_Ledger "
                                + "where regNum = '" + regNum + "' ; ";
                
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = query1;
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();


                
                sqlDataAdap.Fill(dtRecord);
                dataGridViewLedger.DataSource = dtRecord;

                
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message);
            }


        }

        private void FormLedgerClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private DataTable GetTransposedTable(DataTable dt)
        {
            DataTable newTable = new DataTable();
            newTable.Columns.Add(new DataColumn("0", typeof(string)));
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataRow newRow = newTable.NewRow();
                newRow[0] = dt.Columns[i].ColumnName;
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    if (newTable.Columns.Count < dt.Rows.Count + 1)
                        newTable.Columns.Add(new DataColumn(j.ToString(), typeof(string)));
                    newRow[j] = dt.Rows[j - 1][i];
                }
                newTable.Rows.Add(newRow);
            }
            return newTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(faculty,regNum);
            this.Visible = false;
            form1.Show();
            //this.Hide();
        } 

        private void formLoad()
        {
        this.TopMost = true;
        //this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
        }

        private void fontTextChanged(object sender, EventArgs e)
        {
          
            int font = Int32.Parse(toolStripComboBox2.Text);
            dataGridViewLedger.Font = new Font("Arial", font);
            
        
        }

        

    }
}
