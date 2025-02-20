using Microsoft.AspNetCore.Mvc;
using RealTime_Data_Analysis_Dashboard.Data;
using RealTime_Data_Analysis_Dashboard.Model;
using RealTime_Data_Analysis_Dashboard.Service;

namespace RealTime_Data_Analysis_Dashboard.Controller;


[ApiController]
[Route("api/[controller]")] // Route will be /api/tweet 
public class TweetController
{
    private readonly TweetService _tweetService;

    public TweetController(TweetService tweetService)
    {
        _tweetService = tweetService; 
    }

    [HttpGet("getTweets")] // /api/Tweet/getTweets
    public ActionResult<Tweet[]> GetTweets()
    {
        var sampleTweets = TweetSampleData.SampleDataA(); 
        return new ActionResult<Tweet[]>(sampleTweets); 
    }
}