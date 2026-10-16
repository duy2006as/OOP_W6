/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using W6.Interfaces;

namespace W6.Models
{
    // Trợ giảng
    public class TeachingAssistant : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }
        public string AssignedCourse { get; set; }

        public TeachingAssistant(string borrowerId, string fullName, string assignedCourse = "Lập trình hướng đối tượng")
        {
            if (string.IsNullOrWhiteSpace(borrowerId))
                throw new ArgumentException("Mã trợ giảng không được để trống.", nameof(borrowerId));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Họ tên trợ giảng không được để trống.", nameof(fullName));

            BorrowerId = borrowerId.Trim();
            FullName = fullName.Trim();
            AssignedCourse = assignedCourse.Trim();
        }

        // Trợ giảng được mượn tối đa 2 thiết bị trên một phiếu
        public int GetMaximumDevices()
        {
            return 2;
        }

        public override string ToString()
        {
            return $"[Trợ giảng] {FullName} (Mã: {BorrowerId}, Học phần: {AssignedCourse})";
        }
    }
}
