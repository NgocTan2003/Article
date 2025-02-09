using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Dtos.UserDto;

namespace Article.WebApp.ConnectAPI.ArticleAppUserCnAPI
{
    public interface IArticleAppUserConnectAPI
    {
        Task<TokenResponse> Authentication(AuthenticationRequest request);
        Task<TokenResponse> AuthenticationOTP(AuthenticationOTPRequest request);
        Task<ResponseMessage> ForgotPassword(string EmailForgotPassword);
        Task<ResponseMessage> ResetPassword(ResetPassword resetPassword);
        Task<ResponseMessage> ChangePassword(ChangePassword changePassword);
        Task<ResponseMessage> SendEmailConfirm(string email);
        Task<List<UserDto>> GetAll();
        Task<List<UserDto>> GetPaging(PageRequest request);
        Task<IList<string>> GetAllRoleByName(string UserName);
        Task<UserDto> GetUserById(string id);
        Task<UserDto> GetUserByName(string UserName);
        Task<ResponseMessage> RegisterManager(ManagerRegister request);
        Task<bool> RoleAssign(List<RoleAssignRequest> request);
        //Task<bool> UpdateUser(ArticleAppUser request);
        Task RemoveListUserByName(string value);
    }
}
