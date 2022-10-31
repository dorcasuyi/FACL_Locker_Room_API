using System.Collections.Generic;
using System.Threading.Tasks;

namespace FACL_Locker_Room_Core.Interface
{
    public interface IReadWriteToJson
    {
        Task<T> ReadToJson<T>(string jsonFile);
        Task<bool> WriteToJson<T>(T model, string jsonFile);
    }
}