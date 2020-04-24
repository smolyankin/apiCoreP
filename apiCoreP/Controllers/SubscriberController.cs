using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apiCoreP.Model;
using apiCoreP.Requests;
using apiCoreP.Services;
using apiCoreP.Responses;
using apiCoreP.Attributes;
using apiCoreP.Enums;
using Microsoft.AspNetCore.Http.Extensions;

namespace apiCoreP.Controllers
{
    /// <summary>
    /// subscriber controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriberService"></param>
        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        /// <summary>
        /// get subscribers with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Read)]
        [HttpGet]
        public async Task<ActionResult<SubscribersResponse>> Subscribers(int? skip = 0, int? count = 10)
        {
            return await _subscriberService.GetSubscribers(skip, count);
        }

        /// <summary>
        /// get subscriber by id for edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Edit)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetById(int id)
        {
            var subscriber = await _subscriberService.GetById(id);
            if (subscriber == null)
                return NotFound();

            return subscriber;
        }

        /// <summary>
        /// create subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberRequest request)
        {
            var subscriber = await _subscriberService.Create(request);

            return Created(HttpContext.Request.Path, subscriber);
        }

        /// <summary>
        /// update subscriber
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(EditSubscriberRequest request)
        {
            var subscriber = await _subscriberService.Edit(request);
            if (subscriber == null)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// delete subscriber
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subscriber = await _subscriberService.GetById(id);
            if (subscriber == null)
                return NotFound();

            await _subscriberService.Delete(subscriber);

            return NoContent();
        }

        /// <summary>
        /// get subscribers for printing
        /// </summary>
        /// <returns></returns>
        [CheckAccess(UserRoleType.Print)]
        [HttpGet("print")]
        public async Task<ActionResult<List<Subscriber>>> SubscribersToPrint()
        {
            return await _subscriberService.SubscribersToPrint();
        }
    }
}
