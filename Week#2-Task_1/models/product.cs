namespace Week_2_Task_1.models
{
    public class product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sku
        {
            get; set;
        }
        public float price { get; set; }
        public int stock { get; set; }
        public bool active
        {
            get;
            set;
        }
    }
}
