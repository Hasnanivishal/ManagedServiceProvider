using Grpc.Core;

namespace MSP.Coupon.Service;

public class CouponService : GrpcCoupon.GrpcCouponBase
{
    public override Task<CouponDetailsResponse> GetCouponDetails(CouponDetailsRequest request, ServerCallContext context)
    {
        var result = new CouponDetailsResponse();

        for (int i = 0; i < 3; i++)
        {
            result.Coupons.Add(new CouponDetailsModel()
            {
                CouponNumber = Guid.NewGuid().ToString(),
            });
        }

        return Task.FromResult(result);
    }
}
