using IocExample.Classes;
using Ninject;
using System;
namespace IocExample
{
    class Program
    {
        //initial code
        //static void Main(string[] args)
        //{
        //    var logger = new ConsoleLogger();
        //    var sqlConnectionFactory = new SqlConnectionFactory("SQL Connection", logger);
        //    var createUserHandler = new CreateUserHandler(new UserService(new QueryExecutor(sqlConnectionFactory), new CommandExecutor(sqlConnectionFactory), new CacheService(logger, new RestClient("API KEY"))), logger);

        //    createUserHandler.Handle();
        //}

        //#1
        //static void Main(string[] args)
        //{
        //    IKernel kernel = new StandardKernel();

        //    kernel.Bind<ILogger>().To<ConsoleLogger>();
        //    kernel.Bind<UserService>().To<UserService>();
        //    kernel.Bind<QueryExecutor>().To<QueryExecutor>();
        //    kernel.Bind<CommandExecutor>().To<CommandExecutor>();
        //    kernel.Bind<CacheService>().To<CacheService>();
        //    kernel.Bind<RestClient>()
        //        .ToConstructor(k => new RestClient("API_KEY"));
        //    kernel.Bind<CreateUserHandler>().To<CreateUserHandler>();
        //    kernel.Bind<IConnectionFactory>()
        //        .ToConstructor(k => new SqlConnectionFactory("SQL Connection", k.Inject<ILogger>()))
        //        .InSingletonScope();

        //    var createUserHandler = kernel.Get<CreateUserHandler>();

        //    createUserHandler.Handle();
        //    Console.ReadKey();

        //}

        //#2
        static void Main(string[] args)
        {

            var kernel = new Kernel();

            kernel.Bind(typeof(ILogger), typeof(ConsoleLogger));
            kernel.Bind(typeof(UserService), typeof(UserService));
            kernel.Bind(typeof(QueryExecutor), typeof(QueryExecutor));
            kernel.Bind(typeof(CommandExecutor), typeof(CommandExecutor));
            kernel.Bind(typeof(CacheService), typeof(CacheService));
            kernel.Bind(typeof(CreateUserHandler), typeof(CreateUserHandler));

            kernel.BindToObject(typeof(IConnectionFactory), new SqlConnectionFactory("SQL Connection", kernel.Get<ILogger>()));

            kernel.BindToObject(typeof(RestClient), new RestClient("API_KEY"));

            var createUserHandler = kernel.Get<CreateUserHandler>();

            createUserHandler.Handle();
            Console.ReadKey();

        }
    }
}
