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
    public partial class Z : Form
    {
        public Z()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //查询本地数据库是否有该车主信息 ，如果没有就录入信息以便下次登记。
            String sql = "select 车主姓名 from car where 车牌号码 = '" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            if ((String)cmd.ExecuteScalar() == null)
            {
                sql = "insert into car values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                cmd.CommandText = sql;
                cmd.ExecuteScalar();
                MessageBox.Show("录入成功！");
            }
            
            //判断改车主是否有固定车位， 有则直接录入停车信息 ，没有则分配自由车位再录入停车信息
            string sql1 = "select 车主姓名 from staycar where 车牌号码 = '" + textBox1.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            if ((String)cmd1.ExecuteScalar() == null) //分配自由车位并录入停车记录
            {
                int flag = 0;
                int i;
                for (i = 501; i < 699; i++)
                {
                    if (flag != 0)
                    {
                        break;
                    }
                    string s = Convert.ToString(i);
                    cmd1.CommandText = "Select 车牌号码 from parking where 车位编号 = '" + s + "'";
                    if ((String)cmd1.ExecuteScalar() == null)
                    {
                        flag = i;
                    }
                }
                if(flag == 0)
                {
                    MessageBox.Show("抱歉！自由车位已满！");
                }
                Random x = new Random();
                string s1 = "自由区" + (flag-500) + "号";
                sql = "insert into freecar values('" + flag.ToString() + "','" + s1 + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + x.Next(2, 8)*10 + "')";
                cmd.CommandText = sql;
                cmd.ExecuteScalar();

                cmd.CommandText = "insert into parking values('" + flag.ToString() + "','" + textBox1.Text + "',getdate(),'" + "" + "','" + "自由车位" + "','" + "1" + "','" + "0" + "')";
                cmd.ExecuteScalar();
                textBox7.Text = flag.ToString();
                textBox6.Text = s1;
                
            }
            else //根据固定车位录入停车信息
            {
                cmd.CommandText = "insert into parking values('" + textBox7.Text + "','" + textBox1.Text + "',getdate(),'" + "" + "','" + "固定车位" + "','" + "1" + "','" + "0" + "')";
                cmd.ExecuteScalar();
            }

            textBox9.Text = DateTime.Now.ToString();
            conn.Close();
        }


        //查询数据库中是否存在该车主信息
        private void button2_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            String sql = SqlFlush();
            if (sql == null)
            {
                MessageBox.Show("请输入查询条件");
            }
            else
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                if ((String)cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("无记录，请登记");
                }
                else
                {
                    textBox2.Text = (String)cmd.ExecuteScalar();

                    cmd.CommandText = "select 车牌号码 from car where 车主姓名 = '" + textBox2.Text + "'";
                    textBox1.Text = (String)cmd.ExecuteScalar();

                    cmd.CommandText = "select 车辆品牌 from car where 车主姓名 = '" + textBox2.Text + "'";
                    textBox3.Text = (String)cmd.ExecuteScalar();

                    cmd.CommandText = "select 车辆颜色 from car where 车主姓名 = '" + textBox2.Text + "'";
                    textBox4.Text = (String)cmd.ExecuteScalar();

                    cmd.CommandText = "select 车位编号 from freecar where 车牌号码 = '" + textBox1.Text + "'";
                    if ((String)cmd.ExecuteScalar() != null)
                    {
                        textBox7.Text = (String)cmd.ExecuteScalar();

                        cmd.CommandText = "select 车位位置 from freecar where 车牌号码 = '" + textBox1.Text + "'";
                        textBox6.Text = (String)cmd.ExecuteScalar();
                    }

                    cmd.CommandText = "select 车位编号 from staycar where 车牌号码 = '" + textBox1.Text + "'";
                    if ((String)cmd.ExecuteScalar() != null)
                    {
                        textBox7.Text = (String)cmd.ExecuteScalar();

                        cmd.CommandText = "select 车位位置 from staycar where 车牌号码 = '" + textBox1.Text + "'";
                        textBox6.Text = (String)cmd.ExecuteScalar();
                    }
                    cmd.CommandText = "select 进入时间 from parking where 车牌号码 = '" + textBox1.Text + "'";
                    if (cmd.ExecuteScalar() != null)
                    {
                        textBox9.Text = cmd.ExecuteScalar().ToString();
                    }
                    else
                    {
                        textBox9.Text = DateTime.Now.ToString();
                    }
                    textBox8.Text = "";

                }
                conn.Close();
            }
        }

        protected String SqlFlush()
        {
            String sql = "";
            if (textBox1.Text == "")
            {
                if (textBox2.Text == "")
                {
                    sql = "";
                }
                else
                {
                    sql = "select 车主姓名 from car where 车主姓名 = '" + textBox2.Text + "'";
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    sql = "select 车主姓名 from car where 车牌号码 = '" + textBox1.Text + "'";
                }
                else
                {
                    sql = "select 车主姓名 from car where 车主姓名 = '" + textBox2.Text + "' and 车牌号码 = '" + textBox1.Text + "'";
                }
            }

            return sql;
        }

        //结算当前停车费用
        private void button3_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            string sql1 = "select 车主姓名 from staycar where 车牌号码 = '" + textBox1.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            if ((String)cmd1.ExecuteScalar() == null)
            {
                string sql = "select 收费价格 from freecar where 车牌号码 = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                int price = Convert.ToInt32(cmd.ExecuteNonQuery());

                DateTime end1 = DateTime.Now;
                cmd1.CommandText = "select 进入时间 from parking where 车牌号码 = '" + textBox1.Text + "'";
                DateTime start1 = Convert.ToDateTime(cmd1.ExecuteScalar());
                TimeSpan ti = start1 - end1;
                String fee = "";
                if (ti.Hours == 0)
                {
                    fee = "30";
                }
                fee = Convert.ToString(ti.Seconds * price);
                textBox8.Text = fee;

                cmd.CommandText = "update parking Set 收费总额 = '" + fee + "' where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "update parking Set 在位情况 = 0 where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                cmd.CommandText = "update parking Set 离开时间 = 'getdate()' where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                Random x = new Random();
                cmd.CommandText = "insert into fee values('" + textBox7.Text + "','" + textBox1.Text + "',getdate(),'" + fee + "','" + x.Next(11111111,99999999) + "')";
                cmd.ExecuteScalar();
            }
            else
            {
                string sql = "select 车位余额 from staycar where 车牌号码 = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql1, conn);
                int rest = Convert.ToInt32(cmd.ExecuteScalar());

                DateTime end1 = DateTime.Now;
                cmd1.CommandText = "select 进入时间 from parking where 车牌号码 = '" + textBox1.Text + "'";
                DateTime start1 = Convert.ToDateTime(cmd1.ExecuteScalar());
                TimeSpan ti = start1 - end1;
                String fee = "";
                if (ti.Hours == 0)
                {
                    fee = "20";
                }
                fee = Convert.ToString(ti.Hours * 20);
                textBox8.Text = fee;

                cmd.CommandText = "update parking Set 收费总额 = '" + fee + "' where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                cmd.CommandText = "update parking Set 在位情况 = 0 where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                cmd.CommandText = "update parking Set 离开时间 = 'getdate()' where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                rest -= ti.Hours * 20;
                if(rest <= 20)
                {
                    MessageBox.Show("余额不足，请充值！");
                }
                if(rest < 0)
                {
                    MessageBox.Show("您已欠费，请充值！");
                }

                cmd.CommandText = "update staycar Set 车位余额 = '" + rest + "'where 车牌号码 = '" + textBox1.Text + "'";
                cmd.ExecuteScalar();

                Random x = new Random();
                cmd.CommandText = "insert into fee values('" + textBox7.Text + "','" + textBox1.Text + "',getdate(),'" + fee + "','" + x.Next(11111111,99999999) + "')";
                cmd.ExecuteScalar();
            }

            MessageBox.Show("付款成功！祝您一路顺风！");
            conn.Close();

        }
    }
}
