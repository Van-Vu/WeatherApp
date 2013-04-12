using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Datatables.Mvc;
using WeatherApp.Model;
using WeatherApp.Service;
using WeatherApp.ViewModel;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private IWeatherService _weatherService;

        public HomeController()
        {
            
        }

        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult GetLatestData(DataTable dataTable)
        {
            var list = _weatherService.GetLatestData().ToList();

            var table = new List<List<string>>();

            list.Skip(dataTable.iDisplayStart).Take(dataTable.iDisplayLength).ToList()
                    .ForEach(x => table.Add(new List<string> { "<a  href='/Home/ViewDetail?station=" + x.StationName + "'>"+ x.StationName +"</a>", x.DateTime.ToString("MM/dd/yyyy H:mm:ss"), x.Temperature.ToString("#0.0000") }));

            var result = new DataTableResult(dataTable, list.Count, list.Count, table) {ContentEncoding = Encoding.UTF8};
            return result;
        }

        public ActionResult ViewDetail(string station)
        {
            var stationDetails = _weatherService.GetStationData(station);

            var hightestTemperature = stationDetails.Max(x => x.Temperature);
            var lowestTemperature = stationDetails.Min(x => x.Temperature);
            var timeOfHighestTemperature = stationDetails.FirstOrDefault(x => x.Temperature == hightestTemperature).DateTime;
            var timeOfLowestTemperature = stationDetails.FirstOrDefault(x => x.Temperature == lowestTemperature).DateTime;

            var detailViewModel = new WeatherDetailViewModel
                {
                    StationName = station,
                    HighestTemperature = hightestTemperature,
                    LowestTemperature = lowestTemperature,
                    TimeOfHighestTemperature = timeOfHighestTemperature,
                    TimeOfLowestTemperature = timeOfLowestTemperature,
                    MeanTemperature = stationDetails.Average(x => x.Temperature),
                    History = stationDetails.OrderByDescending(x=>x.DateTime)
                };


            return View(detailViewModel);
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchStation(SearchPanelViewModel searchCondition)
        {
            if (ModelState.IsValid)
            {
                var searchConditions = new SearchConditions
                    {
                        SearchString = searchCondition.StationName
                    };

                if (searchCondition.FromDate.HasValue)
                {
                    searchConditions.FromDate = searchCondition.FromDate.Value;
                }

                if (searchCondition.ToDate.HasValue)
                {
                    searchConditions.ToDate = searchCondition.ToDate.Value;
                }

                var list = _weatherService.SearchStation(searchConditions).ToList();

                return Json(list);
            }

            return View("Search");
        }
    }
}
