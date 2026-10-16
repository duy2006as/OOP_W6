/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

namespace W6.Exceptions
{
    // Trả thiết bị không thuộc phiếu mượn
    public class DeviceNotInSlipException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceNotInSlipException(string deviceId)
            : base($"Thiết bị '{deviceId}' không thuộc phiếu mượn này.")
        {
            DeviceId = deviceId;
        }

        public DeviceNotInSlipException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
