using System;
using System.Linq;
using LogParser.Core.Interfaces;

namespace LogParser.Core.LineParser
{
    public class CsvLineParserBuilder
    {
        private CsvLineParser _parser = new ();
        
        public ILineParser Build()
        {
            return _parser;
        }

        public CsvLineParserBuilder WithSeparator(char separator)
        {
            _parser.Separator = separator;
            return this;
        }
        public CsvLineParserBuilder WithProperty(string propertyName, string errorMessage, params Func<string, bool>[] validationPredicates)
        {
            _parser.LineParserRules.Add(new LineParserRule
            {
                PropertyName = propertyName,
                ErrorMessage = errorMessage,
                ValidationPredicates = validationPredicates.ToList(),
            });

            return this;
        }
        
    }
}