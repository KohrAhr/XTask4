using Microsoft.AspNetCore.Mvc;
using ServiceCollector.Functions;
using ServiceCollector.Core;
using Lib.Suppliers.Types;

namespace ServiceCollector.Controllers
{
    [ApiController]
    [Route("v1/RemoteApi")]
    public class DataController : Controller
    {

        /// <summary>
        ///     Dummy function. Return Welcome message if passed security check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            // Check if the request is secure
            if (!CoreHelper.SecCheck(Request, out string oErrorMessage))
            {
                // If not, return an error message
                return Unauthorized(oErrorMessage);
            }

            // Otherwise, return a string with the current date and time
            return $"Is up and running {DateTime.Now}";
        }

        /// <summary>
        ///     Logic Unit 1
        ///     "Fetch available car offers from each supplier API and store them in a database of your choice such as MS SQL Server or PostgreSQL"
        ///     "Ensure error handling is in place, in case any of the supplier APIs fail to respond or provide erroneous data"
        /// </summary>
        /// <param name="aPID">
        ///     Id of server
        /// </param>
        /// <returns>
        ///     "OK:..." if data has been fetched from remote server and successfully stored in db
        ///     CONST_FAIL otherwise
        /// </returns>
        [HttpPost("{aPID:int}")]
        public async Task<ActionResult<string>> Put(int aPID)
        {
            // Check if the request is secure
            if (!CoreHelper.SecCheck(Request, out string oErrorMessage))
            {
                // Return an error message if the request is not secure
                return Unauthorized(oErrorMessage);
            }

            return await new CoreLogicUnitA(new SupplierHelper()).Main(aPID);
        }

        /// <summary>
        ///     I don't know other way to pass def value :/
        /// </summary>
        /// <param name="aMin"></param>
        /// <param name="aMax"></param>
        /// <returns></returns>
        [HttpGet("{aMin:float=0}/{aMax:float=0}")]
        public async Task<ActionResult<string>> Get(float aMin = 0, float aMax = 0)
        {
            return await Get(aMin, aMax, string.Empty);
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
        [HttpGet("{aMin:float=0}/{aMax:float=0}/{aCarType?}")]
        public async Task<ActionResult<string>> Get(float aMin = 0, float aMax = 0, string aCarType = "")
        {
            // Check if the request is secure
            if (!CoreHelper.SecCheck(Request, out string oErrorMessage))
            {
                // Return an error message if the request is not secure
                return Unauthorized(oErrorMessage);
            }

            // Logic Unit 2
            return await new CoreLogicUnitB().Main(aMin, aMax, aCarType);
        }
    }
}
