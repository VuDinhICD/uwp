using Serilog;
using System.Diagnostics;
using System.IO;
using Windows.Storage;

namespace Logger
{
    public class Logging
    {
        const string fileLog = "logs\\log.txt";

        public static string LogPath => Path.Combine(ApplicationData.Current.LocalFolder.Path, fileLog);

        public static void Info(string message)
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileLog);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information(message);
            Log.CloseAndFlush();

            Debug.WriteLine($"Info: {message}");
        }

        public static void Error(string message)
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileLog);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Error(message);
            Log.CloseAndFlush();

            Debug.WriteLine($"Error: {message}");
        }

        public static void Json(object Object) {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, fileLog);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information(Newtonsoft.Json.JsonConvert.SerializeObject(Object));
            Log.CloseAndFlush();

            Debug.WriteLine($"Json: {Newtonsoft.Json.JsonConvert.SerializeObject(Object)}");
        }
    }
}
