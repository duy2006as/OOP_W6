/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using W6.Enums;

namespace W6.Models
{
    // Lớp trừu tượng Device
    public abstract class Device
    {
        // Mã thiết bị chỉ được thiết lập khi khởi tạo
        public string DeviceId { get; }
        public string DeviceName { get; set; }
        public int YearOfUsage { get; set; }
        public decimal PurchasePrice { get; set; }
        public DeviceStatus Status { get; protected set; }

        protected Device(string deviceId, string deviceName, int yearOfUsage, decimal purchasePrice, DeviceStatus status = DeviceStatus.Available)
        {
            // Mã thiết bị không được rỗng
            if (string.IsNullOrWhiteSpace(deviceId))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));

            // Giá mua phải lớn hơn 0
            if (purchasePrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchasePrice), "Giá mua phải lớn hơn 0.");

            // Năm đưa vào sử dụng không được lớn hơn năm hiện tại
            int currentYear = DateTime.Now.Year;
            if (yearOfUsage > currentYear)
                throw new ArgumentOutOfRangeException(nameof(yearOfUsage), $"Năm đưa vào sử dụng ({yearOfUsage}) không được lớn hơn năm hiện tại ({currentYear}).");

            DeviceId = deviceId.Trim();
            DeviceName = string.IsNullOrWhiteSpace(deviceName) ? "Chưa đặt tên" : deviceName.Trim();
            YearOfUsage = yearOfUsage;
            PurchasePrice = purchasePrice;
            Status = status;
        }

        // Phương thức trừu tượng tính chi phí bảo trì dự kiến trong 1 năm
        public abstract decimal CalculateAnnualMaintenanceCost();

        public virtual void MarkAsBorrowed()
        {
            Status = DeviceStatus.Borrowed;
        }

        public virtual void MarkAsAvailable()
        {
            Status = DeviceStatus.Available;
        }

        public virtual void MarkAsUnderMaintenance()
        {
            Status = DeviceStatus.UnderMaintenance;
        }

        // Ghi đè phương thức biểu diễn chuỗi
        public override string ToString()
        {
            return $"[{DeviceId}] {DeviceName} | Năm SD: {YearOfUsage} | Giá mua: {PurchasePrice:N0} VNĐ | Trạng thái: {Status}";
        }
    }
}
