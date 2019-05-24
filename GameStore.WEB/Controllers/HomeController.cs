using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace GameStore.WEB.Controllers
{
    public class HomeController : Controller
    {
        IServices services;

        public HomeController(IServices serv)
        {
            services = serv;
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<GameDTO> gameDTO = services.OrderService.GetGames();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameViewModel>()).CreateMapper();
            List<GameViewModel> games = mapper.Map<IEnumerable<GameDTO>, List<GameViewModel>>(gameDTO);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_gamesListPartial", games);
            }
            return View(games);
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameViewModel>()).CreateMapper();
            var model = mapper.Map<IEnumerable<GameDTO>, List<GameViewModel>>(services.GameEditor.Search(name));
            return View(model);
        } 

        [HttpGet]
        public ActionResult Game(int id)
        {
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<GameDTO,GameViewModel>()).CreateMapper();
            GameViewModel model = mapper.Map<GameDTO, GameViewModel>(services.OrderService.GetGame(id));
            return View(model);
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
        
        //[HttpGet]
        //public ActionResult Success()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Success()
        //{
        //    return View();
        //}
    }
}