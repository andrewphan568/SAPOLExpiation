using Assignment2.Models;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Globalization;

namespace Assignment2.Controllers
{
    public class ExpiationsWebAPIController : ApiController
    {
        private ExpiationEntities db = new ExpiationEntities();

        /* Use this request to get a list of expiation categories 
         * for each year and the number of tickets issued */

        // GET: api/ExpiationsWebAPI
        [HttpGet]
        public object GetExpiationsWebAPI()
        {
            var expiationCodes = (
                from ec in db.ExpiationCodes
                join e in db.Expiations
                on ec.expiationOffenceCode equals e.expiationOffenceCode
                select new
                {
                    ec.category,
                    ec.expiationCategory,
                    e.incidentStartDate.Year
                })
                   .GroupBy(p => new
                   {
                       p.category,
                       p.expiationCategory,
                       year = p.Year
                   })
                   .Select(p => new
                   {
                       p.Key.category,
                       p.Key.expiationCategory,
                       p.Key.year,
                       ticketCount = p.Count()
                   })
                   .OrderBy(p => p.year)
                   .ThenBy(p => p.category)
                .ToList();
            return expiationCodes;
        }

        /*
         * Use this function to generate the Expiation Graph
         * Selecting a category of speeding and year from the
         * Expiations table will return a JSON for your graph
         * containing the monthly breakdown and number of tickets
         * issued for that category of speeding in the chosen year
         */
        // GET: /api/ExpiationsWebAPI?category=xx&year=yyyy
        [HttpGet]
        public object GetExpiationsWebAPI(int category, int year)
        {
            var expiations = (
                          from ec in db.ExpiationCodes
                          join e in db.Expiations
                          on ec.expiationOffenceCode equals e.expiationOffenceCode
                          where e.incidentStartDate.Year == year
                          && ec.category == category
                          select new
                          {
                              ec.category,
                              ec.expiationCategory,
                              ec.expiationOffenceCode,
                              ec.expiationOffenceLongDescription,
                              e.incidentStartDate.Month
                          })
                             .GroupBy(p => new
                             {
                                 p.category,
                                 p.expiationCategory,
                                 monthNo = p.Month,
                                 monthName = ""
                             })
                             .Select(p => new ExpiationData
                             {
                                 category = p.Key.category,
                                 expiationCategory = p.Key.expiationCategory,
                                 monthNo = p.Key.monthNo,
                                 ticketCount = p.Count()
                             })
                             .OrderBy(p => p.monthNo)
                             .ThenBy(p => p.category)
                          .ToList();

            // the below updates can also be written as Lambda expressions
            // Here, the monthName are being calculated
            // this is not possible using LINQ/Lambda -> SQL as it is unable to perform the translation
            // instead we assign the values after the database has been queried!
            expiations = expiations.Select(p =>
              {
                  p.monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(p.monthNo);
                  return p;
              }).ToList();

            return expiations;
        }




        /* 
         * Use this function for the Expiation Details table generated
         * by clicking on a speeding category in the graph.
         * This function will return a breakdown of the different speeding
         * offences for a given month, year and category of speeding 
         */
        // GET: /api/ExpiationsWebAPI?category=a001&year=2018&monthNo=1
        [HttpGet]
        public object GetExpiationsWebAPI(int category, int year, int monthNo)
        {
            var expiationCodes = (
                          from ec in db.ExpiationCodes
                          join e in db.Expiations
                          on ec.expiationOffenceCode equals e.expiationOffenceCode
                          where e.incidentStartDate.Year == year
                          && e.incidentStartDate.Month == monthNo
                          && ec.category == category
                          select new
                          {
                              ec.category,
                              ec.expiationCategory,
                              ec.expiationOffenceCode,
                              ec.expiationOffenceLongDescription,
                              e.incidentStartDate.Month, 
                              e.penaltyWrittenOnNoticeAmount,
                              e.vehicleSpeed
                          })
                             .GroupBy(p => new
                             {
                                 p.expiationOffenceCode,
                                 p.expiationOffenceLongDescription
                             })
                             .Select(p => new 
                             {
                                 p.Key.expiationOffenceCode,
                                 p.Key.expiationOffenceLongDescription,
                                 ticketCount = p.Count(),
                                 avgFine = p.Average(p2 => p2.penaltyWrittenOnNoticeAmount),
                                 avgSpeed = p.Average(p2 => p2.vehicleSpeed)
                             })
                             .OrderBy(p => p.expiationOffenceCode)
                          .ToList();

            return expiationCodes;
        }

        protected class ExpiationData
        {
            public int category { get; set; }
            public string expiationCategory { get; set; }
            public int monthNo { get; set; }
            public string monthName { get; set; }
            public int ticketCount { get; set; }
        }
    }
}