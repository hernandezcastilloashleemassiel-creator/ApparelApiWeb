using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class MessageDTO
    {
        public MessageDTO()
        {
            Message = new List<string>();
        }
        public List<string> Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
