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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            comboBoxFaculty.DropDownStyle = ComboBoxStyle.DropDownList;

            //faculties in comboBox
            comboBoxFaculty.Items.Add("FBAS");
            comboBoxFaculty.Items.Add("FET");
            comboBoxFaculty.Items.Add("FMS");
            comboBoxFaculty.Items.Add("FSL");
            comboBoxFaculty.Items.Add("FA");

            comboBoxFaculty.SelectedItem = "FBAS";
            //comboBoxFaculty.FormattingEnabled("false");

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string ID = textBoxID.Text;
            string password = textBoxPassword.Text;
            string facult = comboBoxFaculty.Text;

            Form1 form1 = new Form1(facult,null);
            form1.Show();

            this.Hide();


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
