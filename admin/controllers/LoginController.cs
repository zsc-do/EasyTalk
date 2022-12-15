using easy_talk.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace easy_talk.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class LoginController : Controller
	{
		private readonly RoleManager<SysRole> roleManager;
		private readonly UserManager<SysUser> userManager;

		private readonly easy_talkContext easyTalkContext;
		public LoginController(RoleManager<SysRole> roleManager, UserManager<SysUser> userManager,
								easy_talkContext easyTalkContext)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
			this.easyTalkContext = easyTalkContext;

		}

		public IActionResult login()
		{
			return View("login");
		}


		[HttpPost]
		public async Task<JsonResult> doLogin(LoginRequest req,
					[FromServices] IOptions<JWTOptions> jwtOptions)
		{


			string userName = req.UserName;
			string password = req.Password;
			var user = await userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return new JsonResult("not found user");
			}
			var success = await userManager.CheckPasswordAsync(user, password);
			if (!success)
			{
				return new JsonResult("default");
			}
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
			claims.Add(new Claim(ClaimTypes.Name, user.UserName));
			
			string jwtToken = BuildToken(claims, jwtOptions.Value);


			HttpContext.Session.SetString("userId", user.Id.ToString());

			List<String> list = new List<String>();
			list.Add(user.Id.ToString());
			list.Add(jwtToken);
			return new JsonResult(list);
		}

		private static string BuildToken(IEnumerable<Claim> claims, JWTOptions options)
		{
			DateTime expires = DateTime.Now.AddSeconds(options.ExpireSeconds);
			byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
			var secKey = new SymmetricSecurityKey(keyBytes);
			var credentials = new SigningCredentials(secKey,
				SecurityAlgorithms.HmacSha256Signature);
			var tokenDescriptor = new JwtSecurityToken(expires: expires,
				signingCredentials: credentials, claims: claims);
			return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
		}


	}
}
