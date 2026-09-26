using System.ComponentModel.DataAnnotations;
using Week_2_Task_1.data;
using Week_2_Task_1.dto;
using Week_2_Task_1.interfaces;
using Week_2_Task_1.models;
namespace Week_2_Task_1.services

{
    public class services : iservices
    {
        private readonly store_data _store;

        public services(store_data store)
        {
            _store = store;
        }
        public response_cus add_customer(create_customer dto)
        {
            ValidateDto(dto);

            var newCustomer = new customer
            {
                id = _store.id_cus++,
                name = dto.name.Trim(),
                emal = dto.email.Trim()
            };

            _store.customers.Add(newCustomer);

            return ToCustomerResponse(newCustomer);


        }


        public List<response_cus> get_customers()
        {


            return _store.customers.Select(c => ToCustomerResponse(c)).ToList();


        }

        public response_cus get_customer_by_id(int id)
        {
            var exist = _store.customers.FirstOrDefault(c => c.id == id);

            if (exist is null)
            {
                return null;
            }

            return ToCustomerResponse(exist);
        }

        public bool update_customer(int id, update_customer dto)
        {
            ValidateDto(dto);

            var existingCustomer = _store.customers
                .FirstOrDefault(c => c.id == id);

            if (existingCustomer is null)
            {
                return false;
            }

            existingCustomer.name = dto.name;
            existingCustomer.emal = dto.email;

            return true;

        }

        public bool delete_customer(int id)
        {
            var existingCustomer = _store.customers
                    .FirstOrDefault(c => c.id == id);

            if (existingCustomer is null)
            {
                return false;
            }

            _store.customers.Remove(existingCustomer);

            return true;
        }


        public prod_responese add_product(create_product dto)
        {
            ValidateDto(dto);


            string valid = dto.sku;

            bool sku_ok = _store.products.Any(p => string.Equals(p.sku, valid, StringComparison.OrdinalIgnoreCase));

            if (sku_ok)
            {
                throw new ValidationException("product with this sku exists.");
            }

            var newProduct = new product
            {
                id = _store.id_prod++,
                name = dto.name,
                sku =valid,
                price = dto.price,
                stock = dto.stock,
               
            };

            _store.products.Add(newProduct);

            return ToProductResponse(newProduct);

        }

        public List<prod_responese> get_products()
        {
            return _store.products.Select(p => ToProductResponse(p)).ToList();
        }

        public prod_responese? get_product_by_id(int id)
        {
            var p = _store.products.FirstOrDefault(x => x.id == id);

            if (p is null)
            {
                return null;
            }

            return ToProductResponse(p);
        }

        public bool update_product(int id, update_prod dto)
        {
            ValidateDto(dto);

           
                var p = _store.products.FirstOrDefault(p => p.id == id);

                if (p is null)
                {
                    return false;
                }

            //    string Sku = dto.sku;

              //  bool sku_ok = _store.products.Any(p =>p.id != id && string.Equals(p.sku,StringComparison.OrdinalIgnoreCase));

              /*  if (skuExists)
                {
                    throw new ValidationException(
                        "Another product already uses this SKU.");
                }*/

                p.name = dto.name;
              //  p.sky = normalizedSku;
                p.price = dto.price!;
                p.stock =dto.stock;
              //  existingProduct.active = dto.IsActive!.Value;

                return true;
            
        }

        public bool delete_product(int id)
        {
            var p = _store.products.FirstOrDefault(p => p.id == id);

            if (p is null)
            {
                return false;
            }

            _store.products.Remove(p);

            return true;
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
                id = customer.id,
                name = customer.name,
             //   emil = customer.email
            };
        }

        private static prod_responese ToProductResponse(
            product product)
        {
            return new prod_responese { id = product.id, name = product.name,  price = product.price, stock = product.stock, active = product.active };
        }
    }
}
