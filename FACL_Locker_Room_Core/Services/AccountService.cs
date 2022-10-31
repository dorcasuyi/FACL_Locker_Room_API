using FACL_Locker_Room_Core.DTOs;
using FACL_Locker_Room_Core.Interface;
using FACL_Locker_Room_Core.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FACL_Locker_Room_Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IReadWriteToJson _readWrite;
        private readonly IConfiguration _configuration;

        public AccountService(IReadWriteToJson readWrite, IConfiguration configuration)
        {
            _readWrite = readWrite;
            _configuration = configuration;
        }

        public async Task<ResponseDto<string>> GetApiVersion()
        {
            //fetch Api version from Appsettings
            var GetVersion = _configuration["Credential:ApiVersion"];
            if (string.IsNullOrEmpty(GetVersion))
            {
                return ResponseDto<string>.Fail("Api Version", (int)HttpStatusCode.NotFound);
            }

            return ResponseDto<string>.Success("Api Version", GetVersion);
        }
        public async Task<ResponseDto<string>> CreatAccountAsync(CreateAccountModel model)
        {
            try
            {
                //json file storage for data base
                var accountFile = $"{model.FirstName.ToLower()}-{model.LastName.ToLower()}.json";

                //read from the data base and return the data if said location is found and Null if not found
                var data = await _readWrite.ReadToJson<CreateAccountModel>(accountFile);

                if(data == null)
                {
                    //write/save to the json file if said location does not exist/if null is returned
                    var writeResponse = await _readWrite.WriteToJson(model, accountFile);

                    if (!writeResponse)
                    {
                        //return 500 status code if an error occurs
                        return ResponseDto<string>.Fail("Could not Add Account, Try Again Later", (int)HttpStatusCode.InternalServerError);
                    }

                    return ResponseDto<string>.Success("Account Created", null);
                }

                return ResponseDto<string>.Fail("Account already exist");
            }
            catch (Exception ex)
            {
                return ResponseDto<string>.Fail(ex.Message, (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<ResponseDto<object>> GetAccountAsync(GetAccountModel model)
        {
            try
            {
                //how the file name is to be created/stored
                var accountFile = $"{model.FirstName.ToLower()}-{model.LastName.ToLower()}.json";

                var data = await _readWrite.ReadToJson<CreateAccountModel>(accountFile);

                if (data == null)
                {
                    return ResponseDto<object>.Fail("Account Not found");
                }

                return ResponseDto<object>.Success("Account retrived", data);
            }
            catch (Exception ex)
            {
                return ResponseDto<object>.Fail(ex.Message, (int)HttpStatusCode.NotFound);
            }

        }
    }
}
