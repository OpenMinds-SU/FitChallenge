using System.Web.Mvc;
using Fmi.OpenMinds.FitChallenge.Data;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class MuscleGroupController : Controller
    {
        private IFitChallengeDbContext context;

        public MuscleGroupController(IFitChallengeDbContext context) 
        {
            this.context = context;
        }

        public ActionResult Index() 
        {
            return View(context.MuscleGroups);
        }

        public ActionResult Create()
        {
            var muscleGroup = new MuscleGroup();
            return View(muscleGroup);
        }

        [HttpPost]
        public ActionResult Create(MuscleGroup muscleGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(muscleGroup); 
            }

            context.MuscleGroups.Add(muscleGroup);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            var muscleGroup = context.MuscleGroups.Find(id);
            return View("Edit", muscleGroup);
        }

        [HttpPost]
        public ActionResult Edit(MuscleGroup muscleGroup) 
        {
            if (!ModelState.IsValid)
            {
                return View(muscleGroup);
            }

            var mGroup = context.MuscleGroups.Find(muscleGroup.Id);
            mGroup.Name = muscleGroup.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Delete(MuscleGroup muscleGroup)
        {
            int id = muscleGroup.Id;
            var itemToDelete = context.MuscleGroups.Find(id);
            context.MuscleGroups.Remove(itemToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}