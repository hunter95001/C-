using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace mssql_test
{
    class TestDB
    {
        SqlDataReader dr;
        SqlConnection con;
        string MSsql = "Server=DESKTOP-8OBVK29;uid=201495002;pwd=2014;database=연습용;";

        public void connectDB()
        {
            try
            {
                con = new SqlConnection(MSsql);
                con.Open();
                Console.WriteLine("DB 연결");
            }
            catch (Exception e)
            {
                Console.WriteLine("DB 연결실패");
                Console.WriteLine(e.Message);
            }
        }

        #region DDL CREATE ALTER DROP
        public void CREATE()
        {
            SqlCommand cmd = new SqlCommand(
                "CREATE TABLE table1 (" +
                "idk int PRIMARY KEY," +
                "name char(10) NULL," +
                "money float NULL," +
                    ")", con);

            try
            {
                dr = cmd.ExecuteReader();

                Console.WriteLine("DROP TABLE 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("DROP TABLE 오류");
                Console.WriteLine(e.Message);
            }
            dr.Close();
        }

        public void ALTER()
        {
            SqlCommand cmd = new SqlCommand("ALTER TABLE table1 DROP column Money", con);
            try
            {
                dr = cmd.ExecuteReader();

                Console.WriteLine("DROP TABLE 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("DROP TABLE 오류");
                Console.WriteLine(e.Message);
            }
            dr.Close();
        }

        public void DROP()
        {
            SqlCommand cmd = new SqlCommand("DROP TABLE table1", con);
            try
            {
                dr = cmd.ExecuteReader();
                Console.WriteLine("DROP TABLE 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("DROP TABLE 오류");
                Console.WriteLine(e.Message);
            }
            dr.Close();
        }
        #endregion

        #region DML SELECT, INSERT DELETE

        public void INSERT()
        {
            SqlCommand cmd = new SqlCommand(
            "insert into table1 values(0,'a',0.2)" +
            "insert into table1 values(1, 'ab', 0.7)" +
            "insert into table1 values(2,'cc',0.1)" +
            "insert into table1 values(3,'ag',0.2)" +
            "insert into table1 values(4,'aq',0.7)" +
            "insert into table1 values(5,'cj',0.1) "
                , con);
            try
            {
                dr = cmd.ExecuteReader();
                Console.WriteLine("INSERT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("INSERT 에러");
                Console.WriteLine(e.Message);
            }
            dr.Close();
        }

        //public void SELECT()
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT * FROM table1", con);
        //    try
        //    {
        //        dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            Console.Write(String.Format(" {0}, {1}", dr[0], dr[1]));
        //            Console.Write("\n");

        //        }
        //        Console.WriteLine("SELECT 정상적 작동");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("SELECT 오류");
        //        Console.WriteLine(e.Message);
        //    }
        //    dr.Close();
        //}

       

        public void DELETE()
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM table1 WHERE idk =1;", con);
            try
            {
                dr = cmd.ExecuteReader();
                Console.WriteLine("DELETE 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("DELETE 에러");
                Console.WriteLine(e.Message);
            }
            dr.Close();
        }
        #endregion

        public int countSELECT(string table_name)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM " + table_name, con);
            int count = 0;
            try
            {
                dr = cmd.ExecuteReader();               // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                dr.Read();                              // 다음 레코드 읽기
                count = dr.GetFieldValue<int>(0);
                Console.WriteLine("COUNT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("COUNT 오류");
                Console.WriteLine(e.Message);           // 에러 메시지 출력
            }
            dr.Close();                                 // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.
            return count;
        }
        
    
        public void SELECT()
        {
            string table = "table1";
            int count = countSELECT(table);                                     //테이블 명
            SqlCommand cmd = new SqlCommand("SELECT * FROM "+ table, con);      
            object[,] field = null;                                             //데이터가 제각각 이니 object로 묶어주셔야 합니다.
           
            try
            {
                dr = cmd.ExecuteReader();                                       // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                field = new object[count, dr.FieldCount];                       //FieldCount는 cmd.ExecuteReader() 후 확인 가능합니다.

                for (int i = 0; i < field.GetLength(0); i++)
                {
                    dr.Read();                                                  //다음 레코드[열]로 넘어가는 함수입니다.
                    for (int j = 0; j < field.GetLength(1) ; j++)               
                    {
                        field[i, j] = dr.GetFieldValue<object>(j);
                    }
                }

                for (int i = 0; i < field.GetLength(0); i++, Console.WriteLine())
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        Console.Write(field[i, j] + " ");
                    }


                Console.WriteLine("SELECT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("SELECT 오류");
                Console.WriteLine(e.Message);           // 에러 메시지 출력
            }
            dr.Close();                                 // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.

        }

        public void closeDB()
        {
            con.Close();
            Console.WriteLine("종료");
        }


    }
}
#region  C# 함수 설명
//public void SELECT()
//{
//    int count = countSELECT("table1");                                      //테이블 명을 적어주세요
//    SqlCommand cmd = new SqlCommand("SELECT * FROM table1", con);

//    try
//    {
//        dr = cmd.ExecuteReader();                                           // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
//        Console.WriteLine("열의 개수" + count);                             
//        Console.WriteLine("행의 개수" + dr.FieldCount);                    
//        Console.WriteLine("숨기지않은 행의 개수" + dr.VisibleFieldCount);   
//        Console.WriteLine("필드 타입 {0}, {1}, {2} ", dr.GetFieldType(0), dr.GetFieldType(1), dr.GetFieldType(2));
//        Console.WriteLine("필드 이름 {0}, {1}, {2} ", dr.GetName(0), dr.GetName(1), dr.GetName(2));

//        while (dr.Read())
//        {
//            Console.WriteLine(dr[0] + " " + dr[1] + " " + dr[2]);
//        }
//        Console.WriteLine("SELECT 정상적 작동");
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("SELECT 오류");
//        Console.WriteLine(e.Message);                                       // 에러 메시지 출력
//    }
//    dr.Close();                                                             // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.
//}
#endregion
#region #1 Queue[큐] 사용하기
//class test
//{                                                             // Queue에 넣을 컬랙션입니다.
//    int idk;                                                            // GetFieldType(0) int 
//    string name;                                                        // GetFieldType(1) string
//    double money;                                                       // GetFieldType(2) double
//    public test(object idk, object name, object money)                  // Query는 모드 object 타입입니다.
//    {
//        this.idk = (int)idk;
//        this.name = (string)name;
//        this.money = (double)money;
//    }
//    /** get Method **/
//    public int getidk()
//    {
//        return idk;
//    }
//    public string getname()
//    {
//        return name;
//    }
//    public double getmoney()
//    {
//        return money;
//    }
//}

//public void SELECT()
//{
//    SqlCommand cmd = new SqlCommand("SELECT * FROM table1", con);
//    Queue<test> queue = new Queue<test>();
//    try
//    {
//        dr = cmd.ExecuteReader();                                       // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.

//        /** Step #1 큐에 저장하기 **/
//        while (dr.Read())
//        {
//            queue.Enqueue(new test(dr[0], dr[1], dr[2]));               // 필드값을 큐에 넣어줍니다. 
//        }

//        /** Step #2 배열 생성**/
//        int[] idk = new int[queue.Count];
//        string[] name = new string[queue.Count];
//        double[] money = new double[queue.Count];
//        int count = queue.Count();                                      //Dequeue()를 실행하면 Count가 감소하기 때문에 주의!
//        Console.WriteLine("열의 개수 " + queue.Count);

//        /** Step #3 배열에 큐 값 저장**/
//        for (int i = 0; i < count; i++)                                 //Dequeue()를 실행하면 Count가 감소하기 때문에 
//        {                                                               // for (int i = 0; i< queue.Count(); i++)<- 오류 유발
//            test tt = queue.Dequeue();                                  //queue가 <test>이기 때문에 test 클래스로 받아줍니다.
//            idk[i] = tt.getidk();
//            name[i] = tt.getname();
//            money[i] = tt.getmoney();
//        }

//        /** Step #4 출력**/
//        for (int i = 0; i < count; i++)
//        {
//            Console.WriteLine("{0} {1} {2}", idk[i], name[i], money[i]);
//        }
//        Console.WriteLine("SELECT 정상적 작동");
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("SELECT 오류");
//        Console.WriteLine(e.Message);                                   // 에러 메시지 출력
//    }
//    dr.Close();                                                         // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.
//}
#endregion
#region #2 Query[쿼리] 사용하기
#endregion