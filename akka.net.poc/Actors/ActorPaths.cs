namespace Business.Actors
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData CustomerCoordinatorActor = new ActorMetaData("customer-coordinator", "akka://ControlPanel/user/customer-coordinator");
        public static readonly ActorMetaData CustomerCommanderActor = new ActorMetaData("customer-commander", "akka://ControlPanel/user/customer-commander");
    }

    public class ActorMetaData
    {
        public ActorMetaData(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }

        public string Path { get; private set; }
    }
}