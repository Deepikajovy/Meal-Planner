using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Backend.Models;
using Microsoft.AspNet.Identity;

namespace Backend.Controllers
{
    public class VoteController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Vote/Like/{mealId}")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult Like(int mealId) 
        {
            var currentUsersName = RequestContext.Principal.Identity.GetUserName();
            var currentUser = db.Users.First(x => x.Email == currentUsersName);
            var currentMeal = db.Meals.Find(mealId);
            
            IEnumerable<Vote> userVotes = db.MealVotes.Where(m => m.User.Email == currentUser.Email);
            if (userVotes.FirstOrDefault(n => n.Meal == currentMeal) == null)
            {
                Vote userVote = new Vote()
                {
                    Meal = currentMeal,
                    Likes = 1,
                    User = currentUser
                };

                currentMeal.Likes += 1;
                db.Meals.AddOrUpdate(currentMeal);
                db.MealVotes.Add(userVote);

                db.SaveChanges();
            }
            else
            {

                return BadRequest("You cannot vote more than once");
                //("You cannot vote more than once.");
            }

            return Ok(currentMeal.Likes);
        }

        [Route("api/Vote/Dislike/{mealId}")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult Dislike(int mealId)
        {
            var currentUsersName = RequestContext.Principal.Identity.GetUserName();
            var currentUser = db.Users.First(x => x.Email == currentUsersName);
            var currentMeal = db.Meals.Find(mealId);

            IEnumerable<Vote> userVotes = db.MealVotes.Where(m => m.User.Email == currentUser.Email);
            if (userVotes.FirstOrDefault(n => n.Meal == currentMeal) == null)
            {
                Vote userVote = new Vote()
                {
                    Meal = db.Meals.Find(mealId),
                    Dislikes = 1,
                    User = currentUser
                };

                currentMeal.Dislikes += 1;
                db.Meals.AddOrUpdate(currentMeal);
                db.MealVotes.Add(userVote);
                db.SaveChanges();
            }
            else
            {
                return BadRequest("You cannot vote more than once.");
            }
            return Ok(currentMeal.Dislikes);
        }


   }
}
