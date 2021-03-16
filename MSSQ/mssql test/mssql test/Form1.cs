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
using System.Collections;

namespace mssql_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestDB db = new TestDB();
            db.connectDB();
            //db.CREATE();
            //db.ALTER();
            //db.DROP();
            //db.INSERT();
            //db.SELECT();
            //db.DELETE();
            db.SELECT();
            db.closeDB();


            //double[,] weight;
            //int[] taget;
            //DB db = new DB();
            //db.connectDB();
            //taget = db.tagetSELECT();
            //weight = db.WeightSELECT();
            //db.closeDB();

            //for (int i = 0; i < weight.GetLength(0); i++, Console.WriteLine())
            //    for (int j = 0; j < weight.GetLength(1); j++)
            //    {
            //        Console.Write(weight[i, j]);
            //    }

            //for (int i = 0; i < taget.Length; i++)
            //{
            //    Console.WriteLine(taget[i]);
            //}

        }

    }
}


