using System;
using System.Threading.Tasks;
using Application.Batch.Commands.CreateBatch;
using Application.Batch.Commands.UploadBatchFile;
using Application.Batch.Queries.GetBatchDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class BatchController : BaseController
    {
        // POST api/batch
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Create([FromBody]CreateBatchCommand command)
        {
            return Ok(new { batchId = await Mediator.Send(command)});
        }

        // GET api/batch/batchId
        [HttpGet("{batchId}")]
        [ProducesResponseType(typeof(BatchDetailModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status410Gone)]
        public async Task<ActionResult> Details(Guid batchId)
        {
            //return await Mediator.Send(new BatchDetailQuery{BatchId = batchId});
            var result = await Mediator.Send(new BatchDetailQuery{BatchId = batchId});
            if (result.ExpiryDate < DateTime.Now)
            {
                return StatusCode(410);
            }
            return Ok(result);
        }

        [HttpPost("{batchId}/{filename}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Upload(Guid batchId, string filename,
        [FromHeader(Name = "X-Content-Size")] long size,
        [FromHeader(Name = "X-MIME-Type")] string mime = "application/octet-stream")
        {
            var result = await Mediator.Send(new UploadBatchFileCommand{BatchId = batchId, MimeType = mime, FileSize = size, FileName = filename});
            return CreatedAtAction(nameof(Details), new {batchId = batchId}, null);
        }
    }
}