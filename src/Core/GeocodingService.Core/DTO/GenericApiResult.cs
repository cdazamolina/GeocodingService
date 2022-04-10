using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocodingService.Core.DTO
{
    public class GenericApiResult
    {
        public string Message { get; set; }

        public GenericApiResult(string message)
        {
            Message = message;
        }
    }
}
