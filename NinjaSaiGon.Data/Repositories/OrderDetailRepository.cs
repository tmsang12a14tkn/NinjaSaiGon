using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using NinjaSaiGon.Data.Models.Dto;

namespace NinjaSaiGon.Data.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        //private readonly ShoppingCart _shoppingCart;


        public OrderDetailRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_shoppingCart = shoppingCart;
        }

        public IQueryable<OrderDetail> List => _appDbContext.OrderDetails.Include(o => o.Order).Include(o => o.OrderDetailToppings);

        public bool Create(OrderDetail orderDetail)
        {
            try
            {
                _appDbContext.Add(orderDetail);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderDetail = GetById(id);
                _appDbContext.Remove(orderDetail);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(OrderDetail orderDetail)
        {
            _appDbContext.Entry(orderDetail).State = EntityState.Modified;
            try
            {
                _appDbContext.SaveChanges();
                return true;
            }
            catch (DbUpdateException e)
            {
                return false;
            }

        }

        public bool Exists(int orderDetailId)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetById(int id)
        {
            return _appDbContext.OrderDetails.Include(od => od.OrderDetailToppings).FirstOrDefault(d => d.OrderDetailId == id);
        }
    }

}
