using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }

        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }
}
