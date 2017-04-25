using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ASP.Models;
using LeedsSharp.NHib.Domain.Models.Entities;
using LeedsSharp.NHib.Domain.NHib.Extensions;
using LeedsSharp.NHib.UI.Attributes;
using NHibernate;
using NHibernate.Linq;

namespace LeedsSharp.NHib.UI.Controllers
{
    public class PersonController : Controller
    {
	    private readonly ISession _session;
	    private readonly IStatelessSession _statelessSession;

	    public PersonController(ISession session, IStatelessSession statelessSession)
	    {
		    _session = session;
		    _statelessSession = statelessSession;
	    }

		#region Index

		public ActionResult Index()
		{
			// Retrieve all persons
			var allPersons = _session.Query<Person>()
				.FetchMany(x => x.PermittedServers)
				.OrderByDescending(x => x.LastUpdated ?? x.DateCreated)
				.ToList();

			// Determine id of the last person added
			var lastPersonId = allPersons.Select(x => x.Id).First();

			// Ask NHibernate to retrieve the last person
			var lastPerson = _session.Get<Person>(lastPersonId);

			return View(new PersonPageViewModel()
			{
				AllPersons = allPersons,
				LastPersonCreated = lastPerson
			});
		}

		#endregion

		#region Add

		public ActionResult Add()
	    {
		    return View(new Person());
	    }

	    [HttpPost]
	    [AsTransaction(IsolationLevel.RepeatableRead)]
	    public ActionResult Add(Person person)
	    {
		    int serverCount = _session.Query<Server>().Count();

		    // Add login audit
		    person.LoginHistory.Add(new Login
		    {
			    Person = person,
			    DateLogged = DateTime.UtcNow,
			    Ip = IPAddress.Parse("127.0.0.1")
		    });

		    // Create and assign dev machine
		    person.PermittedServers.Add(new Server
		    {
			    Name = $"DEV-{(++serverCount).ToString("000")}",
			    AssignedPeople = new[] { person },
			    IpWhitelist = new[] { "127.0.0.1" }
		    });

		    _session.Save(person);

		    return RedirectToAction("Index");
	    }

		#endregion

		#region Edit

		public ActionResult Edit(Guid id)
	    {
		    var person = _session.Get<Person>(id);

		    return View("EditLite", person);
	    }

	    [HttpPost]
	    [AsTransaction(IsolationLevel.RepeatableRead)]
	    public ActionResult Edit(Person person)
	    {
		    _session.Merge(person);

		    return RedirectToAction("Index");
	    }

		#endregion

	    #region Extended edit

	    /*public ActionResult Edit(Guid id)
        {
	        var person = _session.Get<Person>(id);

	        var model = new EditPersonFormModel()
	        {
		        Id = person.Id,
		        FirstName = person.FirstName,
		        LastName = person.LastName,
		        Line1 = person.Address.Line1,
		        Line2 = person.Address.Line2,
		        TownCity = person.Address.TownCity,
		        County = person.Address.County,
		        PostCode = person.Address.PostCode
	        };

	        return View(model);
        }

        [HttpPost]
        [AsTransaction(IsolationLevel.RepeatableRead)]
        public ActionResult Edit(EditPersonFormModel postedModel)
        {
	        var person = _session.Get<Person>(postedModel.Id);

	        person.FirstName = postedModel.FirstName;
	        person.LastName = postedModel.LastName;
	        person.Address = new Address()
	        {
		        Line1 = postedModel.Line1,
		        Line2 = postedModel.Line2,
		        TownCity = postedModel.TownCity,
		        County = postedModel.County,
		        PostCode = postedModel.PostCode
	        };

	        return RedirectToAction("Index");
        }*/

	    #endregion

		#region Delete

		[AsTransaction(IsolationLevel.RepeatableRead)]
	    public ActionResult Delete(Guid id)
	    {
			_session.Delete<Person>(id);

			return RedirectToAction("Index");
		}

		#endregion

	    #region Futures

	    public ActionResult Futures()
	    {
			// Retrieve person count
		    var personCountQuery = _session.Query<Person>().ToFutureValue(x => x.Count());

		    // Retrieve latest last updated date/created date
		    var lastEditQuery = _session.Query<Person>()
			    .OrderByDescending(x => x.LastUpdated ?? x.DateCreated)
			    .Take(1)
			    .Select(x => x.LastUpdated ?? x.DateCreated)
			    .ToFuture();

		    // Retrieve last servers to be assigned
		    var lastAssignedServerQuery = _session.Query<Person>()
			    .OrderByDescending(x => x.DateCreated)
			    .Take(1)
			    .Select(x => x.PermittedServers.Select(s => s.Name))
			    .ToFuture();

			return View(new FuturesPageViewModel()
		    {
				PersonCount = personCountQuery.Value,
				LastEdit = lastEditQuery.First(),
				LastAssignedServer = lastAssignedServerQuery.First()
			});
	    }

	    #endregion

	}
}