using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherApp.Model;

namespace WeatherApp.Data.Xml.Test
{
    [TestClass]
    public class WeatherRepositoryTest
    {
        private Mock<IDataSource> _datasourceMock;
        [TestInitialize]
        public void TestInitialize()
        {
            _datasourceMock = new Mock<IDataSource>();
        }

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] {new Observation {StationName = "Station Name 1",DateTime = new DateTime(2013,4,11)}}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.GetAll();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);            
        }

        [TestMethod]
        public void GetAllWithOrder()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                        new Observation{StationName = "Station Name 1",DateTime = new DateTime(2013,4,10)} }
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.GetAll(x=>x.DateTime);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 10), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetWithCriteria()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                  new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 10) },
                                                  new Observation { StationName = "Station Name 2", DateTime = new DateTime(2013, 4, 11) }}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.Get(x => x.StationName == "Station Name 1",o=>o.DateTime);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 10), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetByNoSpecification()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                  new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 10) },
                                                  new Observation { StationName = "Station Name 2", DateTime = new DateTime(2013, 4, 11) }}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.Get(null);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
        }

        [TestMethod]
        public void GetByNamedSpecification()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                  new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 10) },
                                                  new Observation { StationName = "Station Name 2", DateTime = new DateTime(2013, 4, 11) }}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.Get(new StationNameContainsText("1"));

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
        }

        [TestMethod]
        public void GetByNamedAndDateFromSpecification()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                  new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 10) },
                                                  new Observation { StationName = "Station Name 2", DateTime = new DateTime(2013, 4, 11) }}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.Get(new StationNameContainsText("1").And(new WeatherDataRecordedFromDate(new DateTime(2013, 4, 11))));

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
            Assert.AreEqual(new DateTime(2013, 4, 11), result.FirstOrDefault().DateTime);
        }

        [TestMethod]
        public void GetByNamedAndDateFromAndDateToSpecification()
        {
            // Arrange
            _datasourceMock.Setup(x => x.BuildDataSource<Observations>())
                          .Returns(new Observations
                          {
                              Observation = new[] { new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 11) },
                                                  new Observation { StationName = "Station Name 1", DateTime = new DateTime(2013, 4, 10) },
                                                  new Observation { StationName = "Station Name 2", DateTime = new DateTime(2013, 4, 11) }}
                          });
            var weatherRepository = new WeatherRepository(_datasourceMock.Object);

            // Action
            var result = weatherRepository.Get(new StationNameContainsText("1").And(new WeatherDataRecordedFromDate(new DateTime(2013, 4, 10)).And(new WeatherDataRecordedToDate(new DateTime(2013, 4, 11)))));

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Station Name 1", result.FirstOrDefault().StationName);
        }


    }
}
