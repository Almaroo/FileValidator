using System.Collections.Generic;

namespace LogParser.Core.Interfaces
{
    public interface ILineParser
    {
        void ParseLine(int lineNumber, string line);
        List<string> Errors { get; }
    }
}