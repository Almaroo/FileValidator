using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LogParser.Core.Interfaces;
using LogParser.Core.LineParser;

using static LogParser.Core.LineParser.LineParserPredefinedRules;

namespace LogParser.Services
{
    public class LogFileParserService
    {
        private readonly ILineParser _lineParser;

        public LogFileParserService()
        {
            _lineParser = new CsvLineParserBuilder()
                .WithSeparator(';')
                .WithProperty("Event-Name", "Empty or longer than 32 letters", NotNullOrEmpty, MaxLength(32))
                .WithProperty("Event-Description", "Empty or longer than 255 letters", NotNullOrEmpty, MaxLength(255))
                .WithProperty("Start-Date-Time", "Datetime does not match given format",
                    DateFormat("yyyy-MM-ddTHH:mmzzz"))
                .WithProperty("End-Date-Time", "Datetime does not match given format",
                    DateFormat("yyyy-MM-ddTHH:mmzzz"))
                .Build();
        }

        public async Task ParseFile(StreamReader uploadedFile)
        {
            _lineParser.Errors.Clear();
            
            var lineNumber = 1;
            string line;

            while ((line = await uploadedFile.ReadLineAsync()) != null)
            {
                _lineParser.ParseLine(lineNumber, line);
                lineNumber++;
            }
        }

        public List<string> ParsingErrorMessages => _lineParser.Errors;
        
    }
}