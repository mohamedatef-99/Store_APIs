using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, ICasheRepository casheRepository) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork, mapper);

        public IBasketService BasketService {get; } = new BasketService(basketRepository, mapper);

        public ICasheService CasheService { get; } = new CashService(casheRepository);
    }
}
