using SimpleForum.Database;
//using SimpleForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleForum.Entities;
using System.Diagnostics;
using SimpleForum.Models;
using System.Web.Security;
using WebMatrix.WebData;
using SimpleForum.AppSource.Helpers;
using System.Data.Entity.Validation;
using System.IO;

namespace SimpleForum.Controllers
{
    [Authorize]
    //http://metanit.com/sharp/mvc/11.5.php
    //http://metanit.com/sharp/helpdeskmvc/3.2.php
    public class ForumController : Controller
    {
        AppStorage Storage = null;

        public ForumController()
        {
            Storage = new AppStorage();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View("MainPage", Storage.MainForumPartitions.ToList());
        }

        #region Authorization Area

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View("~/Views/Auth/NewUserRegistration.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationFormModel model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                try
                {
                    model.CreationDate = DateTime.Today;
                    WebSecurity.CreateUserAndAccount(model.Email, MD5Helper.GetHash(model.ConfirmPassword),
                    new { FirstName = "Test", Type = UserType.Common, Reputation = 10, NickName = model.NickName, MessageCount = 0 });
                    WebSecurity.Login(model.Email, MD5Helper.GetHash(model.ConfirmPassword));
                    return RedirectToAction("Index", "Forum");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", e.StatusCode.ToString());
                    model.Error = e.Message;
                }
            }

            return RedirectToAction("Registration", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            if(WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if(WebSecurity.IsAuthenticated)
            {
                var user = Storage.Users.Where(u => u.Id == WebSecurity.CurrentUserId).FirstOrDefault();
                if(user.DeactivationDate != null)
                {
                    WebSecurity.Logout();
                    return PartialView("~/Views/Auth/UserNotAuthorizated.cshtml");//, new LoginFormModel());
                }

                return PartialView("~/Views/Auth/UserWellcome.cshtml", user);
            }

            //Авторизация
            return PartialView("~/Views/Auth/UserNotAuthorizated.cshtml");//, new LoginFormModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginFormModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                {
                    return RedirectToAction("Index");
                }
                if (WebSecurity.Login(model.Login, MD5Helper.GetHash(model.Password), model.RememberMe))
                {
                    return RedirectToAction("Index");
                }
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError(Guid.NewGuid().ToString(), e.StatusCode.ToString());
            }

            return RedirectToAction("Registration");
        }
        #endregion


        [AllowAnonymous]
        [HttpGet]
        public ActionResult ThreadsPage(string ForumId = "")
        {
            if(string.IsNullOrEmpty(ForumId))
            {
                return RedirectToAction("Index");
            }
            ForumPartition partition = null;
            int partitionId;
            if (!int.TryParse(ForumId, out partitionId) || (partition = Storage.ForumPartitions.Where(p => p.Id == partitionId).FirstOrDefault()) == null)
            {
                return RedirectToAction("Index");
            }

            return View("ThreadsPage", partition);
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShowThread(string ThreadId = "")
        {
            Topic topic = null;
            int topicId = -1;
            if (string.IsNullOrEmpty(ThreadId) || !int.TryParse(ThreadId, out topicId) || (topic = Storage.Topics.Where(p => p.Id == topicId).FirstOrDefault()) == null)
            {
                return RedirectToAction("Index");
            }
            
            
            //try
            //{
            //    topic.Views++;
            //    Storage.SaveChanges();
                
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            

            return View("ShowThreadPage", topic);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShowUserInforamtion(string UserId = "")
        {
            User user = null;
            int topicId;
            if (string.IsNullOrEmpty(UserId) || !int.TryParse(UserId, out topicId) || (user = Storage.Users.Where(p => p.Id == topicId).FirstOrDefault()) == null)
            {
                return RedirectToAction("Index");
            }

            return View("ShowUserInforamtionPage", user);
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public FileContentResult GetForumAvatar(string UserId)
        //{
        //    User user = null;
        //    int topicId;
        //    if (!int.TryParse(UserId, out topicId) || (user = Storage.Users.Where(p => p.Id == topicId).FirstOrDefault()) == null || user.ForumAvatar == null)
        //    {
        //        return new FileContentResult(new byte[] { }, "image/jpeg");
        //    }

        //    return new FileContentResult(user.ForumAvatar, user.ForumAvatarType);
        //}

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetForumAvatar(string UserId)
        {
            User user = null;
            int topicId;
            if (!int.TryParse(UserId, out topicId) || (user = Storage.Users.Where(p => p.Id == topicId).FirstOrDefault()) == null || user.ForumAvatar == null)
            {
                return File("~/Images/DefaultAvatar.png", "image/png");
                //return File(new byte[] { }, "image/jpeg");
            }

            return File(user.ForumAvatar, user.ForumAvatarType);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetProfilePhoto(string UserId)
        {
            User user = null;
            int topicId;
            if (!int.TryParse(UserId, out topicId) || (user = Storage.Users.Where(p => p.Id == topicId).FirstOrDefault()) == null || user.ProfilePhoto == null)
            {
                return new FileContentResult(new byte[] { }, "image/jpeg");
                //return HttpNotFound();
            }

            return new FileContentResult(user.ProfilePhoto, user.ProfilePhotoType);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            if(WebSecurity.IsAuthenticated)
            {
                User currentUser = Storage.Users.Where(u => u.Id == WebSecurity.CurrentUserId).FirstOrDefault();
                if(currentUser != null)
                {
                    return View("EditProfile", currentUser);
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(User model, HttpPostedFileBase uploadPhoto, HttpPostedFileBase uploadAvatar)
        {
            if (WebSecurity.IsAuthenticated)
            {
                User currentUser = Storage.Users.Where(u => u.Id == WebSecurity.CurrentUserId).FirstOrDefault();

                if(uploadAvatar != null && uploadAvatar.ContentLength > 0)
                {
                    currentUser.ForumAvatar = ReadFully(uploadAvatar.InputStream);
                    currentUser.ForumAvatarType = uploadAvatar.ContentType;
                }

                if (uploadPhoto != null && uploadPhoto.ContentLength > 0)
                {
                    currentUser.ProfilePhoto = ReadFully(uploadPhoto.InputStream);
                    currentUser.ProfilePhotoType = uploadPhoto.ContentType;
                }

                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;
                currentUser.Location = model.Location;

                Storage.SaveChanges();
                if (currentUser != null)
                {
                    return RedirectToAction("EditProfile");
                }
            }

            return RedirectToAction("Index");
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewThread(string ForumId = "")
        {
            if(string.IsNullOrEmpty(ForumId))
            {
                return RedirectToAction("Index");
            }
            ForumPartition partition = null;
            int partitionId;
            if (!int.TryParse(ForumId, out partitionId) || (partition = Storage.ForumPartitions.Where(p => p.Id == partitionId).FirstOrDefault()) == null)
            {
                return RedirectToAction("Index");
            }

            return View("CreateNewThreadPage", partition);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult QuickMessagePost(string postmessage, int topicId)
        {
            if(WebSecurity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(postmessage))// && TempData.ContainsKey("Topic") && TempData["Topic"] is Topic)
                {
                    Topic t = Storage.Topics.Where(tp => tp.Id == topicId).FirstOrDefault(); //TempData["Topic"] as Topic;
                    var user = Storage.Users.Where(u => u.Id == WebSecurity.CurrentUserId).FirstOrDefault();
                    if(user != null && t != null)
                    {
                        Message newMessage = new Message { CreationDate = DateTime.Now, ModeratorAction = ModedatorActionType.None, MessageOwner = user, Text = postmessage };
                        t.Messages.Add(newMessage);
                        Storage.SaveChanges();
                        return RedirectToAction("ShowThread", new { ThreadId = topicId });
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostNewThread(string subject, string message)
        {
            var fVal = TempData.Where(t => t.Key == "ForumId").FirstOrDefault().Value;
            if(fVal == null) 
            {
                return RedirectToAction("Index");
            }
            int forumId = (int)fVal;
            ForumPartition model = Storage.ForumPartitions.Where(p => p.Id == forumId).First();

            if (model == null || model.DeactivationDate != null)
            {
                return RedirectToAction("Index");
            }

            //Debug.Assert(false);
            if (!string.IsNullOrWhiteSpace(subject) && !string.IsNullOrWhiteSpace(message))
            {
                User owner = Storage.Users.Where(u => u.Id == WebSecurity.CurrentUserId).FirstOrDefault();
                if (owner == null)
                {
                    return RedirectToAction("Index");
                }
                if (owner.DeactivationDate != null)
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Index");
                }
                
                Message newMessage = new Message
                { 
                    CreationDate = DateTime.Now, 
                    MessageOwner = owner, 
                    Text = message, 
                    ModeratorAction = ModedatorActionType.None 
                };

                Storage.Messages.Add(newMessage);
                Topic newTopic = new Topic()
                {
                    TopicOwner = owner,
                    FirstMessage = newMessage,
                    TopicName = subject,
                    CreationDate = DateTime.Now,
                    Views = 0,
                    TopicType = TopicTypes.Common
                };

                Storage.Messages.Add(newMessage);
                //Storage.Topics.Add(newTopic);
                model.Topics.Add(newTopic);
                Storage.SaveChanges();
            }
            return RedirectToAction("ThreadsPage", new { ForumId = model.Id });
            //return RedirectToAction()
        }

        
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Storage.Dispose();
        }
    }
}
