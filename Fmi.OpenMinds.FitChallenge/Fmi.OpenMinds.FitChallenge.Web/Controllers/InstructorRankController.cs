﻿using System.Linq;
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
    }
}