using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RE1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //button1_Click
        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = null;
            string filter = "JSON files (*.json)|*.json";

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = filter;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }

            if (fileName != null)
            {
                textBox1.Text = fileName;
                textBox1.SelectionStart = fileName.Length;
                textBox1.ScrollToCaret();
            }
        }


        //button2_Click
        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = null;
            string filter = "CSV files (*.csv)|*.csv";

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = filter;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }

            if (fileName != null)
            {
                textBox2.Text = fileName;
                textBox2.SelectionStart = fileName.Length;
                textBox2.ScrollToCaret();
            }
        }



        //button3_Click
        private void button3_Click(object sender, EventArgs e)
        {
            var parser1 = new ParserClass();
            var invalidSignalList = parser1.parser(textBox1.Text,textBox2.Text);
            //richTextBox1.Text = invalidSignalList.ToString();
            richTextBox1.Clear();
            foreach (var element in invalidSignalList)
            {
                if (element.ValueType == "Decimal") 
                    richTextBox1.Text += element.Signal + "  " + element.Value + "  " + "Integer" + "\n";
                else
                    richTextBox1.Text += element.Signal + "  " + element.Value + "  " + element.ValueType +"\n";
            }
        }

    }
}
