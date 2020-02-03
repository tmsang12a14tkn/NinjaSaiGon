using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class CommissionFormulaRepository : ICommissionFormulaRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CommissionFormulaRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CommissionFormula> CommissionFormulas => _appDbContext.CommissionFormulas;
        public CommissionFormula GetById(int id) => _appDbContext.CommissionFormulas.SingleOrDefault(m => m.Id == id);
        public void Add(CommissionFormula commissionFormula)
        {
            _appDbContext.CommissionFormulas.Add(commissionFormula);
            _appDbContext.SaveChanges();
        }
        public void Update(CommissionFormula commissionFormula)
        {
            _appDbContext.Update(commissionFormula);
            _appDbContext.SaveChanges();
        }
        public bool Exists(int id) => _appDbContext.CommissionFormulas.Any(e => e.Id == id);
    }
}
