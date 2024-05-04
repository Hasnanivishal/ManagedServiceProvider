using Grpc.Net.Client;
using MSP.Profile.Model;
using MSP.Profile.Service;

namespace MSP.Profile.SyncCommunication.gRPC
{
    public class GrpcCommunicationService : IGrpcCommunicationService
    {
        private readonly IConfiguration configuration;

        public CouponDetailsResponse CouponDetailsResponses { get; set; }

        public GrpcCommunicationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CouponDetailsResponse GetCouponDetails(Guid profileId)
        {
            var baseAddress = configuration.GetValue<string>($"CouponService:BaseAddress")!;

            var channel = GrpcChannel.ForAddress(baseAddress);
            var client = new GrpcCoupon.GrpcCouponClient(channel);

            try
            {
                // invoke the gRPC method
                CouponDetailsResponses = client.GetCouponDetails(new CouponDetailsRequest()
                {
                    Id = profileId.ToString(),
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return CouponDetailsResponses;
        }
    }
}
