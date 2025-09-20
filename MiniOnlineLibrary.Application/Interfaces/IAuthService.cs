using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniOnlineLibrary.Application.DTO.AuthDto;

namespace MiniOnlineLibrary.Application.Interfaces
{
    
        public interface IAuthService
        {
            Task<AuthResultDto> RegisterAsync(RegisterDto dto);
            Task<AuthResultDto> LoginAsync(LoginDto dto);
        }
    
}
