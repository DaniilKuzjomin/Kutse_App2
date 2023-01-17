using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string pidu = "";
            if (DateTime.Now.Month == 1) { pidu = "Jõulud."; } 
            
            else if (DateTime.Now.Month == 2) { pidu = "iseseisvuspäev."; }

            else if (DateTime.Now.Month == 3) { pidu = "Naistepäev."; }

            else if (DateTime.Now.Month == 4) { pidu = "Aprillinali."; }

            else if (DateTime.Now.Month == 5) { pidu = "Võidupüha pidu."; }

            else if (DateTime.Now.Month == 6) { pidu = "Lastekaitsepäev."; }

            else if (DateTime.Now.Month == 7) { pidu = "Ülemaailmne UFO päev."; }

            else if (DateTime.Now.Month == 8) { pidu = "Nostalgiline päev."; }

            else if (DateTime.Now.Month == 9) { pidu = "Teadmiste päev."; }

            else if (DateTime.Now.Month == 10) { pidu = "Loomade päev."; }

            else if (DateTime.Now.Month == 11) { pidu = "Veegani päev."; }

            else if (DateTime.Now.Month == 12) { pidu = "Vanaasta õhtu."; }


            ViewBag.Message = "Ootan sind meie peole! " + pidu + " Palun tule kindlasti!";

            int hour = DateTime.Now.Hour;
            if (hour <= 16)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust" : "Tere päevast";
            }
            else if (hour > 16)
            {
                ViewBag.Greeting = hour < 20 ? "Tere õhtu" : "Tere päevast";
            }
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        public ViewResult About()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            ViewBag.Mail = guest.Email;
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                return View("Thanks", guest);
            }
            else
            { return View(); }
        }
        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "yeezyresell350@gmail.com";
                WebMail.Password = "okoladdbpgzkambl";
                WebMail.From = "yeezyresell350@gmail.com";
                WebMail.Send("daniil.kuzjomin@gmail.com", "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ? "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }
        }
        public void Meeldetuletus(Guest guest)
        {
            try
            {

            
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "yeezyresell350@gmail.com";
            WebMail.Password = "okoladdbpgzkambl";
            WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ara unusta et meie peo on super! Sind ootavad väga!",
                    null, "yeezyresell350@gmail.com",
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("vecherinki-pati.jpg")) });
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Kahju! Ei saa kirja saada";
            }
        }

        GuestContext db = new GuestContext();
        PiduContext dbp = new PiduContext();
        [Authorize]

        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

        public ActionResult Peod() 
        {
            IEnumerable<Pidu> peod = dbp.Peod;
            return View(peod);


        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
            {
                return HttpNotFound();

            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");

        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View(guests);
        }
        public ActionResult Unaccept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == false);
            return View(guests);
        }
        [HttpGet]
        


        /// -------------------------------------------------------
        public ActionResult CreateP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateP(Pidu peod)
        {
            dbp.Peod.Add(peod);
            dbp.SaveChanges();
            return RedirectToAction("Peod");
        }
        [HttpGet]
        public ActionResult DeleteP(int id)
        {
            Pidu g = dbp.Peod.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("DeleteP")]
        public ActionResult DeleteConfirmedP(int id)
        {
            Pidu g = dbp.Peod.Find(id);
            if (g == null)
            {
                return HttpNotFound();

            }
            dbp.Peod.Remove(g);
            dbp.SaveChanges();
            return RedirectToAction("Peod");

        }
        [HttpGet]
        public ActionResult EditP(int? id)
        {
            Pidu g = dbp.Peod.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("EditP")]
        public ActionResult EditConfirmedP(Pidu peod)
        {
            dbp.Entry(peod).State = EntityState.Modified;
            dbp.SaveChanges();
            return RedirectToAction("Peod");
        }

    }
}