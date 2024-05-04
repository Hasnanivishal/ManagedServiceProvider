using MSP.Profile.Model;
namespace MSP.Profile.SyncCommunication.Http;

public interface IHttpCommunicationClient
{
    Task<IEnumerable<SubscriptionResult>?> GetDataOverHttp<T>(string configurationName, string url);
}