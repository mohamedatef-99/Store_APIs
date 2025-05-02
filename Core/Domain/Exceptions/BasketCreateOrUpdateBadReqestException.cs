using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketCreateOrUpdateBadReqestException(): BadRequestException($"Invalid Operation When Create or Update Basket")
    {
    }
}
