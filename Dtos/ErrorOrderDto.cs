namespace Dtos
{
    public class ErrorOrderDto
    {
        public int StaffId { get; set; }

        public int CustomerId { get; set; }

        public List<ErrorOrderItemDto> ItemErrors { get; set; } = new List<ErrorOrderItemDto>();
    }
}