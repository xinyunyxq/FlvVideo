
namespace http
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using System.IO;
    using System.Threading;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;
    using System.Drawing;

    public class httpquest
    {
        public static string GetHtml(string URL, out string cookie)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                string htmlContent = new StreamReader(response.GetResponseStream(), Encoding.Default).ReadToEnd();
                cookie = response.Headers.Get("Set-Cookie");

                return htmlContent;
            }
            catch
            {
                cookie = "";
                return "";
            }
        }



        public static string GetImageCode(PictureBox picBox, string Url, string cookie, out string curCookie)
        {
            Stream responseStream = null;
            Stream stream2 = null;
            Stream stream3 = null;
            try
            {
                int num;
                HttpWebRequest request2 = HttpWebRequest.Create(Url) as HttpWebRequest;
                request2.Method = "GET";
                request2.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";
                request2.Referer = Url;
                request2.Headers.Add("Accept-Language", "zh-cn");
                request2.Headers.Add("UA-CPU", "x86");
                request2.Headers.Add("Accept-Encoding", "gzip, deflate");
                request2.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.";
                request2.KeepAlive = true;
                request2.Headers.Add("Cookie", cookie);
                request2.AllowAutoRedirect = false;
                HttpWebResponse response2 = request2.GetResponse() as HttpWebResponse;
                responseStream = response2.GetResponseStream();


                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[0x400];
                    do
                    {
                        num = responseStream.Read(buffer, 0, buffer.Length);
                        if (num > 0)
                            ms.Write(buffer, 0, num);
                    }
                    while (num > 0);
                    curCookie = response2.Headers.Get("Set-Cookie");
                    byte[] buffer2 = ms.ToArray();
                    picBox.Image = Image.FromStream(ms);
                    responseStream.Close();
                    ms.Close();
                }



            }
            catch (Exception exception)
            {
                exception.ToString();
                curCookie = "";
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream3 != null)
                {
                    stream3.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
            }
            return "";
        }

        public static string GetImageCode_Create(PictureBox picBox, string Url, string cookie, out string curCookie)
        {
            WebResponse response = null;
            Stream responseStream = null;
            Stream stream2 = null;
            Stream stream3 = null;
            try
            {
                int num;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; CIBA)";
                request.KeepAlive = true;
                CookieContainer container = new CookieContainer();
                container.SetCookies(new Uri(Url), cookie);
                request.CookieContainer = container;
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                byte[] buffer = new byte[0x400];
                stream2 = System.IO.File.Create(@"L_baidu.JPG");
                stream3 = response.GetResponseStream();
                do
                {
                    num = stream3.Read(buffer, 0, buffer.Length);
                    if (num > 0)
                    {
                        stream2.Write(buffer, 0, num);
                    }
                }
                while (num > 0);
                curCookie = response.Headers.Get("Set-Cookie");
                Bitmap bitmap = new Bitmap(stream2);
                picBox.Image = bitmap;
                stream2.Close();
                stream3.Close();
            }
            catch (Exception exception)
            {
                exception.ToString();
                curCookie = "";
            }
            finally
            {
                if (stream2 != null)
                {
                    stream2.Close();
                }
                if (stream3 != null)
                {
                    stream3.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
            }
            return "";
        }


        public static string GetHtml(string URL, string cookie)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                request.Headers["Cookie"] = cookie;

                string htmlContent = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

                return htmlContent;
            }
            catch
            {
                return "";
            }
        }


        public static string POSTHtml(string URL, string PostStr, string Referer, string cookie, string Encode, out string COOKIE)
        {
            Stream requestStream = null;
            Stream responseStream = null;
            StreamReader reader = null;
            string str2;
            string outcookie = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding(Encode);
                byte[] bytes = encoding.GetBytes(PostStr);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                //request.AllowAutoRedirect = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = Referer;
                ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();


                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; CIBA)";
                request.ContentLength = bytes.Length;
                CookieContainer container = new CookieContainer();
                container.SetCookies(new Uri(URL), cookie);
                request.CookieContainer = container;
                requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                requestStream.Close();
                WebResponse response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, encoding);
                char[] buffer = new char[0x400];
                int length = reader.Read(buffer, 0, 0x400);
                string htmlContent = null;
                while (length > 0)
                {
                    htmlContent = htmlContent + new string(buffer, 0, length);
                    length = reader.Read(buffer, 0, 0x400);
                }
                reader.Close();

                outcookie = response.Headers.Get("Set-Cookie");

                response.Close();

                str2 = htmlContent;
            }
            catch (Exception e)
            {
                //str2 = "";
                str2 = String.Format("{0}--{1}---{2}--{3}---{4}", e.Message, e.Data, e.Source, e.TargetSite, e.InnerException);
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            COOKIE = outcookie;
            return str2;
        }

        public static string GetCookie(string URL, string curcookie, bool AllowAutoRedirect, out string cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            CookieContainer container = new CookieContainer();
            container.SetCookies(new Uri(URL), curcookie);
            request.CookieContainer = container;
            request.AllowAutoRedirect = AllowAutoRedirect;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Encoding encoding = Encoding.Default;
            StreamReader reader = new StreamReader(responseStream, encoding);
            char[] buffer = new char[0x400];
            int length = reader.Read(buffer, 0, 0x400);
            StringBuilder builder = new StringBuilder();
            while (length > 0)
            {
                string str = new string(buffer, 0, length);
                builder.Append(str);
                length = reader.Read(buffer, 0, 0x400);
            }
            response.Headers.ToString();
            cookie = response.Headers.Get("Set-Cookie");
            response.Close();
            reader.Close();
            return builder.ToString();
        }





        public static string GetHeader(string URL, string curcookie, bool AllowAutoRedirect, string HeaderTag, out string ReturnHeader)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            CookieContainer container = new CookieContainer();
            container.SetCookies(new Uri(URL), curcookie);
            request.CookieContainer = container;
            request.AllowAutoRedirect = AllowAutoRedirect;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = new StreamReader(responseStream, encoding);
            char[] buffer = new char[0x400];
            int length = reader.Read(buffer, 0, 0x400);
            //StringBuilder builder = new StringBuilder();

            string builder = "";
            while (length > 0)
            {
                string str = new string(buffer, 0, length);
                builder += str;
                length = reader.Read(buffer, 0, 0x400);
            }
            response.Headers.ToString();
            ReturnHeader = response.Headers.Get(HeaderTag);
            response.Close();
            reader.Close();

            return builder;

        }

        public static void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static string GetHtml(string URL)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                string htmlContent = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();

                return htmlContent;
            }
            catch
            {
                return "";
            }
        }

        internal class AcceptAllCertificatePolicy : ICertificatePolicy
        {

            public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
            {
                return true;
            }
        }

    }


}
