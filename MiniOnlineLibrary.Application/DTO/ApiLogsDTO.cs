using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.DTO
{
    public class ApiLogsDTO
    {
        public int api_log_id { get; set; }
        public string endpoint { get; set; }
        public string request_body { get; set; } = string.Empty;
        public string response_body { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
        public int? user_id { get; set; } = null;
        public int status_code { get; set; } = 200;
        public string ip_address { get; set; }

        public long response_time_ms { get; set; } = 200;
        public DateTime created_at { get; set; }
    }
}
