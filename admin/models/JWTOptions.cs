﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easy_talk.models
{
    public class JWTOptions
    {
        public string SigningKey { get; set; }
        public int ExpireSeconds { get; set; }
    }
}
