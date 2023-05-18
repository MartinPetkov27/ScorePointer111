using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScorePointer1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle2_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle4_Click(object sender, EventArgs e)
        {

        }
        private void ShowHistory()
        {
            Process.Start("notepad.exe", @"D:\Programing\Proizvodstwena praktika\ScorePointer1\Games");
        }
        private void btCreateGame_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            Visible = false;
        }

        private void btShowHistory_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }
        //
        // TO DO method for starting a previous game by getting its name from the history shown.
        //
    }
}
