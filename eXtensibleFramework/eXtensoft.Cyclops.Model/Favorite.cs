using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyclops
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public string Username { get; set; }
        public string Model { get; set; }
        public int ModelId { get; set; }
        public DateTime Tds { get; set; }
    }
}