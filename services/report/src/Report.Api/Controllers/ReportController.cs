using MediatR;
using Microsoft.AspNetCore.Mvc;
using Report.Application.UseCases.CreateReport;
using Report.Application.UseCases.GetAllReports;
using Report.Application.UseCases.GetReport;
using Report.Domain.Exceptions;
using System.Net;

namespace Report.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get all reports.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllReportsRequest() { });
            return Ok(response);
        }
        /// <summary>
        /// Get the report for the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetReportRequest() { Id = id });
            return Ok(response);
        }

        /// <summary>
        /// Creates a new report.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateReportRequest item)
        {
            var response = await _mediator.Send(item);
            return Ok(response);
        }
    }
}