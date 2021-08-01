using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBlogProject.Data;

namespace TheBlogProject.Services
{
    public class SlugService : ISlugService
    {
        private readonly ApplicationDbContext _context;

        public SlugService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsUnique(string slug)
        {
            return !_context.Posts.Any(post => post.Slug == slug);
        }

        public string UrlFriendly(string title)
        {
            if ( title is null )
                return "";

            const int maxLenght = 80;
            var length = title.Length;
            var prevDash = false;

            var sb = new StringBuilder(length);

            char c;
            for ( int i = 0; i < length; i++ )
            {
                c = title[i];
                if((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevDash = false;
                }
                else if(c >= 'A' && c <= 'Z')
                {

                    sb.Append(char.ToLower(c));
                    prevDash = false;
                }
                else if(c == ' ' || c == ',' || c == '.' || c == '/' ||
                       c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if(!prevDash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevDash = true;
                    }
                }
                else if(c == '#')
                {
                    if ( i > 0 )
                        if ( title[i - 1] == 'c' || title[i - 1] == 'F' )
                            sb.Append("-sharp");
                }
                else if(c == '+')
                {
                    sb.Append("-plus");
                }
                else if((int)c >= 128)
                {
                    int prevLength = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if ( prevLength != sb.Length )
                        prevDash = false;
                }

                if ( sb.Length == maxLenght )
                    break;
            }

            if ( prevDash )
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }
    }
}
