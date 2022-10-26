using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Data;
using MyEshop.Models;

namespace Repositories
{
    public interface IUserRepository
    {
        bool IsUserExistByEmail(String Email);
        void AddUser(User user);

        //by email and password
        User GetUserToLogin(string Email, string Password);
    }

    public class UserRepository : IUserRepository
    {
        MyEshopContext _context;

        public UserRepository(MyEshopContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserToLogin(string Email, string Password)
        {
            return _context.users
            .SingleOrDefault(p => p.Email == Email && p.Password == Password);
        }

        public bool IsUserExistByEmail(string Email)
        {
            return _context.users.Any(p => p.Email == Email);
        }
    }
}
