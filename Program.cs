using System;
using System.Linq;

namespace ManagementInformation
{
    internal static class Program
    {
        static Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        private static void Main()
        {
            try
            {
                var eventLogs = ManagementInformationUtility.ManagementInformationExtractor("Win32_NTEventlogFile",
                    logFile => logFile["name"] == null ? "unknown" : logFile["name"].ToString()).ToList();

                Console.Out.WriteLine($"Event Logs: {string.Join(", ", eventLogs)}");
            }
            catch (Exception e)
            {
                e.Wrap().PrintMessage();
            }
        }
    }
}