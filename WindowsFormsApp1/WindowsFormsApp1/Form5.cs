using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            A f = new A();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            B f = new B();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            C f = new C();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            D f = new D();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            E f = new E();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            F f = new F();
            f.Show();
        }
    }
}
