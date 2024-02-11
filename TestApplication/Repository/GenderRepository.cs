using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestApplication.Repository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext _context;

        public GenderRepository(DataContext context)
        {
            _context = context;
        }

        public Gender GetGenderByKey(int genderKey)
        {
            // Retrieve gender from the database based on the gender key (Id)
            return _context.Genders.FirstOrDefault(g => g.Id == genderKey);
        }


    }
}
