using System;

namespace VintedShipping.Services
{
    public class ConsoleService
    {
        private readonly InputFileService _inputFileService;

        public ConsoleService(InputFileService inputFileService)
        {
            _inputFileService = inputFileService;
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Select action number: ");
                Console.WriteLine("1 - Get shipment discounts.");
                Console.WriteLine("2 - Exit an application");
                Console.WriteLine();
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        
                        break;
                    case "2":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}
