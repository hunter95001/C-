using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gauss
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 저장하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savestrFilename;
            saveFileDialog1.Title = "이미지 파일저장...";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Filter = "JPEG File(*.jpg)|*.jpg|Bitmap File(*.bmp)|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savestrFilename = saveFileDialog1.FileName;
                chart1.SaveImage(savestrFilename, ChartImageFormat.Jpeg);
            }

        }

        private void 랜덤발생ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] Init = new double[10000];
            double[] count = new double[10];
            count.Initialize();
            #region Random
            //Random random = new Random();
            //for (int x = 0; x < Init.Length; x++)
            //{
            //    Init[x] = 2*random.NextDouble()-1;
            //}
            #endregion

            #region 가우스 랜덤
            CGauss gauss = new CGauss();
            Init = gauss.NextDouble(0.5, 0, Init.Length);
            #endregion


            Array.Sort(Init); //정렬
            Series series1 = new Series();
            chart1.Series.Clear();
            chart1.Titles.Clear();
            for (int x = 0; x < Init.Length; x++)
            {
                series1.Points.AddXY(x.ToString(), Init[x]);
            }
            chart1.Series.Add(series1);                                 //chart1.Series를 초기화 합니다.
          
        }
    }
}
