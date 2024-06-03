using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Exception
{
    public class HttpValidResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; } = String.Empty;
        public object? Content { get; set; }

        public HttpValidResponse() { }

        public HttpValidResponse(object content) {
            this.Content = content;
        }

        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
