using MySql.Data.MySqlClient;
using System;

namespace MySQL
{
    class Program
    {
        private const string Server = "165.194.49.169";
        private const string Port = "8080";
        private const string Database = "test1";
        private const string Userid = "user1";
        private const string Password = "1234";
        static void Main(string[] args)
        {
            DBconnection();
        }
        #region DBconnection();
        private static void DBconnection()
        {
            using (MySqlConnection connection = new MySqlConnection("Server="+Server+";Port="+Port+ ";Database="+ Database + ";Uid="+ Userid + ";Pwd="+ Password + ";")) //DB 연결 구문
            {
                try//예외 처리
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_tb"; //쿼리 구문

                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader table = cmd.ExecuteReader();
                    //table.
                    while (table.Read())
                    {
                        Console.WriteLine("{0}", table["userid"]);
                    }
                    table.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        #endregion

    }
}
