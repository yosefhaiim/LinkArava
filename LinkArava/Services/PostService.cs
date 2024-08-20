using LinkArava.Dal;
using LinkArava.DTO;
using LinkArava.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkArava.Services
{
    public class PostService
    {
        private DataLayer db;

        public PostService(DataLayer _db) { db = _db; }




        public async Task<List<PostModel>> getAllPosts()
        {
            return db.Posts.Include(p => p.user).ToList();
        }


        public async Task<PostModel> getPostById(int id)
        {
            return db.Posts.Include(p => p.user).FirstOrDefault(p => p.Id == id);
        }






        // יצירת פוסת חדש
        public async Task<bool> addNewPost(NewPostDTO req)
        {
            try
            {
                UserModel user = db.Users.Find(req.UserId);
                req.Post.user = user;
                db.Posts.Add(req.Post);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }





        public async Task<string> editPostBody(int postId, string newBody)
        {
            PostModel post = db.Posts.Find(postId);
            string oldBody = post.body;
            post.body = newBody;
            db.SaveChanges();
            return oldBody;
        }





        public async Task<int> deletePost(int postId)
        {
            try
            {
                PostModel post = db.Posts.Find(postId);
                db.Posts.Remove(post);
                db.SaveChanges();
                return post.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
