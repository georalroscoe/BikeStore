namespace Dtos
{
    public class OrderDto
    {
        public int StaffId { get; set; }
        
        public int CustomerId { get; set; }

        public bool AllStoresStrategy { get; set; }

        public List<OrderProductDto> Products { get; set; } = new List<OrderProductDto>();
    }
}