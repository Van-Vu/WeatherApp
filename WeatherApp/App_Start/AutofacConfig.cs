using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using WeatherApp.Data;
using WeatherApp.Data.Xml;
using WeatherApp.Model;
using WeatherApp.Service;

namespace WeatherApp.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<WeatherService>().As<IWeatherService>();
            builder.RegisterType<WeatherRepository>().As<IRepository<Observation>>();
            builder.RegisterType<XmlDataSource>().As<IDataSource>().WithParameter(new NamedParameter("fileLocation", "WeatherData.xml"));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}