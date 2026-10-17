/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using W6.Enums;
using W6.Exceptions;
using W6.Interfaces;

namespace W6.Models
{
    public class BorrowingSlip
    {
        private const decimal DAILY_LATE_FEE = 20000m;

        private readonly List<BorrowItem> _items = new();

        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }

        public IReadOnlyList<BorrowItem> Items => _items.AsReadOnly();

        public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
        {
            if (string.IsNullOrWhiteSpace(slipId))
                throw new ArgumentException("Mã phiếu mượn không được để trống.", nameof(slipId));

            Borrower = borrower ?? throw new ArgumentNullException(nameof(borrower), "Người mượn không được null.");

            if (string.IsNullOrWhiteSpace(teachingRoom))
                throw new ArgumentException("Phòng giảng dạy không được để trống.", nameof(teachingRoom));

            SlipId = slipId.Trim();
            TeachingRoom = teachingRoom.Trim();
            BorrowedDate = borrowedDate;
        }

        public void AddDevice(TeachingDevice device, int borrowDays)
        {
            // Device không được null
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Thiết bị không được null.");

            // Kiểm tra số ngày mượn
            if (borrowDays <= 0 || borrowDays > device.MaxBorrowDays)
            {
                throw new InvalidBorrowPeriodException(device.DeviceId, borrowDays, device.MaxBorrowDays);
            }

            // Kiểm tra thiết bị trùng lặp trong phiếu
            if (_items.Any(i => i.Device.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicateDeviceException(device.DeviceId);
            }

            // Thiết bị phải ở trạng thái Available
            if (device.Status != DeviceStatus.Available)
            {
                throw new DeviceUnavailableException(device.DeviceId, device.Status);
            }

            // Kiểm tra giới hạn mượn
if (_items.Count >= Borrower.GetMaximumDevices())
{
    throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
}

// Tạo BorrowItem và thêm vào phiếu
DateTime dueDate = BorrowedDate.AddDays(borrowDays);
var item = new BorrowItem(device, BorrowedDate, dueDate);
_items.Add(item);

// Đánh dấu thiết bị đã mượn sau khi thêm thành công vào phiếu
device.MarkAsBorrowed();
        }

        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không hợp lệ.", nameof(deviceId));

            // Tìm BorrowItem theo mã thiết bị
            var item = _items.FirstOrDefault(i => i.Device.DeviceId.Equals(deviceId.Trim(), StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                throw new DeviceNotInSlipException(deviceId);
            }

            // Không được trả một BorrowItem hai lần
            if (item.IsReturned)
            {
                throw new DeviceAlreadyReturnedException(deviceId);
            }

            // Ghi nhận ngày trả
            item.Return(returnedDate);

            // Tính tiền phạt trễ hạn
            decimal lateFee = 0m;
            int lateDays = (returnedDate.Date - item.BorrowedDate.Date).Days;
            if (lateDays > 0)
            {
                lateFee = lateDays * DAILY_LATE_FEE;
            }

            return lateFee;
        }

        // Tính tổng tiền phạt trễ hạn của cả phiếu
        public decimal CalculateTotalLateFee()
        {
            decimal totalFee = 0m;
            foreach (var item in _items)
            {
                if (item.IsReturned && item.ReturnedDate.HasValue)
                {
                    int lateDays = (item.ReturnedDate.Value.Date - item.BorrowedDate.Date).Days;
                    if (lateDays > 0)
                    {
                        totalFee += lateDays * DAILY_LATE_FEE;
                    }
                }
                else if (!item.IsReturned && DateTime.Now.Date > item.DueDate.Date)
                {
                    int overdueDays = (DateTime.Now.Date - item.DueDate.Date).Days;
                    totalFee += overdueDays * DAILY_LATE_FEE;
                }
            }
            return totalFee;
        }

        public override string ToString()
        {
            return $"[Phiếu mượn: {SlipId}] Phòng: {TeachingRoom} | Ngày mượn: {BorrowedDate:yyyy-MM-dd} | Người mượn: {Borrower.FullName} - {Borrower.BorrowerId} | Số TB: {_items.Count}";
        }
    }
}
