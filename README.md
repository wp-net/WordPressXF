# WordPressXF
This is a Xamarin.Forms app/framework (supporting Android, iOS and UWP) designed to turn easily WordPress Blogs / Sites into nice little apps.

It's built on
* [WordPressPCL (WordPress REST API Wrapper)](https://github.com/ThomasPe/WordPressPCL)

## Features
working and planned features for WordPressXF:
- [x] Show posts
- [x] Show comments
- [x] Use SplashScreen
- [ ] Settings page
- [x] Sign In
- [x] Add comment

# Quickstart

## WordPress Plugins
Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality. If you want to access protected endpoints, this library supports authentication through JSON Web Tokens (JWT) (plugin required).

* [WordPress 4.7 or newer](https://wordpress.org/)
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)

## Getting Started

Just clone or download the repo and open it in Visual Studio. Before you can build you'll need to enter the URL to your WordPress blog/site in the file [WordPressXF/WordPressXF/WordPressXF/Utils/Statics.cs](https://github.com/wp-net/WordPressXF/blob/master/WordPressXF/WordPressXF/WordPressXF/Utils/Statics.cs). And don't forget to add `/wp-json/` at the end.

```c#
namespace WordPressXF.Utils
{
    public static class Statics
    {
        public static string WordpressUrl = "http://www.example.com/wp-json/";
    }
}
```
