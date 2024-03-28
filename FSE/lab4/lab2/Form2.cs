using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form2 : Form
    {
        private Form1 mainForm;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
        }

        public int amountOfNews { get; set; }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            amountOfNews = trackBar1.Value * 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.UpdateAmountOfNews(amountOfNews);
            this.Close();
            MessageBox.Show(amountOfNews.ToString());
        }
    }
}
