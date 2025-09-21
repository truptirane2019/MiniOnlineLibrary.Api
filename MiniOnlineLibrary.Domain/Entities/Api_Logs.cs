using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Domain.Entities
{
    public class api_logs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Api_Log_Id { get; set; }
        public string Edpoint { get; set; } = string.Empty;
        public string Request_Body { get; set; } = string.Empty;
        public string Response_Body { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public int? UserId { get; set; }
         public int Status_Code { get; set; } = 200;
        
        public string Ip_Address { get; set; } = string.Empty;

        public int Response_Time_Ms { get; set; } = 200;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
