using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Description;
using Backend.Models;

namespace Backend.Controllers
{
    public class MealsController : ApiController
    {
     
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Meals
        public IQueryable<Meal> GetMeals()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Meals.Include("Ingredients");
        }

        // GET: api/Meals/5
           [Authorize]
        [ResponseType(typeof(Meal))]
        public IHttpActionResult GetMeal(int id)
        {
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

          
        // PUT: api/Meals/5
           [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeal(int id, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Meals
        [ResponseType(typeof(Meal))]
        [Authorize]
        public IHttpActionResult PostMeal(Meal meal)
          
           
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUsersName = RequestContext.Principal.Identity.Name;
            var currentUser = db.Users.Where(x => x.Email == currentUsersName).First();
            meal.User= currentUser;
            db.Meals.Add(meal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
        [ResponseType(typeof(Meal))]
        [Authorize]
        public IHttpActionResult DeleteMeal(int id)
        {
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            db.SaveChanges();

            return Ok(meal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(int id)
        {
            return db.Meals.Count(e => e.Id == id) > 0;
        }
    }
}