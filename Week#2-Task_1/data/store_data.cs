using Week_2_Task_1.models;

namespace Week_2_Task_1.data
{
    public class store_data
    {

        public List<customer> customers { get; set; }=new List<customer>();
        public List<product> products { get; set; } = new List<product>();
        public int id_prod { get; set; }


        public int id_cus { get; set; } = 1;
    }
        
}
