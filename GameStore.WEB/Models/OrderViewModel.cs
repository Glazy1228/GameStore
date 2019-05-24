using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class OrderViewModel
    {
        public int GameId { get; set; }
        public DateTime DateTime { get; set; }
        public int KeyId { get; set; }
        public int UserId { get; set; }
    }
}