using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class OrderDTO
    {
        public int GameId { get; set; }
        public DateTime DateTime { get; set; }
        public int KeyId { get; set; }
        public int UserId { get; set; }
    }
}
