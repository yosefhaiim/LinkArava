using LinkArava.DTO;
using LinkArava.Models;
using LinkArava.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkArava.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {
        private PostService ppostService; 
        public PostController(PostService _postService) {  ppostService = _postService; }

        // הצגת כל הפוסטים
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostListDTO>> getAllPosts()
        {
            return Ok(await ppostService.getAllPosts());
        }


       
        // הצגת פוסט מסויים
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostListDTO>> getSinglePost(int id)
        {
            return Ok(await ppostService.getPostById(id));
        }



        // יצירת פוסט חדש
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> createPost([FromBody] NewPostDTO req)
        {
            bool res = await ppostService.addNewPost(req);
            if (!res)
            {
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }



        // פונקצית עריכה
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> editPost([FromBody] editPostDTO req)
        {
            string oldBody = await ppostService.editPostBody(req.postId, req.newBody);
            return oldBody != string.Empty ? Ok(oldBody) : BadRequest();
        }

        // פונקציית מחיקה
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> deletePost(int id)
        {
            int res = await ppostService.deletePost(id);
            return res != -1 ? Ok(res) : NotFound();
        }


    }
}
