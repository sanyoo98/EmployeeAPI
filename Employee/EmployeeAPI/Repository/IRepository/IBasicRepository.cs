using EmployeeAPI.Models;
using System;
using System.Collections.Generic;

namespace EmployeeAPI.Repository.IRepository
{
    public interface IBasicRepository
    {

        ICollection<Basic> GetBasics();
        Basic GetBasic(Guid basicId);
        bool BasicExists(string name);
        bool BasicExists(Guid id);

        bool CreateBasic(Basic basic);

        bool UpdateBasic(Basic basic);

        bool DeleteBasic(Basic basic);

        bool Save();
    }
}
