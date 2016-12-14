using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsCollectionManager.Models
{
    public class QueryBuilderHelper
    {
        public List<string> JoinsList { get; } = new List<string>();
        public List<string> WhereList { get; } = new List<string>();

        public void UserFilter(int userId, bool isEditable)
        {
            if (!isEditable)
            {
                JoinsList.Add("Inner join UserCards u on c.id = u.cardId ");
                WhereList.Add($"u.userId = {userId} ");
            }
        }

        public void ClassFilter(string className)
        {
            if (className != "All")
            {
                JoinsList.Add("inner join Class cl on cl.id = c.class_id ");
                WhereList.Add($"cl.name = '{className}' ");
            }
        }

        public void ManacostFilter(int manacost)
        {
            if (0 <= manacost && manacost < 7)
            {
                WhereList.Add($"manacost = {manacost} ");
            }

            if (manacost >= 7)
            {
                WhereList.Add("manacost >= 7");
            }
        }
    }
}