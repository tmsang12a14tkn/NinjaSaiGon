using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class BusinessAreaRepository : IBusinessAreaRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public BusinessAreaRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<BusinessArea> BusinessAreas => _appDbContext.BusinessAreas;
        public BusinessArea GetById(int id) => _appDbContext.BusinessAreas.Include(b => b.Parent).SingleOrDefault(m => m.Id == id);
        public void Add(BusinessArea businessArea)
        {
            businessArea.Code += "BA" + HiResDateTime.UtcNowTicks;
            _appDbContext.BusinessAreas.Add(businessArea);
            _appDbContext.SaveChanges();
        }
        public void Update(BusinessArea businessArea)
        {
            _appDbContext.Update(businessArea);
            _appDbContext.SaveChanges();
        }
        public bool Exists(int id) => _appDbContext.BusinessAreas.Any(e => e.Id == id);
    }
}
