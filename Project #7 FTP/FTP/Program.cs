using System;
using System.IO;
using System.Net;

namespace FTP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FtpUpload();
        }

        public static void FtpUpload()
        {
            string ftpPath = "ftp://127.0.0.1:21/test.pdf";                 // FTP 서버에서 파일 저장 경로 FTP 서버 URL/파일 이름
            string user = "qweqr";                                          // FTP 아이디 설정
            string pwd = "123";                                             // FTP 비밀번호 설정
            string inputFile = "example.pdf";                               // 내 컴퓨터에서 보내는 파일의 경로 [FTP\FTP\bin\Debug\net5.0\example.pdf]
            byte[] buffer = new byte[2048];                                 // overflower 뜨면 여기 배열 고려해봐야함

            FileInfo fileInfo = new FileInfo(inputFile);                    // 파일 읽기 [파일정보]
            FileStream fileStream = fileInfo.OpenRead();                    // 파일 -> 스트림 [Stream 형식]
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath);  // URL 연동
            req.Credentials = new NetworkCredential(user, pwd);             // 아이디와 비밀번호 확인
            req.Method = WebRequestMethods.Ftp.UploadFile;                  // 파일 업로드

            Stream uploadStream = req.GetRequestStream();                   // Stream 형식으로 받을거다
            int contentLength = fileStream.Read(buffer, 0, buffer.Length);  // 파일 크기

            while (contentLength != 0)                                      // 내용이 없을때까지 반복
            {
                uploadStream.Write(buffer, 0, contentLength);               // 전달할 내용에 적기
                contentLength = fileStream.Read(buffer, 0, buffer.Length);  // 파일크기 -> 0이 될떄까지 반복해서 반복문을 나감.
            }
            uploadStream.Close();                                           
            fileStream.Close();
            req = null;
        }

    }
}
