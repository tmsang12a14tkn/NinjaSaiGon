using Microsoft.Extensions.DependencyInjection;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaSaiGon.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            if (!context.Cities.Any())
            {
                var city = new City {
                    Name = "Hồ Chí Minh",
                    Districts = new List<District> {
                        new District { Name = "Quận 1" },
                        new District { Name = "Quận 12" },
                        new District { Name = "Thủ Đức" },
                        new District { Name = "Quận 9" },
                        new District { Name = "Gò Vấp" },
                        new District { Name = "Bình Thạnh" },
                        new District { Name = "Tân Bình" },
                        new District { Name = "Tân Phú" },
                        new District { Name = "Phú Nhuận" },
                        new District { Name = "Quận 2" },
                        new District { Name = "Quận 3" },
                        new District { Name = "Quận 10" },
                        new District { Name = "Quận 11" },
                        new District { Name = "Quận 4" },
                        new District { Name = "Quận 5" },
                        new District { Name = "Quận 6" },
                        new District { Name = "Quận 8" },
                        new District { Name = "Quận Bình Tân" },
                        new District { Name = "Quận 7" },
                        new District { Name = "Củ Chi" },
                        new District { Name = "Hóc Môn" },
                        new District { Name = "Bình Chánh" },
                        new District { Name = "Nhà Bè" },
                        new District { Name = "Cần Giờ" },
                    },
                    MatchingNames = "Hồ Chí Minh,Ho Chi Minh City,Hồ Chí Minh 700000,Thành phố Hồ Chí Minh,Thành phố Hồ Chí Minh 700000"
                };
                context.Cities.Add(city);
                context.SaveChanges();
            }
        }
    }
}
