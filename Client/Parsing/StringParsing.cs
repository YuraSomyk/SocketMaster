using Client.Parsing.Exceptions;
using System;

namespace Client.Parsing {
    public static class StringParsing {

        public static int TryToParse(string value) {
            int number;
            bool result = Int32.TryParse(value, out number);

            if (!result) throw new ParsingException();

            return number;
        }
    }
}
