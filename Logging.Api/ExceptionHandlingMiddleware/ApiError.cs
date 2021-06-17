namespace Logging.Api.ExceptionHandlingMiddleware
{
    public class ApiError
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public string Links { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}