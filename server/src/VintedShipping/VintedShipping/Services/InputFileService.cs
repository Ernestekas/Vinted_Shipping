using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace VintedShipping.Services
{
    public class InputFileService
    {
        private readonly string file = "input.txt";

        public async Task<string[]> ReadInputAsync()
        {
            return await File.ReadAllLinesAsync(file);
        }
    }
}
