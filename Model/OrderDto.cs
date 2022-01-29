namespace Northwind.Model
{
    public class OrderDto
    {
        public int? ShipVia { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public OrderDetailsDto OrderDetails { get; set; }

        public class OrderDetailsDto
        {
            public int? ProductId { get; set; }
            public short? Quantity { get; set; }
        }
    }
}