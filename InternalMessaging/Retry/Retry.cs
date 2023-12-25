using Polly;
using Polly.Retry;

namespace InternalMessaging;

public class Retry : IRetry
{
    readonly ResiliencePipeline retryPipeline;
    public Retry()
    {
        retryPipeline = new ResiliencePipelineBuilder()
                       .AddRetry(new RetryStrategyOptions()
                       {
                           Delay = TimeSpan.FromSeconds(1),
                           BackoffType = DelayBackoffType.Exponential,
                           MaxRetryAttempts = 5
                       })
                       .AddTimeout(TimeSpan.FromSeconds(10)) // Add 10 seconds timeout
                       .Build();


    }
    public void Execute(Action callback)
    => retryPipeline.Execute(callback);

}