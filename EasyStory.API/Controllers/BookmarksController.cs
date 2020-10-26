using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services;
using EasyStory.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyStory.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookmarksController: ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IMapper _mapper;


        public BookmarksController(IBookmarkService bookmarkService, IMapper mapper)
        {
            _bookmarkService = bookmarkService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Bookmarks",
            Description = "List of Bookmarks",
            OperationId = "ListAllBookmarks",
            Tags = new[] { "Bookmarks" }
        )]
        [SwaggerResponse(200, "List of Bookmarks", typeof(IEnumerable<BookmarkResource>))]
        [HttpGet]
        public async Task<IEnumerable<BookmarkResource>> GetBookmarks()
        {
            var bookmark = await _bookmarkService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Bookmark>, IEnumerable<BookmarkResource>>(bookmark);
            return resources;
        }
        [SwaggerResponse(200, "Bookmark was found", typeof(BookmarkResource))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookmarkById(long id)
        {
            var bookmark = await _bookmarkService.GetByIdAsync(id);
            var resource = _mapper.Map<Bookmark, BookmarkResource>(bookmark.Resource);
            return Ok(resource);
        }
        [SwaggerResponse(200, "Bookmark was removed", typeof(BookmarkResource))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmarkAsync(long id)
        {
            var result = await _bookmarkService.DeleteBookmarkAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var bookmarkresource = _mapper.Map<Bookmark, BookmarkResource>(result.Resource);
            return Ok(bookmarkresource);
        }
        
        
        
        
    }
}