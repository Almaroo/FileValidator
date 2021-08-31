using System.Linq;

namespace LogParser.Core.LineParser
{
    internal class CsvLineParser : LineParserBase
    {
        public char Separator { get; set; } = ';';
        public override void ParseLine(int lineNumber, string line)
        {
            var tokens = line.Split(Separator);
            if (tokens.Length != LineParserRules.Count)
            {
                LogIncorrectLineStructureError(lineNumber);
                return;
            }

            for (var i = 0; i < tokens.Length; i++)
            {
                var isTokenValid = this.LineParserRules[i].ValidationPredicates
                    .Aggregate(true, ((result, func) => result && func(tokens[i])));

                if (!isTokenValid)
                {
                    LogRuleRequirementNotMetError(lineNumber, this.LineParserRules[i]);
                }
            }
        }
        private void LogIncorrectLineStructureError(int lineNumber)
        {
            ParsingErrorsMessages.Add($"Line {lineNumber}: Line does not match given set of rules");
        }
    }
}