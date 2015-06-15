using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Fmi.OpenMinds.FitChallenge.Data;
using System;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    [Authorize]
    public class InstructorRankController : Controller
    {
        private readonly IFitChallengeDbContext context;
        private ApplicationUserManager userManager;

        public InstructorRankController(IFitChallengeDbContext context)
        {
            this.context = context;
        }

        public InstructorRankController(IFitChallengeDbContext context, ApplicationUserManager userManager) 
        {
            this.context = context;
            this.userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        public ActionResult Index(bool isAscending = false, string orderByColumn = null)
        {
            ViewBag.IsAscending = isAscending;

            var users = context.Users
                .ToList()
                .Where(user => UserManager.IsInRole(user.Id, "Instructor"));

            var orderFunc = GetOrderFunction(orderByColumn);
            users = isAscending ? users.OrderBy(orderFunc) : users.OrderByDescending(orderFunc);

            foreach (var user in users)
            {
                // Compute average rank
                var instructorRanks = context.Ranks.Where(rank => rank.InstructorId == user.Id);
                if(instructorRanks.Any()) 
                {
                    user.Rank = instructorRanks.Average(item => item.Value);
                }

                // Can assess
                string userId = this.User.Identity.GetUserId();

                var request = this.context.TrainingScheduleRequests
                    .FirstOrDefault(r => r.SportsmanId == userId &&
                        r.InstructorId == user.Id &&
                        r.State == TrainingScheduleRequestState.Approved);
                user.CanBeAssessed = request != null;
            }

            return View(users);
        }

        private Func<User, object> GetOrderFunction(string orderByColumn)
        {
            Func<User, object> orderFunc;
            switch (orderByColumn)
            {
                case "Name":
                    orderFunc = user => string.Format("{0} {1}", user.FirstName, user.LastName);
                    break;
                case "Experience":
                    orderFunc = user => user.ExperienceYears;
                    break;
                case "PhoneNumber":
                    orderFunc = user => user.PhoneNumber;
                    break;
                case "Skype":
                    orderFunc = user => user.Skype;
                    break;
                case "Rank":
                    orderFunc = user => user.Rank;
                    break;
                default:
                    orderFunc = user => user.Rank;
                    break;
            }

            return orderFunc;
        }

        public ActionResult AddRank(string instructorId, int value)
        {
            string userId = this.User.Identity.GetUserId();
            var rank = context.Ranks.FirstOrDefault(item => item.InstructorId == instructorId &&
                item.UserId == userId);
            if (rank == null)
            {
                rank = new Rank() 
                {
                    InstructorId = instructorId,
                    UserId = userId
                };
                this.context.Ranks.Add(rank);
            }

            rank.Value = value;
            
            this.context.SaveChanges();
            return Json(new { success = true });
        }
    }
}