using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DataLoader.Core;
using DataLoader.Functions;

namespace DataLoader.Controllers
{
    [ApiController]
    [Route("v2/ManageOffers")]
    public class ManageOffers : Controller
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
            if (aPID == 0) 
            {
                return AppData.CONST_FAIL;
            }

            ActionResult<string> result = await new CoreLogic().Main(aPID);
            return JsonSerializer.Serialize(result);
        }

        /// <summary>
        ///     Reset Cache
        /// </summary>
        /// <returns>
        ///     "OK:..." + number of entries
        ///     CONST_FAIL otherwise
        /// </returns>
        [HttpDelete()]
        public async Task<ActionResult<string>?> Delete()
        {
            await CoreHelper.LoadSuppliers();

            if (AppData.supplierEntries == null)
            {
                return null;
            }

            ActionResult<string> result = "OK:" + AppData.supplierEntries?.Count.ToString();
            return JsonSerializer.Serialize(result);
        }
    }
}
