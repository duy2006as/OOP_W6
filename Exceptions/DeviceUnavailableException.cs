/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using W6.Enums;

namespace W6.Exceptions
{
    // Thiết bị không ở trạng thái Available
    public class DeviceUnavailableException : BorrowingException
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
            : base($"Thiết bị '{deviceId}' hiện không sẵn sàng để mượn. Trạng thái hiện tại: {currentStatus}.")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus, string message)
            : base(message)
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}
