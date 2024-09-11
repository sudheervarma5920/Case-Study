using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterAPI.Entities;
using TwitterAPI.Repositories;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesRepository _likesRepository;

        public LikesController(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        [HttpPost("AddLike")]
        public async Task<IActionResult> AddLike( Likes likes)
        {
            // Ensure the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _likesRepository.AddLike(likes);
                return Ok(likes);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, $"An error occurred while adding the Like: {ex.Message}");
            }
        }

        [HttpDelete("DeleteLike")]
        public async Task<IActionResult> DeleteLike([FromQuery] string userId, [FromQuery] int tweetId)
        {
            // Ensure valid query parameters
            if (string.IsNullOrEmpty(userId) || tweetId <= 0)
                return BadRequest("Invalid userId or tweetId.");

            try
            {
                await _likesRepository.DeleteLike(userId, tweetId);
                return NoContent(); // No content is returned for delete operations
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, $"An error occurred while deleting the Like: {ex.Message}");
            }
        }

        [HttpGet("GetLikes")]
        public async Task<IActionResult> GetLikes([FromQuery] int tweetId)
        {
            // Ensure tweetId is valid
            if (tweetId <= 0)
                return BadRequest("Invalid tweetId.");

            try
            {
                var likes = await _likesRepository.GetLikes(tweetId);
                return Ok(likes);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, $"An error occurred while retrieving likes: {ex.Message}");
            }
        }
    }
}
