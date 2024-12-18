using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRMVC.Data;
using SignalRMVC.Models;

namespace SignalRMVC.Controllers;

[Route("/[controller]")]
[ApiController]
public class PrivateChatsController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    // GET: api/PrivateChats
    [HttpGet]
    [Route("/[controller]/GetPrivateChats")]
    public async Task<ActionResult<IEnumerable<PrivateChat>>> GetPrivateChats()
    {
        if (_context.PrivateChats == null)
            return NotFound();
        return await _context.PrivateChats.ToListAsync();
    }

    [HttpGet]
    [Route("/[controller]/GetPrivateChatUser")]
    public async Task<ActionResult<IEnumerable<object>>> GetPrivateChatUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var users = await _context.Users.ToListAsync();
        if (users is null)
            return NotFound();
        return users.Where(u => u.Id != userId).Select(u => new { u.Id, u.UserName }).ToList();
    }

    /*// GET: api/PrivateChats/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<PrivateChat>> GetPrivateChat(int id)
    //{
    //    var privateChat = await _context.PrivateChats.FindAsync(id);

    //    if (privateChat == null)
    //    {
    //        return NotFound();
    //    }

    //    return privateChat;
    //}

    // PUT: api/PrivateChats/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutPrivateChat(int id, PrivateChat privateChat)
    //{
    //    if (id != privateChat.ChatId)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(privateChat).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!PrivateChatExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}*/

    // POST: api/PrivateChats
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Route("/[controller]/PostPrivateChat")]
    public async Task<ActionResult<PrivateChat>> PostPrivateChat(PrivateChat privateChat)
    {
        if (_context.PrivateChats == null)
            return Problem("Entity set 'ApplicationDbContext.PrivateChats' is null");
        _context.PrivateChats.Add(privateChat);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPrivateChat", new { id = privateChat.ChatId }, privateChat);
    }

    // DELETE: api/PrivateChats/5
    [HttpDelete("{id}")]
    [Route("/[controller]/DeletePrivateChat/{id}")]
    public async Task<IActionResult> DeletePrivateChat(int id)
    {
        if (_context.PrivateChats == null)
            return NotFound();

        var privateChat = await _context.PrivateChats.FindAsync(id);
        if (privateChat == null)
            return NotFound();

        _context.PrivateChats.Remove(privateChat);
        await _context.SaveChangesAsync();
        var chat = await _context.PrivateChats.FirstOrDefaultAsync();
        return Ok(new { deleted = id, selected = (chat == null ? 0 : chat.ChatId) });
    }

    //private bool PrivateChatExists(int id)
    //{
    //    return _context.PrivateChats.Any(e => e.ChatId == id);
    //}
}
