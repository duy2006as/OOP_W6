/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;

namespace W6.Exceptions
{
    // Ngày trả không hợp lệ
    public class InvalidReturnDateException : BorrowingException
    {
        public DateTime ReturnedDate { get; }
        public DateTime BorrowedDate { get; }

        public InvalidReturnDateException(DateTime returnedDate, DateTime borrowedDate)
            : base($"Ngày trả ({returnedDate:yyyy-MM-dd}) không được trước ngày mượn ({borrowedDate:yyyy-MM-dd}).")
        {
            ReturnedDate = returnedDate;
            BorrowedDate = borrowedDate;
        }

        public InvalidReturnDateException(DateTime returnedDate, DateTime borrowedDate, string message)
            : base(message)
        {
            ReturnedDate = returnedDate;
            BorrowedDate = borrowedDate;
        }
    }
}
