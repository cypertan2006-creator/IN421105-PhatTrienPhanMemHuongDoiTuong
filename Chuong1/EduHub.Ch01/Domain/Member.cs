namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public class Member
{
    private const char EmailSeparator = '@';

    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Họ tên.</summary>
    public string Name { get; private set; }

    /// <summary>Email liên hệ, lưu ở dạng chữ thường.</summary>
    public string Email { get; private set; }

    /// <summary>Mã số sinh viên.</summary>
    public string StudentCode { get; private set; }

    /// <summary>Thời điểm tạo thành viên.</summary>
    public DateTime JoinedAt { get; private set; }

    /// <summary>Thời điểm ngừng hoạt động; null nghĩa là còn hoạt động.</summary>
    public DateTime? LeftAt { get; private set; }

    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.Now;
    }

    /// <summary>Kiểm tra dữ liệu rồi tạo thành viên mới.</summary>
    /// <exception cref="ArgumentException">Tên, mã số sinh viên trống hoặc email thiếu @.</exception>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên thành viên không được để trống.", nameof(name));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã số sinh viên không được để trống.", nameof(studentCode));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains(EmailSeparator))
            throw new ArgumentException($"Email '{email}' không hợp lệ.", nameof(email));

        return new Member(name.Trim(), email.Trim().ToLowerInvariant(), studentCode.Trim());
    }

    /// <summary>Thành viên còn hoạt động hay không.</summary>
    public bool IsActive() => LeftAt is null;

    /// <summary>Ngừng hoạt động thành viên.</summary>
    /// <exception cref="InvalidOperationException">Thành viên đã ngừng từ trước.</exception>
    public void Deactivate()
    {
        if (!IsActive())
            throw new InvalidOperationException($"{this} đã ngừng hoạt động từ trước.");

        LeftAt = DateTime.Now;
    }

    /// <summary>Hiển thị dạng "Họ tên (MSSV)".</summary>
    public override string ToString() => $"{Name} ({StudentCode})";
}