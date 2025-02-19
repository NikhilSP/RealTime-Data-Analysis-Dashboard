using RealTime_Data_Analysis_Dashboard.Model;

namespace RealTime_Data_Analysis_Dashboard.Data;

public static class TweetSampleData
{
    public static Tweet[] SampleDataA()
    {
        return
        [
            new Tweet(id: 1, text: "Learning about #CSharp and #dotnet core for web development"),
            new Tweet(id: 2, text: "The advancements in #AI are fascinating. Especially in #MachineLearning"),
            new Tweet(id: 3, text: "Using #ASPNetCore to build a REST API. #WebDevelopment"),
            new Tweet(id: 4, text: "Is #cloud computing the future? #Azure #AWS #GCP"),
            new Tweet(id: 5, text: "Exploring #MachineLearning algorithms with #Python. #AI"),
            new Tweet(id: 6, text: "Developing a web app using #CSharp and #Blazor. #WebDevelopment #dotnet")
        ];
    }
}