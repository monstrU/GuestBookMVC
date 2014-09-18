using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FacadeServices;
using FacadeServices.Interfaces;
using GuestBook.DI;
using StructureMap;

namespace GuestBook
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ObjectFactory.Initialize(cfg =>
            {
                cfg.For<IGuestBookService>().Use<GuestBookService>();
                cfg.For<IDataProvider>().Use<DataProvider>();
            });
        

    
            DependencyResolver.SetResolver(new ControllerResolver());


        

        }

        
    }
}