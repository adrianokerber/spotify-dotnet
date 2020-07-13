using System;
using System.Collections.Generic;
using System.Text;

namespace Kerber.SpotifyLibrary.Specs.SpecFlow_CalculatorExample
{
    public static class ManAndBullPhraseGenerator
    {
        public static string Phrase(string manName, string bullName)
        {
            return $"Um homem chamado {manName} se encontra com o touro {bullName}";
        }
    }
}
