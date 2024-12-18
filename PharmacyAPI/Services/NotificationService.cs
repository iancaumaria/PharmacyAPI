using PharmacyAPI.Data;
using PharmacyAPI.Models;

namespace PharmacyAPI.Services
{
    public class NotificationService
    {
        private readonly PharmacyDbContext _db;

        public NotificationService(PharmacyDbContext db)
        {
            _db = db;
        }

        public void CheckLowStockNotifications()
        {
            var lowStockProducts = _db.Products.Where(p => p.Stock < 5).ToList();
            foreach (var product in lowStockProducts)
            {
                var notification = new Notification
                {
                    UserId = 1, // Admin user
                    Message = $"Produsul {product.Name} are stoc scăzut ({product.Stock})."
                };
                _db.Notifications.Add(notification);
            }
            _db.SaveChanges();
        }

        public void CheckNewOrders()
        {
            var recentOrders = _db.Orders
                .Where(o => o.OrderDate >= DateTime.Now.AddHours(-1)) // Comenzi din ultima oră
                .ToList();

            foreach (var order in recentOrders)
            {
                var notification = new Notification
                {
                    UserId = order.UserId,
                    Message = $"Comanda dvs. #{order.Id} a fost înregistrată."
                };
                _db.Notifications.Add(notification);
            }
            _db.SaveChanges();
        }
    }

}
