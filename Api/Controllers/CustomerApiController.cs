using System;
using System.Web.Http;
using Akka.Actor;
using Business.Actors;
using Business.Commands;

namespace Api.Controllers
{
    public class CustomerApiController : ApiController
    {
        private IActorRef _commander;

        public CustomerApiController()
        {
            // TODO mudar.
            var actorSystem = ActorSystem.Create("ControlPanel");
            _commander = actorSystem.ActorOf(Props.Create(() => new CustomerCommanderActor()), ActorPaths.CustomerCommanderActor.Name);
        }

        public IHttpActionResult Create(CreateCustomer cmd)
        {
            return Json(_commander.Ask<Guid>(cmd).Result);
        }

        public IHttpActionResult ChangeName(ChangeCustomerName cmd)
        {
            
        }
        

    }
}