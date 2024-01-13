namespace WebBTL1.Models
{
    public class Response
    {
        public Response(ErrorMessage errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.Employee = new List<Employee>();
        }

        public Response(List<Employee> employees)
        {
            this.Employee = employees;
            this.ErrorMessage = null;
        }

        public ErrorMessage? ErrorMessage { get; set; }
        public List<Employee> Employee { get; set; }
    }
}
