using System;
using System.Collections.Generic;

namespace LogParser.Core.LineParser
{
    public class LineParserRule
    {
        public string PropertyName { get; set; }
        public List<Func<string, bool>> ValidationPredicates { get; set; }
        // possible refactor: add error msg for every validation predicate
        // better solution: something like ValidationRules as combined predicate and error message
        public string ErrorMessage { get; set; }
    }
}