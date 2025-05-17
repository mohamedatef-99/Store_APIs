using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using ServicesAbstractions;

namespace Services
{
    public class CashService(ICasheRepository casheRepository) : ICasheService
    {
        public async Task<string?> GetCashValueAsync(string key)
        {
            var value = await casheRepository.GetAsync(key);
            return value == null ? null : value;
        }

        public async Task SetCashValueAsync(string key, object value, TimeSpan duration)
        {
            await casheRepository.SetAsync(key, value, duration);
        }
    }
}
