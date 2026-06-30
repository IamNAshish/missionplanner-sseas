// 29june26_task2 
/*
 * created this file so that we dont use mainv2.cs for calling the webserver\SimpleWebserver.cs 
 * instead now mainv2.cs will call this file and it will handle remaining things
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPlanner.WebServer
{
    public static class WebServerManager
    {
        private static readonly SimpleWebServer server = new SimpleWebServer();

        public static void Start()
        {
            server.Start();
        }

        public static void Stop()
        {
            server.Stop();
        }
    }
}
