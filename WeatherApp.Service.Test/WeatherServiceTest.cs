using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherApp.Data.Xml;
using WeatherApp.Model;
using WeatherApp.ViewModel;

namespace WeatherApp.Service.Test
{
    [TestClass]
    public class WeatherServiceTest
    {
        private Mock<IDataSource> _datasourceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _datasourceMock = new Mock<IDataSource>();
        }

        [TestMethod]
        public void GetLatestDataWithSameStationExpectsLatestData()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                              {
                                  Observation = new[] {new Observation {StationName = "Station Name 1",DateTime = new DateTime(2013,4,11)},
                                                        new Observation{StationName = "Station Name 1",DateTime = new DateTime(2013,4,10)} }
                              });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.GetLatestData();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetLatestDataWithTwoDifferentStationsExpectsTwoStations()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] {new Observation {StationName = "Station Name 1",DateTime = new DateTime(2013,4,11)},
                                                        new Observation{StationName = "Station Name 2",DateTime = new DateTime(2013,4,10)} }
                          });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.GetLatestData();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetStationDataWithEmptyStationNameExpectsNull()
        {
            // Arrange
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.GetStationData(string.Empty);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetStationDataWithCorrectDataExpectsDataReturn()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) } }
                          });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.GetStationData("Station Name 1");

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetStationDataWithDummyStationNameExpectsNoObjectReturn()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) } }
                          });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.GetStationData("This is dummy station");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void SearchStationWithoutConditionExpectsNull()
        {
            // Arrange
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.SearchStation(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchStationWithCorrectDataExpectsDataReturn()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) } }
                          });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.SearchStation(new SearchConditions
                {
                    SearchString = "Station",
                    FromDate = new DateTime(2013, 4, 11)
                });

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void SearchStationWithDummyDataExpectsNoObjectReturn()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) } }
                          });
            var weatherService = new WeatherService(new WeatherRepository(_datasourceMock.Object));

            // Action
            var result = weatherService.SearchStation(new SearchConditions
            {
                SearchString = "this is dummy station"
            });

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
