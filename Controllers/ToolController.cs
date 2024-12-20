using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToolController : Controller
    {
        private readonly ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<bool> CodeFirst()
        {
            //1,创建数据库
            _db.DbMaintenance.CreateDatabase();
            //2,通过反射，加载程序集，读取所有类型，然后根据实体创建表
            string nspace = "Model.Entitys";
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            //初始化超级管理员和菜单
            Users user = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NickName = "超级管理员",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserId = ""
            };
            return await _db.Insertable(user).ExecuteCommandIdentityIntoEntityAsync();
        }
    }
}
