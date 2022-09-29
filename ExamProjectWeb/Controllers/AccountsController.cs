using ExamProjectWeb.Data;
using ExamProjectWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject_Net6_Test.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _db;

        //Making the data base connectio work
        public AccountsController(ApplicationDbContext db)
        {
            _db = db;
        }


        #region SignUp
        //Changing the route, and setting up the Sign up page
        [Route("SignUp", Name = "SignUpPage")]
        public IActionResult SignUp()
        {
            ViewData["Title"] = $"Web Page - Sign Up";
            return View();
        }

        //Setting up the post version of the Sign up page
        //[ValidateAntiForgeryToken]
        [HttpPost, Route("SignUp", Name = "SignUpPage")]
        public IActionResult SignUp(UserRegistrationModel obj)
        {
            //Checks if the password and the conf. password matches
            if (obj.PasswordConfirm != obj.Password)
            {
                ModelState.AddModelError("passwordconfirm", "The passwords are not matching!");
                return View(obj);
            }


            if (ModelState.IsValid)
            {
                if (_db.UserInfos.ToList().Any(x => obj.Email.Equals(x.Email)))
                {
                    ModelState.AddModelError("email", "This eamil is already in use!");
                    return View(obj);
                }

                var UIM = new UserInformationModel();
                UIM.UserName = obj.UserName;
                UIM.Password = obj.Password;
                UIM.Email = obj.Email;



                _db.UserInfos.Add(UIM);
                _db.SaveChanges();
                return RedirectToRoute("HomePage");
            }

            return View(obj);

        }
        #endregion

        #region SignIn
        //Changing the route, and setting up the Sign in page
        [Route("SignIn", Name = "SignInPage")]
        public IActionResult SignIn()
        {
            ViewData["Title"] = $"Web Page - Sign In";
            return View();
        }

        //Setting up the post version of the Sing in page
        [HttpPost, Route("SignIn", Name = "SignInPage")]
        public IActionResult SignIn(UserInformationModel obj)
        {
            List<UserInformationModel> userList;
            UserInformationModel user;
            //Tries to make a list of the data base
            try{userList = _db.UserInfos.ToList();}
            catch (Exception)
            {
                TempData["Error"] = "No database connection!";
                return RedirectToRoute("HomePage");
            }

            //Checks if the username can be found in the data base, and saves it
            if (userList.Any(x => x.UserName.Equals(obj.UserName)))
                user = userList.Where(x => x.UserName.Equals(obj.UserName)).First();
            else
            {
                ModelState.AddModelError("username", "This username hasn't been registered yet!");
                return View(obj);
            }
            
            //Checks if the saved data model's password matches with the given password
            if (obj.Password.Equals(user.Password))
            {
                //If everything is correct, the username and if the user is admin is saved to session data
                HttpContext.Session.SetString("userName", user.UserName);
                HttpContext.Session.SetString("isAdmin", user.IsAdmin.ToString());
                return RedirectToRoute("HomePage");
            }
            else
            {
                ModelState.AddModelError("password", "The password is incorrect!");
                return View(obj);
            }
        }
        #endregion

        #region Accounts
        //Changing the route, and setting up the Account page
        [Route("Account", Name = "AccountPage")]
        public async Task<IActionResult> Account()
        {
            //Checks if the session has a saved userName
            if (HttpContext.Session.GetString("userName") != null)
            {
                //Checks if the user is an admin, if is, it hets the database's content
                if (Convert.ToBoolean(HttpContext.Session.GetString("isAdmin")))
                {
                    ViewData["Title"] = $"Web Page - Accounts";
                    return View(await _db.UserInfos.ToListAsync());
                }

                return View();
            }
            return RedirectToRoute("SignInPage");
        }
        #endregion

        #region EditAccounts
        //Changing the route, and setting up the Edit page
        [Route("Edit/UserId_{id}", Name = "EditAccount")]
        public async Task<IActionResult> Edit(int? id)
        {
            //Checks if the session has a saved userName
            if (HttpContext.Session.GetString("userName") != null)
            {
                //Checks if the user is an admin
                if (Convert.ToBoolean(HttpContext.Session.GetString("isAdmin")))
                {
                    //Checks if there is any passed id, if not redirects back to the Account page
                    if (id == null)
                    {
                        return RedirectToRoute("AccountPage");
                    }

                    var projectUser = await _db.UserInfos.FindAsync(id);
                    //Checks if there is a user with the given primary key
                    if (projectUser == null)
                    {
                        TempData["Error"] = "There is no user with the given id!";
                        return RedirectToRoute("AccountPage");
                    }
                    ViewData["Title"] = $"Edit - {projectUser.UserName}";
                    return View(projectUser);
                }
                TempData["Info"] = "You don't have the right to visit this page!";
                return RedirectToRoute("HomePage");
            }

            return RedirectToRoute("HomePage");
        }

        //Setting up the post version of the Edit in page
        [HttpPost, Route("Edit", Name = "EditAccountPost")]
        public async Task<IActionResult> Edit(int id, UserInformationModel projectUser)
        {
            if (id != projectUser.ID)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("userName") != null)
            {
                if (Convert.ToBoolean(HttpContext.Session.GetString("isAdmin")))
                {
                    try
                    {
                        _db.Update(projectUser);
                        await _db.SaveChangesAsync();
                        TempData["Success"] = "Changes have been made!";
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        TempData["Error"] = e.Message;
                        return RedirectToRoute("HomePage");
                    }
                    return RedirectToRoute("AccountPage");
                }
            }

            return View(projectUser);
        }
        #endregion

        #region DeleteAccounts
        //Changing the route, and setting up the Delete page
        [Route("Delete", Name = "DeleteAccount")]
        public async Task<IActionResult> Delete(int? id)
        {
            //Checks if the session has a saved userName
            if (HttpContext.Session.GetString("userName") != null)
            {
                //Checks if the user is an admin
                if (Convert.ToBoolean(HttpContext.Session.GetString("isAdmin")))
                {
                    //Checks if there is any passed id, if not redirects back to the Account page
                    if (id == null)
                    {
                        return RedirectToRoute("AccountPage");
                    }

                    var projectUser = await _db.UserInfos.FirstOrDefaultAsync(m => m.ID == id);
                    //Checks if there is a user with the given primary key
                    if (projectUser == null)
                    {
                        TempData["Error"] = "There is no user with the given id!";
                        return RedirectToRoute("AccountPage");
                    }
                    ViewData["Title"] = $"Delete - {projectUser.UserName}";
                    return View(projectUser);
                }
                TempData["Info"] = "You don't have the right to visit this page!";
                return RedirectToRoute("HomePage");

            }

            return RedirectToRoute("HomePage");

        }

        //Setting up the post version of the Sing in page
        [HttpPost, ActionName("Delete"), Route("Delete", Name = "DeleteAccountPost")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectUser = await _db.UserInfos.FindAsync(id);
            _db.UserInfos.Remove(projectUser);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Changes have been made!";
            return RedirectToAction(nameof(Account));
        }
        #endregion

        #region LogOut
        [Route("LogOut", Name = "LogOutPage")]
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("userName") != null)
            {
                HttpContext.Session.Remove("userName");
                HttpContext.Session.Remove("isAdmin");
            }
            return RedirectToRoute("HomePage");
        }
        #endregion
    }
}
