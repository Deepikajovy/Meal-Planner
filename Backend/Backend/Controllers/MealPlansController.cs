using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
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
        [Authorize]
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
        [Authorize]
        public IEnumerable<Ingredient> GetShoppingList()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();

            var mealplan = db.MealPlans.Where(w => w.User.Id == userId).OrderByDescending(m => m.Id).FirstOrDefault();

            if (mealplan == null)
            {
                return new List<Ingredient>().AsQueryable();
            }


            var listofIngredients = mealplan.MealPlanItems.SelectMany(s => s.Meal.Ingredients);
            var groupedIngredients = listofIngredients.GroupBy(p => p.Name, p => p.Quantity, (key, g) => new { Name = key, Quantities = g }).ToList();


            var summedUpIngredients =
                groupedIngredients.Select(
                    item =>
                        new Ingredient()
                        {
                            Name = item.Name,
                            Quantity = item.Quantities.Sum(v => Convert.ToDouble(v)),
                            Measurement = db.Ingredients.First(y => y.Name == item.Name).Measurement
                        });

            return summedUpIngredients;
        }

        [Route("api/MealPlans/AddTo")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddToCurrentUsersPlan(Meal currentMeal, string day)
        {
            var currentUsersName = RequestContext.Principal.Identity.Name;
            var id = currentMeal.Id;

            var mealPlan = db.MealPlans.FirstOrDefault(w => w.User.Email == currentUsersName) ?? new MealPlan();

            var currentUser = db.Users.First(x => x.Email == currentUsersName);
            mealPlan.User = mealPlan.User ?? currentUser;



            var meal = db.Meals.Find(id);
            var mealPlanItem = new MealPlanItem
            {
                Day = day,
                Date = DateTime.Now,
                Meal = meal
            };

            //Meals is an ICollection of MealPlanData. so create the list and add the mealPlanData that we created before to it.
            mealPlan.MealPlanItems = mealPlan.MealPlanItems ?? new List<MealPlanItem>();

            mealPlan.MealPlanItems.Add(mealPlanItem);



            db.MealPlans.AddOrUpdate(mealPlan);

            db.SaveChanges();

            return Ok();
        }


        [Route("api/MealPlans/DeleteFromMealPlan/{mealPlanItemId}")]
        [Authorize]
        [AcceptVerbs("POST")]
        public IHttpActionResult DeletemealFromMealPlan(int mealPlanItemId)
        {
            var currentUsersName = RequestContext.Principal.Identity.Name;


            var mealPlan = db.MealPlans.First(w => w.User.Email == currentUsersName);
            var mealPlanItem = db.MealPlanItems.Find(mealPlanItemId);

            mealPlan.MealPlanItems.Remove(mealPlanItem);
            

            db.SaveChanges();

            return Ok(mealPlan);
        }




        // GET: api/MealPlans/5
        [ResponseType(typeof(MealPlan))]
        [Authorize]
        public IHttpActionResult GetMealPlan(int id)
        {
            var mealPlan = db.MealPlans.Find(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return Ok(mealPlan);
        }

        // PUT: api/MealPlans/5
        [ResponseType(typeof(void))]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IHttpActionResult DeleteMealPlan(int id)
        {
            var mealPlan = db.MealPlans.Find(id);
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