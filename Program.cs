/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using System.Text;
using W6.Tests;

namespace W6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập font hiển thị tiếng Việt trên Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var runner = new TestCaseRunner();

            while (true)
            {
                Console.WriteLine("\n==================================================================");
                Console.WriteLine("    HỆ THỐNG QUẢN LÝ THIẾT BỊ VÀ PHIẾU MƯỢN TRƯỜNG ĐẠI HỌC");
                Console.WriteLine("==================================================================");
                Console.WriteLine("--- 11.1 LUỒNG THÀNH CÔNG ---");
                Console.WriteLine("  1. Tạo một giảng viên");
                Console.WriteLine("  2. Tạo điều khiển máy chiếu, điều khiển điều hòa, micro không dây");
                Console.WriteLine("  3. Tạo phiếu mượn cho một phòng dạy");
                Console.WriteLine("  4. Thêm điều khiển máy chiếu và micro vào phiếu");
                Console.WriteLine("  5. Trả điều khiển máy chiếu đúng hạn");
                Console.WriteLine("  6. Trả micro muộn 2 ngày");
                Console.WriteLine("  7. Hiển thị các phiếu trả muộn, chưa trả");
                Console.WriteLine("--- 11.2 LUỒNG PHÁT SINH NGOẠI LỆ ---");
                Console.WriteLine("  8. Mượn điều khiển điều hòa đang bảo trì");
                Console.WriteLine("  9. Trợ giảng mượn thiết bị thứ ba");
                Console.WriteLine(" 10. Thêm cùng một micro hai lần vào phiếu");
                Console.WriteLine(" 11. Mượn micro 3 ngày dù giới hạn là 1 ngày");
                Console.WriteLine(" 12. Trả thiết bị không thuộc phiếu mượn");
                Console.WriteLine(" 13. Trả cùng một thiết bị hai lần");
                Console.WriteLine(" 14. Trả thiết bị với ngày trả trước ngày mượn");
                Console.WriteLine("--- CHỨC NĂNG BỔ SUNG & TỔNG HỢP ---");
                Console.WriteLine(" 15. Kiểm thử kết tập lớp học ClassRoom");
                Console.WriteLine(" 16. Chạy tự động lần lượt toàn bộ 14 trường hợp kiểm thử");
                Console.WriteLine("  0. Thoát chương trình");
                Console.WriteLine("==================================================================");
                Console.Write("Nhập lựa chọn của bạn: ");

                string? choice = Console.ReadLine();
                if (choice == null)
                {
                    break;
                }

                string cleanChoice = choice.Trim().Trim('\0', '\uFEFF', '\r', '\n', ' ');
                switch (cleanChoice)
                {
                    case "1":
                        runner.RunTestCase1();
                        break;
                    case "2":
                        runner.RunTestCase2();
                        break;
                    case "3":
                        runner.RunTestCase3();
                        break;
                    case "4":
                        runner.RunTestCase4();
                        break;
                    case "5":
                        runner.RunTestCase5();
                        break;
                    case "6":
                        runner.RunTestCase6();
                        break;
                    case "7":
                        runner.RunTestCase7();
                        break;
                    case "8":
                        runner.RunTestCase8();
                        break;
                    case "9":
                        runner.RunTestCase9();
                        break;
                    case "10":
                        runner.RunTestCase10();
                        break;
                    case "11":
                        runner.RunTestCase11();
                        break;
                    case "12":
                        runner.RunTestCase12();
                        break;
                    case "13":
                        runner.RunTestCase13();
                        break;
                    case "14":
                        runner.RunTestCase14();
                        break;
                    case "15":
                        runner.RunClassRoomTest();
                        break;
                    case "16":
                        runner.RunAll14Cases();
                        break;
                    case "0":
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập số từ 0 đến 16.");
                        break;
                }

                Console.WriteLine("\n-> Đã lưu vào lịch sử hệ thống! Nhấn phím Enter để quay lại Menu...");
                if (Console.ReadLine() == null)
                {
                    break;
                }
            }
        }
    }
}
