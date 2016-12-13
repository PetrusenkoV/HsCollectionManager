using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsCollectionManager.Models
{
    public class UserIdModel
    {
        public int UserId { get; set; }
        public bool IsNull { get; set; }

        public UserIdModel(object userId)
        {
            if (userId != null)
            {
                UserId = (int) userId;
            }
            else
            {
                IsNull = true;
            }
        }
    }
}