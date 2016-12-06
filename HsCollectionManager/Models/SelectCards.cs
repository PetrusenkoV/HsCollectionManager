using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsCollectionManager.Models
{
    public class SelectCards
    {
        public IEnumerable<Card> Cards { get; set; }
        public PageInfo PageInfo { get; set; }
        //Use UserModel instead of these 2 properties
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}