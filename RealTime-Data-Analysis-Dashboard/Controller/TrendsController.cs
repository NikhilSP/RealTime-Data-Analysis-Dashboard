using Microsoft.AspNetCore.Mvc;
using RealTime_Data_Analysis_Dashboard.Data;
using RealTime_Data_Analysis_Dashboard.Service;

namespace RealTime_Data_Analysis_Dashboard.Controller;

[ApiController]
[Route("api/[controller]")] // Route will be /api/trends
public class TrendsController
{
    private readonly TweetService _tweetService;

    public TrendsController(TweetService tweetService)
    {
        _tweetService = tweetService;
    }
    
    [HttpGet("keywordtrends")] //../api/trends/keywordtrends
    public ActionResult<Dictionary<string, int>> GetKeywordTrends()
    {
        var sampleTweets = TweetSampleData.SampleDataA(); 
        
        var keywordGraph = _tweetService.BuildKeywordGraph(sampleTweets);
        var degreeCentralityResults = _tweetService.CalculateDegreeCentrality(keywordGraph);
        var orderedResults = degreeCentralityResults.OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        return new ActionResult<Dictionary<string, int>>(orderedResults);
    }

}