using System;
using System.Collections.Generic;
using System.Text;
using Prism.Logging;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class XunitLogger : ILoggerFacade, ILogger
    {
        private ITestOutputHelper _testOutputHelper { get; }

        public List<string> Logs => new List<string>();

        public XunitLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Log(string message, Category category, Priority priority)
        {
            var output = $"{category} - {priority}: {message}";
            Logs.Add(output);
            _testOutputHelper.WriteLine(output);
        }

        private void Write(object message, IDictionary<string, string> additionalInfo)
        {
            var output = $"{message}\n";
            if (additionalInfo != null)
                foreach (var pair in additionalInfo)
                    output += $"    {pair.Key}: {pair.Value}\n";

            Logs.Add(output);
            _testOutputHelper.WriteLine(output);
        }

        public void Log(string message, IDictionary<string, string> additionalInfo)
        {
            Write(message, additionalInfo);
        }

        public void Debug(object message, IDictionary<string, string> additionalInfo)
        {
            Write(message, additionalInfo);
        }

        public void Info(object message, IDictionary<string, string> additionalInfo)
        {
            Write(message, additionalInfo);
        }

        public void Report(Exception message, IDictionary<string, string> additionalInfo)
        {
            Write(message, additionalInfo);
        }

        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            Write(name, properties);
        }
    }
}
