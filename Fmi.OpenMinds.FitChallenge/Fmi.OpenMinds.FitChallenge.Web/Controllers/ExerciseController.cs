using System.Web.Mvc;
using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class ExerciseController : Controller
    {
        private IFitChallengeDbContext context;

        public ExerciseController(IFitChallengeDbContext context) 
        {
            this.context = context;        
        }
        
        public ActionResult Index()
        {
            return View(context.Exercises);
        }

        // GET : Exercise/Create
        public ActionResult Create()
        {
            var exercise = new Exercise();
            return View(exercise);
        }

        // POST : Exercise/Create
        [HttpPost]
        public ActionResult Create( Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            context.Exercises.Add(exercise);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET : Exercise/Edit
        public ActionResult Edit(int id)
        {
            var exerciseEdit = context.Exercises.Find(id);
            return View("Edit", exerciseEdit);
        }

        // POST : Exercise/Edit
        [HttpPost]
        public ActionResult Edit(Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return View(exercise);
            }

            var oldExercise = context.Exercises.Find(exercise.Id);
            oldExercise.Name = exercise.Name;
            oldExercise.Url = exercise.Url;
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET : Exercise/Delete
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        // POST : Exercise/Delete
        [HttpPost]
        public ActionResult Delete(Exercise exercise)
        {
            var exerciseToDelete = context.Exercises.Find(exercise.Id);
            context.Exercises.Remove(exerciseToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}