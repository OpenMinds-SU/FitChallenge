using Fmi.OpenMinds.FitChallenge.Data;
using System.Web.Mvc;
using System.Linq;
using Microsoft.AspNet.Identity;
using Fmi.OpenMinds.FitChallenge.Models;

namespace Fmi.OpenMinds.FitChallenge.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if(this.User.Identity.IsAuthenticated)
            {
                var context = DependencyResolver.Current.GetService<IFitChallengeDbContext>();
                var currentUserId = User.Identity.GetUserId();
                var request = context.TrainingScheduleRequests
                    .FirstOrDefault(r => r.SportsmanId == currentUserId &&
                        r.State == TrainingScheduleRequestState.Approved);
                if (request != null)
                {
                    ViewBag.InstructorSkypeName = request.Instructor.Skype;
                }
            }
        }
    }
}