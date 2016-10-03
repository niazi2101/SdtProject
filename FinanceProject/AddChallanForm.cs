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
using System.Text.RegularExpressions;
using System.Windows.Input;


namespace FinanceProject
{
    public partial class AddChallanForm : Form
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["newConnectionString"].ConnectionString);

        SqlCommand sqlCmd;
        string regNum;

        string bankChallan, date, semester, tutionFee, labCharges, security, outstanding, busCharges,totalFee;

        int numtutionFee = 0, numlabCharges = 0, numBusCharges = 0, numSecurity = 0;

        int total;
        public AddChallanForm(string regNum)
        {
            this.regNum = regNum;
            InitializeComponent();

            total = 0;

            try
            {
                if (!regNum.Equals(""))
                {

                    loadDataGridView(regNum);
                    labelRegNum.Text = "Record of Reg# " + regNum;

                    //Split container to 30 70
                    splitContainer1.SplitterDistance =
                        (int)(splitContainer1.ClientSize.Width * 0.30);

                    //chaning combox styles
                    comboBoxSemester.DropDownStyle = ComboBoxStyle.DropDownList;

                    /* basic approach, i have a better idea but will implement it later
                     * ===> get student batch from student detail form (Form1.cs)
                     * ==> fill the comboBox with semesters after the student's batch
                     */

                    //adding items to Department filter
                    comboBoxSemester.Items.Insert(0, "Fall-09");
                    comboBoxSemester.Items.Insert(1, "Spring-09");
                    comboBoxSemester.Items.Insert(2, "Summer-09");
                    comboBoxSemester.Items.Insert(3, "Fall-10");
                    comboBoxSemester.Items.Insert(4, "Fall-10");
                    comboBoxSemester.Items.Insert(5, "Spring-10");
                    comboBoxSemester.Items.Insert(6, "Summer-10");
                    comboBoxSemester.Items.Insert(7, "Fall-11");
                    comboBoxSemester.Items.Insert(8, "Spring-11");
                    comboBoxSemester.Items.Insert(9, "Summer-11");
                    comboBoxSemester.Items.Insert(10, "Fall-12");
                    comboBoxSemester.Items.Insert(11, "Spring-12");
                    comboBoxSemester.Items.Insert(12, "Summer-12");

                    comboBoxSemester.Items.Insert(13, "Fall-13");
                    comboBoxSemester.Items.Insert(14, "Spring-13");
                    comboBoxSemester.Items.Insert(15, "Summer-13");
                    comboBoxSemester.Items.Insert(16, "Fall-14");
                    comboBoxSemester.Items.Insert(17, "Spring-14");
                    comboBoxSemester.Items.Insert(18, "Summer-14");
                    comboBoxSemester.Items.Insert(19, "Fall-17");
                    comboBoxSemester.Items.Insert(20, "Spring-17");
                    comboBoxSemester.Items.Insert(21, "Summer-17");
                    comboBoxSemester.Items.Insert(22, "Fall-16");
                    comboBoxSemester.Items.Insert(23, "Spring-16");
                    comboBoxSemester.Items.Insert(24, "Summer-16");


                    //comboBoxSemester.SelectedItem = "Select Department";
                    comboBoxSemester.SelectedIndex = 0;

                }
                else
                {
                    MessageBox.Show("Registration Number not recieved");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Exception " + ex.Message);
                
            }

            
        }

        private void insertData(string bankChallan,string date,string semester,string tutionFee,string labCharges,string security,string outstanding,string busCharges,string totalFee)
        {
            try{

            /*
                sqlCmd = new SqlCommand("insert into tbl_Record(Name,State) values(@name,@state)", con);
                sqlCon.Open();
                sqlCmd.Parameters.AddWithValue("@name", txt_Name.Text);
                sqlCmd.Parameters.AddWithValue("@state", txt_State.Text);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                MessageBox.Show("Record Inserted Successfully");
                loadDataGridView(regNum);
                clearAll();
           
               */
        }catch(Exception ex)
            {

            }
            
            
            MessageBox.Show("Bank Challan No. = " + bankChallan +
                    "\n Date = " + date +
                    "\n Semester = " + semester +
                    "\n Tution Fee = " + tutionFee +
                    "\n Lab Charges = " + labCharges +
                    "\n Total Fees = " + totalFee);
        }

        

        private void loadDataGridView(string regNum)
        {
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
                dataGridView1.DataSource = dtRecord;


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
            //this.DialogResult = DialogResult.OK;
            this.Hide();
            /*Form1 form1 = new Form1(regNum);
            form1.Show();*/
        }

        private void clearAll()
        {
            textBoxBusCharges.Text = "0";
            textBoxChallanNum.Text = "0";
            textBoxCreditHrs.Text = "0";
            textBoxLabCharges.Text = "0";
            textBoxOutStanding.Text = "0";

            textBoxSecurity.Text = "0";
            textBoxTotalFees.Text = "0";
            textBoxTutionFee.Text = "0";
            total = 0;

        }

        private void textFocused(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bankChallan = textBoxChallanNum.Text;
            date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            semester = comboBoxSemester.Text;
            tutionFee = textBoxTutionFee.Text;
            labCharges = textBoxLabCharges.Text;
            security = textBoxSecurity.Text;
            outstanding = textBoxOutStanding.Text;
            busCharges = textBoxBusCharges.Text;
            totalFee = textBoxTotalFees.Text;

            if (textBoxChallanNum.Text != "0")
            {
                

                DialogResult result = MessageBox.Show("Bank Challan No. = " + bankChallan +
                    "\n Date = " + date +
                    "\n Semester = " + semester +
                    "\n Tution Fee = " + tutionFee +
                    "\n Lab Charges = " + labCharges +
                    "\n Total Fees = " + totalFee, "Warning",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //insertData();
                }
                else if (result == DialogResult.No)
                {
                    //code for No
                }
                else if (result == DialogResult.Cancel)
                {
                    //code for Cancel
                }
            }
            else
            {
                MessageBox.Show("Please Enter a Challan Number");
            }
            //insertData();

            
        }

        private void leaveTutionFee(object sender, EventArgs e)
        {

            if (!textBoxTutionFee.Text.Equals(""))
            {
            total -= numtutionFee;

            numtutionFee = Int32.Parse(textBoxTutionFee.Text);

            total += numtutionFee;

            textBoxTotalFees.Text = ""+total;

            //MessageBox.Show("Tution Fee = " + tutionFee);
            }
            else if (textBoxTutionFee.Text.Equals("0"))
            {
                total -= numtutionFee;
                textBoxTotalFees.Text = "" + total;

            }
            else
            {
                textBoxTutionFee.Text = "0";
            }
        }

        private void leaveLabCharges(object sender, EventArgs e)
        {
            if (!textBoxLabCharges.Text.Equals(""))
            {
                total -= numlabCharges;
                
                numlabCharges = Int32.Parse(textBoxLabCharges.Text);

                total += numlabCharges;
            //    MessageBox.Show("Tution Fee = " + labCharges);
                textBoxTotalFees.Text = "" + total;
            }
            else if (textBoxLabCharges.Text.Equals("0"))
            {
                total -= numlabCharges;
                textBoxTotalFees.Text = "" + total;
                
            }
            else
            {
                textBoxLabCharges.Text = "0";
            }
        }

        private void leaveBusCharges(object sender, EventArgs e)
        {
            if (!textBoxBusCharges.Text.Equals(""))
            {
                total -= numBusCharges;

                numBusCharges = Int32.Parse(textBoxBusCharges.Text);

                total += numBusCharges;
                //    MessageBox.Show("Tution Fee = " + labCharges);
                textBoxTotalFees.Text = "" + total;
            }
            else if (textBoxBusCharges.Text.Equals("0"))
            {
                total -= numBusCharges;
                textBoxTotalFees.Text = "" + total;

            }
            else
            {
                textBoxBusCharges.Text = "0";
            }
        }

        private void leaveSecurity(object sender, EventArgs e)
        {
            if (!textBoxSecurity.Text.Equals(""))
            {
                total -= numSecurity;

                numSecurity = Int32.Parse(textBoxSecurity.Text);

                total += numSecurity;
                //    MessageBox.Show("Tution Fee = " + labCharges);
                textBoxTotalFees.Text = "" + total;
            }
            else if (textBoxSecurity.Text.Equals("0"))
            {
                total -= numSecurity;
                textBoxTotalFees.Text = "" + total;

            }
            else
            {
                textBoxSecurity.Text = "0";
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void textBoxCreditValidating(object sender, CancelEventArgs e)
        {
            
            string textToCheck = textBoxCreditHrs.Text;
            
            //validates first and second digit, but always expects two digits
            string pattern = @"^[0-9][0-9]$";   //first and second
            string pattern1 = @"^[0-9]";        //only first
            string pattern0 = @"^[0]";          //or zero

            try
            {
                bool isValid = Regex.IsMatch(textToCheck, pattern0) || Regex.IsMatch(textToCheck, pattern1) || Regex.IsMatch(textToCheck, pattern); //^[1-9]\d{8}$ -- @"^\d{9}$"

                if(isValid)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    textBoxCreditHrs.Select(0, textBoxCreditHrs.Text.Length);

                    this.errorProvider1.SetError(textBoxCreditHrs, "Please Enter Numbers only");
                    textBoxCreditHrs.BackColor = Color.OrangeRed;

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Unable to Validate : " + ex.Message);
            }
            
        }

        private void textBoxCreditValidated(object sender, EventArgs e)
        {
            
            this.errorProvider1.SetError(textBoxCreditHrs, "");
            textBoxCreditHrs.BackColor = Color.White;
        }

        private void tfieldChallan_Validating(object sender, CancelEventArgs e)
        {
            
            string textToCheck = textBoxChallanNum.Text;

            //validates first and second digit, but always expects two digits
            string pattern = @"^\d{9}$";
            string pattern1 = @"^[0]";

            try
            {
                bool isValid = Regex.IsMatch(textToCheck, pattern) || Regex.IsMatch(textToCheck, pattern1); //^[1-9]\d{8}$ -- @"^\d{9}$"

                //if pattern is valid, then do nothing.. else display error
                if (isValid)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    textBoxChallanNum.Select(0, textBoxChallanNum.Text.Length);

                    this.errorProvider1.SetError(textBoxChallanNum, "Please Enter a 9 Digit Number only ");
                    textBoxChallanNum.BackColor = Color.OrangeRed;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to Validate : " + ex.Message);
            }

        }

        private void tfieldChallan_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.SetError(textBoxChallanNum, "");
            textBoxChallanNum.BackColor = Color.White;
        }

        private void textTution_KeyDown(object sender, KeyEventArgs e)
        {
            textNumberOnly(sender, e);

        }

        private void textFieldLab_KeyDown(object sender, KeyEventArgs e)
        {
            textNumberOnly(sender, e);
        }

        private void textFieldBus_KeyDown(object sender, KeyEventArgs e)
        {
            textNumberOnly(sender, e);
        }

        private void textFieldSecurity_KeyDown(object sender, KeyEventArgs e)
        {
            textNumberOnly(sender, e);
        }

        private void textFieldOutstanding_KeyDown(object sender, KeyEventArgs e)
        {
            textNumberOnly(sender, e);
        }

        private void textNumberOnly(object sender, KeyEventArgs e)
        {
            //Allow navigation keyboard arrows and NUMPAD keys and NUMLOCK
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Delete:
                    e.SuppressKeyPress = false;
                    return;
                case Keys.NumLock: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad0: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad1: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad2: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad3: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad4: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad5: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad6: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad7: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad8: e.SuppressKeyPress = false;
                    return;
                case Keys.NumPad9: e.SuppressKeyPress = false;
                    return;

                default:
                    break;
            }



            //Block non-number characters
            char currentKey = (char)e.KeyCode;
            bool modifier = e.Control || e.Alt || e.Shift; // || e.KeyCode > Keys.NumLock;
            bool nonNumber = char.IsLetter(currentKey) ||
                             char.IsSymbol(currentKey) ||
                             char.IsWhiteSpace(currentKey) ||
                             char.IsPunctuation(currentKey);




            if (!modifier && nonNumber)
                e.SuppressKeyPress = true;



            //Handle pasted Text
            if (e.Control && e.KeyCode == Keys.V)
            {
                //Preview paste data (removing non-number characters)
                string pasteText = Clipboard.GetText();
                string strippedText = "";
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (char.IsDigit(pasteText[i]))
                        strippedText += pasteText[i].ToString();
                }

                if (strippedText != pasteText)
                {
                    //There were non-numbers in the pasted text
                    e.SuppressKeyPress = true;

                    //OPTIONAL: Manually insert text stripped of non-numbers
                    TextBox me = (TextBox)sender;
                    int start = me.SelectionStart;
                    string newTxt = me.Text;
                    newTxt = newTxt.Remove(me.SelectionStart, me.SelectionLength); //remove highlighted text
                    newTxt = newTxt.Insert(me.SelectionStart, strippedText); //paste
                    me.Text = newTxt;
                    me.SelectionStart = start + strippedText.Length;
                }
                else
                    e.SuppressKeyPress = false;
            }
        }

        

       
    }
}
