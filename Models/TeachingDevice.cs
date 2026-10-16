/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using W6.Enums;

namespace W6.Models
{
    // Lớp trừu tượng TeachingDevice
    public abstract class TeachingDevice : Device
    {
        // Số ngày mượn tối đa
        public int MaxBorrowDays { get; set; }

        protected TeachingDevice(
            string deviceId, 
            string deviceName, 
            int yearOfUsage, 
            decimal purchasePrice, 
            int maxBorrowDays, 
            DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, status)
        {
            if (maxBorrowDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxBorrowDays), "Số ngày mượn tối đa phải lớn hơn 0.");

            MaxBorrowDays = maxBorrowDays;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Mượn tối đa: {MaxBorrowDays} ngày";
        }
    }
}
