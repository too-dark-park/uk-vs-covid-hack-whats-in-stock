using System;
using System.Collections.Generic;
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

        public Post Add(int productId, int placeId)
        {
            var post = new Post()
            {
                PostedUtc = DateTime.UtcNow,
                ProductId = productId,
                PlaceId = placeId
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
