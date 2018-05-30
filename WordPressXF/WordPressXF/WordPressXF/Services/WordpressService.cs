using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressXF.Utils;
using Xamarin.Auth;

namespace WordPressXF.Services
{
    public class WordpressService
    {
        private readonly WordPressClient _client;

        public WordpressService()
        {
            _client = new WordPressClient(Statics.WordpressUrl);
        }

        public async Task<IEnumerable<Post>> GetLatestPostsAsync(int page = 0, int perPage = 20)
        {
            page++;

            var posts = await _client.Posts.Query(new PostsQueryBuilder
            {
                Page = page,
                PerPage = perPage,
                Embed = true
            });

            return posts;
        }
        
        public async Task<List<CommentThreaded>> GetCommentsForPostAsync(int postid)
        {        
            var comments = await _client.Comments.Query(new CommentsQueryBuilder
            {
                Posts = new[] { postid },
                Page = 1,
                PerPage = 100
            });
            
            return ThreadedCommentsHelper.GetThreadedComments(comments);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            _client.AuthMethod = AuthMethod.JWT;
            await _client.RequestJWToken(username, password);

            var isAuthenticated = await _client.IsValidJWToken();

            if (isAuthenticated)
                return await _client.Users.GetCurrentUser();

            return null;
        }

        public void Logout()
        {
            _client.Logout();
        }
    }
}
