using ContactManager.Application.UseCases.CreateContact;
using ContactManager.Application.UseCases.CreatePerson;
using ContactManager.Application.UseCases.DeleteContact;
using ContactManager.Application.UseCases.DeletePerson;
using ContactManager.Application.UseCases.GetAllContacts;
using ContactManager.Application.UseCases.GetAllPersons;
using ContactManager.Application.UseCases.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get all persons.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllPersonsRequest() { });

            return Ok(response);
        }
        /// <summary>
        /// Get the person for the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetPersonRequest() { Id = id });
            return Ok(response);

        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreatePersonResponse>> Create([FromBody] CreatePersonRequest item)
        {
            var response = await _mediator.Send(item);

            return Ok(response);
        }


        /// <summary>
        /// Delete existing person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {

            var response = await _mediator.Send(new DeletePersonRequest() { Id = id });

            return Ok(response);
        }

        /// <summary>
        /// Gets all contacts information of person's.
        /// </summary>
        /// <returns></returns>
        [HttpGet("contacts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetAllContactsResponse>> GetContacts()
        {

            var response = await _mediator.Send(new GetAllContactsRequest());

            return Ok(response);
        }

        /// <summary>
        /// Creates a new contact information for specified person.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{personId}/contacts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateContactResponse>> Create([FromRoute] Guid personId, [FromBody] CreateContactRequest item)
        {
            item.PersonId = personId;

            var response = await _mediator.Send(item);

            return Ok(response);
        }
        /// <summary>
        /// Delete existing contact information of specified person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{personId}/contacts/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] Guid personId, Guid id)
        {
            var response = await _mediator.Send(new DeleteContactRequest() { Id = id, PersonId = personId });

            return Ok(response);
        }
    }
}