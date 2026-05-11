using PC_2JNC.Models;
using PC_2JNC.Repositories;

namespace PC_2JNC.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<List<Client>> Ej1() => _uow.Ej1.Ejecutar();
        public Task<List<Product>> Ej2() => _uow.Ej2.Ejecutar();
        public Task<object> Ej3() => _uow.Ej3.Ejecutar();
        public Task<int> Ej4() => _uow.Ej4.Ejecutar();
        public Task<Product?> Ej5() => _uow.Ej5.Ejecutar();
        public Task<List<Order>> Ej6() => _uow.Ej6.Ejecutar();

        public Task<decimal> Ej7()
        {
           return  _uow.Ej7.Ejecutar(); 
        } 
        public Task<List<Product>> Ej8() => _uow.Ej8.Ejecutar();
        public Task<object> Ej9() => _uow.Ej9.Ejecutar();
        public Task<object> Ej10() => _uow.Ej10.Ejecutar();
        public Task<List<string>> Ej11() => _uow.Ej11.Ejecutar();
        public Task<List<string>> Ej12() => _uow.Ej12.Ejecutar();
    }
}