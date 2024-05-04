using MSP.Profile.Service;

namespace MSP.Profile.SyncCommunication.gRPC
{
    public interface IGrpcCommunicationService
    {
        CouponDetailsResponse CouponDetailsResponses { get; set; }

        CouponDetailsResponse GetCouponDetails(Guid profileId);
    }
}