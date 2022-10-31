using FACL_Locker_Room_Core.DTOs;
using FACL_Locker_Room_Core.Model;
using System.Threading.Tasks;

namespace FACL_Locker_Room_Core.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> CreatAccountAsync(CreateAccountModel model);
        Task<ResponseDto<object>> GetAccountAsync(GetAccountModel model);
        Task<ResponseDto<string>> GetApiVersion();
    }
}