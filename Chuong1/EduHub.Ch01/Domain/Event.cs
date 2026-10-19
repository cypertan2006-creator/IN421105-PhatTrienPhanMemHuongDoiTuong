namespace EduHub.Ch01.Domain;

/// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
public class Event
{
    /// <summary>Khoá chính.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khóa ngoại: Id của câu lạc bộ .</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Tiêu đề sự kiện.</summary>
    public string Title { get; private set; }

    /// <summary>Ngày giờ diễn ra.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Số người tham dự tối đa.</summary>
    public int Capacity { get; private set; }

    /// <summary>Sự kiện trực tiếp hay trực tuyến.</summary>
    public bool IsOnline { get; private set; }

    private Event(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = Guid.NewGuid();
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>Kiểm tra dữ liệu rồi tạo sự kiện mới.</summary>
    /// <exception cref="ArgumentException">Id câu lạc bộ không được để trống hoặc tiêu đề rỗng hoặc số người tham dự không hợp lệ.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("Id câu lạc bộ không được để trống.", nameof(clubId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));
        if (capacity <= 0)
            throw new ArgumentException("Số người tham dự không hợp lệ.", nameof(capacity));
        
        return new Event(clubId, title.Trim(), date, capacity, isOnline);
    }
    
    /// <summary>Sự kiện còn chỗ hay không.</summary>
    public bool HasAvailableSpots(int confirmedCount) => confirmedCount < Capacity;
}