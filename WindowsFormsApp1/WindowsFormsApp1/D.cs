using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class D : Form
    {
        public D()
        {
            InitializeComponent();
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

        protected String SqlFlush()
        {
            String sql = "";
            if (textBox1.Text == "")
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from fee";
                }
                else
                {
                    sql = "select * from fee where 车位编号 = '" + textBox2.Text + "'"; 
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from fee where 车牌号码 like '" + textBox1.Text + "%'";
                }
                else
                {
                    sql = "select * from fee where 车牌号码 like '" + textBox1.Text + "%' and 车位编号 = '" + textBox2.Text + "'";
                }
            }

            return sql;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Form5 f = new Form5();
            f.Show();
        }
    }
}
