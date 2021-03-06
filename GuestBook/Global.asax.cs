﻿using System;
using System.Collections.Generic;
using System.Configuration;
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

            var connectionString = ConfigurationManager.ConnectionStrings["GuestBookConnect"].ConnectionString;
            
            ObjectFactory.Initialize(cfg =>
            {
                cfg.For<IGuestBookService>().Use<GuestBookService>();
                cfg.For<IDataProvider>().Use<DataProvider>()
                   .Ctor<string>("connectionString").Is(connectionString);
            });
            

            IContainer Container;
            Container = new Container(x =>
            {
                x.For<IGuestBookService>().Use<GuestBookService>();
                x.For<IDataProvider>().Use<DataProvider>()
                    .Ctor<string>("connectionString").Is(connectionString); ;
            });
            
            Container.AssertConfigurationIsValid();

            DependencyResolver.SetResolver(new ControllerResolver());


            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

        }

        
    }
}