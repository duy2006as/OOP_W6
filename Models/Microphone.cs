/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using W6.Enums;

namespace W6.Models
{
    // Micro không dây
    public class Microphone : TeachingDevice
    {
        public Microphone(
            string deviceId, 
            string deviceName, 
            int yearOfUsage, 
            decimal purchasePrice, 
            int maxBorrowDays = 1, 
            DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, maxBorrowDays, status)
        {
        }

        // Tính chi phí bảo trì dự kiến trong 1 năm
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return 100000m + (PurchasePrice * 0.10m);
        }

        public override string ToString()
        {
            return $"[Microphone] {base.ToString()} | Phí bảo trì: {CalculateAnnualMaintenanceCost():N0} VNĐ";
        }
    }
}
