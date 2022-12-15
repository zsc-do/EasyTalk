using easy_talk.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace easy_talk.controllers
{
    public class FriendController : Controller
    {


		private readonly RoleManager<SysRole> roleManager;
		private readonly UserManager<SysUser> userManager;

		private readonly easy_talkContext easyTalkContext;
		public FriendController(RoleManager<SysRole> roleManager, UserManager<SysUser> userManager,
								easy_talkContext easyTalkContext)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
			this.easyTalkContext = easyTalkContext;

		}


		public JsonResult GetUserFriend([FromQuery(Name = "curId")] String userId)
        {
			//int curUserId= int.Parse(HttpContext.Session.GetString("userId"));
			var friendIds = easyTalkContext.UserFriends.Where(a => a.userId == int.Parse(userId)).Select(a => a.friendId).ToList();

			List<SysUser> userFriends = new List<SysUser>();

			foreach(int friendId in friendIds)
            {
				var friend = easyTalkContext.Users.Where(a => a.Id == friendId).ToList().First();
				userFriends.Add(friend);
            }

			return new JsonResult(userFriends);
        }

		public ActionResult SearchPage()
        {
			return View();
        }


		public JsonResult SearchUsers([FromQuery(Name ="username")] String username)
        {

			var userList = easyTalkContext.Users.Where(a => a.UserName == username).ToList();

			return new JsonResult(userList);
        }


		public JsonResult AddFriend([FromQuery(Name = "curUserId")] String curUserId,
			                        [FromQuery(Name = "userId")] String userId)
		{
			var friend = easyTalkContext.Users.Where(a => a.Id == int.Parse(userId)).ToList().First();

			//int curUserId = int.Parse(HttpContext.Session.GetString("userId"));

			easyTalkContext.Database.ExecuteSqlRaw($"insert into user_friend (user_id,friend_id) values ({curUserId}, {friend.Id})");
			return new JsonResult("ok");
		}

	}
}
