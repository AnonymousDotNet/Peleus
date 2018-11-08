using SqlUtil;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Peleus
{
    class SqlClient
    {
        public static int GetData(string sql)
        {
            //SqlConnection con = MsSqlUtil.Connect(Config.DB);
            //string id = null;
            //try
            //{
            ////获取Id
            //DataTable dt = MsSqlUtil.Get(con, sql);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    id = dr["Id"].ToString();
            //}
            //SqlConnection con = MsSqlUtil.Connect(Config.DB);



            string con = Config.DB;
            //创建一个新的连接，用using()是为了避免资源释放不及时导致的冲突或性能问题，减少因为争抢资源发生冲突或性能问题的概率
            //简单的说就是可以自动释放对象（托管资源）
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [CheHuiJia_Dev].[dbo].[Meta_Zone_Cities] WHERE id=Object_Id('AdminsBase')", connection))//执行T-SQL语句
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();//测试返回数据表行数
                        return rows;
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }


        //update
        public static int UpdateTask(string sql)
        {
            SqlConnection con = MsSqlUtil.Connect(Config.DB);
            int mark = 0;
            try
            {
                mark = MsSqlUtil.Set(con, sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            con.Close();
            return mark;
        }
    }
}

