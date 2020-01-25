using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Number2Words
{
    public class NumConverter
    {
        public Dictionary<string, Func<uint, string, string>> _resultDictionary { get; set; }

        private readonly Dictionary<uint, string> _lessTwentyDictionary;

        private readonly Dictionary<uint, string> _tensDictionary;

        private List<string> _groupedCharacteristic;

        private string _mantissa;

        public NumConverter()
        {
            _lessTwentyDictionary = new Dictionary<uint, string>();
            BuildLessTwentyDictionary();
            _tensDictionary = new Dictionary<uint, string>();
            BuildTensDictionary();
            _resultDictionary = new Dictionary<string, Func<uint, string, string>>();

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

        private void InitializeResultDictionary()
        {
            _resultDictionary.Clear();
            _resultDictionary.Add("cents", null);
            _resultDictionary.Add("hundred", null);
            _resultDictionary.Add("thousand", null);
            _resultDictionary.Add("million", null);
            _resultDictionary.Add("billion", null);
            _resultDictionary.Add("trillion", null);
            _resultDictionary.Add("quadrillion", null);
            _resultDictionary.Add("quintillion", null);
            _resultDictionary.Add("sextillion", null);
            _resultDictionary.Add("septillion", null);
        }


        public virtual string GetLessTwenty(uint input, string prefix = "")
        {
            if (input > 20)
                throw new ArgumentException("AcceptOnlyLessThanTwentys");

            string returnString = String.Empty;
            if (_lessTwentyDictionary.TryGetValue(input, out returnString))
            {
                return $"{prefix} {returnString}";
            }
            else
            {
                throw new ArgumentException("GetLessTwentyBadInput");
            }

        }

        public virtual string GetTens(uint input, string prefix = "")
        {
            if (input < 20)
                return GetLessTwenty(input, prefix);

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
                returnString = $"{prefix} {tensUnit}";

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

        public string GetHundred(uint input, string prefix = "")
        {
            if (input < 100)
                GetTens(input, prefix);

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
                    returnString = $" {hundredUnit} hundred ";
                }
                //Process the tens component                    
                uint tensComp = input - ((uint)hFloorResult * 100); //Remove the hundred component
                if (tensComp > 0)
                {
                    returnString = returnString + GetTens(tensComp, prefix);
                }

            }
            else
            {
                //Tens component only
                returnString = GetTens(input, prefix);

            }

            return returnString;
        }

        /// <summary>
        /// Determines which function (GetTens, GetHundred) to use on which kv pair in resultDictionary
        /// </summary>
        private void BuildResult(string strInput)
        {

            InitializeResultDictionary();

            //No Cent assumption
            string characteristic = strInput;

            //Clean up no comma
            characteristic = characteristic.Replace(",", "");

            //Check for cents
            if (strInput.Contains('.'))
            {
                var precision = strInput.Split(new char[] { '.' });  //Separate Mantissa with Characteristic
                _mantissa = precision[1];
                characteristic = precision[0];
                _resultDictionary["cents"] = GetTens;
            }

            var arrCharacteristic = characteristic.ToCharArray();
            Array.Reverse(arrCharacteristic); //Reverse so we can read from right to left
            string sResult = String.Empty;
            for (int idx = 1; idx <= arrCharacteristic.Length; idx++)
            {
                if (idx % 3 == 0) //mod 3 so we can group by hundreds
                {
                    sResult = sResult + arrCharacteristic[idx - 1] + ",";
                }
                else
                {
                    sResult = sResult + arrCharacteristic[idx - 1];
                }
            }

            var splitCharacteristic = sResult.Split(new char[] { ',' });
            _groupedCharacteristic = new List<string>(); //this is where hundreds are added;
            foreach (var charac in splitCharacteristic)
            {
                var splitstrings = charac.ToCharArray();
                Array.Reverse(splitstrings);
                string sp = new string(splitstrings);
                if (!String.IsNullOrWhiteSpace(sp))
                    _groupedCharacteristic.Add(sp);

            }

            int kvIndex = 0;
            //Finally fillout resultDictionary
            foreach (string key in _resultDictionary.Keys.ToList())
            {
                if (key.Equals("cents")) continue;

                try { var x = _groupedCharacteristic[kvIndex]; } catch (ArgumentOutOfRangeException) { break; }

                if (_groupedCharacteristic[kvIndex].Length < 3)
                {
                    _resultDictionary[key] = GetTens;
                }
                else
                {
                    if(_groupedCharacteristic[kvIndex] != "000") //no function for the zeroes example 120,000,304 - skip 000
                        _resultDictionary[key] = GetHundred;
                }

                kvIndex++;

            }


        }

        public string NumToWords(decimal input)
        {
            return NumToWords(input.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public string NumToWords(string strInput)
        {
            Double d;
            if (!Double.TryParse(strInput, out d))
                throw new ArgumentException("NotANumber");

            string sResult = String.Empty;

            BuildResult(strInput.Trim());

            int kvIndex = _groupedCharacteristic.Count - 1;
            foreach (var pair in _resultDictionary.Reverse())
            {
                if (kvIndex < 0 && _mantissa != null)
                {
                    //Resolve Mantissa
                    sResult = sResult + pair.Value.Invoke(uint.Parse(_mantissa), " and") + " " + pair.Key;
                    break;
                }

                if (pair.Value != null)
                {
                    if (pair.Key.Equals("hundred"))
                    {
                        sResult = sResult + pair.Value.Invoke(uint.Parse(_groupedCharacteristic[kvIndex]), "");
                        kvIndex--;
                    }
                    else
                    {
                        sResult = sResult + pair.Value.Invoke(uint.Parse(_groupedCharacteristic[kvIndex]), "and") + " " + pair.Key + " ";
                        kvIndex--;
                    }
                }


            }

            //Small clean up.
            if (sResult.StartsWith("and"))
                sResult = sResult.Substring(4, sResult.Length - 4);

            sResult = sResult.Trim();
            sResult = sResult.Replace("  ", " ");

            return sResult;
        }

    }
}
