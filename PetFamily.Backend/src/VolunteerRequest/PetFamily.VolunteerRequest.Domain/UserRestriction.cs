using CSharpFunctionalExtensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.EntityIds;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.VolunteerRequest.Domain
{
    public class UserRestriction : SharedKernel.Entity<UserRestrictionId>
    {
        private const int BAN_DAYS = 7;
        public Guid UserId { get; private set; }
        public DateTime BannedUntil { get; private set; }
        public RejectionDescription Reason { get; private set; }

        private UserRestriction(UserRestrictionId id) : base(id){}
        private UserRestriction(UserRestrictionId id, Guid userId, DateTime bannedUntil, RejectionDescription reason) : base(id)
        {
            UserId = userId;
            BannedUntil = bannedUntil;
            Reason = reason;
        }

        /// <summary>
        /// Создаёт новую блокировку для пользователя с заданной продолжительностью и причиной.
        /// </summary>
        /// <param name="restrictionId">Идентификатор блокировки.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="banDurationDays">Продолжительность блокировки в днях.</param>
        /// <param name="reason">Причина блокировки.</param>
        /// <returns>Результат создания блокировки или ошибка.</returns>
        public static Result<UserRestriction, Error> Create(UserRestrictionId restrictionId, Guid userId, RejectionDescription reason, int banDurationDays = BAN_DAYS)
        {
            if (banDurationDays <= 0)
                return Errors.UserRestriction.InvalidBanDuration();

            var bannedUntil = DateTime.UtcNow.AddDays(banDurationDays);
            var banUser = new UserRestriction(restrictionId, userId, bannedUntil, reason);
            return Result.Success<UserRestriction, Error>(banUser);
        }

        /// <summary>
        /// Проверяет, активна ли блокировка в данный момент.
        /// </summary>
        /// <returns>True, если блокировка активна; иначе false.</returns>
        public bool IsBanActive() => DateTime.UtcNow < BannedUntil;

        /// <summary>
        /// Возвращает оставшееся время до окончания блокировки.
        /// </summary>
        /// <returns>Время, оставшееся до окончания блокировки, или ноль, если блокировка завершена.</returns>
        public TimeSpan GetRemainingBanTime()
        {
            if (!IsBanActive())
                return TimeSpan.Zero;

            return BannedUntil - DateTime.UtcNow;
        }

        /// <summary>
        /// Завершает блокировку вручную.
        /// </summary>
        /// <returns>Результат выполнения операции или ошибка, если блокировка уже завершена.</returns>
        public UnitResult<Error> EndBanManually()
        {
            if (!IsBanActive())
                return Errors.UserRestriction.BanExpired(UserId);

            BannedUntil = DateTime.UtcNow;
            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Продлевает текущую блокировку на заданное количество дней.
        /// </summary>
        /// <param name="additionalDays">Количество дополнительных дней для продления блокировки.</param>
        /// <returns>Результат выполнения операции или ошибка, если указано некорректное количество дней.</returns>
        public UnitResult<Error> ExtendBan(int additionalDays)
        {
            if (additionalDays <= 0)
                return Errors.UserRestriction.InvalidBanDuration();

            BannedUntil = BannedUntil.AddDays(additionalDays);
            return UnitResult.Success<Error>();
        }
    }
}
