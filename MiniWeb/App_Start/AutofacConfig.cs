using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Owin;

namespace MiniWeb
{
    public static class AutofacConfig
    {
        public static void Register(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof (AutofacConfig).Assembly);
            
            // Register your MVC controllers.
            builder.RegisterControllers(typeof(AutofacConfig).Assembly);

            builder.RegisterType<UserCache>().As<IUserCache>().SingleInstance();

            // OPTIONAL: Register model binders that require DI.
            // builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            // builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}