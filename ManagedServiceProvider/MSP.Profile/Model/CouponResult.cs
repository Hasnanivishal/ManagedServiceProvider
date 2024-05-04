using MSP.Profile.Service;

namespace MSP.Profile.Model;

public class CouponResult
{
    public string CouponNumber { get; set; }
}

public static class CouponResultExtensions
{
    public static IEnumerable<CouponResult> AsCouponResult(this CouponDetailsResponse couponDetailsResponse)
    {
        var couponResult = new List<CouponResult>();

        foreach (var item in couponDetailsResponse.Coupons)
        {
            couponResult.Add(new CouponResult()
            {
                CouponNumber = item.CouponNumber
            });
        }

        return couponResult;
    }
}

