using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Login;
using Model.Dto.User;
using Model.Other;
using Shoping_WebAPI.Config;

namespace Shoping_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController:Controller
    {
        public readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        private readonly ICustomJWTService _JWTService;

        public LoginController(ILogger<LoginController> logger, IUserService userService, ICustomJWTService JWTService)
        {
            _logger = logger;
            _userService = userService;
            _JWTService = JWTService;
        }
        [HttpPost]

        public async Task<ApiResult> GetToken([FromBody]LoginReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                UserRes user = await _userService.GetUser(req);
                if (user==null)
                {
                    return ResultHelper.Error("账号或密码错误，请检查后重试");
                }
                _logger.LogInformation("登录");
                return ResultHelper.Succes(await _JWTService.GetTocken(user));
            }
            else
            {
                return ResultHelper.Error("参数错误");
            }
            
            
        }
    }
}
