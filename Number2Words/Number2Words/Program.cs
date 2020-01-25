using System;
using System.Threading.Tasks;
using NumberConversion;

namespace Number2Words
{
    class Program
    {
        static void Main(string[] args)
        {

                /**Cheeky way to do it is to call a service.  Uncomment below to try that.**/
                //UseNumberToWordsWebService(ulong.Parse(args[0]));
                NumConverter converter = new NumConverter();

                Console.WriteLine(converter.NumToWords(new decimal(1000)));
                Console.WriteLine(converter.NumToWords(new decimal(10)));
                Console.WriteLine(converter.NumToWords(new decimal(100)));
                Console.WriteLine(converter.NumToWords(new decimal(100000)));
                Console.WriteLine(converter.NumToWords(new decimal(1000000)));

                Console.WriteLine(converter.NumToWords(new decimal(34834902347.45)));


        }




        #region Web service style.
        /// <summary>
        /// The easiest way is to just call a service that converts numbers to words.
        /// But I assume that is not the goal of the exercise.  But here is an example anyway.
        /// </summary>
        static void UseNumberToWordsWebService(ulong numValue)
        {
            var response = CallNumberToWordsService(numValue);
            Console.WriteLine(response.Result.Body.NumberToWordsResult);
        }

        static async Task<NumberToWordsResponse> CallNumberToWordsService(ulong numValue)
        {
            var client = new NumberConversionSoapTypeClient(NumberConversionSoapTypeClient.EndpointConfiguration.NumberConversionSoap);
            var numberInWords = await client.NumberToWordsAsync(numValue);
            return numberInWords;
        }
        #endregion 
    }
}
