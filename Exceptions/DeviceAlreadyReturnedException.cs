/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

namespace W6.Exceptions
{
    // Trả một thiết bị đã được trả trước đó
    public class DeviceAlreadyReturnedException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceAlreadyReturnedException(string deviceId)
            : base($"Thiết bị '{deviceId}' trong phiếu mượn đã được trả trước đó.")
        {
            DeviceId = deviceId;
        }

        public DeviceAlreadyReturnedException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
