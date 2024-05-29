namespace LoggerService
{
    public class LoggerManager: ILoggerManager
    {
        public LoggerManager() { }

        public void LogError(string message)
        {
            Console.WriteLine("ERROR {1}: {2}\n", DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy "), message);
        }

        public void LogFatal(string message)
        {
            Console.WriteLine("FATAL {1}: {2}\n", DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy "), message);
        }

        public void LogInfo(string message) { 
            Console.WriteLine("INFO {1}: {2}\n", DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy "), message);
        }

        public void LogWarn(string message)
        {
            Console.WriteLine("WARN {1}: {2}\n", DateTime.Now.ToString("hh:mm:ss dd-MM-yyyy "), message);
        }
    }
}
