using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WEB.Models;
using AutoMapper;

namespace GameStore.WEB.Controllers
{
    public class HelperController : Controller
    {
        IServices services;

        public HelperController(IServices ser)
        {
            services = ser;
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public FileContentResult ToImage(string position, int? id = null)
        {
            try
            {
                var imageDTO = services.ImageEditor.GetGameImagesById(id);
                if (imageDTO != null)
                {
                    var image = imageDTO.Where(i => i.Position == position).First();
                    return File(image.ImageData, "application/png", image.FileName);
                }
            }
            catch { }
            ImageDTO image_p = services.ImageEditor.GetImages().Where(i => i.Position == position).First();
            return File(image_p.ImageData, "application/png", image_p.FileName);
        }

        [HttpGet]
        public string GetUserRole(int id)
        {
            var user = services.UserEditor.GetUser(id);
            var role = services.UserEditor.GetRoles().FirstOrDefault(r => r.Id == user.RoleId);
            return role.Name;
        }

        [HttpGet]
        public string GetGamesName(int id)
        {
            var game = services.OrderService.GetGame(id);
            return game.Name;
        }

        [HttpGet]
        public string GetUsersName(int id)
        {
            var user = services.UserEditor.GetUser(id);
            return user.Login;
        }         
    }
}