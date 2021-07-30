using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataASync()
        {
            //Create the database from the migrations
            await _context.Database.MigrateAsync();

            //Create roles
            await CreateRolesAsync();

            //Create new users
            await AddUsersAsync();
        }

        private async Task CreateRolesAsync()
        {
            // Check if roles exist. If so, do nothing.
            if(_context.Roles.Any())
            {
                return;
            }

            //Create roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                // User Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

        }


        private async Task AddUsersAsync()
        {
            // Check if users exist. If so, do nothing
            if(_context.Users.Any())
            {
                return;
            }

            //Create users
            var adminUser = new BlogUser()
            {
                Email = "razvan.roman91@outlook.com",
                UserName = "razvan",
                FirstName = "Razvan",
                LastName = "Roman",
                PhoneNumber = "00000000000",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(adminUser, "Aaaa%1234!");

            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());
        }





    }
}