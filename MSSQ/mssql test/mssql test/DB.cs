using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace mssql_test
{
    class DB
    {
        SqlDataReader dr;
        SqlConnection con;
        string MSsql = "Server=DESKTOP-8OBVK29;uid=201495002;pwd=2014;database=test;";
  
        //MSSQL Connection
        public void connectDB()
        {
            try
            {
                con = new SqlConnection(MSsql);
                con.Open();
                Console.WriteLine("DB 연결");
            }
            catch (Exception e) {
                Console.WriteLine("DB 연결실패");
                Console.WriteLine(e.Message);
            }
        }

        #region DDL CREATE ALTER DROP
    
        public void CREATE()
        {
            SqlCommand cmd = new SqlCommand(
                "CREATE TABLE table1 ("+
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
        }
   
        public void ALTER()
        {
            SqlCommand cmd = new SqlCommand("ALTER TABLE table2 DROP column Money", con);
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
        }

        public void DROP(){
            SqlCommand cmd = new SqlCommand("DROP TABLE test_table", con);
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
        }
    #endregion

     #region DML SELECT, INSERT DELETE

    /* MSSQL SELECT Query */
        public void SELECT()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM table1", con);
           try
            {
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.Write(String.Format(" {0}, {1}", dr[0], dr[1]));
                    Console.Write("\n");
                  
                }
                Console.WriteLine("SELECT 정상적 작동");
            }
            catch (Exception e) {
                Console.WriteLine("SELECT 오류");
                Console.WriteLine(e.Message);
            }
        }

      

        //MSSQL INSERT Query
        public void Insert()
        {
            SqlCommand cmd = new SqlCommand(
            "insert into table1 values(0,'a',0.2)"+
            "insert into table1 values(1, 'ab', 0.7)" +
            "insert into table1 values(2,'cc',0.1)" +
            "insert into table1 values(3,'ag',0.2)" +
            "insert into table1 values(4,'aq',0.7)" +
            "insert into table1 values(5,'cj',0.1) " 
                , con) ;
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
        }

        //MSSQL DELETE Query
        public void Delete() {
            SqlCommand cmd = new SqlCommand("DELETE FROM table1 WHERE idk =1;", con);
            try
            {
                dr = cmd.ExecuteReader();
                Console.WriteLine("DELETE 정상적 작동");
            }
            catch (Exception e) {
                Console.WriteLine("DELETE 에러");
                Console.WriteLine(e.Message);
            }
          
        }

        #endregion

        //MSSQL close
        public void closeDB()
        {
            con.Close();
            Console.WriteLine("종료");
        }

        /*
       테이블에 있는 개수가 몇개인지 모르기 때문에 List나 Queue를 사용해줍니다.
       배열에 넣기 편하게 하기위해서 선입선출 방식인 Queue를 사용해줍니다.
      */
        public int[] tagetSELECT()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Taget", con);
            Queue<object> taget = new Queue<object>();  // mssql 쿼리는 object 타입 입니다.
            int[] array = null;                         // 초기화 안하면 return에서 에러가 발생합니다. 
                                                        // *try catch문을 실행하지 않을 수도 있기 때문입니다.
            try
            {
                dr = cmd.ExecuteReader();               // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                while (dr.Read())
                {
                    taget.Enqueue(dr[1]);               // [0] = Sequence Number, [1] = Value
                }

                array = new int[taget.Count];           // queue의 크기만큼 배열의 크기를 잡아 줍니다.
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (int)taget.Dequeue();    // 배열에 queue의 값을 넣어줍니다.
                }
                Console.WriteLine("SELECT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("SELECT 오류");
                Console.WriteLine(e.Message);           // 에러 메시지 출력
            }
            dr.Close();                                 // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.

            return array;
        }


        /*
            행의 개수는 Query문을 사용하고
            열의 개수는 FieldCount를 사용합니다.
       */
        public double[,] WeightSELECT()
        {
            int count = countSELECT("Weight");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Weight", con);
            Queue<object> taget = new Queue<object>();              // mssql 쿼리는 object 타입 입니다.
            double[,] array = null;                                  // 초기화 안하면 return에서 에러가 발생합니다. 
                                                                     // *try catch문을 실행하지 않을 수도 있기 때문입니다.
            try
            {
                dr = cmd.ExecuteReader();                           // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                array = new double[count, dr.FieldCount - 1];          // FieldCount-1 하는 이유 -> 0째는 idk이기 때문에 사용하지 않습니다.

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    dr.Read();                                  //다음 레코드로 넘어가는 함수입니다.
                    for (int j = 1; j < array.GetLength(1) + 1; j++)
                    {
                        array[i, j - 1] = dr.GetFieldValue<double>(j);
                    }
                }

                Console.WriteLine("SELECT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("SELECT 오류");
                Console.WriteLine(e.Message);           // 에러 메시지 출력
            }
            dr.Close();                                 // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.

            return array;
        }

        public int countSELECT(string table_name)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM " + table_name, con);
            int count = 0;
            try
            {
                dr = cmd.ExecuteReader();               // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                dr.Read();                              // 다음 레코드 읽기
                count = dr.GetFieldValue<int>(0);
                Console.WriteLine(dr.GetFieldType(0));
                Console.WriteLine(dr.GetFieldValue<int>(0));
                Console.WriteLine("SELECT 정상적 작동");
            }
            catch (Exception e)
            {
                Console.WriteLine("SELECT 오류");
                Console.WriteLine(e.Message);           // 에러 메시지 출력
            }
            dr.Close();                                 // 닫아줘야 다른 SELECT 구문을 실행할 수 있습니다.
            return count;
        }
        public void TEST()
        {
            int count = countSELECT("Weight");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Weight", con);
            Queue<object> taget = new Queue<object>();  // mssql 쿼리는 object 타입 입니다.
            double[,] array = new double[count, dr.FieldCount];                         // 초기화 안하면 return에서 에러가 발생합니다. 
                                                                                        // *try catch문을 실행하지 않을 수도 있기 때문입니다.
            try
            {
                dr = cmd.ExecuteReader();               // 추출, 읽기 => 구문을 보내고 읽어오는 역할을 합니다.
                Console.WriteLine("행 수준" + dr.FieldCount); // 기본 필드 수
                Console.WriteLine(dr.VisibleFieldCount);    // 숨긴 상태가 아닌 필드수
                //Console.WriteLine("중첩 수준" + dr.GetFieldType(1));
                Console.WriteLine(dr.GetName(0));//idk
                Console.WriteLine(dr.RecordsAffected);

                for (int i = 0; i < array.GetLength(0); i++)
                {
                    dr.Read();
                    for (int j = 1; j < array.GetLength(1) + 1; j++)
                    {
                        array[i, j - 1] = dr.GetFieldValue<double>(j);
                    }
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


    }
}
