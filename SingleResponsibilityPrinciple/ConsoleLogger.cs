using System;
using System.IO;
using System.Xml.Linq;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        private readonly string logFilePath;

        public ConsoleLogger(string logFilePath = "log.xml")
        {
            this.logFilePath = logFilePath;

            // Initialize log file if it doesn't exist
            if (!File.Exists(logFilePath))
            {
                new XDocument(new XElement("logs")).Save(logFilePath);
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            Console.WriteLine("WARN: " + formattedMessage);
            LogToXml("WARN", formattedMessage);
        }

        public void LogInfo(string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            Console.WriteLine("INFO: " + formattedMessage);
            LogToXml("INFO", formattedMessage);
        }

        private void LogToXml(string type, string message)
        {
            var logEntry = new XElement("log",
                new XElement("type", type),
                new XElement("message", message)
            );

            XDocument logDoc = XDocument.Load(logFilePath);
            logDoc.Root?.Add(logEntry);
            logDoc.Save(logFilePath);
        }
    }
}