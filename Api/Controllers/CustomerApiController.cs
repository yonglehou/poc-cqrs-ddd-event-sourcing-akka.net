using System;
using System.Web.Http;
using Akka.Actor;
using Akka.Configuration;
using Akka.Persistence;
using Business.Actors;
using Business.Commands;

namespace Api.Controllers
{
    public class CustomerApiController : ApiController
    {
        private readonly IActorRef _commander;

        public CustomerApiController()
        {
            // TODO mudar.
            var config = ConfigurationFactory.FromResource<Persistence>("Api.Akka.conf");
            var actorSystem = ActorSystem.Create("ControlPanel", config);
            _commander = actorSystem.ActorOf(Props.Create(() => new CustomerCommanderActor()), ActorPaths.CustomerCommanderActor.Name);
        }

        [HttpPost]
        public IHttpActionResult Create(CreateCustomer cmd)
        {
            return Json(_commander.Ask<Guid>(cmd).Result);
        }

        [HttpPost]
        public IHttpActionResult ChangeName(ChangeCustomerName cmd)
        {
            return Json(_commander.Ask<bool>(cmd).Result);
        }
        

    }
}