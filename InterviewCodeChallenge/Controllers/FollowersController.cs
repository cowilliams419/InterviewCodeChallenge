using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InterviewCodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InterviewCodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        // GET api/followers
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Add a username to get followers for the given user ('api/followers/*username*/*agent*(optional)')";
        }

        // GET api/followers/id
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var user = new User() { ID = id, Level = 1, Followers = new List<User>() };
            var completedUser = GetFollowers(user);
            if (completedUser == null)
                return "Failed to process user";

            return JsonConvert.SerializeObject(completedUser);
        }    
        
        // GET api/followers/id/agent
        [HttpGet("{id}/{agent}")]
        public ActionResult<string> Get(string id, string agent)
        {
            var user = new User() { ID = id, Level = 1, Followers = new List<User>() };
            var completedUser = GetFollowers(user);
            if (completedUser == null)
                return "Failed to process user";

            return JsonConvert.SerializeObject(completedUser);
        }

        public User GetFollowers(User user, string agent = "cowilliams419")
        {
            var uri = "https://api.github.com/users/" + user.ID + "/followers";
            var response = GetResponseFromGitHub(new Uri(uri), agent);
            if (response == null)
                return null;
            dynamic followersJson = JsonConvert.DeserializeObject(response);
            var count = 0;

            foreach (var i in followersJson)
            {
                if (count == 5)
                    break;
                var newFollower = new User() { ID = i.login, Level = user.Level + 1 };
                if (newFollower.Level <= 1)
                {
                    newFollower.Followers = new List<User>();
                    newFollower = GetFollowers(newFollower);
                }
                user.Followers.Add(newFollower);
                count++;
            }

            return user;
        }

        private string GetResponseFromGitHub(Uri uri, string agent)
        {
            try
            {

                var request = WebRequest.Create(uri) as HttpWebRequest;
                request.ContentType = "application/json";
                request.Headers.Add("User-Agent", agent);
                var response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
