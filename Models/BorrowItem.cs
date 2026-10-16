/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using W6.Exceptions;

namespace W6.Models
{
    public class BorrowItem
    {
        public TeachingDevice Device { get; }
        public DateTime BorrowedDate { get; }
        public DateTime DueDate { get; }
        public int FromPeriod { get; set; }
        public int ToPeriod { get; set; }
        public DateTime? ReturnedDate { get; private set; }

        public bool IsReturned => ReturnedDate.HasValue;

        public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate, int fromPeriod = 1, int toPeriod = 2)
        {
            // Device không được null
            Device = device ?? throw new ArgumentNullException(nameof(device), "Thiết bị không được null.");

            // DueDate phải sau hoặc bằng BorrowedDate
            if (dueDate.Date < borrowedDate.Date)
                throw new ArgumentException("Hạn trả phải sau hoặc bằng ngày mượn.");

            // Tiết kết thúc phải lớn hơn hoặc bằng tiết bắt đầu
            if (toPeriod < fromPeriod)
                throw new ArgumentException("Tiết kết thúc phải lớn hơn hoặc bằng tiết bắt đầu.");

            BorrowedDate = borrowedDate;
            DueDate = dueDate;
            FromPeriod = fromPeriod;
            ToPeriod = toPeriod;
            // ReturnedDate ban đầu là null
            ReturnedDate = null;
        }

        public void Return(DateTime returnedDate)
        {
            // Không được trả một BorrowItem hai lần
            if (IsReturned)
            {
                throw new DeviceAlreadyReturnedException(Device.DeviceId);
            }

            // Ngày trả không được trước ngày mượn
            if (returnedDate.Date < BorrowedDate.Date)
            {
                throw new InvalidReturnDateException(returnedDate, BorrowedDate);
            }

            ReturnedDate = returnedDate;
        }

        public override string ToString()
        {
            string statusStr = IsReturned 
                ? $"Đã trả ngày {ReturnedDate:yyyy-MM-dd}" 
                : $"Chưa trả (Hạn trả: {DueDate:yyyy-MM-dd})";
            return $"Thiết bị: [{Device.DeviceId}] {Device.DeviceName} | Mượn: {BorrowedDate:yyyy-MM-dd} | Tiết: {FromPeriod}-{ToPeriod} | {statusStr}";
        }
    }
}
