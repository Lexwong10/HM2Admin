﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum ResultType {
        Sucess = 0,
        Error = 1
    }


    public class JsonResult
    {
        public ResultType Result { get; set; }
        public string ErrorTips { get; set; }
        public Object Data { get; set; }
    }
}
