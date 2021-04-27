using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ProductData.Commands.GetProductVersions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductDataController : BaseController
    {
        //* ** POST
        [HttpPost]
        [Route("productVersions")]
        public async Task<ActionResult> ProductVersions([FromBody]List<ProductVersion> ProductVersions, string callbackUri)
        {
            GetProductVersionsCommand command = new GetProductVersionsCommand();
            command.ProductVersions = ProductVersions;
            command.CallbackUri = callbackUri;
            return Ok(await Mediator.Send(command));
        }
    }
}