using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GissataletMVC.Models;

namespace GissataletMVC.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            var model = GetListToSession();
            return View(model);
        }

        public ActionResult SessionErrow()
        {
            return View();
        }
        public ActionResult NewPage()
        {
            GetListToSession().Initialize();
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(Nullable<int> number)
        {
            if (Session.IsNewSession)
            {
                return View("SessionError"); //detta är en ny dvs after timeout.
            }

            var model = GetListToSession();
            if (!number.HasValue)
            {
                ModelState.AddModelError("number", "Ange ett tal");
            }
            else if (number < 1 || number > 100)
            {
                ModelState.AddModelError("number", "Gissningen misslyckades, rätta till felet och försök igen.");
                ModelState.AddModelError("number", "Talet måste vara mellan 1 och 100");
            }
            else
            {
                model.MakeGuess(number.Value);
            }
            return View(model);
        }


        private SecretNumber GetListToSession()
        {
            var guessList = Session["savedList"] as SecretNumber;
            if (guessList == null)
            {
                guessList = new SecretNumber();
                Session["savedList"] = guessList;
            }
            return guessList;

        }

    }
}