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

        AddDomainEvent(new CouponCreatedByAdminDomainEvent(this));
    }

    public void SaveCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow)
        {
            throw new CouponExpiredException();
        }
        if (CouponInfomation.Amount == 0)
        {
            throw new AmountIsValidate();
        }
        CouponUsers ??= new();

        bool checkUserHadSaveCoupon = CouponUsers.Any(c => c.UserId.Equals(userId));
        if (checkUserHadSaveCoupon)
        {
            throw new UserHadCouponException();
        }

        CouponInfomation.SetCouponAmount(CouponInfomation.Amount - 1);
        CouponUsers.Add(new CouponUser(userId));

        AddDomainEvent(new SavedCouponDomainEvent(
            userId, CouponId, CouponInfomation.Amount));
    }

    public void UnSaveCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow)
        {
            throw new CouponExpiredException();
        }
        CouponUsers ??= new();

        bool checkUserHadSaveCoupon = CouponUsers.Any(c => c.UserId.Equals(userId));
        if (!checkUserHadSaveCoupon)
        {
            throw new UserHaveNotCouponException();
        }

        CouponUsers.RemoveWhere(c => c.UserId.Equals(userId));
        CouponInfomation.SetCouponAmount(CouponInfomation.Amount + 1);

        AddDomainEvent(new
        CouponUnSavedByUserDomainEvent(userId, CouponId, CouponInfomation.Amount, CouponUsers.Count));
    }
    public void UseCoupon(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow)
        {
            throw new CouponExpiredException();
        }
        CouponUsers.RemoveWhere(c => c.UserId.Equals(userId));
        CouponInfomation.SetCountCouponUse(CouponInfomation.CountCouponUse + 1);

        AddDomainEvent(new UsedCouponDomainEvent(userId, CouponId, CouponInfomation.CountCouponUse));
    }

    public void UseCouponFail(UserId userId)
    {
        if (CouponInfomation.Expired < DateTime.UtcNow)
        {
            throw new CouponExpiredException();
        }
        CouponUsers.Add(new CouponUser(userId));
        CouponInfomation.SetCountCouponUse(CouponInfomation.CountCouponUse - 1);

        AddDomainEvent(new RecoveredCouponDomainEvent(CouponId, userId, CouponInfomation.CountCouponUse));
    }
    public void RemoveCoupon(UserId adminId)
    {
        if (CouponStatus.Equals(CouponStatus.Activity) || CouponStatus.Equals(CouponStatus.Expires))
        {
            CouponStatus = CouponStatus.Deleted;

            AddDomainEvent(new CouponRemovedByAdminDomainEvent(adminId, CouponId));
            return;
        }
        throw new CouponRemovedException();
    }
    public void SetCouponExpired(UserId adminId)
    {
        if (CouponStatus.Equals(CouponStatus.Expires))
        {
            throw new CouponRemovedException();
        }

        if (CouponStatus.Equals(CouponStatus.Activity))
        {
            CouponStatus = CouponStatus.Expires;
            AddDomainEvent(new CouponUpdatedExpiredDomainEvent(CouponId, adminId));
            return;
        }
        throw new CouponExpiredException();
    }
}
