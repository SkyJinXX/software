using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class F : Form
    {
        public F()
        {
            InitializeComponent();
        }
        protected String SqlFlush()
        {
            String sql = "";
            if (textBox1.Text == "")
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from freecar";
                }
                else
                {
                    sql = "select * from freecar where 车位位置 = '" + textBox2.Text + "'";
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from freecar where 车位编号 = '" + textBox1.Text + "'";
                }
                else
                {
                    sql = "select * from freecar where 车位位置 = '" + textBox2.Text + "' and 车位编号 = '" + textBox1.Text + "'";
                }
            }

            return sql;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = SqlFlush();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = true;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form5 f = new Form5();
            f.Show();
        }
    }
}
