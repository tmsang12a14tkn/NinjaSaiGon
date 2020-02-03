using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class PersonLevelRepository : IPersonLevelRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        
        public PersonLevelRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PersonLevel> PersonLevels => _appDbContext.PersonLevels;

        public PersonLevel GetById(int id) => _appDbContext.PersonLevels.SingleOrDefault(m => m.Id == id);

        public void Add(PersonLevel personLevel)
        {
            personLevel.Code = "CL" + HiResDateTime.UtcNowTicks;
            _appDbContext.PersonLevels.Add(personLevel);
            _appDbContext.SaveChanges();
        }

        public void Update(PersonLevel personLevel)
        {
            _appDbContext.Update(personLevel);
            _appDbContext.SaveChanges();
        }

        public bool Exists(int id) => _appDbContext.PersonLevels.Any(e => e.Id == id);
    }
}
