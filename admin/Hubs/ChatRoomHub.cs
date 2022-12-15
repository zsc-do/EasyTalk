using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using easy_talk.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;


namespace easy_talk.Hubs
{
	[Authorize]
    public class ChatRoomHub : Hub
	{
	


        public async Task<string> SendPrivateMessage(string srcUserId,string destUserId, string message)
        {
            SysUser destUser = easyTalkContext.Users.Where(a => a.Id== int.Parse(destUserId)).ToList().First();


            if (destUser == null)
            {
                return "DestUserNotFound";
            }

           // string srcUserName = Context.User.Identity.Name;
           // var srcUserId = easyTalkContext.Users.Where(a => a.UserName == srcUserName).Select(a => a.Id).ToList().First();

            string time = DateTime.Now.ToShortTimeString();

            List<String> list = new List<String>();
            list.Add(destUserId);
            list.Add(srcUserId);

            await this.Clients.Users(list).SendAsync("ReceivePrivateMessage",
                srcUserId,time, message);
            return "ok";
        }


        private readonly easy_talkContext easyTalkContext;
        private readonly UserManager<SysUser> userManager;

        public ChatRoomHub(easy_talkContext easyTalkContext, UserManager<SysUser> userManager)
        {
            this.easyTalkContext = easyTalkContext;
            this.userManager = userManager;
        }
    }



}
