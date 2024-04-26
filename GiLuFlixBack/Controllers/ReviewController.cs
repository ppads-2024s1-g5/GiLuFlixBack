using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using GiLuFlixBack.Repository;




namespace GiLuFlixBack.Controllers;

    public class ReviewController : Controller
    {
        private readonly Context _context;

        public ReviewController(Context context)
        {
            _context = context;
        }


        //AVALIAR O FILME
        [HttpPost, ActionName("RateMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateMovie(int? id)
        {
            var movie = await _context.movie.FindAsync(id);
            if (movie != null)
            {
                //aplico logica para avaliar o filme

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
