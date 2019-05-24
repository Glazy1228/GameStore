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
    public class OrderController : Controller
    {
        IServices service;

        public OrderController(IServices ser)
        {
            service = ser;
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Success(int gameId)
        {
            int userId = service.UserEditor.GetUsers().FirstOrDefault(u=> u.Login == User.Identity.Name).Id;
            KeyDTO key = service.GameEditor.GetKeysByGameId(gameId).FirstOrDefault(k => k.Active == false);
            service.GameEditor.KeyActivity(key.Id);
            OrderDTO order = new OrderDTO {  GameId = gameId, KeyId = key.Id, UserId = userId };
            service.OrderService.MakeOrder(order);
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<KeyDTO,KeyViewModel>()).CreateMapper();
            var model = mapper.Map<KeyDTO, KeyViewModel>(key);
            ViewBag.Game = service.OrderService.GetGame(gameId).Name;
            return View(model);
        }

        //[HttpPost]
        //public void CallBack()
        //{
        //    OrderDTO order = new OrderDTO { DateTime = System.DateTime.Now, KeyId = 2, GameId = 10, UserId = 6};
        //    service.OrderService.MakeOrder(order);
        //}
    }
}