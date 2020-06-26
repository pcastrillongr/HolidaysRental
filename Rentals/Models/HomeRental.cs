

namespace Rentals.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HomeRental
    {
        public int HomeId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    
        public virtual Owner Owner { get; set; }
    }
}
