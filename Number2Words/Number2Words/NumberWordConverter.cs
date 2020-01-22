using System;
using System.Collections.Generic;
using System.Text;

namespace Number2Words
{
    public class NumberWordConverter
    {
        private readonly IDictionary<ulong, string> _numberWordsDictionary;
        private readonly IDictionary<ulong, string> _digitsDictionary;

        public NumberWordConverter(IDictionary<ulong, string> numberWordsDictionary, IDictionary<ulong, string> digitsDictionary)
        {
            if(numberWordsDictionary == null)
            {
                _numberWordsDictionary = new Dictionary<ulong, string>();
                BuildEnglishNumberDictionary();
            }
            else
            {
                if (numberWordsDictionary.Count < 1)
                { 
                    throw new ArgumentException("NumDictionaryEmpty"); 
                }
                else
                {
                    _numberWordsDictionary = numberWordsDictionary;
                }
            }

            if(digitsDictionary == null)
            {
                _digitsDictionary = new Dictionary<ulong, string>();
                BuildEnglishDigitsDictionary();
            }
            else
            {
                if (digitsDictionary.Count < 1)
                {
                    throw new ArgumentException("DigitDictionaryEmpty");
                }
                else
                {
                    _digitsDictionary = digitsDictionary;
                }
            }
        }

        private void BuildEnglishNumberDictionary()
        {
            
            _numberWordsDictionary.Add(1, "one");
            _numberWordsDictionary.Add(2, "two");
            _numberWordsDictionary.Add(3, "three");
            _numberWordsDictionary.Add(4, "four");
            _numberWordsDictionary.Add(5, "five");
            _numberWordsDictionary.Add(6, "six");
            _numberWordsDictionary.Add(7, "seven");
            _numberWordsDictionary.Add(8, "eight");
            _numberWordsDictionary.Add(9, "nine");
            _numberWordsDictionary.Add(10, "ten");

            _numberWordsDictionary.Add(11, "eleven");
            _numberWordsDictionary.Add(12, "twelve");
            _numberWordsDictionary.Add(13, "thirteen");
            _numberWordsDictionary.Add(14, "fourteen");
            _numberWordsDictionary.Add(15, "fifteen");
            _numberWordsDictionary.Add(16, "sixteen");
            _numberWordsDictionary.Add(17, "seventeen");
            _numberWordsDictionary.Add(18, "eighteen");
            _numberWordsDictionary.Add(19, "nineteen");

            _numberWordsDictionary.Add(20, "twenty");
            _numberWordsDictionary.Add(30, "thirty");
            _numberWordsDictionary.Add(40, "forty");
            _numberWordsDictionary.Add(50, "fifty");
            _numberWordsDictionary.Add(60, "sixty");
            _numberWordsDictionary.Add(70, "seventy");
            _numberWordsDictionary.Add(80, "eighty");
            _numberWordsDictionary.Add(90, "ninety");

        }

        private void BuildEnglishDigitsDictionary()
        {
            _digitsDictionary.Add(100, "hundred");
            _digitsDictionary.Add(1000, "thousand");
            _digitsDictionary.Add(1000000, "million");
            _digitsDictionary.Add(1000000000, "billion");
            _digitsDictionary.Add(1000000000000, "trillion");
            _digitsDictionary.Add(1000000000000000, "quandrillion");
        }


        public string QuickReturn(ulong input)
        {
            string returnString = String.Empty;
            if(_numberWordsDictionary.TryGetValue(input, out returnString))
            {
                return returnString;
            }
            else
            {
                return String.Empty;
            }

        }

    }
}
