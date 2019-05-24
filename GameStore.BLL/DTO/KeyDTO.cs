using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class KeyDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public bool Active { get; set; }
    }
}
