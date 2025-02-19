using System.Text.RegularExpressions;
using RealTime_Data_Analysis_Dashboard.Model;

namespace RealTime_Data_Analysis_Dashboard.Service;

public class TweetService
{
    private readonly char[] _separators = [' ', '\t', '\n', '\r'];

    private readonly char[] _startTokens = ['#'];
    
    private readonly string[] _stopWords =
    [
        "the", "a", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "do", "does", "did", "and",
        "or", "but", "if", "then", "else", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more",
        "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "can",
        "cannot", "could", "may", "might", "must", "should", "would", "will", "let", "get", "make", "go", "see", "know",
        "take", "use", "want"
    ];

    public IReadOnlyList<string> ExtractKeywords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        text = text.ToLowerInvariant();

        // Replace any character that is NOT a word character (alphanumeric + underscore) or a hashtag
        text = Regex.Replace(text, @"[^\w\s#]", "");

        var words = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

        return words
            .Where(word => !_stopWords.Contains(word))// && _startTokens.Any(word.StartsWith)) // Filter stop words AND hashtags
            .Select(word => word.TrimStart(_startTokens)) // Remove the '#' prefix from hashtags
            .ToList();
    }
    
    // A very basic keyword occurence graph
    public Dictionary<string, Dictionary<string, int>> BuildKeywordGraph(IEnumerable<Tweet> tweets)
    {
        var keywordGraph = new Dictionary<string, Dictionary<string, int>>();

        foreach (var tweet in tweets)
        {
            var keywords = ExtractKeywords(tweet.Text);

            for (var i = 0; i < keywords.Count; i++)
            {
                var keyword1 = keywords[i];
                
                for (var j = i + 1; j < keywords.Count; j++)
                {
                    var keyword2 = keywords[j];

                    if (String.CompareOrdinal(keyword1, keyword2) > 0)
                    {
                        (keyword1, keyword2) = (keyword2, keyword1);
                    }
                    
                    if (!keywordGraph.ContainsKey(keyword1))
                    {
                        keywordGraph[keyword1] = new Dictionary<string, int>();
                    }
                    
                    if (keywordGraph[keyword1].ContainsKey(keyword2))
                    {
                        keywordGraph[keyword1][keyword2]++;
                    }
                    else
                    {
                        keywordGraph[keyword1][keyword2] = 1;
                    }
                }
            }
        }

        return keywordGraph;
    }
    
    public Dictionary<string, int> CalculateDegreeCentrality(Dictionary<string, Dictionary<string, int>> graph)
    {
        var degreeCentralities = new Dictionary<string, int>();

        foreach (var keyword in graph.Keys)
        {
            // Degree centrality is simply the count of co-occurring keywords for this keyword.
            // This is the number of keys in the inner dictionary.
            var degree = graph[keyword].Count;
            degreeCentralities[keyword] = degree;
        }

        return degreeCentralities;
    }

}