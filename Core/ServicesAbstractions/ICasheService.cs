using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface ICasheService
    {
        Task SetCashValueAsync(string key, object value, TimeSpan duration);
        Task<string?> GetCashValueAsync(string key);
    }
}
