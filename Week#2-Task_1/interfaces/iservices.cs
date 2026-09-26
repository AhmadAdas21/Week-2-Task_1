using Week_2_Task_1.dto;

namespace Week_2_Task_1.interfaces
{
    public interface iservices
    {
        response_cus add_customer(create_customer dto);

        List<response_cus> get_customers();

        response_cus get_customer_by_id(int id);

        bool update_customer(int id, update_customer dto);

        bool delete_customer(int id);


        prod_responese add_product(create_product dto);

        List<prod_responese> get_products();

        prod_responese? get_product_by_id(int id);

        bool update_product(int id, update_prod dto);

        bool delete_product(int id);

    }
}
