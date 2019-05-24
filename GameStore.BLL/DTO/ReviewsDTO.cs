using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class ReviewsDTO
    {
        public int GameId { get; set; }
        public int PersonalInfoId { get; set; }
        public string Review { get; set; }
        public DateTime DateTime { get; set; }
    }
}
