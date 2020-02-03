using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaSaiGon.Data.Repositories
{
    public class CityRepository: ICityRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        
        public CityRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<CityDto> All => _appDbContext.Cities.Include(c => c.Districts).AsEnumerable().Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.MatchingNames.Split(','),
            Districts = c.Districts.Select(d => new DistrictDto { Id = d.Id, Name = d.Name }).ToArray()
        });
    }
}
