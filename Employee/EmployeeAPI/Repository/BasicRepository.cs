using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using EmployeeAPI.Models.Dtos;
using EmployeeAPI.Repository.IRepository;

namespace EmployeeAPI.Repository
{
    public class BasicRepository : IBasicRepository
    {
        private readonly ApplicationDbContext _db;

        public BasicRepository(ApplicationDbContext db)
        {
            _db=db; //to access the application DB Context
        }
        public bool BasicExists(string name)
        {
            bool value = _db.basics.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool BasicExists(Guid id)
        {
            return _db.basics.Any(a => a.Id == id);
        }

        public bool CreateBasic(Basic basic)
        {
        _db.basics.Add(basic);
            return Save();
        }

        public bool DeleteBasic(Basic basic)
        {
            _db.basics.Remove(basic);
            return Save();
        }

        public Basic GetBasic(Guid basicId)
        {
            return _db.basics.FirstOrDefault(a => a.Id == basicId);
        }

        public ICollection<Basic> GetBasics()
        {
            return _db.basics.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateBasic(Basic basic)
        {
            _db.basics.Update(basic);
            return Save();
        }
    }
}
