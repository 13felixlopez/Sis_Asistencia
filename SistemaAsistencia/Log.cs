using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencia
{
    public static class Log
    {
        public static void WriteLog(string message)
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now}:{message}");
                writer.WriteLine("---------------------------------------------------------");
            }
        }

        public static void WriteUser(string ms)
        {
            string logUser = ConfigurationManager.AppSettings["logUser"];
            using (StreamWriter writer = new StreamWriter(logUser, true))
            {
                writer.WriteLine($"{DateTime.Now}:{ms}");
                writer.WriteLine("---------------------------------------------------------");
            }
        }
        public static void WriteCon(string ms)
        {
            string logCon = ConfigurationManager.AppSettings["logCon"];
            using (StreamWriter writer = new StreamWriter(logCon, true))
            {
                writer.WriteLine($"{DateTime.Now}:{ms}");
                writer.WriteLine("---------------------------------------------------------");
            }
        }
        public static void WritePerso(string ms)
        {
            string logPerso = ConfigurationManager.AppSettings["logPerso"];
            using (StreamWriter writer = new StreamWriter(logPerso, true))
            {
                writer.WriteLine($"{DateTime.Now}:{ms}");
                writer.WriteLine("---------------------------------------------------------");
            }
        }
        public static void Writeerror(string ms)
        {
            string logerror = ConfigurationManager.AppSettings["logerror"];
            using (StreamWriter writer = new StreamWriter(logerror, true))
            {
                writer.WriteLine($"{DateTime.Now}:{ms}");
                writer.WriteLine("---------------------------------------------------------");
            }
        }
    }
}
