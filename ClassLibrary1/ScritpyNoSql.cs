using System.Data.SqlClient;
using System.Configuration;
using System;

namespace NoSql
{
    public class ScritpyNoSql
    {
        public int SqlTest()
        {
            #region 使用using()的方式
            string con = ConfigurationManager.AppSettings["chj_data"];//获取配置文件里的数据库地址信息

            //创建一个新的连接，用using()是为了避免资源释放不及时导致的冲突或性能问题，减少因为争抢资源发生冲突或性能问题的概率
            //简单的说就是可以自动释放对象（托管资源）
            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand cmd = new SqlCommand(con))
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
            #endregion
            //SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["test"]);
            //try
            //{
            //using (SqlConnection connection = new SqlConnection(con))
            //{
            //    connection.Open();
            //    Console.WriteLine("open database successfully!!!");
            //}
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);//输出错误信息
            //}

        }
        private void NoSql()
        {
            SqlConnection sql = new SqlConnection(ConfigurationManager.AppSettings["chj_data"]);
            try
            {
                sql.Open();
                Console.WriteLine("open database successfully!!!");
            }
            catch (SqlException ex)
            {

            }
        }

    }
}
