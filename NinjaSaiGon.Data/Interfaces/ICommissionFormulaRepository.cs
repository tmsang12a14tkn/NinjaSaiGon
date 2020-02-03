using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface ICommissionFormulaRepository
    {
        IEnumerable<CommissionFormula> CommissionFormulas { get; }
        CommissionFormula GetById(int id);
        void Add(CommissionFormula commissionFormula);
        void Update(CommissionFormula commissionFormula);
        bool Exists(int id);
    }
}
