using MiniOnlineLibrary.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrary.Application.Interfaces
{
    public interface IAPILogService
    {
        Task CreateAsync(ApiLogsDTO dto);
    }
}
