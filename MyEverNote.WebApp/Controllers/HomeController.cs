using MyEvernote.BusinessLayer;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using MyEverNote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MyEverNote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            //  Categoryler üzerinden gelen view talebi ve model..
            //    if (TempData["mm"]!=null)
            //    {
            //        return View(TempData["mm"] as List<Note>);
            //    }

            NoteManager nm = new NoteManager();
            return View(nm.GetAllNote().OrderByDescending(x => x.Modifiedon).ToList());
            // return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.Modifiedon).ToList());

        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);


            if (cat == null)
            {
                return HttpNotFound();
                // return RedirectToAction("Index", "Home");
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.Modifiedon).ToList());

        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();


            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        // GET:
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);

                if (res.Errors.Count > 0)
                {

                    if (res.Errors.Find(x => x.Code ==ErrorMessageCode.UserIsNotAvtive) != null)
                    {
                        ViewBag.SetLink = "E-posta Gönder";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                Session["login"] = res.Result;  // Session'a kullanıcı bilgi saklama
                return RedirectToAction("Index"); // Yönlendirme
            }
            return View(model);
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {


            if (ModelState.IsValid)
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                
                //EvernoteUser user = null;
                
                //try
                //{
                //    user=eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);
                //    throw;
                //}


                //    bool hasError = false;
                //    if (model.Username=="aa")
                //    {
                //        ModelState.AddModelError("", "Kullanıcı Adı kullanılıyor");
                //    }

                //    if (model.Email=="aa@aa.com")
                //    {
                //        ModelState.AddModelError("", "Email kullanılıyor");
                //    }

                //    foreach (var item in ModelState)
                //    {
                //        if (item.Value.Errors.Count>0)
                //        {
                //            return View(model);
                //        }
                //    }


                //if (user==null)
                //{
                //    return View(model);
                //}
                return RedirectToAction("RegisterOk");
            }

            return View(model);
        }

        public ActionResult RegisterOk()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");

        }

        public ActionResult UsserActivate(Guid activate_id)
        {

            return View();

        }

    }
 }