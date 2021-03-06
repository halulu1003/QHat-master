using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QHat.Data;
using QHat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace QHat.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Orders.ToListAsync());
            return View(await _context.Orders.Include(i => i.User).AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderId,City,Country,FirstName,LastName,OrderDate,Phone,PostalCode,State,Total")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("City,Country,FirstName,LastName,Phone,PostalCode,State")] Order order)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                List<CartItem> items = cart.GetCartItems(_context);
                List<Orderitem> details = new List<Orderitem>();
                foreach (CartItem item in items)
                {

                    Orderitem detail = CreateOrderDetailForThisItem(item);
                    detail.Order = order;
                    details.Add(detail);
                    _context.Add(detail);

                }

                order.User = user;
                order.OrderDate = DateTime.Today;
                order.Total = ShoppingCart.GetCart(this.HttpContext).GetTotal(_context);
                order.Orderitems = details;
                _context.SaveChanges();


                return RedirectToAction("Purchased", new RouteValueDictionary(
                new { action = "Purchased", id = order.OrderId }));
            }

            return View(order);
        }

        private Orderitem CreateOrderDetailForThisItem(CartItem item)
        {

            Orderitem detail = new Orderitem();


            detail.Quantity = item.Count;
            detail.Hat = item.Hat;
            detail.UnitPrice = item.Hat.Price;

            return detail;

        }
        public async Task<IActionResult> Purchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var details = _context.Orderitems.Where(detail => detail.Order.OrderId == order.OrderId).Include(detail => detail.Hat).ToList();

            order.Orderitems = details;
            ShoppingCart.GetCart(this.HttpContext).EmptyCart(_context);
            return View(order);
        }



        // GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("OrderId,City,Country,FirstName,LastName,OrderDate,Phone,PostalCode,State,Total")] Order order)
        //{
        //    if (id != order.OrderId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.OrderId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            var order = await _context.Orders.Include(i => i.User).AsNoTracking().SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            var details = _context.Orderitems.Where(detail => detail.Order.OrderId == order.OrderId).Include(detail => detail.Hat).ToList();

            order.Orderitems = details;

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.OrderId == id);
        //}
    }
}
