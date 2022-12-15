using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_talk.models
{
    public record LoginRequest(string UserName, string Password);
}
