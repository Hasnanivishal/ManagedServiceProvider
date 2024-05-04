using MSP.Profile.Model;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net;
using System.Threading;
namespace MSP.Profile.SyncCommunication.Http;

public class HttpCommunicationClient(HttpClient httpClient, IConfiguration configuration) : IHttpCommunicationClient
{
    private readonly HttpClient httpClient = httpClient;

    public IEnumerable<SubscriptionResult>? SubscriptionResults { get; set; }

    public async Task<IEnumerable<SubscriptionResult>?> GetDataOverHttp<T>(string configurationName, string url)
    {
        // Using "Polly.Core" to define Resilience Pipeline --> 
        // -- Expoential Retries - with 3 attempts
        // -- Circuit Breaker
        // -- Timeout - 60 sec

        ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            .AddRetry(GetRetryStrategy())
            .AddCircuitBreaker(GetCircuitBreakerStrategy())
            .AddTimeout(TimeSpan.FromSeconds(60))
        .Build();

        await pipeline.ExecuteAsync(async token => { return await FetchData(configurationName, url); }, CancellationToken.None);

        return SubscriptionResults;
    }

    private async Task<IEnumerable<SubscriptionResult>?> FetchData(string configurationName, string url)
    {
        var baseAddress = configuration.GetValue<string>($"{configurationName}:BaseAddress");

        SubscriptionResults = await httpClient.GetFromJsonAsync<IEnumerable<SubscriptionResult>>(baseAddress + url);

        return SubscriptionResults;
    }

    private static RetryStrategyOptions GetRetryStrategy()
    {
        return new RetryStrategyOptions()
        {
            MaxRetryAttempts = 5,
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            Delay = TimeSpan.FromSeconds(3),
            OnRetry = static args =>
            {
                Console.WriteLine("OnRetry, Attempt: {0} and Duration: {1}", args.AttemptNumber,
                    args.Duration);
                return default;
            }
        };
    }

    private static CircuitBreakerStrategyOptions GetCircuitBreakerStrategy()
    {
        // The circuit will break if more than 50% of actions result in handled exceptions,
        // within any 30-second sampling duration, and at least 2 actions are processed.
        return new CircuitBreakerStrategyOptions()
        {
            SamplingDuration = TimeSpan.FromSeconds(30),
            FailureRatio = 0.5,
            MinimumThroughput = 5,
            BreakDuration = TimeSpan.FromSeconds(15),
            OnOpened = static args =>
            {
                Console.WriteLine("OnOpened, Outcome: {0} and BreakDuration: {1}", args.Outcome, args.BreakDuration);
                return ValueTask.CompletedTask;
            },
            OnClosed = args =>
            {
                Console.WriteLine("OnOpened, Context: {0} and Outcome: {1}", args.Context, args.Outcome);
                return ValueTask.CompletedTask;
            }
        };
    }
}
