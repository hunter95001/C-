using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;

namespace PDFDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHello_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            //page.Size = PdfSharp.PageSize.A5;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);
            const string filename = "c:\\PDFDemoTemp\\ResultHelloWorld.pdf";
            document.Save(filename);
        }

        #region JPEG 파일을 PDF파일로
        private void btnJPGtoPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "C:\\PDFDemoTemp";
            dlg.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created using PDFsharp";

                foreach (string fileSpec in dlg.FileNames)
                {
                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    DrawImage(gfx, fileSpec, 0, 0, (int)page.Width, (int)page.Height);
                }
                if (document.PageCount > 0) document.Save(@"C: \Users\winst\Desktop\testResultOneImagePerPage.pdf");  //경로가 절대 주소여서 바꿔줘야 합니다.
            }
        }
        #endregion
        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        private void btnMultiplePDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "C:\\PDFDemoTemp";
            dlg.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created using PDFsharp";

                PdfPage page = document.AddPage();
                int StartX = 0 - (int)page.Width/2;
                int StartY = 0 - (int)page.Height / 2;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                foreach (string fileSpec in dlg.FileNames)
                {
                    DrawImage(gfx, fileSpec, StartX, StartY, (int)page.Width*2, (int)page.Height*2);
                    StartX += (int)page.Width / 2;
                    StartY += (int)page.Height / 2;
                }
                if (document.PageCount > 0) document.Save(@"C:\PDFDemoTemp\ResultZoomInOnImage.pdf");
            }
        }

        private void btnFitPageToImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "C:\\PDFDemoTemp";
            dlg.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created using PDFsharp";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
            
                foreach (string fileSpec in dlg.FileNames)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(fileSpec);
                    page.Width = img.Width;
                    page.Height = img.Height;
                    DrawImage(gfx, fileSpec, 0, 0, (int)page.Width, (int)page.Height);
                }
                if (document.PageCount > 0) document.Save(@"C:\PDFDemoTemp\ResultFitPageToImage.pdf");
            }
        }

        private void btnMultipleImagesPerPage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "C:\\PDFDemoTemp";
            dlg.Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dlg.Multiselect = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created using PDFsharp";

                PdfPage page = document.AddPage();
                int StartX = 0;
                int StartY = 0;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                foreach (string fileSpec in dlg.FileNames)
                {
                    DrawImage(gfx, fileSpec, StartX, StartY, (int)page.Width / 2, (int)page.Height / 2);
                    StartX += (int)page.Width / 2;
                    StartY += (int)page.Height / 2;
                }
                if (document.PageCount > 0) document.Save(@"C:\PDFDemoTemp\ResultMultipleImagesPerPage.pdf");
            }
        }

        #region 폴더에 있는 jpg 파일을 모두 읽어서 자동으로 PDF파일로 변환
        private void button1_Click(object sender, EventArgs e)
        {
            string pdfPath = "/"; //PDF 경로
            string pdfname = "test.pdf"; //PDF 파일 이름
            string[] jpgFiles = System.IO.Directory.GetFiles(pdfPath, "*.jpg"); //파일 읽어옴
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created using PDFsharp";

            for (int i = 0; i < jpgFiles.Length; i++)
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                DrawImage(gfx, jpgFiles[i], 0, 0, (int)page.Width, (int)page.Height);
            }
            if (document.PageCount > 0) document.Save(pdfPath + pdfname);
        }
        #endregion

    }
}
