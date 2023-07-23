using Market.Domain.Core;
using Market.Domain.Coupons.Events;
using Market.Domain.Coupons.Exceptions;
using Market.Domain.Users;

namespace Market.Domain.Coupons;

public class CouponAggregate : Entity, IAggregateRoot
{
    public CouponId CouponId { get; private set; }
    public CouponInfomation CouponInfomation { get; private set; }
    public CouponStatus CouponStatus { get; private set; }
    public HashSet<CouponUser> CouponUsers { get; private set; }

    public CouponAggregate(CouponId couponId, CouponInfomation couponInfomation)
    {
        CouponId = couponId;
        CouponInfomation = couponInfomation;
        CouponStatus = CouponStatus.Activity;

        this.AddDomainEvent(new CouponCreatedByAdminDomainEvent(this));
    }

    public void UserSaveCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow) {
            throw new CouponExpiredException();
        }
        if (CouponInfomation.Amount == 0) {
            throw new AmountIsValidate();
        }
        CouponUsers ??= new();

        bool checkUserHadSaveCoupon = CouponUsers.Any(c => c.UserId.Equals(userId));
        if (checkUserHadSaveCoupon) {
            throw new UserHadCouponException();
        }

        CouponUsers.Add(new CouponUser(userId));
        this.AddDomainEvent(new CouponSavedByUserDomainEvent(userId, CouponId));
    }

    public void UserUnSaveCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow) {
            throw new CouponExpiredException();
        }
        CouponUsers ??= new();

        bool checkUserHadSaveCoupon = CouponUsers.Any(c => c.UserId.Equals(userId));
        if (!checkUserHadSaveCoupon) {
            throw new UserHaveNotCouponException();
        }

        CouponUsers.RemoveWhere(c => c.UserId.Equals(userId));
        CouponInfomation.SetCouponAmount(CouponInfomation.Amount + 1);

        this.AddDomainEvent(new CouponUnSavedByUserDomainEvent(userId, CouponId));
    }
    public void UserUseCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow) {
            throw new CouponExpiredException();
        }
        CouponInfomation.SetCountCouponUse(CouponInfomation.CountCouponUse + 1);
        this.AddDomainEvent(new CouponUsedByUserDomainEvent(userId, CouponId));
    }
    public void RemoveCouponByAdmin(UserId adminId)
    {
        if (CouponStatus.Equals(CouponStatus.Activity) || CouponStatus.Equals(CouponStatus.Expires)) {
            CouponStatus = CouponStatus.Deleted;

            this.AddDomainEvent(new CouponRemovedByAdminDomainEvent(adminId, CouponId));
            return;
        }
        throw new CouponRemovedException();
    }
    public void AdminSetCouponExpired(UserId adminId)
    {
        if (CouponStatus.Equals(CouponStatus.Expires)) {
            throw new CouponRemovedException();
        }

        if (CouponStatus.Equals(CouponStatus.Activity)) {
            CouponStatus = CouponStatus.Expires;
            this.AddDomainEvent(new CouponUpdatedExpiredDomainEvent(CouponId, adminId));
            return;
        }
        throw new CouponExpiredException();
    }
}
