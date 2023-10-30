using Microsoft.AspNetCore.Mvc;
using DataProvider.Core;

namespace DataProvider.Controllers
{
    [ApiController]
    [Route("v2/AvailableOffers")]
    public class DataController : Controller
    {
        /// <summary>
        ///     Dummy function. Return Welcome message
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            // Otherwise, return a string with the current date and time
            return $"Is up and running {DateTime.Now}";
        }

        /// <summary>
        /// </summary>
        /// <param name="aMin"></param>
        /// <param name="aMax"></param>
        /// <returns></returns>
        [HttpGet("{aSupplierId:int=0}/{aMin:float=0}/{aMax:float=0}")]
        public async Task<ActionResult<string>> Get(int aSupplierId = 0, float aMin = 0, float aMax = 0)
        {
            return await Get(aSupplierId, aMin, aMax, string.Empty);
        }

        /// <summary>
        ///     Aggregate the results from all suppliers into a single list of available car offers
        ///     The aggregated list should be sorted by price (ascending) and then by supplier name
        ///     The list can be filtered by optional parameters including but not limited to car type and price range
        /// </summary>
        /// <param name="aCarType"></param>
        /// <param name="aMin"></param>
        /// <param name="aMax"></param>
        /// <returns></returns>
        [HttpGet("{aSupplierId:int=0}/{aMin:float=0}/{aMax:float=0}/{aCarType?}")]
        public async Task<ActionResult<string>> Get(int aSupplierId = 0, float aMin = 0, float aMax = 0, string aCarType = "")
        {
            return await new CoreLogic().Main(aSupplierId, aMin, aMax, aCarType);
        }
    }
}
