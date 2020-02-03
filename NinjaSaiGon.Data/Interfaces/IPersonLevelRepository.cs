using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IPersonLevelRepository
    {
        IEnumerable<PersonLevel> PersonLevels { get; }
        PersonLevel GetById(int id);
        void Add(PersonLevel personLevel);
        void Update(PersonLevel personLevel);
        bool Exists(int id);
    }
}
