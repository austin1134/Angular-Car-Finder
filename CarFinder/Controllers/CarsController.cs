using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarFinder.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CarFinder.Controllers
{
    public class CarsController : ApiController
    {
        CarsDb db = new CarsDb();

        /// <summary>
        /// This action retrieves a list of available car model years.
        /// </summary>
        /// <returns>IEnumerable list of years</returns>
        [Route("api/years")]
        public async Task<IHttpActionResult> GetYears()
        {
            var retval = await db.Database.SqlQuery<string>("EXEC GetYears").ToListAsync();
            if (retval != null)
                return Ok(retval);
            else
                return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Route("api/makes")]
        public async Task<IHttpActionResult> GetMakes(string year)
        {
            var retval = await db.Database.SqlQuery<string>("EXEC GetMakesByYear @year",
                new SqlParameter("year", year)).ToListAsync();
            if (retval != null)
                return Ok(retval);
            else
                return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("api/models")]
        public async Task<IHttpActionResult> GetModels(int year, string make)
        {
            var retval = await db.Database.SqlQuery<string>("EXEC GetModelsByYearAndMake @year, @make",
                new SqlParameter("year", year),
                new SqlParameter("make", make)).ToListAsync();
            if (retval != null)
                return Ok(retval);
            else
                return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("api/trims")]
        public async Task<IHttpActionResult> GetTrims(int year, string make, string model)
        {
            var retval = await db.Database.SqlQuery<string>("EXEC GetTrimsByYearMakeAndModel @year, @make, @model",
                new SqlParameter("year", year),
                new SqlParameter("make", make),
                new SqlParameter("model", model)).ToListAsync();
            if (retval != null)
                return Ok(retval);
            else
                return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        [Route("api/cars")]
        public async Task<IHttpActionResult> GetCars(string year, string make, string model, string trim)
        {
            var carData = new CarData();

            carData.car =
                await db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModelAndTrim @year, @make, @model, @trim",
                    new SqlParameter("year", year),
                    new SqlParameter("make", make),
                    new SqlParameter("model", model),
                    new SqlParameter("trim", trim)).FirstAsync();

            carData.recalls = GetRecalls(year, make, model);
            carData.imageURLS = GetImages(year, make, model, trim);

            if (carData != null)
                return Ok(carData);
            else
                return NotFound();
        }

        private Recalls GetRecalls(string year, string make, string model)
        {
            HttpResponseMessage response;
            Recalls recalls;

            using (var client = new HttpClient())
            {
                // 1) establish base client address
                client.BaseAddress = new System.Uri("http://www.nhtsa.gov/");
                try
                {
                    // 2) make request to specific URL on the client
                    response = client.GetAsync("webapi/api/Recalls/vehicle/modelyear/"
                                               + year + "/make/" + make + "/model/" + model + "?format=json").Result;
                    // 3) construct a Recalls object from the resulting JSON data
                    recalls = response.Content.ReadAsAsync<Recalls>().Result;
                }
                catch (Exception)
                {
                    //return InternalServerError(e);
                    return null;
                }
            }
            return recalls;
        }

        private string[] GetImages(string year, string make, string model, string trim)
        {
            string query = year + " " + make + " " + model + " " + trim;

            //Create a Bing container
            string rootUri = "https://api.datamarket.azure.com/Bing/Search";
            var bingContainer = new Bing.BingSearchContainer(new Uri(rootUri));

            var accountkey = "deKmywQL9zTT0DV30aJYSxVGmLCQj279OtHw0QcYDyU";

            //Configure bingContainer to use your credentials.
            bingContainer.Credentials = new NetworkCredential(accountkey, accountkey);

            //Build the query.
            var imageQuery = bingContainer.Image(query, null, null, null, null, null, null);
            imageQuery = imageQuery.AddQueryOption("$top", 5);
            var imageResults = imageQuery.Execute();
            //extract the properties needed for the images
            List<string> images = new List<string>();
            foreach (var result in imageResults)
            {
                images.Add(result.MediaUrl);
            }
            return images.ToArray();
        }
    }
}