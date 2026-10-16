/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

namespace W6.Exceptions
{
    // Một thiết bị được thêm hai lần vào cùng phiếu
    public class DuplicateDeviceException : BorrowingException
    {
        public string DeviceId { get; }

        public DuplicateDeviceException(string deviceId)
            : base($"Thiết bị '{deviceId}' đã tồn tại trong phiếu mượn này.")
        {
            DeviceId = deviceId;
        }

        public DuplicateDeviceException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
