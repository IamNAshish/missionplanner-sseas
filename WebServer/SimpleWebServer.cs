// 29june26_task1
/*
 * this classs is written by me for creating local webserver // 29june26_task1
 */

using System;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;

namespace MissionPlanner.WebServer
{
    public class SimpleWebServer
    {
        private HttpListener listener;
        private Thread serverThread;
        private bool running;

        public void Start()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");

            listener.Start();

            running = true;

            serverThread = new Thread(ListenLoop);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void ListenLoop()
        {
            while (running)
            {
                try
                {
                    var context = listener.GetContext();
                    ProcessRequest(context);


                }
                catch (HttpListenerException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            // 30june2026_step2 commented string html = "<html><body><h1>Mission Planner Web Server Running</h1></body></html>";

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebUI", "index.html");// 30june2026_step2
            string html = File.ReadAllText(path);// 30june2026_step2
            byte[] buffer;
            string requestPath = context.Request.Url.AbsolutePath;

            if (requestPath.StartsWith("/api/")) // 30june2026_step3
            {
                HandleApiRequest(context, requestPath);
                return;
            }
                  


            if (requestPath == "/")
                requestPath = "/index.html";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebUI", requestPath.TrimStart('/'));
            if (!File.Exists(filePath))
            {
                context.Response.StatusCode = 404;
                context.Response.Close();
                return;
            }
            buffer = File.ReadAllBytes(filePath);
            string extension = Path.GetExtension(filePath);

            switch (extension)
            {
                case ".html":
                    context.Response.ContentType = "text/html";
                    break;

                case ".css":
                    context.Response.ContentType = "text/css";
                    break;

                case ".js":
                    context.Response.ContentType = "application/javascript";
                    break;

                default:
                    context.Response.ContentType = "application/octet-stream";
                    break;
            }


            //byte[] buffer = Encoding.UTF8.GetBytes(html);

            context.Response.ContentType = "text/html";
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }
        private void HandleApiRequest(HttpListenerContext context, string requestPath)// 30june2026_step3
        {
            if (requestPath == "/api/test") 
            {
                string json =
                    "{\"message\":\"Hello from Mission Planner!\"}";

                byte[] buffer = Encoding.UTF8.GetBytes(json);

                context.Response.ContentType = "application/json";

                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();

                return;
            }
        }
        public void Stop()
        {
            running = false;
            listener.Stop();
        }
    }
}