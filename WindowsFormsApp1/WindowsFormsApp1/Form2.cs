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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connstr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            try
            {
                String SqlStr1 = "select 员工编号 from manager where 员工编号 = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(SqlStr1, conn);
                if (cmd.ExecuteScalar() == null)
                    MessageBox.Show("该用户名不存在！", "友情提示");
                else
                {
                    String Sql = "Select 员工密码 From manager where 员工编号 = '" + textBox1.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(Sql, conn);

                    String st = textBox2.Text;
                    int l1 = st.Length;
                    String st1 = (String)cmd.ExecuteScalar();
                    int l2 = st1.Length;

                    for (int i = 0; i < l2 - l1; i++)
                        st += ' ';
                    if (st == st1)
                    {
                        MessageBox.Show("登录成功！", "友情提示");
                        this.Close();
                        Form5 form = new Form5();
                        form.Show();

                    }
                    else
                    {
                        MessageBox.Show("密码不正确！", "友情提示");
                    }
                }
            }
            catch
            {
                MessageBox.Show("登陆失败！", "友情提示");
            }
            String sql = "select* from parking where 员工编号 = '" + textBox1.Text + "'";

            conn.Close();
           // MessageBox.Show("登录成功！", "友情提示");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Close();
            Form3 form = new Form3();
            form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
