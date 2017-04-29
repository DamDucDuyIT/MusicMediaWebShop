using MusicMediaWebShop.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMediaWebShop.Models.Repository
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private ApplicationDbContext _context;
        public EFPurchaseRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        //public IEnumerable<Order> NotYetShippedOrders =>
        //    _context.Orders.Where(o => !o.Shipped);

        public async Task<IEnumerable<ShippingInfo>> UnpaidRegistrationsAsync() =>
            await _context.ShippingInfos.Include(o => o.LineItems)
                .ThenInclude(l => l.Product)
            .Where(o => !o.Paid).ToListAsync();

        public IEnumerable<ShippingInfo> Registrations => _context.ShippingInfos
            .Include(o => o.LineItems)
                .ThenInclude(l => l.Product);

        public async Task<IEnumerable<ShippingInfo>> RegistrationsAsync() =>
            await _context.ShippingInfos
               .Include(o => o.LineItems)
                   .ThenInclude(l => l.Product).ToListAsync();

        public void SaveOrder(ShippingInfo shippingInfos)
        {
            _context.AttachRange(shippingInfos.LineItems.Select(l => l.Product));
            if (shippingInfos.ShippingInfoID == 0)
            {
                _context.ShippingInfos.Add(shippingInfos);
            }
            _context.SaveChanges();
        }

        public async Task SaveOrderAsync(ShippingInfo shippingInfos)
        {
            _context.AttachRange(shippingInfos.LineItems.Select(l => l.Product));
            if (shippingInfos.ShippingInfoID == 0)
            {
                _context.ShippingInfos.Add(shippingInfos);
            }
            await _context.SaveChangesAsync();
        }

        public ShippingInfo Search(int shippingInfosID) =>
            _context.ShippingInfos.FirstOrDefault(o => o.ShippingInfoID == shippingInfosID);

        public async Task<ShippingInfo> SearchAsync(int registrationID) =>
         await _context.ShippingInfos
            .Include(o => o.LineItems)
                .ThenInclude(l => l.Product)
            .FirstOrDefaultAsync(o => o.ShippingInfoID == registrationID);
    }
}
