using AttributeRouting.Web.Http;
using Chat.Models;
using Chat.Repositories;
using Chat.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class UsersController : ApiController
    {
        private UserRepository repository;
        public UsersController(IRepository<User> repo)
        {
            this.repository = repo as UserRepository;
        }

        // GET api/Users
        public IEnumerable<UserModel> GetUsers()
        {
            var users = this.repository.All();

            var userModels =
                                from user in users
                                select new UserModel()
                                {
                                    Id = user.Id,
                                    Nickname = user.Nickname
                                };

            return userModels.ToList();
        }

        [POST("api/users/login")]
        public HttpResponseMessage LoginUser(UserModelLogin user)
        {
            string nickname = string.Empty;

            Chat.Models.User userFull = new Chat.Models.User()
            {
                Username = user.Username,
                Password = user.Password
            };

            var userLog = this.repository.LoginUser(userFull);

            var loggedUser = new UserModelLogged()
            {
                Nickname = userLog.Nickname,
                SessionKey = userLog.SessionKey
            };


            return Request.CreateResponse(HttpStatusCode.OK, loggedUser.SessionKey);
        }

        [POST("api/users/register")]
        public HttpResponseMessage RegisterUser(UserModelRegister user)
        {
            Chat.Models.User userFull = new Chat.Models.User()
            {
                Username = user.Username,
                Password = user.Password,
                Nickname = user.Nickname
            };

            var userReg = this.repository.Add(userFull);
            var userLog = this.repository.LoginUser(userReg);

            var loggedUser = new UserModelLogged()
            {
                Nickname = userLog.Nickname,
                SessionKey = userLog.SessionKey
            };

            return Request.CreateResponse(HttpStatusCode.OK, loggedUser.SessionKey);
        }

        [GET("api/users/logout")]
        public HttpResponseMessage LogoutUser(string sessionKey)
        {
            this.repository.LogoutUser(sessionKey);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //// PUT api/Users/5
        //public HttpResponseMessage PutUser(int id, User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //// POST api/Users
        //public HttpResponseMessage PostUser(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //// DELETE api/Users/5
        //public HttpResponseMessage DeleteUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    db.Users.Remove(user);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, user);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}