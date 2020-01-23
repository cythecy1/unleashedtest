using System;
using System.Collections.Generic;
using System.Text;

namespace Number2Words
{
    public class NumConverter
    {
        public Dictionary<string, Func<uint, string>> BuilderDictionary { get; set; }

        private readonly Dictionary<uint, string> _lessTwentyDictionary;

        private readonly Dictionary<uint, string> _tensDictionary;

        private readonly Dictionary<ulong, string> _scaleDictionary;

        public NumConverter()
        {
            _lessTwentyDictionary = new Dictionary<uint, string>();
            BuildLessTwentyDictionary();
            _tensDictionary = new Dictionary<uint, string>();
            BuildTensDictionary();
            _scaleDictionary = new Dictionary<ulong, string>();
            BuildScaleDictionary();

        }
        private void BuildLessTwentyDictionary()
        {
            _lessTwentyDictionary.Add(0, "zero");
            _lessTwentyDictionary.Add(1, "one");
            _lessTwentyDictionary.Add(2, "two");
            _lessTwentyDictionary.Add(3, "three");
            _lessTwentyDictionary.Add(4, "four");
            _lessTwentyDictionary.Add(5, "five");
            _lessTwentyDictionary.Add(6, "six");
            _lessTwentyDictionary.Add(7, "seven");
            _lessTwentyDictionary.Add(8, "eight");
            _lessTwentyDictionary.Add(9, "nine");
            _lessTwentyDictionary.Add(10, "ten");

            _lessTwentyDictionary.Add(11, "eleven");
            _lessTwentyDictionary.Add(12, "twelve");
            _lessTwentyDictionary.Add(13, "thirteen");
            _lessTwentyDictionary.Add(14, "fourteen");
            _lessTwentyDictionary.Add(15, "fifteen");
            _lessTwentyDictionary.Add(16, "sixteen");
            _lessTwentyDictionary.Add(17, "seventeen");
            _lessTwentyDictionary.Add(18, "eighteen");
            _lessTwentyDictionary.Add(19, "nineteen");


        }

        private void BuildTensDictionary()
        {
            _tensDictionary.Add(20, "twenty");
            _tensDictionary.Add(30, "thirty");
            _tensDictionary.Add(40, "forty");
            _tensDictionary.Add(50, "fifty");
            _tensDictionary.Add(60, "sixty");
            _tensDictionary.Add(70, "seventy");
            _tensDictionary.Add(80, "eighty");
            _tensDictionary.Add(90, "ninety");
        }

        private void BuildScaleDictionary()
        {
            _scaleDictionary.Add(100, "hundred");
            _scaleDictionary.Add(1000, "thousand");
            _scaleDictionary.Add(1000000, "million");
            _scaleDictionary.Add(1000000000, "billion");
            _scaleDictionary.Add(1000000000000, "trillion");
            _scaleDictionary.Add(1000000000000000, "quandrillion");
        }


        public string GetLessTwenty(uint input)
        {
            if (input > 20)
                return GetHundred(input);

            if (input > 99)
                throw new ArgumentException("AcceptOnlyLessThanTwentys");

            string returnString = String.Empty;
            if (_lessTwentyDictionary.TryGetValue(input, out returnString))
            {
                return $" {returnString}";
            }
            else
            {
                throw new ArgumentException("GetLessTwentyBadInput");
            }

        }

        public string GetTens(uint input)
        {
            if (input < 20)
                return GetLessTwenty(input);

            if (input > 99)
                throw new ArgumentException("AcceptOnlyTens");

            string returnString = String.Empty;

            decimal dInput = input;
            decimal tFloorResult = Math.Floor(dInput / 10); //example Floor(45/10) = 4

            uint tenskey = (uint)tFloorResult * 10;
            uint oneskey = input - tenskey;
            string tensUnit, onesunit;
            if (_tensDictionary.TryGetValue(tenskey, out tensUnit))
            {
                returnString = $" {tensUnit}";

                if (oneskey > 0) //test if forty or forty one/forty two etc..
                {
                    if (_lessTwentyDictionary.TryGetValue(oneskey, out onesunit))
                    {
                        returnString = $"{returnString} {onesunit}";
                    }
                    else
                    {
                        throw new NotSupportedException("OnesUnitKeyInvalid");
                    }
                }

            }
            else
            {
                throw new NotSupportedException("TensUnitKeyInvalid");
            }
            return returnString;
        }

        public string GetHundred(uint input)
        {
            if (input < 20)
                return GetLessTwenty(input);

            if (input > 999)
                throw new ArgumentException("AcceptOnlyHundreds");

            string returnString = String.Empty;

            decimal dInput = input;
            decimal hFloorResult = Math.Floor(dInput / 100); //example Floor(635/100) = 6
            if (hFloorResult > 0)
            {
                //This has a hundred component.
                //Process hundred first
                string hundredUnit;
                if (_lessTwentyDictionary.TryGetValue((uint)hFloorResult, out hundredUnit))
                {
                    returnString = $" {hundredUnit} hundred";
                }
                //Process the tens component                    
                uint tensComp = input - ((uint)hFloorResult * 100); //Remove the hundred component
                if (tensComp > 0)
                {
                    returnString = returnString + GetTens(tensComp);
                }

            }
            else
            {
                //Tens component only
                returnString = GetTens(input);

            }

            return returnString;
        }


        public static string ToWord(this decimal input)
        {
            throw new NotImplementedException();
        }
    }
}
