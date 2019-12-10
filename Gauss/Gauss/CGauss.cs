using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    class CGauss
    {

        public CGauss() {
            //int[] num = new int[] { 0, 10, 210, 300, 4040, 50, 6530, 700, 8000, 10000 }; //3668
            //int[] num = new int[] { 27, 8, 43, 1, 9, 50, 17, 69, 99, 57 }; //29
            //int[] num = new int[] { 30, 8, 43,34, 50, 50, 53, 59, 61, 82 }; //19
            //int[] num = new int[] { 57, 50, 52, 51, 50, 50, 53, 59, 56, 55 }; //3.1
            int[] num = new int[] { 50, 50, 50, 50, 50, 50, 50, 50, 51, 50 }; //0.3
            double[] disnum = new double[num.Length];

            int max = 0;        //총합 Total
            double avg = 0.0;   //평균 Average
            double dis = 0.0;   //분산 Dispersion
            double std = 0.0;   //표준편차 standard deviation
            //평균
            for (int x = 0; x < num.Length; x++)
            {
                max +=num[x];
            }
            Console.WriteLine("총합 : " + max);

            //평균
            avg = (double)max / num.Length;
            Console.WriteLine("평균 : " + avg);
            //분산
            for (int x = 0; x < num.Length; x++)
            {
                disnum[x] += num[x]-avg;
                
                Console.WriteLine(num[x]+" - "+ avg+" " +disnum[x]+" = "+Math.Pow(disnum[x],2));
                dis += Math.Pow(disnum[x], 2);
            }
            dis= dis / num.Length;
            Console.WriteLine("분산 : " + dis);

            //표준편차
            std = Math.Sqrt(dis);
            Console.WriteLine("표준편차 : " + std);
        }

        public double[] NextDouble(double alph,double avg, int size) 
        {
            Random random = new Random();
            double[] num = new double[size];
            double[] Elete = new double[size];

            double dis = 0.0;   //분산 Dispersion
            double std = 0.0;   //표준편차 standard deviation
            Elete.Initialize();

            do
            {
                dis = 0.0;   //분산 Dispersion
                for (int x = 0; x < num.Length; x++)
                {
                    if (Elete[x] == 0)
                    {
                        num[x] = Math.Round(2 * random.NextDouble() - 1, 1);  //-1~1사이의 값.
                    }
                }

                //분산
                for (int x = 0; x < num.Length; x++)
                {
                    if (alph+avg>= num[x] && num[x] >= avg- alph)  //중간 분포구역을 늘리기위해서. 0.2+평균 > 평균 >평균-0.2
                    {
                        Elete[x] = 1;
                        Console.WriteLine(num[x]);
                    }
                    double swap = num[x] - avg;
                    dis += Math.Pow(swap, 2);
                }
                dis = dis / num.Length;
                //표준편차
                std = Math.Sqrt(dis);
            } while (std >= alph || std == 0);
            Console.WriteLine("표준편차 : " + std);
            return num;
        }
    

    }

}
