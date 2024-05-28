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
using GiLuFlixBack.Models.ReviewDTO;
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
        public async Task<IActionResult> RateMovie([FromForm] ReviewForm review)
        {
            //retrieving user data
            var claimsPrincipal = HttpContext.User;
            // if (claimsPrincipal.Identity.IsAuthenticated)
            // {
            string UserId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            string sendToReview = $"INFORMAÇÃO RECEBIDA DO FORMULARIO (review):\n" +
                                $"Texto avaliação: {review.ReviewText}\n" +
                                $"Item Rating: {review.Rating}\n" +
                                $"Review user ID: {UserId}\n" +
                                $"Review item ID: {review.ItemId}\n" +
                                $"Texto id: {review.ReviewId}\n";
            Console.WriteLine(sendToReview);

            if (UserId != null)
            {
                Console.WriteLine("CHAMANDO O METODO Pstring UserIdOST REVIEW");
                _reviewRepository.PostReview(review, UserId);
            }
            else
            {
                Console.WriteLine("METODO POST REVIEW NÃO FOI CHAMADO");
            }
            // }
            return RedirectToAction("Index","Movies");
        }
    }
