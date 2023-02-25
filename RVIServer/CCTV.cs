using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RVIServer
{
    internal class CCTV
    {
        public static string errMessage;
        public static string PhotosPath;
        public static Boolean debugMode;
        public static string Username;
        public static string Password;
        public static string CCTVConfigFilename;
        public static string Location;
        public static async Task TakePicEach(string flowTimeAndCarId)
        {
            int i_str = 0;

            CreateFolder(PhotosPath, flowTimeAndCarId);
            List<string> CCTV_Config = Read_CCTV_CGI_Config();
            //for (int i = 1; i <= 13; i++)
            //{
            //    i_str = i.ToString().PadLeft(2, '0');
            //    url = $"http://192.168.7.1{i_str}/images/snapshot.jpg";
            //    errMessage = HttpGetRequest_SaveCamPic(username, password, url, i_str, flowTimeAndCarId, PhotosPath);
            //}
            //i_str = "14";
            //url = $"http://192.168.7.1{i_str}/PictureCatch.cgi?username={username}&password={password}&channel=1";

            foreach (string url in CCTV_Config)
            {
                errMessage = HttpGetRequest_SaveCamPic(Username, Password, url, i_str.ToString(), flowTimeAndCarId, PhotosPath);
                i_str++;
            }

            //string url = CCTV_Config.ElementAt(0).ToString();
            //Task task = TakePicOne(url, i_str.ToString(), flowTimeAndCarId);

            //for (int i = 1; i < CCTV_Config.Count; i++)
            //{
            //    url = CCTV_Config.ElementAt(i).ToString();
            //    task = TakePicOne(url, i_str.ToString(), flowTimeAndCarId);
            //    i_str++;
            //}
            //await task;

            //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini")
        }
        static public async Task TakePicOne(string url, string i_str, string flowTimeAndCarId)
        {
            errMessage = HttpGetRequest_SaveCamPic(Username, Password, url, i_str.ToString(), flowTimeAndCarId, PhotosPath);
            await Task.Delay(100);
        }
        static void CreateFolder(string photosPath, string flowTimeAndCarId)
        {
            try
            {
                string foldrePath = "";
                foldrePath = photosPath + $@"\\{flowTimeAndCarId}";
                //判斷檔案路徑是否存在，不存在則建立資料夾 
                if (!System.IO.Directory.Exists(foldrePath))
                {
                    System.IO.Directory.CreateDirectory(foldrePath);//不存在就建立目錄 
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
                if (debugMode) File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload_err_log.txt"), DateTime.Now + " " + errMessage.ToString() + "\n");
            }
        }
        static List<string> Read_CCTV_CGI_Config()
        {
            string myLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CCTVConfigFilename);
            List<string> allLines = new List<string>();
            try
            {
                using (var fs = new FileStream(myLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var logFileReader = new StreamReader(fs, ASCIIEncoding.Default))
                {
                    while (!logFileReader.EndOfStream)
                    {
                        allLines.Add(logFileReader.ReadLine());
                    }

                }

            }
            catch (Exception ex)
            {
            }
            return allLines;

        }

        static string HttpGetRequest_SaveCamPic(string username, string password, string url, string i_str, string flowTimeAndCarId, string folderPath)
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential(username, password);
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Method = "GET";
            request.ContentType = "image/jpeg";

            try
            {
                //看到.GetResponse()才代表真正把 request 送到 伺服器
                using (FileStream fs = new FileStream($@"{folderPath}{flowTimeAndCarId}//{flowTimeAndCarId}_cam{i_str}.jpeg", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (var sr = response.GetResponseStream())
                        {
                            // sr 就是伺服器回覆的資料
                            sr.CopyTo(fs);
                            return $"{DateTime.Now} Photos Saved.\r\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMessage = ex.ToString();
                if (debugMode) File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload_err_log.txt"), DateTime.Now + " " + errMessage.ToString() + "\n");
                return errMessage;
            }
        }
    }
}
