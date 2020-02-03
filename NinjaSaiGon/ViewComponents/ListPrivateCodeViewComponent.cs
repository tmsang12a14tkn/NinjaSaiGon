using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class ListPrivateCodeViewComponent : ViewComponent
    {
        private readonly IPrivatePromotionRepository _privatePromotionRepository;
        public ListPrivateCodeViewComponent(IPrivatePromotionRepository privatePromotionRepository)
        {
            _privatePromotionRepository = privatePromotionRepository;
        }

        public IViewComponentResult Invoke(int privatePromotionId)
        {
            var listCode = _privatePromotionRepository.AllCode.Where(c => c.PrivatePromotionId == privatePromotionId && !c.IsDeleted).ToList();
            ViewBag.PrivatePromotionId = privatePromotionId;
            return View(listCode);
        }
    }
}
