using System.Net;

namespace WebMarket.ViewModels
{
    public class ErrorViewModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}