namespace EduHub.Ch01.Domain;

/// <summary>Câu lạc bộ sinh viên.</summary>
public class Club
{
    private readonly List<Member> _members = new();

    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Tên câu lạc bộ.</summary>
    public string Name { get; private set; }

    /// <summary>Mô tả ngắn.</summary>
    public string Description { get; private set; }

    /// <summary>Thời điểm thành lập.</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>Danh sách thành viên, chỉ xem. Muốn thêm phải gọi <see cref="AddMember"/>.</summary>
    public IReadOnlyList<Member> Members => _members;

    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>Kiểm tra dữ liệu rồi tạo câu lạc bộ mới.</summary>
    /// <exception cref="ArgumentException">Tên hoặc mô tả rỗng.</exception>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Mô tả câu lạc bộ không được để trống.", nameof(description));

        return new Club(name.Trim(), description.Trim());
    }

    /// <summary>Kết nạp một thành viên.</summary>
    /// <exception cref="InvalidOperationException">Thành viên đã ngừng hoạt động hoặc đã có trong câu lạc bộ.</exception>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);
        if (!member.IsActive())
            throw new InvalidOperationException($"{member} đã ngừng hoạt động nên không thể kết nạp.");
        if (_members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException($"{member} đã là thành viên của {Name}.");

        _members.Add(member);
    }

    /// <summary>Số thành viên hiện có.</summary>
    public int MemberCount() => _members.Count;
}