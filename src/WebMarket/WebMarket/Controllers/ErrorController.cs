using System.Net;
using System.Web.Mvc;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(HttpStatusCode statusCode)
        {
            var viewModel = new ErrorViewModel { StatusCode = statusCode };
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    {
                        viewModel.Message = "Сторінка, яку ви намагаєтесь відкрити не існує. Змініть параметри запиту та спробуйте ще раз!";
                        break;
                    }
                case HttpStatusCode.InternalServerError:
                    {
                        viewModel.Message = "На сайті сталася помилка! Спробуйте оновити сторінку пізніше!";
                        break;
                    }
            }

            return View(viewModel);
        }
    }
}
