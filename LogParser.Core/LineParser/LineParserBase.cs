using System.Collections.Generic;
using LogParser.Core.Interfaces;

namespace LogParser.Core.LineParser
{
    internal abstract class LineParserBase : ILineParser
    {
        protected readonly List<string> ParsingErrorsMessages = new();
        public List<LineParserRule> LineParserRules { get; private set; } = new();

        public abstract void ParseLine(int lineNumber, string line);

        public List<string> Errors => ParsingErrorsMessages;
        
        protected void LogRuleRequirementNotMetError(int lineNumber, in LineParserRule rule)
        {
            ParsingErrorsMessages.Add($"Line {lineNumber}: {rule.PropertyName} {rule.ErrorMessage}");
        }
    }
}