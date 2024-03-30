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

    // GET ITEM
    public async Task<ActionResult<Filme>> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var filme = await _context.filme.FirstOrDefaultAsync(m => m.Id == id);
        if (filme == null)
        {
            return NotFound();
        }
        return View(filme);
    }

    // GET Filme/Create
    public IActionResult Create()
    {
        return View();
    }

    //o bind (vinculo), serve para se proteger de ataques de overposting. 
    // bind é um processo de mapeamento automático entre os campos do formulário e atributos do modelo
    public async Task<IActionResult> Create(Filme filme)
    {
        if (ModelState.IsValid)
        {
            _context.Add(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View(filme);
        }
    }

    // DELETE
    public async Task<IActionResult> Delete(int? id)
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

    private bool FilmeExists(int id)
    {
        return _context.filme.Any(e => e.Id == id);
    }



}
