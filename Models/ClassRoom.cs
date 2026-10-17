/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using W6.Enums;

namespace W6.Models
{
    // Lớp ClassRoom kết tập với Device
    public class ClassRoom
    {
        private readonly List<Device> _devices = new();

        public string RoomId { get; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }

        public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

        public ClassRoom(string roomId, string roomName, int capacity)
        {
            if (string.IsNullOrWhiteSpace(roomId))
                throw new ArgumentException("Mã phòng không được để trống.", nameof(roomId));

            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa phòng phải lớn hơn 0.");

            RoomId = roomId.Trim();
            RoomName = string.IsNullOrWhiteSpace(roomName) ? "Phòng học" : roomName.Trim();
            Capacity = capacity;
        }

        public void AddDevice(Device device)
        {
            // Không chấp nhận đối tượng thiết bị null
            if (device == null)
                throw new ArgumentNullException(nameof(device), "Đối tượng thiết bị không được null.");

            // Không thêm hai thiết bị có cùng mã vào một phòng
            if (_devices.Any(d => d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Thiết bị có mã '{device.DeviceId}' đã tồn tại trong phòng {RoomId}.");

            _devices.Add(device);
        }

        public bool RemoveDevice(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) return false;

            var existing = FindDevice(deviceId);
            if (existing != null)
            {
                return _devices.Remove(existing);
            }
            return false;
        }

        public Device? FindDevice(string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId)) return null;
            return _devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // Tính chi phí bảo trì đa hình
        public decimal CalculateAnnualMaintenanceCost()
        {
            return _devices.Sum(d => d.CalculateAnnualMaintenanceCost());
        }

        // Đang ở trạng thái UnderMaintenance hoặc có thời gian sử dụng trên 5 năm
        public List<Device> GetDevicesRequiringMaintenance()
        {
            int currentYear = DateTime.Now.Year;
            return _devices
                .Where(d => d.Status == DeviceStatus.UnderMaintenance || (currentYear - d.YearOfUsage) > 5)
                .ToList();
        }

        public override string ToString()
        {
            return $"[Phòng học] {RoomName} - {RoomId} | Sức chứa: {Capacity} người | Số lượng thiết bị: {_devices.Count}";
        }
    }
}
