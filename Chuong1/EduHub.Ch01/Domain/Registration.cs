namespace EduHub.Ch01.Domain;

/// <summary>Các trạng thái của phiếu đăng ký.</summary>
public static class RegistrationStatus
{
    /// <summary>Chờ xác nhận.</summary>
    public const string Pending = "Pending";

    /// <summary>Đã xác nhận.</summary>
    public const string Confirmed = "Confirmed";

    /// <summary>Đã huỷ.</summary>
    public const string Cancelled = "Cancelled";
}

/// <summary>Phiếu đăng ký của một thành viên cho một sự kiện.</summary>
public class Registration
{
    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại: Id của thành viên đăng ký.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Khoá ngoại: Id của sự kiện được đăng ký.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Thời điểm đăng ký.</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Trạng thái hiện tại, lấy từ <see cref="RegistrationStatus"/>.</summary>
    public string Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.Now;
        Status = RegistrationStatus.Pending;
    }

    /// <summary>Tạo phiếu đăng ký mới ở trạng thái Pending.</summary>
    /// <exception cref="ArgumentException">Thiếu thành viên hoặc thiếu sự kiện.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("Phiếu đăng ký phải gắn với một thành viên.", nameof(memberId));
        if (eventId == Guid.Empty)
            throw new ArgumentException("Phiếu đăng ký phải gắn với một sự kiện.", nameof(eventId));

        return new Registration(memberId, eventId);
    }

    /// <summary>Xác nhận phiếu.</summary>
    /// <exception cref="InvalidOperationException">Phiếu không ở trạng thái Pending.</exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Chỉ xác nhận được phiếu Pending; phiếu này đang {Status}.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>Huỷ phiếu.</summary>
    /// <exception cref="InvalidOperationException">Phiếu đã huỷ từ trước.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đã bị huỷ từ trước.");

        Status = RegistrationStatus.Cancelled;
    }
}