using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPI.Entities;
using TwitterAPI.Repositories;

namespace TwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetRepository _tweetRepository;
        public TweetController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }
        [HttpPost, Route("AddTweet")]
        public async Task<IActionResult> Add(Tweet tweet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _tweetRepository.Add(tweet);
                return Ok(tweet);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "An error occurred while adding the tweet.");
            }
        }
        [HttpGet, Route("GetTweets")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tweets = await _tweetRepository.GetAll();
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "An error occurred while retrieving the tweets.");
            }
        }
        [HttpGet, Route("GetTweetsByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var tweets = await _tweetRepository.GetByUserIdAsync(userId);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetTweetsByTweetId/{tweetId}")]
        public async Task<IActionResult> GetByTweetId(int tweetId)
        {
            try
            {
                var tweets = await _tweetRepository.GetByTweetId(tweetId);
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditTweet")]
        public async Task<IActionResult> Edit([FromBody] Tweet tweet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _tweetRepository.Update(tweet);
                return Ok(tweet);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "An error occurred while updating the tweet.");
            }
        }
        [HttpDelete, Route("DeleteTweet/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tweetRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, "An error occurred while deleting the tweet.");
            }
        }
    }
}
