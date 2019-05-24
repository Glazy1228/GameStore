using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class Services : IServices
    {
        IUnitOfWork rep;

        public Services(IUnitOfWork uow)
        {
            rep = uow;
        }

        private GameEditor gameEditor;
        private UserEditor userEditor;
        private OrderService orderService;
        private AccountService accountService;
        private ImageEditor imageEditor;

        public IImageEditor ImageEditor
        {
            get
            {
                if (imageEditor == null)
                    imageEditor = new ImageEditor(rep);
                return imageEditor;
            }
        }

        public IAccountService AccountService
        {
            get
            {
                if (accountService == null)
                    accountService = new AccountService(rep);
                return accountService;
            }
        }

        public IOrderService OrderService
        {
            get
            {
                if (orderService == null)
                    orderService = new OrderService(rep);
                return orderService;
            }
        }

        public IGameEditor GameEditor
        {
            get
            {
                if (gameEditor == null)
                    gameEditor = new GameEditor(rep);
                return gameEditor;
            }
        }

        public IUserEditor UserEditor
        {
            get
            {
                if (userEditor == null)
                    userEditor = new UserEditor(rep);
                return userEditor;
            }
        }

        public void Dispose()
        {
            rep.Dispose();
        }
    }
}
