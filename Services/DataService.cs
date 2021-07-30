using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ManageDataASync()
        {
            //Create roles
            await CreateRolesAsync();

            //Create new users
            await AddUsersAsync();
        }

        private async Task CreateRolesAsync()
        {

        }


        private async Task AddUsersAsync()
        {

        }





    }
}