using System;
using System.Text;

namespace Maestro.Core.Extensions
{
    public static class ExceptionExtensions
    {        
        public static string ReadException(this Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);

            while (ex.InnerException != null)
            {
                sb.AppendLine(ex.InnerException.Message);
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
