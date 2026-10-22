namespace EduHub.Ch01.Domain;

/// <summary>Hình thức check-in.</summary>
public enum CheckInMethod
{
    /// <summary>Sinh viên tự quét mã QR.</summary>
    QrCode,

    /// <summary>Ban tổ chức ghi tay.</summary>
    Manual
}

/// <summary>Bản ghi điểm danh của một phiếu đăng ký tại sự kiện.</summary>
public class CheckIn
{
    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại: Id của phiếu đăng ký được điểm danh.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Thời điểm check-in.</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Hình thức check-in đã dùng.</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn(Guid registrationId, CheckInMethod method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    /// <summary>Tạo bản ghi điểm danh cho một phiếu đã xác nhận.</summary>
    /// <exception cref="InvalidOperationException">Phiếu chưa được xác nhận hoặc đã bị huỷ.</exception>
    public static CheckIn Create(Registration registration, CheckInMethod method)
    {
        ArgumentNullException.ThrowIfNull(registration);
        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException($"Chỉ check-in được phiếu Confirmed; phiếu này đang {registration.Status}.");

        return new CheckIn(registration.Id, method);
    }

    /// <summary>Tóm tắt lần điểm danh để in báo cáo.</summary>
    public string Summary()
    {
        var method = Method == CheckInMethod.QrCode ? "quét QR" : "ghi tay";
        return $"Phiếu {RegistrationId} check-in lúc {CheckedInAt.ToLocalTime():HH:mm dd/MM/yyyy} bằng {method}";
    }
}