# User Stories - EduHub

## Sinh viên

**US-01.** Huỷ phiếu đăng ký

- Given phiếu đang Pending hoặc Confirmed, When tôi huỷ, Then phiếu chuyển sang Cancelled
- Given phiếu đã Cancelled, When tôi huỷ lần nữa, Then hệ thống từ chối và báo phiếu đã huỷ từ trước

**US-02.** Check-in bằng mã QR hoặc nhờ ban tổ chức ghi tay

- Given phiếu của tôi đã Confirmed, When tôi check-in bằng QR hoặc ghi tay, Then hệ thống tạo bản ghi điểm danh và lưu hình thức đã dùng
- Given phiếu của tôi còn Pending hoặc đã Cancelled, When tôi check-in, Then hệ thống từ chối

## Ban CLB

**US-03.** Xác nhận phiếu đăng ký

- Given phiếu đang Pending, When tôi xác nhận, Then phiếu chuyển sang Confirmed
- Given phiếu đã Cancelled, When tôi xác nhận, Then hệ thống từ chối và phiếu vẫn là Cancelled

**US-04.** Xem danh sách thành viên

- Given câu lạc bộ có 3 thành viên, When tôi mở danh sách, Then tôi thấy họ tên, mã số sinh viên, email của từng người và tổng số là 3
- Given tôi thêm một người đã có trong danh sách, When tôi kết nạp, Then hệ thống từ chối và tổng số không đổi

## Admin

**US-05.** Tạo câu lạc bộ mới

- Given tôi nhập đủ tên và mô tả, When tôi tạo, Then có câu lạc bộ mới với 0 thành viên
- Given tôi bỏ trống tên hoặc mô tả, When tôi tạo, Then hệ thống báo lỗi và không tạo câu lạc bộ