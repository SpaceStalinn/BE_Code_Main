using Core.Exception;
using System.Text.Json;

namespace Core.NewFolder
{

    public class HttpErrorResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; } = String.Empty;
        public object? Content { get; set; }
        public string? errorDetail { get; set; } = String.Empty;

        public HttpErrorResponse() {}
        public HttpErrorResponse(object content) 
        {
            this.Content = content;
        }

        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
