using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Data;
using PharmacyAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize]

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly PharmacyDbContext _context;

    public NotificationsController(PharmacyDbContext context)
    {
        _context = context;
    }

    // GET: api/notifications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
    {
        return await _context.Notifications.ToListAsync();
    }

    // GET: api/notifications/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetNotification(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);

        if (notification == null)
        {
            return NotFound(new { message = $"Notification with ID {id} not found." });
        }

        return Ok(notification);
    }

    // POST: api/notifications
    [HttpPost]
    public async Task<ActionResult<Notification>> PostNotification(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNotification), new { id = notification.Id }, notification);
    }

    // PUT: api/notifications/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNotification(int id, Notification notification)
    {
        if (id != notification.Id)
        {
            return BadRequest(new { message = "Notification ID mismatch." });
        }

        _context.Entry(notification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotificationExists(id))
            {
                return NotFound(new { message = $"Notification with ID {id} not found." });
            }
            else
            {
                throw;
            }
        }

        return Ok(new { message = "Notification updated successfully." });
    }

    // DELETE: api/notifications/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
        {
            return NotFound(new { message = $"Notification with ID {id} not found." });
        }

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Notification deleted successfully." });
    }

    private bool NotificationExists(int id)
    {
        return _context.Notifications.Any(e => e.Id == id);
    }
}
