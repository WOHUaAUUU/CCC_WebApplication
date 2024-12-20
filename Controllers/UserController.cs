using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using Org.BouncyCastle.Ocsp;
using Shoping_WebAPI.Config;

namespace Shoping_WebAPI.Controllers
{
    public class UserController:BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ApiResult> Add(UserAdd userAdd) { 
        userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Succes(await _userService.Add(userAdd,userId));
        }
        [HttpPost]
        public async Task<ApiResult> Edt(UserEdit userEdit) {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Succes(await _userService.Edit(userEdit, userId));
        }
        [HttpGet]
        public async Task<ApiResult> Del(string id) {
            return ResultHelper.Succes(await _userService.Del(id));
        }
        [HttpGet]
        public async Task<ApiResult> BatchDel(string ids)
        {
            return ResultHelper.Succes(await _userService.Del(ids));
        }

        [HttpPost]
        public async Task<ApiResult> EditNickNameOrPassword([FromBody]PersonEdit req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Succes(await _userService.EditNickNameOrPassword(userId, req));
        }
    } 
}
