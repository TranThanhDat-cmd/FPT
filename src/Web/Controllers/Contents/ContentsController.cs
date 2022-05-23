using Infrastructure.Definitions;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Contents.Requests;
using Infrastructure.Modules.Contents.Services;
using Infrastructure.Modules.Items.Enums;
using Infrastructure.Modules.Items.Requests;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Web.Controllers.Contents
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContentsController : BaseController
    {
        private readonly IContentService _contentService;
        public ContentsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetContentRequest request)
        {
            PaginationResponse<Content> data = await _contentService.GetAllAsync(request);
            return Ok(data, Messages.Contents.GetSuccessfully);
        }

        [HttpGet("{contentId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid contentId)
        {
            (Content? content, string? errorMessage) = await _contentService.GetByIdAsync(contentId);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(content, Messages.Contents.GetSuccessfully);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContentRequest request)
        {
            Content content = await _contentService.CreateAsync(request);
            return Ok(content, Messages.Contents.CreateSuccessfully);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<Guid> contentIds)
        {
            string? errorMessage = await _contentService.DeleteRangeAsync(contentIds);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(null, Messages.Contents.DeleteSuccessfully);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateContentRequest request)
        {
            (Content? content, string? errorMessage) = await _contentService.UpdateAsync(request);
            if (errorMessage is not null) return BadRequest(errorMessage);
            return Ok(content, Messages.Contents.UpdateSuccessfully);
        }

    }
}
