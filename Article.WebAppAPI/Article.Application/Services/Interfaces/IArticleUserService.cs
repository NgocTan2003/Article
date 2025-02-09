using Article.Common.ReponseBase;
using Article.Common.Seedwork;
using Article.Data.Entity;
using Article.Dtos.UserDto;

namespace Article.Application.Services.Interfaces
{
    public interface IArticleUserService
    {
        Task<TokenResponse> Authentication(AuthenticationRequest request);
        Task<TokenResponse> AuthenticationOTP(AuthenticationOTPRequest request);
        Task<ResponseMessage> ForgotPassword(string EmailForgotPassword);
        Task<ResponseMessage> ResetPassword(ResetPassword resetPassword);
        Task<ResponseMessage> ChangePassword(ChangePassword changePassword);
        Task<ResponseMessage> SendEmailConfirm(string email);
        Task<List<ArticleAppUser>> GetAllUser();
        Task<PagedList<UserDto>> GetPaging(PageRequest request);
        Task<IList<string>> GetAllRoleByName(string UserName);
        Task<UserDto> GetUserById(string id);
        Task<UserDto> GetUserByName(string UserName);
        Task<ResponseMessage> Register(RegisterRequest request);
        Task<ResponseMessage> RegisterManager(ManagerRegister request);
        Task<bool> RoleAssign(List<RoleAssignRequest> request);
        Task<bool> UpdateUser(ArticleAppUser request);
        Task RemoveListUserByName(string value);
    }
}
