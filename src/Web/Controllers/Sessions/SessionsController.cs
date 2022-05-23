using Core.Bases;
using Infrastructure.Definitions;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Contents.Enums;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Sessions.Requests;
using Infrastructure.Modules.Sessions.Services;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers.Sessions
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class SessionsController : BaseController
    {
        private readonly ISessionService _sessionService;
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            PaginationResponse<Session> data = await _sessionService.GetAllAsync(request);
            return Ok(data, Messages.Sessions.GetSuccessfully);
        }

        [HttpGet("{sessionId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetailAsync(Guid sessionId)
        {
            (Session? session, string? errorMessage) = await _sessionService.GetDetailAsync(sessionId);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(session, Messages.Sessions.GetSuccessfully);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionRequest request)
        {
            Session session = await _sessionService.CreateAsync(request);
            return Ok(session, Messages.Sessions.CreateSuccessfully);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody] UpdateSessionRequest request)
        {
            (Session? session, string? errorMessage) = await _sessionService.GetByIdAsync(id);
            if (errorMessage is not null) return BadRequest(errorMessage);
            await _sessionService.UpdateAsync(session!, request);
            return Ok(session, Messages.Sessions.UpdateSuccessfully);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<Guid> sessionIds)
        {
            string? errorMessage = await _sessionService.DeleteRangeAsync(sessionIds);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(null, Messages.Sessions.DeleteSuccessfully);
        }

        [HttpGet("{sessionId}/Contents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetContentsBySessionId(Guid sessionId, [FromQuery] GetContentBySessionIdRequest request)
        {
            PaginationResponse<Content> contents = await _sessionService.GetContentBySessionId(sessionId, request);
            return Ok(contents, Messages.Sessions.GetSuccessfully);
        }

        [HttpDelete("{sessionId}/Contents")]
        public async Task<IActionResult> DeleteContentSession(Guid sessionId, DeleteContentSessionRequest request)
        {
            string? errorMessage = await _sessionService.DeleteRangeContentSessionAsync(sessionId, request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(null, Messages.Contents.DeleteSuccessfully);
        }

        [HttpPost("{sessionId}/Contents")]
        public async Task<IActionResult> CreateContentSession(Guid sessionId,
            List<CreateContentSessionRequest> request)
        {
            (List<ContentSession>? contentSessions, string? errorMessage) = await _sessionService.CreateRangeContentSessionAsync(sessionId, request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(contentSessions, null);
        }

        [HttpPut("{sessionId}/{contentType}/Contents")]
        public async Task<IActionResult> DeleteContentSession(Guid sessionId,  ContentType contentType,
            List<UpdateContentSessionRequest> request)
        {
            (List<ContentSession>? contentSessions, string? errorMessage) = await _sessionService.UpdateRangeContentSessionAsync(sessionId,contentType, request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(contentSessions, null);
        }

        [HttpGet("{sessionId}/Items")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemBySessionId(Guid sessionId, [FromQuery] GetItemBySessionIdRequest request)
        {
            PaginationResponse<Item> items = await _sessionService.GetItemBySessionId(sessionId, request);
            return Ok(items, Messages.Sessions.GetSuccessfully);
        }
        [HttpPost("{sessionId}/Items")]
        public async Task<IActionResult> CreateItemsAsync(Guid sessionId, [FromBody] List<CreateItemRequest> request)
        {
            Console.WriteLine("Create");
            (List<Item>? items, string? errorMessage) = await _sessionService.CreateRangeItemAsync(sessionId, request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(items, Messages.Items.CreateSuccessfully);
        }
        
        [HttpPut("{sessionId}/Items")]
        public async Task<IActionResult> UpdateItemsAsync(Guid sessionId, [FromBody] List<UpdateItemsRequest> request)
        {
            Console.WriteLine("Update");
            (List<Item>? items, string? errorMessage) = await _sessionService.UpdateRangeItemAsync(sessionId, request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(items, Messages.Items.UpdateSuccessfully);
        }
    }
}
