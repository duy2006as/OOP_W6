/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using W6.Enums;

namespace W6.Models
{
    // Điều khiển điều hòa
    public class AirConditionerRemote : TeachingDevice
    {
        public AirConditionerRemote(
            string deviceId, 
            string deviceName, 
            int yearOfUsage, 
            decimal purchasePrice, 
            int maxBorrowDays = 3, 
            DeviceStatus status = DeviceStatus.Available)
            : base(deviceId, deviceName, yearOfUsage, purchasePrice, maxBorrowDays, status)
        {
        }

        // Tính chi phí bảo trì dự kiến trong 1 năm
        public override decimal CalculateAnnualMaintenanceCost()
        {
            return 50000m + (PurchasePrice * 0.05m);
        }

        public override string ToString()
        {
            return $"[Điều khiển điều hòa] {base.ToString()} | Phí bảo trì: {CalculateAnnualMaintenanceCost():N0} VNĐ";
        }
    }
}
