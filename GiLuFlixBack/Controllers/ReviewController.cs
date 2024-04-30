using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GiLuFlixBack.Repository;
using System.Threading.Tasks;
using System.Security.Claims;
using GiLuFlixBack.Models;
using GiLuFlixBack.Data;
using System;




namespace GiLuFlixBack.Controllers;

    public class ReviewController : Controller
    {
        private readonly Context _context;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(Context context, IReviewRepository reviewRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
        }


        //AVALIAR O FILME
        [HttpPost, ActionName("RateMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateMovie([FromForm] Review review)
        {
            //retrieving user data
            var claimsPrincipal = HttpContext.User;
            // if (claimsPrincipal.Identity.IsAuthenticated)
            // {
            string userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            string sendToReview = $"INFORMAÇÃO RECEBIDA DO FORMULARIO (review):\n" +
                                $"Texto avaliação: {review.reviewText}\n" +
                                $"Item rating: {review.rating}\n" +
                                $"Review user ID: {userId}\n" +
                                $"Review item ID: {review.itemId}\n" +
                                $"Texto id: {review.reviewId}\n";
            Console.WriteLine(sendToReview);

            if (userId != null)
            {
                Console.WriteLine("CHAMANDO O METODO POST REVIEW");
                _reviewRepository.PostReview(review, userId);
            }
            else
            {
                Console.WriteLine("METODO POST REVIEW NÃO FOI CHAMADO");
            }
            // }
            return RedirectToAction("Index","Movies");
        }
    }
