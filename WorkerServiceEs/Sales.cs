namespace WorkerServiceEs
{
    public class Sales
    {
        public string VendorName { get; }
        public string Product { get; }
        public decimal Price { get; }

        public Sales(string vendorName, string product, decimal price)
        {
            VendorName = vendorName;
            Product = product;
            Price = price;
        }
    }
}
