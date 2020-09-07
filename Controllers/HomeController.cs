using Microsoft.AspNetCore.Mvc;
using System;
using EFCoreCodeFirst.Models;
using System.Linq;

namespace EFCoreCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        //GET: /Home
        public ViewResult Index()
        {
            ViewBag.Time = DateTime.Now.ToLocalTime();
            return View();
        }

        //GET: /Home/Foo
        public string Foo()
        {
            return "Hello from Foo";
        }

        public string GenDB()
        {
            string returnString = "";
            string NL = "<br/>";

            using (var db = new BloggingContext())
            {
                // Create
                returnString += "Inserting a new blog" + NL;
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                returnString += "Querying for a blog" + NL;
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                returnString += "Updating the blog and adding a post" + NL;
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    });
                db.SaveChanges();

                // Delete
                returnString += "Delete the blog" + NL;
                db.Remove(blog);
                db.SaveChanges();
            }
            return returnString;
        }
    }
}
