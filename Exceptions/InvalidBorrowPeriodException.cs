/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

namespace W6.Exceptions
{
    // Thời hạn mượn không hợp lệ
    public class InvalidBorrowPeriodException : BorrowingException
    {
        public string DeviceId { get; }
        public int RequestedDays { get; }
        public int MaxDays { get; }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxDays)
            : base($"Thời gian mượn không hợp lệ cho thiết bị '{deviceId}'. Yêu cầu: {requestedDays} ngày, giới hạn tối đa: {maxDays} ngày.")
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaxDays = maxDays;
        }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maxDays, string message)
            : base(message)
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaxDays = maxDays;
        }
    }
}
