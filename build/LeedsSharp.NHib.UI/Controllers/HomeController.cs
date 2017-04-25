using System.Linq;
using System.Web.Mvc;
using LeedsSharp.NHib.Domain.Models.Entities;
using NHibernate;
using NHibernate.Linq;

namespace LeedsSharp.NHib.UI.Controllers
{
    public class HomeController : Controller
    {
	    private readonly ISession _session;

	    public HomeController(ISession session)
	    {
		    _session = session;
	    }

		public ActionResult Index()
		{
			var allPersons = _session.Query<Person>().ToList();

			return View(allPersons);
        }

    }
}