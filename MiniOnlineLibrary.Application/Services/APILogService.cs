using MiniOnlineLibrar.Infrastructure.Repositories;
using MiniOnlineLibrary.Application.DTO;
using MiniOnlineLibrary.Application.Interfaces;
using MiniOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.Services
{

    public class APILogService :IAPILogService
    {
        private readonly IUnitOfWork _uow;
        public APILogService(IUnitOfWork uow) { _uow = uow; }
        public async Task CreateAsync(ApiLogsDTO dto)
        {
            var log = new api_logs { Edpoint = dto.endpoint, Ip_Address = dto.ip_address,  Method= dto.method, Request_Body = dto.request_body,Response_Body = dto.response_body,Response_Time_Ms = Convert.ToInt32(dto.response_time_ms), UserId = Convert.ToInt16( dto.user_id) , Status_Code = dto.status_code,CreatedAt = DateTime.UtcNow };
            await _uow.APILogs.AddAsync(log);
            await _uow.SaveChangesAsync();
             
        }
    }
}
