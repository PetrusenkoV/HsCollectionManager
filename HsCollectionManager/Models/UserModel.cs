﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsCollectionManager.Models
{
    public class UserModel
    {
        //How to properly Incapsulate this?
        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool IsEditable { get; set; } 
        public int Manacost { get; set; } = -1;
        public string Category { get; set; } = "All";

    }
}