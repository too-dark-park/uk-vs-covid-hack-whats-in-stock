using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WhatsIn.Models;

namespace WhatsIn.Services
{
    public class Posts : IPosts
    {
        private readonly WhatsInContext _context;

        public Posts(WhatsInContext context)
        {
            _context = context;
        }

        public Post Add(int productId, int placeId, string fileName = null)
        {
            if (!File.Exists(fileName))
            {
                fileName = null;
            }

            var post = new Post()
            {
                PostedUtc = DateTime.UtcNow,
                ProductId = productId,
                PlaceId = placeId,
                ImageFileName = fileName
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        public IEnumerable<Post> GetProductPosts(IEnumerable<int> productIds)
        {
            return _context.Posts.Where(x => productIds.Contains(x.ProductId));
        }
    }
}
