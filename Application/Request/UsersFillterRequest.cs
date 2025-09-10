namespace Application.Request
{
    public class UsersFillterRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
    }
}
