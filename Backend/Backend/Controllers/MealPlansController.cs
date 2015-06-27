using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Description;
using Backend.Models;
using Microsoft.AspNet.Identity;

namespace Backend.Controllers
{
    public class MealPlansController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       // protected UserManager<ApplicationUser> UserManger { get; set; }
        // GET: api/MealPlans
        //public IQueryable<MealPlan> GetMealPlans()
        //{
        //    return db.MealPlans;
        //}

         //GET: api/MealPlans
        public MealPlan GetMealPlansForLoggedInUser()
        {

            var currentUsersName = RequestContext.Principal.Identity.Name;
           // var currentUser = db.Users.Where(w => w.Email == currentUsersName).First();
            var mealplan = db.MealPlans.Where(w => w.User.Email == currentUsersName).FirstOrDefault();
           // var currentUser = UserManger.FindById(id);
           //return db.MealPlans.Where(w => w.User == currentUsers).FirstOrDefault();
            return mealplan;

        }

        
        [Route("api/MealPlans/ShoppingList")]
        [AcceptVerbs("GET")]
        public List<Ingredient> GetShoppingList()
        {
            var currentUsersName = RequestContext.Principal.Identity.Name;
          
           var mealplan = db.MealPlans.Where(w => w.User.Email == currentUsersName).First();
            List<Ingredient> listofIngredients = new List<Ingredient>();
            foreach (var meal in mealplan.Meals)
            {
                foreach (var Ing in meal.Ingredients)
                {
                    listofIngredients.Add(Ing);
                }

            }
            
            var result = listofIngredients.GroupBy(p => p.Name, p => p.Quantity, (key, g) => new {Name = key, Quantities = g});
           
              List<Ingredient> IngSummary = new List<Ingredient>();

              foreach (var item in result)
              {
                  IngSummary.Add(new Ingredient() { Name = item.Name, Quantity = item.Quantities.Sum(v => Convert.ToDouble(v)) });
              }
            return IngSummary;
        }

        [Route("api/MealPlans/AddTo")]
        //[Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddToCurrentUsersPlan(Meal currentMeal)
        {
            var currentUsersName = RequestContext.Principal.Identity.Name;
            var id = currentMeal.Id;
            if (db.MealPlans.Where(w => w.User.Email == currentUsersName).FirstOrDefault() == null)
            {
                MealPlan mealPlan = new MealPlan();
                //var meal = db.Meals.Find(id);
                mealPlan.Meals.Add(currentMeal);
                var currentUser = db.Users.Where(x => x.Email == currentUsersName).First();
                mealPlan.User = currentUser;
            }
            else
            {
            
            var mealPlan = db.MealPlans.Where(w => w.User.Email == currentUsersName).First();
           // var meal = db.Meals.Find(id);
            mealPlan.Meals.Add(currentMeal);
            }
        
            db.SaveChanges();
            return Ok();
        }




        // GET: api/MealPlans/5
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult GetMealPlan(int id)
        {
            MealPlan mealPlan = db.MealPlans.Find(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return Ok(mealPlan);
        }

        // PUT: api/MealPlans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMealPlan(int id, MealPlan mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mealPlan.Id)
            {
                return BadRequest();
            }

            db.Entry(mealPlan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealPlanExists(id))
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

        // POST: api/MealPlans
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult PostMealPlan(MealPlan mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MealPlans.Add(mealPlan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mealPlan.Id }, mealPlan);
        }

        // DELETE: api/MealPlans/5
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult DeleteMealPlan(int id)
        {
            MealPlan mealPlan = db.MealPlans.Find(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            db.MealPlans.Remove(mealPlan);
            db.SaveChanges();

            return Ok(mealPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealPlanExists(int id)
        {
            return db.MealPlans.Count(e => e.Id == id) > 0;
        }
    }
}