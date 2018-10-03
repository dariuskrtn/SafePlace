using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePlace.Service
{
    class ConsoleLogger : ILogger
    {

        public void LogInfo(string message)
        {
            Log("Info: " + message);
        }
        public void LogWarning(string message)
        {
            Log("Warning: " + message);
        }
        public void LogError(string message)
        {
            Log("Error: " + message);
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
