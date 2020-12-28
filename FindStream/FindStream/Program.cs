using FindStream.Core;
using System;

namespace FindStream
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit;
            do
            {
                IStream stream = new Stream();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Digite um stream: ");
                var streamText = Console.ReadLine();

                char result = stream.FirstChar(streamText);

                if (result == '\0')
                    Console.WriteLine("Pesquisa não encontrada");
                else
                    Console.WriteLine($"Pesquisa enconstrada é: {result}");

                Console.WriteLine($"Tentar novamente ? Y/N");
                var answer = Console.ReadLine();
                quit = answer.Trim().ToUpper() == "Y";
                Console.Clear();

            } while (quit);
        }
    }
}
