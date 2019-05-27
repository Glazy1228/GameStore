using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.WEB.Models;
using System.Web;

namespace GameStore.WEB.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminController : Controller
    {
        IServices service;

        public AdminController(IServices ser)
        {
            service = ser;
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
        #region Lists
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsersList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            List<UserViewModel> model = mapper.Map<IEnumerable<UserDTO>, List<UserViewModel>>(service.UserEditor.GetUsers());
            return View(model);
        }

        
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult GamesList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameViewModel>()).CreateMapper();
            List<GameViewModel> model = mapper.Map<IEnumerable<GameDTO>, List<GameViewModel>>(service.OrderService.GetGames());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OrdersList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()).CreateMapper();
            List<OrderViewModel> model = mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(service.OrderService.GetOrders());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GameImagesList(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ImageDTO, ImageViewModel>()).CreateMapper();
            List<ImageViewModel> model = mapper.Map<IEnumerable<ImageDTO>, List<ImageViewModel>>(service.ImageEditor.GetGameImagesById(id));
            return View(model);
        }

        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult KeysList(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<KeyDTO,KeyViewModel>()).CreateMapper();
            var model = mapper.Map<IEnumerable<KeyDTO>, List<KeyViewModel>>(service.GameEditor.GetKeysByGameId(id));
            ViewData["Id"] = id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_keysTablePartial", model);
            }
            return View(model);
        }
        #endregion

        #region AddGame
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddGame(GameViewModel gameView)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameViewModel, GameDTO>()).CreateMapper();
            GameDTO game = mapper.Map<GameViewModel, GameDTO>(gameView);
            service.GameEditor.AddGame(game);
            game = service.OrderService.GetGames().FirstOrDefault(g => g.Name == game.Name);
            return RedirectToAction("AddGameImages", "Admin", new { gameName = game.Name });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddGameImages(string gameName)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameViewModel>()).CreateMapper();
            var game = mapper.Map<GameDTO, GameViewModel>(service.OrderService.GetGames().
                FirstOrDefault(g => g.Name == gameName));
            return View(game.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddGameImages(HttpPostedFileBase model,ImageViewModel imageView)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    byte[] imageData = new byte[model.ContentLength];
                    model.InputStream.Read(imageData, 0, imageData.Length);
                    imageView.ImageData = imageData;
                    imageView.FileName = System.IO.Path.GetFileName(model.FileName);
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ImageViewModel, ImageDTO>()).CreateMapper();
                    ImageDTO image = mapper.Map<ImageViewModel, ImageDTO>(imageView);
                    service.ImageEditor.AddImage(image);
                    return RedirectToAction("GamesList", "Admin");
                }
                else
                {
                    ModelState.AddModelError("model", "Загрузите изображение");
                }
            }
            else
            {
                ModelState.AddModelError("model","Error");
            }
            return View(imageView.GameId);
        }
        #endregion

        #region EditGame
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int GameId)
        {
            GameDTO game = service.OrderService.GetGame(GameId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO,GameViewModel>()).CreateMapper();
            var gameView = mapper.Map<GameDTO, GameViewModel>(game);
            return View(gameView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(GameViewModel model)
        {
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<GameViewModel,GameDTO>()).CreateMapper();
            var game = mapper.Map<GameViewModel, GameDTO>(model);
            //service.GameEditor.UpdateGame(game);
            service.GameEditor.DeleteGame(game.Id);
            service.GameEditor.AddGame(game);
            return RedirectToAction("AddGameImages", "Admin", new { gameName = game.Name });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(string name)
        {
            GameDTO game = service.GameEditor.Search(name).First();
            service.GameEditor.DeleteGame(game.Id);
            return RedirectToAction("GamesList", "Admin");
        }
        #endregion

        #region EditKey
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddKey(KeyViewModel key)
        {            
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<KeyViewModel,KeyDTO>()).CreateMapper();
            var k = mapper.Map<KeyViewModel, KeyDTO>(key);
            service.GameEditor.AddKey(k);
            return RedirectToAction("KeysList","Admin",new { id = key.GameId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult AddKey(int id)
        {
            return View(id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult DeleteKey(int id,int gameId)
        {
            service.GameEditor.DeleteKey(id);
            return RedirectToAction("KeysList", "Admin", new { id = gameId });
        }
        #endregion

        #region UserRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeRole(int id,string role)
        {
            service.UserEditor.ChangeRole(id,role);
            return RedirectToAction("UsersList");
        }
        #endregion
    }
}