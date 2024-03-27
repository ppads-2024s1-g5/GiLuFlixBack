using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiLuFlixBack.Data;
using GiLuFlixBack.Models;


namespace GiLuFlixBack.Controllers;
public class FilmeController : Controller
{
    private readonly Context _context;
    public FilmeController(Context context)
    {
        _context = context;
    }

    // GET: Filmes
    public async Task<IActionResult> Index()
    {
        return View(await _context.filme.ToListAsync());
    }

//    // GET ITEM
//    [HttpGet("{id}"),ActionName("Details")]
//    public async Task<ActionResult<Filme>> GetTodoItem(string id)
//    {
//        var filme = await _context.filme.FindAsync(id);
//
//        if (filme == null)
//        {
//            return NotFound();
//        }
//
//        return filme;
//    }

    // POST
    
    public async Task<IActionResult> Create([Bind("Id, Titulo, Diretor, ElencoPrincipal, Pais, Ano")] Filme filme)
    {
        if (ModelState.IsValid)
        {
            _context.Add(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(filme);
    }

    // DELETE
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var filme = await _context.filme
            .FirstOrDefaultAsync(m => m.Id == id);
        if (filme == null)
        {
            return NotFound();
        }

        return View(filme);
    }

    // POST: Usuarios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var filme = await _context.filme.FindAsync(id);
        _context.filme.Remove(filme);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FilmeExists(string id)
    {
        return _context.filme.Any(e => e.Id == id);
    }



}
