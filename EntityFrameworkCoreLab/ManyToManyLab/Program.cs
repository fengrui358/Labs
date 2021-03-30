using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreLab;

namespace ManyToManyLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.
                var blogs = db.Blogs.ToList();
                foreach (var blog1 in blogs)
                {
                    db.Entry(blog1).Collection(s => s.Posts).Load();

                    foreach (var blog1Post in blog1.Posts)
                    {
                        db.Entry(blog1Post).Collection(s => s.PostTags);
                    }
                }

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                //Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs.OrderBy(b => b.BlogId).First();

                //Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                var post1 = new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" };
                var post2 = new Post { Title = "Hello World 2", Content = "I wrote an app using EF Core 2!" };
                blog.Posts.Add(post1);
                blog.Posts.Add(post2);
                blog.Posts.Add(new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

                //Add Tags
                Console.WriteLine("Add Tags");
                var tag1 = new Tag { Text = "Tag1" };
                var tag2 = new Tag { Text = "Tag1" };
                var tag3 = new Tag { Text = "Tag1" };
                db.Tags.Add(tag1);
                db.Tags.Add(tag2);
                db.Tags.Add(tag3);

                db.SaveChanges();

                db.PostTags.Add(new PostTag {PostId = post1.PostId, TagId = tag1.Id});
                db.PostTags.Add(new PostTag { PostId = post1.PostId, TagId = tag2.Id});

                db.PostTags.Add(new PostTag { PostId = post2.PostId, TagId = tag1.Id });
                db.PostTags.Add(new PostTag { PostId = post2.PostId, TagId = tag2.Id });

                db.SaveChanges();

                //Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}
