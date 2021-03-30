using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCoreLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog {Url = "http://blogs.msdn.com/adonet"});
                db.SaveChanges();

                //Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs.OrderBy(b => b.BlogId).First();

                //Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                var post1 = new Post {Title = "Hello World", Content = "I wrote an app using EF Core!"};
                var post2 = new Post { Title = "Hello World 2", Content = "I wrote an app using EF Core 2!" };
                blog.Posts.Add(post1);
                blog.Posts.Add(post2);
                blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

                //Add Tags
                Console.WriteLine("Add Tags");
                var tag1 = new Tag {Text = "Tag1"};
                var tag2 = new Tag { Text = "Tag1" };
                var tag3 = new Tag { Text = "Tag1" };

                post1.Tags = new List<Tag> {tag1, tag3};

                post2.Tags = new List<Tag> {tag1, tag2};

                db.SaveChanges();

                //Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}
