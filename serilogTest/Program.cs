using System;
using System.Net;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace SerilogExample
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(@"D:\logs\myapp.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Email(new EmailConnectionInfo
                {
                    FromEmail = "log.dever@gmail.com",
                    ToEmail = "log.dever@gmail.com",
                    MailServer = "smtp.gmail.com",
                    NetworkCredentials = new NetworkCredential
                    {
                        UserName = "",//Your email username
                        Password = ""//Your email password
                    },
                    EnableSsl = true,
                    Port = 587,
                    EmailSubject = "Error in app"
                },
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                batchPostingLimit: 100)
                .CreateLogger();

            Log.Information("Hello, world!");
            Log.Information("Hello, world!");

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error("Something went wrong");
                Log.Error(ex, "Something went wrong");
                Log.Error(ex, "Something went wrong");
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                Log.CloseAndFlush();
                Console.ReadLine();
            }
        }
    }
}