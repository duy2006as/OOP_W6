/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using W6.Interfaces;

namespace W6.Models
{
    // Giảng viên
    public class Lecturer : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }
        public string Department { get; set; }

        public Lecturer(string borrowerId, string fullName, string department = "Khoa CNTT")
        {
            if (string.IsNullOrWhiteSpace(borrowerId))
                throw new ArgumentException("Mã giảng viên không được để trống.", nameof(borrowerId));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Họ tên giảng viên không được để trống.", nameof(fullName));

            BorrowerId = borrowerId.Trim();
            FullName = fullName.Trim();
            Department = department.Trim();
        }

        // Giảng viên được mượn tối đa 3 thiết bị trên một phiếu
        public int GetMaximumDevices()
        {
            return 3;
        }

        public override string ToString()
        {
            return $"[Giảng viên] {FullName} (Mã: {BorrowerId}, Bộ môn/Khoa: {Department})";
        }
    }
}
