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
        ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Vote/Like/{mealId}")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult Like(int mealId)
        {
            var currentUsersName = RequestContext.Principal.Identity.GetUserName();
            var currentUser = db.Users.First(x => x.Email == currentUsersName);

            Vote userVote = new Vote()
            {
                Meal = db.Meals.Find(mealId),
                Likes = 1,
                User = currentUser
            };

            db.MealVotes.Add(userVote);
            db.SaveChanges();

            return Ok();
        }

        [Route("api/Vote/Dislike/{mealId}")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult Dislike(int mealId)
        {
            var currentUsersName = RequestContext.Principal.Identity.GetUserName();
            var currentUser = db.Users.First(x => x.Email == currentUsersName);

            Vote userVote = new Vote()
            {
                Meal = db.Meals.Find(mealId),
                Dislikes = 1,
                User = currentUser
            };

            db.MealVotes.Add(userVote);
            db.SaveChanges();

            return Ok();
        }
        
    }
}
