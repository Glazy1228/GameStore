using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class KeyViewModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public bool Active { get; set; }
    }
}