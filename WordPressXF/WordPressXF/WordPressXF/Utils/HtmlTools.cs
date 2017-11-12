using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WordPressPCL.Models;

namespace WordPressXF.Utils
{
    public static class HtmlTools
    {
        public static string Strip(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string WrapContent(Post post)
        {
            var sb = new StringBuilder();
            var content = post.Content.Rendered;
            
            // remove first img from post if there's one
            if (post.Embedded.WpFeaturedmedia != null)
            {
                content = Regex.Replace(content, "^<img.*?", "");
                content = Regex.Replace(content, "^<p><img.*?</p>", "");
            }

            sb.Append("<html><head>");
            sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, user-scalable=no\">");
            sb.Append("<style>");
            sb.Append("body { max-width:700px; margin: 0 auto; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; padding: 0 10px 10px 10px; box-sizing: border-box; } div[id^=\"attachment\"] { max-width: calc(100% + 30px) !important; } img { max-width: calc(100% + 30px) !important; height: auto; margin-left: calc(-15px); margin-right: calc(-15px); } img#featured { width: calc(100% + 30px); margin-left: calc(-15px); margin-right: calc(-15px); } h1 { font-weight: lighter; } #postmeta { font-style: italic; } iframe { width: calc(100% + 30px); margin-left: calc(-15px); margin-right: calc(-15px); } @media (max-width: 639px) { img { width: calc(100% + 25px); margin-left: calc(-15px); margin-right: calc(-15px); } }");
            sb.Append("</style>");
            sb.Append("</head><body>");

            sb.Append(FeaturedImage(post));
            sb.Append($"<h1>{post.Title.Rendered}</h1>");

            var authors = new List<User>(post.Embedded.Author);
            sb.Append($"<p id=\"postmeta\">{authors[0].Name} | {post.Date}</p>");
            sb.Append(content);
            sb.Append("</body></html>");

            return sb.ToString();
        }

        public static string FeaturedImage(Post post)
        {
            if (post.Embedded.WpFeaturedmedia == null)
                return string.Empty;

            var images = new List<MediaItem>(post.Embedded.WpFeaturedmedia);
            var img = images[0];
            var imgSrc = img.SourceUrl;
            
            var sb = new StringBuilder();
            sb.Append("<img class=\"alignnone size-full\" ");
            sb.Append($"src=\"{imgSrc}\" width=\"{img.MediaDetails.Width}\" height=\"{img.MediaDetails.Height}\" ");
            sb.Append("srcset=\"");

            foreach (var size in img.MediaDetails.Sizes)
                sb.Append($"{size.Value.SourceUrl} {size.Value.Width}w ");

            sb.Append("\" ");
            sb.Append($"sizes =\"(max-width: {img.MediaDetails.Width}px) 100vw, {img.MediaDetails.Width}px\" />");

            return sb.ToString();
        }
    }
}