using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class CurrencyMismatchException : Exception
    {
         public CurrencyMismatchException (string message) : base(message) { }
    } 
}
