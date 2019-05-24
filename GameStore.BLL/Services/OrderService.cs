using System;
using System.Collections.Generic;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;
using AutoMapper;

namespace GameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork rep { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            rep = uow;
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            Game game = rep.Games.Get(orderDto.GameId);
            if (game == null)
            {
                throw new ValidationException("Игра не найдена", "");
            }
            Order order = new Order
            {
                DateTime = DateTime.Now,
                GameId = game.Id,
                KeyId = orderDto.KeyId,
                UserId = orderDto.UserId
            };
            rep.Orders.Create(order);
            rep.Save();
        }

        public IEnumerable<GameDTO> GetGames()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Game, GameDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(rep.Games.GetAll());
        }

        public GameDTO GetGame(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id игры", "");
            var game = rep.Games.Get(id.Value);
            if (game == null)
                throw new ValidationException("Игра не найден", "");
            return new GameDTO
            {
                Id = game.Id,
                Name = game.Name,
                Price = game.Price,
                Description = game.Description,
                Genre = game.Genre,
                Platform = game.Platform,
                SystemRequirement = game.SystemRequirement
            };
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(rep.Orders.GetAll());
        }

        public void Dispose()
        {
            rep.Dispose();
        }
    }
}
