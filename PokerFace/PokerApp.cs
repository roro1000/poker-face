using Services.Interfaces;
using System;
using System.IO;

namespace ConsoleApp
{
    public class PokerApp
    {
        private readonly IPokerService _pokerService;
        public PokerApp(IPokerService pokerService)
        {
            _pokerService = pokerService ?? throw new ArgumentNullException(nameof(pokerService));
        }
        public void Run()
        {
            Console.WriteLine("Enter file path...");
            var path = Console.ReadLine();
            StartFileRead(path);
        }

        public void StartFileRead(string path)
        {
            var lineNo = 1;
            try
            {
                using var sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var cards = line.Split(' ');

                    Console.WriteLine($"{ line } => { _pokerService.GetHandType(cards) }");
                    lineNo++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read at line: " + lineNo);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
