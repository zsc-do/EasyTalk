using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_talk.controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index([FromQuery(Name = "friendId")] String friendId)
        {
            // int curUserId = int.Parse(HttpContext.Session.GetString("userId"));
            int curUserId = 1;

            ViewData["friendId"] = friendId;
            ViewData["curUserId"] = curUserId;
            return View();
        }
    }
}
