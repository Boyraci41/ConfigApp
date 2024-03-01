using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager.Exceptions
{
    public class CacheException: Exception
    {
        public CacheException(string message,Exception exception):base(message,exception)
        {
            
        }
    }
}
