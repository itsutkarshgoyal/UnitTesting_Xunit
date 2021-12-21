using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ebroker.Common.Helper
{
    [ExcludeFromCodeCoverage]
    public  class CurrentDateTime : ICurrentDateTime
    {
        public DateTime GetUserDate() {
            return DateTime.Now;
        }
    }
}
