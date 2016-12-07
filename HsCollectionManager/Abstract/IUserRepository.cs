using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HsCollectionManager.Models;

namespace HsCollectionManager.Abstract
{
    public interface IUserRepository
    {
        int GetUserId(string userName);
        bool InsertUser(string userName);

     
    }
}
