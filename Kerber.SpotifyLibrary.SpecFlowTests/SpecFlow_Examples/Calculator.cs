using System;
using System.Collections.Generic;
using System.Text;

namespace Kerber.SpotifyLibrary.Specs
{
    class Calculator
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public int Add()
        {
            return FirstNumber + SecondNumber;
        }
    }
}
