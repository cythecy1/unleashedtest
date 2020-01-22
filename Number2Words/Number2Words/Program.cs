using System;
using NumberConversion;

namespace Number2Words
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberConversion.NumberToWordsRequest request = new NumberToWordsRequest();
            NumberToWordsRequestBody body = new NumberToWordsRequestBody();
            body.ubiNum = 3475474;
            request.Body = body;

            Console.WriteLine("Hello World!");
        }
    }
}
