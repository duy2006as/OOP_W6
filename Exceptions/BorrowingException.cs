/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;

namespace W6.Exceptions
{
    // Ngoại lệ cơ sở
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) 
            : base(message)
        {
        }

        public BorrowingException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
