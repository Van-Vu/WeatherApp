using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Datatables.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using WeatherApp.Controllers;
using WeatherApp.Data.Xml;
using WeatherApp.Model;
using WeatherApp.Service;
using WeatherApp.ViewModel;

namespace WeatherApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void GetLatestData()
        {
            // Arrange
            var controller = new HomeController(new WeatherService(new WeatherRepository(new XmlDataSource("WeatherData.xml"))));

            // Act
            var result = controller.GetLatestData(new DataTable { iDisplayStart = 0, iDisplayLength = 1 }) as DataTableResult;

            // Assert
            Assert.AreEqual(1, result.aaData.Count());
            Assert.AreEqual(340, result.iTotalDisplayRecords);
            Assert.AreEqual(340, result.iTotalRecords);
        }

        [TestMethod]
        public void ViewDetail()
        {
            // Arrange
            var controller = new HomeController(new WeatherService(new WeatherRepository(new XmlDataSource("WeatherData.xml"))));

            // Act
            var result = controller.ViewDetail("ADELONG POST OFFICE") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var returnModel = result.Model as WeatherDetailViewModel;
            Assert.IsNotNull(returnModel);
            Assert.AreEqual("ADELONG POST OFFICE", returnModel.StationName);
        }

        [TestMethod]
        public void SearchStationWithCorrectDataExpectsOneObservationObject()
        {
            // Arrange
            var controller = new HomeController(new WeatherService(new WeatherRepository(new XmlDataSource("WeatherData.xml"))));

            // Act
            var result = controller.SearchStation(new SearchPanelViewModel
                {
                    StationName = "adelong",
                    FromDate = new DateTime(2010, 9, 30),
                    ToDate = new DateTime(2010, 9, 30)
                }) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var returnData = result.Data as List<Observation>;
            Assert.IsNotNull(returnData);
            Assert.AreEqual(1, returnData.Count());
            Assert.AreEqual("ADELONG POST OFFICE", returnData.FirstOrDefault().StationName);
        }

        [TestMethod]
        public void SearchStationWithDummyDataExpectsNoObject()
        {
            // Arrange
            var controller = new HomeController(new WeatherService(new WeatherRepository(new XmlDataSource("WeatherData.xml"))));

            // Act
            var result = controller.SearchStation(new SearchPanelViewModel
            {
                StationName = "this is test",
                FromDate = new DateTime(2010, 9, 30),
                ToDate = new DateTime(2010, 9, 30)
            }) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            var returnData = result.Data as List<Observation>;
            Assert.IsNotNull(returnData);
            Assert.AreEqual(0, returnData.Count());
        }
    }
}
