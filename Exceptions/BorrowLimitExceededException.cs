/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

namespace W6.Exceptions
{
    // Người mượn vượt quá số thiết bị cho phép
    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices)
            : base($"Người mượn '{borrowerId}' đã đạt giới hạn mượn tối đa ({maximumDevices} thiết bị).")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices, string message)
            : base(message)
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }
    }
}
