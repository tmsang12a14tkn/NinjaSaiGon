using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models.Dto;

namespace NinjaSaigon.Order.Controllers
{
    public class MapController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public MapController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public MapDataDto Data()
        {
            return new MapDataDto {
                Country = "Việt Nam",
                Cities = _cityRepository.All
            };
        }
    }
}