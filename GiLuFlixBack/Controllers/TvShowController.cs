using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GiLuFlixBack.Repository;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using System.Linq;
using System;

namespace GiLuFlixBack.Controllers
{

    //Notation to verify user auth
    [Authorize]
    public class TvShowController : Controller
    {
        private readonly Context _context;
        private readonly ReviewRepository _reviewRepository;

        public TvShowController(Context context, ReviewRepository reviewRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
        }

        // GET: tvShow
        public async Task<IActionResult> Index()
        {
            return View(await _context.tvShow.ToListAsync());
        }

        // GET: tvShow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            var reviews = await _reviewRepository.GetAllItemReviews(id.Value);
            tvShow.Reviews = reviews;

            return View(tvShow);
        }

        // GET: tvShow/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: tvShow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Director,Cast")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                //return CreatedAtAction(nameof(tvShow), new { id = tvShow.Id }, tvShow); 
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: tvShow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShow.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: tvShow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Title,Director,Cast")] TvShow tvShow)
        {
            if (id != tvShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: tvShow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.tvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: tvShow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tvShow = await _context.tvShow.FindAsync(id);
            if (tvShow != null)
            {
                _context.tvShow.Remove(tvShow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowExists(int id)
        {
            return _context.tvShow.Any(e => e.Id == id);
        }
    }
}