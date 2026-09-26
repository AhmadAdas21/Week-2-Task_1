using System.ComponentModel.DataAnnotations;
using Week_2_Task_1.data;
using Week_2_Task_1.dto;
using Week_2_Task_1.interfaces;
using Week_2_Task_1.models;
namespace Week_2_Task_1.services

{
    public class services:iservices
    {
        private readonly store_data _store;

        public services(store_data store)
        {
            _store = store;
        }
        response_cus add_customer(create_customer dto)
        {
            ValidateDto(dto);
            lock (_store.SyncRoot)
            {
                var newCustomer = new customer
                {
                    Id = _store.id_cus++,
                    Name = dto.Name.Trim(),
                    Email = dto.Email.Trim()
                };

                _store.customers.Add(newCustomer);

                return ToCustomerResponse(newCustomer);
            }

        }


        List<response_cus> get_customers()
        {
            lock (_store.SyncRoot)
            {
                return _store.customers.Select(c => ToCustomerResponse(c)).ToList();
            }

        }

        response_cus get_customer_by_id(int id)
        {

        }

        bool update_customer(int id, update_customer dto)
        {

        }

        bool delete_customer(int id)
        {

        }


        prod_responese add_product(create_product dto)
        {

        }

        List<prod_responese> get_products()
        {

        }

        prod_responese? get_product_by_id(int id)
        {

        }

        bool update_product(int id, update_prod dto)
        {

        }

        bool delete_product(int id)
        {

        }
        private static void ValidateDto(object dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var context = new ValidationContext(dto);

            Validator.ValidateObject(
                dto,
                context,
                validateAllProperties: true);
        }
        private static response_cus ToCustomerResponse(
           customer customer)
        {
            return new response_cus
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        private static prod_responese ToProductResponse(
            product product)
        {
            return new prod_responese
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive
            };
        }
    }
}
