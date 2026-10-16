/*************************************************
Mã sinh viên: 202418887
Họ tên: Hoàng Đình Duy
*************************************************/

using System;
using System.Collections.Generic;
using W6.Enums;
using W6.Exceptions;
using W6.Interfaces;
using W6.Models;

namespace W6.Tests
{
    public class TestCaseRunner
    {
        public Lecturer? CurrentLecturer { get; private set; }
        public ProjectorRemote? CurrentProjectorRemote { get; private set; }
        public AirConditionerRemote? CurrentAcRemote { get; private set; }
        public Microphone? CurrentMicrophone { get; private set; }
        public BorrowingSlip? CurrentSlip { get; private set; }

        private static string ReadString(string prompt, string defaultValue)
        {
            Console.Write(prompt);
            string? val = Console.ReadLine();
            val = val?.Trim('\0', '\uFEFF', '\r', '\n', ' ');
            if (string.IsNullOrWhiteSpace(val))
            {
                Console.WriteLine($"   Áp dụng: {defaultValue}");
                return defaultValue;
            }
            return val.Trim();
        }

        private static int ReadInt(string prompt, int defaultValue)
        {
            Console.Write(prompt);
            string? val = Console.ReadLine();
            val = val?.Trim('\0', '\uFEFF', '\r', '\n', ' ');
            if (int.TryParse(val, out int result))
            {
                return result;
            }
            Console.WriteLine($"   Áp dụng: {defaultValue}");
            return defaultValue;
        }

        private static decimal ReadDecimal(string prompt, decimal defaultValue)
        {
            Console.Write(prompt);
            string? val = Console.ReadLine();
            val = val?.Trim('\0', '\uFEFF', '\r', '\n', ' ');
            if (decimal.TryParse(val, out decimal result) && result > 0)
            {
                return result;
            }
            Console.WriteLine($"   Áp dụng: {defaultValue}");
            return defaultValue;
        }

        private static DateTime ReadDateTime(string prompt, DateTime defaultValue)
        {
            Console.Write(prompt);
            string? val = Console.ReadLine();
            val = val?.Trim('\0', '\uFEFF', '\r', '\n', ' ');
            if (DateTime.TryParse(val, out DateTime result))
            {
                return result;
            }
            Console.WriteLine($"   Áp dụng: {defaultValue:yyyy-MM-dd}");
            return defaultValue;
        }

        // Trường hợp 1: Tạo một giảng viên
        public void RunTestCase1()
        {
            Console.WriteLine("\n--- KIỂM THỬ 1: TẠO MỘT GIẢNG VIÊN ---");
            string id = ReadString("Nhập mã giảng viên: ", "GV101");
            string name = ReadString("Nhập họ tên giảng viên: ", "Nguyễn Văn An");
            string dept = ReadString("Nhập khoa: ", "Công nghệ thông tin");

            CurrentLecturer = new Lecturer(id, name, dept);
            Console.WriteLine("Kết quả: Khởi tạo giảng viên thành công!");
            Console.WriteLine($"Thông tin: {CurrentLecturer}");
            Console.WriteLine($"Hạn mức mượn tối đa: {CurrentLecturer.GetMaximumDevices()} thiết bị");
        }

        // Trường hợp 2: Tạo điều khiển máy chiếu, điều khiển điều hòa, micro không dây
        public void RunTestCase2()
        {
            Console.WriteLine("\n--- KIỂM THỬ 2: TẠO 3 LOẠI THIẾT BỊ GIẢNG DẠY ---");

            Console.WriteLine("1. Khởi tạo Điều khiển máy chiếu:");
            string mcId = ReadString("Nhập mã điều khiển máy chiếu: ", "MC01");
            string mcName = ReadString("Nhập tên thiết bị: ", "Điều khiển máy chiếu Sony");
            int mcYear = ReadInt("Nhập năm đưa vào sử dụng: ", 2023);
            decimal mcPrice = ReadDecimal("Nhập giá mua: ", 350000m);
            int mcMaxDays = ReadInt("Nhập số ngày mượn tối đa: ", 3);
            CurrentProjectorRemote = new ProjectorRemote(mcId, mcName, mcYear, mcPrice, mcMaxDays);

            Console.WriteLine("\n2. Khởi tạo Điều khiển điều hòa:");
            string dhId = ReadString("Nhập mã điều khiển điều hòa: ", "DH01");
            string dhName = ReadString("Nhập tên thiết bị: ", "Điều khiển điều hòa Daikin");
            int dhYear = ReadInt("Nhập năm đưa vào sử dụng: ", 2022);
            decimal dhPrice = ReadDecimal("Nhập giá mua: ", 400000m);
            int dhMaxDays = ReadInt("Nhập số ngày mượn tối đa: ", 3);
            CurrentAcRemote = new AirConditionerRemote(dhId, dhName, dhYear, dhPrice, dhMaxDays);

            Console.WriteLine("\n3. Khởi tạo Micro không dây:");
            string micId = ReadString("Nhập mã micro: ", "MIC01");
            string micName = ReadString("Nhập tên thiết bị: ", "Micro không dây Shure");
            int micYear = ReadInt("Nhập năm đưa vào sử dụng: ", 2024);
            decimal micPrice = ReadDecimal("Nhập giá mua: ", 1200000m);
            int micMaxDays = ReadInt("Nhập số ngày mượn tối đa: ", 1);
            CurrentMicrophone = new Microphone(micId, micName, micYear, micPrice, micMaxDays);

            Console.WriteLine("\nKết quả tạo thiết bị:");
            Console.WriteLine($"- {CurrentProjectorRemote}");
            Console.WriteLine($"- {CurrentAcRemote}");
            Console.WriteLine($"- {CurrentMicrophone}");
        }

        // Trường hợp 3: Tạo phiếu mượn cho một phòng dạy
        public void RunTestCase3()
        {
            Console.WriteLine("\n--- KIỂM THỬ 3: TẠO PHIẾU MƯỢN CHO PHÒNG DẠY ---");
            if (CurrentLecturer == null)
            {
                CurrentLecturer = new Lecturer("GV101", "Nguyễn Văn An", "Công nghệ thông tin");
            }

            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM2026-001");
            string room = ReadString("Nhập phòng dạy: ", "Phòng A205");
            DateTime borrowDate = ReadDateTime("Nhập ngày mượn: ", DateTime.Today);

            CurrentSlip = new BorrowingSlip(slipId, CurrentLecturer, room, borrowDate);
            Console.WriteLine("Kết quả: Khởi tạo phiếu mượn thành công!");
            Console.WriteLine(CurrentSlip);
        }

        // Trường hợp 4: Thêm điều khiển máy chiếu và micro vào phiếu
        public void RunTestCase4()
        {
            Console.WriteLine("\n--- KIỂM THỬ 4: THÊM ĐIỀU KHIỂN MÁY CHIẾU VÀ MICRO VÀO PHIẾU ---");
            if (CurrentSlip == null)
            {
                RunTestCase1();
                RunTestCase3();
            }

            if (CurrentProjectorRemote == null || CurrentMicrophone == null)
            {
                RunTestCase2();
            }

            try
            {
                int days1 = ReadInt("Nhập số ngày mượn cho điều khiển máy chiếu: ", 2);
                CurrentSlip!.AddDevice(CurrentProjectorRemote!, days1);
                Console.WriteLine($"-> Đã thêm: {CurrentProjectorRemote!.DeviceName}");

                int days2 = ReadInt("Nhập số ngày mượn cho micro: ", 1);
                CurrentSlip!.AddDevice(CurrentMicrophone!, days2);
                Console.WriteLine($"-> Đã thêm: {CurrentMicrophone!.DeviceName}");

                Console.WriteLine("\nDanh sách thiết bị trong phiếu:");
                foreach (var it in CurrentSlip.Items)
                {
                    Console.WriteLine($"  + {it}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm thiết bị: {ex.Message}");
            }
        }

        // Trường hợp 5: Trả điều khiển máy chiếu đúng hạn
        public void RunTestCase5()
        {
            Console.WriteLine("\n--- KIỂM THỬ 5: TRẢ ĐIỀU KHIỂN MÁY CHIẾU ĐÚNG HẠN ---");
            if (CurrentSlip == null || CurrentProjectorRemote == null)
            {
                RunTestCase1();
                RunTestCase2();
                RunTestCase3();
                RunTestCase4();
            }

            string devId = ReadString("Nhập mã thiết bị trả: ", CurrentProjectorRemote!.DeviceId);
            DateTime returnDate = ReadDateTime("Nhập ngày trả: ", CurrentSlip!.BorrowedDate.AddDays(2));

            try
            {
                decimal fee = CurrentSlip.ReturnDevice(devId, returnDate);
                Console.WriteLine($"Kết quả trả thiết bị '{devId}': Đã ghi nhận ngày trả.");
                Console.WriteLine($"Ngày trả: {returnDate:yyyy-MM-dd}");
                Console.WriteLine($"Tiền phạt trễ hạn tính được: {fee:N0} VNĐ");
                Console.WriteLine($"Trạng thái thiết bị sau khi trả: {CurrentProjectorRemote.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ phát sinh: {ex.Message}");
            }
        }

        // Trường hợp 6: Trả micro muộn 2 ngày
        public void RunTestCase6()
        {
            Console.WriteLine("\n--- KIỂM THỬ 6: TRẢ MICRO MUỘN 2 NGÀY ---");
            if (CurrentSlip == null || CurrentMicrophone == null)
            {
                RunTestCase1();
                RunTestCase2();
                RunTestCase3();
                RunTestCase4();
            }

            string devId = ReadString("Nhập mã micro trả: ", CurrentMicrophone!.DeviceId);
            DateTime returnDate = ReadDateTime("Nhập ngày trả: ", CurrentSlip!.BorrowedDate.AddDays(3));

            try
            {
                decimal fee = CurrentSlip.ReturnDevice(devId, returnDate);
                Console.WriteLine($"Kết quả trả micro '{devId}': Đã ghi nhận trả muộn.");
                Console.WriteLine($"Ngày trả: {returnDate:yyyy-MM-dd}");
                Console.WriteLine($"Tiền phạt trễ hạn: {fee:N0} VNĐ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ phát sinh: {ex.Message}");
            }
        }

        // Trường hợp 7: Hiển thị các phiếu trả muộn, chưa trả
        public void RunTestCase7()
        {
            Console.WriteLine("\n--- KIỂM THỬ 7: HIỂN THỊ TÌNH TRẠNG PHIẾU MƯỢN VÀ TIỀN PHẠT ---");
            if (CurrentSlip == null)
            {
                Console.WriteLine("Chưa có phiếu mượn để hiển thị!");
                return;
            }

            Console.WriteLine("THÔNG TIN PHIẾU MƯỢN:");
            Console.WriteLine($"- Mã phiếu: {CurrentSlip.SlipId}");
            Console.WriteLine($"- Người mượn: {CurrentSlip.Borrower.FullName} ({CurrentSlip.Borrower.BorrowerId})");
            Console.WriteLine($"- Phòng học: {CurrentSlip.TeachingRoom}");
            Console.WriteLine($"- Ngày mượn: {CurrentSlip.BorrowedDate:yyyy-MM-dd}");
            Console.WriteLine("\nCHI TIẾT CÁC THIẾT BỊ:");

            foreach (var item in CurrentSlip.Items)
            {
                string statusDesc;
                if (!item.IsReturned)
                {
                    statusDesc = $"CHƯA TRẢ (Hạn trả: {item.DueDate:yyyy-MM-dd})";
                }
                else
                {
                    int overdue = (item.ReturnedDate!.Value.Date - item.DueDate.Date).Days;
                    statusDesc = overdue > 0 
                        ? $"ĐÃ TRẢ MUỘN {overdue} NGÀY (Ngày trả: {item.ReturnedDate:yyyy-MM-dd})" 
                        : $"ĐÃ TRẢ ĐÚNG HẠN (Ngày trả: {item.ReturnedDate:yyyy-MM-dd})";
                }
                Console.WriteLine($"  * [{item.Device.DeviceId}] {item.Device.DeviceName}: {statusDesc}");
            }

            decimal totalLateFee = CurrentSlip.CalculateTotalLateFee();
            Console.WriteLine($"\n-> TỔNG TIỀN PHẠT TRỄ HẠN CỦA PHIẾU: {totalLateFee:N0} VNĐ");
        }

        // Ngoại lệ 1: Mượn điều khiển điều hòa đang UnderMaintenance
        public void RunTestCase8()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 1: MƯỢN THIẾT BỊ ĐANG UNDERMAINTENANCE ---");
            string devId = ReadString("Nhập mã điều hòa: ", "DH-BAOTRI");
            string devName = ReadString("Nhập tên điều khiển điều hòa: ", "Điều khiển Panasonic");
            
            var ac = new AirConditionerRemote(devId, devName, 2022, 450000m, 3, DeviceStatus.UnderMaintenance);
            var slip = new BorrowingSlip("PM-EX01", new Lecturer("GV99", "Trần Văn B"), "Phòng B101", DateTime.Today);

            Console.WriteLine($"Trạng thái ban đầu của thiết bị: {ac.Status}");
            try
            {
                slip.AddDevice(ac, 2);
            }
            catch (DeviceUnavailableException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ DeviceUnavailableException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Mã thiết bị: {ex.DeviceId} | Trạng thái: {ex.CurrentStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bắt được ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 2: Trợ giảng mượn thiết bị thứ ba
        public void RunTestCase9()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 2: TRỢ GIẢNG MƯỢN THIẾT BỊ THỨ BA ---");
            string taId = ReadString("Nhập mã trợ giảng: ", "TA01");
            string taName = ReadString("Nhập họ tên trợ giảng: ", "Lê Văn C");
            var ta = new TeachingAssistant(taId, taName);
            Console.WriteLine($"Hạn mức mượn của trợ giảng: {ta.GetMaximumDevices()} thiết bị");

            var slip = new BorrowingSlip("PM-EX02", ta, "Phòng Lab 1", DateTime.Today);
            var dev1 = new ProjectorRemote("MC10", "Điều khiển máy chiếu 10", 2023, 300000m);
            var dev2 = new Microphone("MIC10", "Micro 10", 2023, 800000m);
            var dev3 = new AirConditionerRemote("DH10", "Điều khiển điều hòa 10", 2023, 350000m);

            try
            {
                Console.WriteLine("Thêm thiết bị 1...");
                slip.AddDevice(dev1, 2);
                Console.WriteLine("Thêm thiết bị 2...");
                slip.AddDevice(dev2, 1);
                Console.WriteLine("Thêm thiết bị 3 (vượt quá giới hạn 2 thiết bị)...");
                slip.AddDevice(dev3, 2);

                Console.WriteLine($"Số thiết bị hiện tại trong phiếu: {slip.Items.Count}");
                Console.WriteLine("Thử thêm thiết bị 4...");
                var dev4 = new ProjectorRemote("MC11", "Điều khiển máy chiếu 11", 2023, 300000m);
                try
                {
                    slip.AddDevice(dev4, 1);
                }
                catch (BorrowLimitExceededException)
                {
                    Console.WriteLine("-> Thêm thiết bị 4 bị từ chối do vượt quá giới hạn.");
                    Console.WriteLine($"-> Trạng thái của thiết bị 4: {dev4.Status}");
                }
            }
            catch (BorrowLimitExceededException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ BorrowLimitExceededException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Mã người mượn: {ex.BorrowerId} | Giới hạn tối đa: {ex.MaximumDevices}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 3: Thêm cùng một micro hai lần vào phiếu
        public void RunTestCase10()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 3: THÊM CÙNG MỘT MICRO HAI LẦN VÀO PHIẾU ---");
            string micId = ReadString("Nhập mã micro: ", "MIC99");
            var mic = new Microphone(micId, "Micro hội trường", 2024, 1500000m);
            var slip = new BorrowingSlip("PM-EX03", new Lecturer("GV01", "Phạm Văn D"), "Phòng Hội thảo", DateTime.Today);

            try
            {
                Console.WriteLine("Thêm micro lần 1...");
                slip.AddDevice(mic, 1);
                Console.WriteLine("Thêm lại chính micro đó lần 2...");
                slip.AddDevice(mic, 1);
            }
            catch (DuplicateDeviceException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ DuplicateDeviceException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Mã thiết bị: {ex.DeviceId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 4: Mượn micro trong 3 ngày dù giới hạn là 1 ngày
        public void RunTestCase11()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 4: MƯỢN MICRO TRONG 3 NGÀY DÙ GIỚI HẠN LÀ 1 NGÀY ---");
            string micId = ReadString("Nhập mã micro: ", "MIC02");
            var mic = new Microphone(micId, "Micro cầm tay", 2024, 1000000m, maxBorrowDays: 1);
            var slip = new BorrowingSlip("PM-EX04", new Lecturer("GV01", "Phạm Văn D"), "Phòng C301", DateTime.Today);

            int requestedDays = ReadInt("Nhập số ngày mượn: ", 3);
            Console.WriteLine($"Giới hạn của thiết bị: {mic.MaxBorrowDays} ngày | Yêu cầu: {requestedDays} ngày");

            try
            {
                slip.AddDevice(mic, requestedDays);
            }
            catch (InvalidBorrowPeriodException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ InvalidBorrowPeriodException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Thiết bị: {ex.DeviceId} | Ngày yêu cầu: {ex.RequestedDays} | Tối đa: {ex.MaxDays}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 5: Trả một điều khiển máy chiếu không thuộc phiếu
        public void RunTestCase12()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 5: TRẢ THIẾT BỊ KHÔNG THUỘC PHIẾU ---");
            var slip = new BorrowingSlip("PM-EX05", new Lecturer("GV01", "Phạm Văn D"), "Phòng D102", DateTime.Today);
            var dev1 = new ProjectorRemote("MC01", "Điều khiển phòng D102", 2023, 300000m);
            slip.AddDevice(dev1, 2);

            string invalidId = ReadString("Nhập mã thiết bị lạ để trả: ", "MC999");
            try
            {
                slip.ReturnDevice(invalidId, DateTime.Today.AddDays(1));
            }
            catch (DeviceNotInSlipException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ DeviceNotInSlipException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Mã thiết bị: {ex.DeviceId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 6: Trả cùng một thiết bị hai lần
        public void RunTestCase13()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 6: TRẢ CÙNG MỘT THIẾT BỊ HAI LẦN ---");
            var slip = new BorrowingSlip("PM-EX06", new Lecturer("GV01", "Phạm Văn D"), "Phòng E201", DateTime.Today);
            var dev = new ProjectorRemote("MC50", "Điều khiển phòng E201", 2023, 300000m);
            slip.AddDevice(dev, 2);

            try
            {
                Console.WriteLine("Trả thiết bị lần 1...");
                slip.ReturnDevice("MC50", DateTime.Today.AddDays(2));
                Console.WriteLine("-> Trả lần 1 thành công.");

                Console.WriteLine("Thử trả lại thiết bị lần 2...");
                slip.ReturnDevice("MC50", DateTime.Today.AddDays(2));
            }
            catch (DeviceAlreadyReturnedException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ DeviceAlreadyReturnedException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Mã thiết bị: {ex.DeviceId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Ngoại lệ 7: Trả thiết bị với ngày trả trước ngày mượn
        public void RunTestCase14()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 7: TRẢ THIẾT BỊ VỚI NGÀY TRẢ TRƯỚC NGÀY MƯỢN ---");
            DateTime borrowDate = DateTime.Today;
            var slip = new BorrowingSlip("PM-EX07", new Lecturer("GV01", "Phạm Văn D"), "Phòng F301", borrowDate);
            var dev = new ProjectorRemote("MC60", "Điều khiển phòng F301", 2023, 300000m);
            slip.AddDevice(dev, 2);

            DateTime invalidDate = borrowDate.AddDays(-2);
            Console.WriteLine($"Ngày mượn: {borrowDate:yyyy-MM-dd} | Ngày trả: {invalidDate:yyyy-MM-dd}");

            try
            {
                slip.ReturnDevice("MC60", invalidDate);
            }
            catch (InvalidReturnDateException ex)
            {
                Console.WriteLine("Bắt được ngoại lệ InvalidReturnDateException:");
                Console.WriteLine($"-> Thông điệp: {ex.Message}");
                Console.WriteLine($"-> Ngày trả: {ex.ReturnedDate:yyyy-MM-dd} | Ngày mượn: {ex.BorrowedDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ngoại lệ: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Kiểm thử kết tập lớp học ClassRoom
        public void RunClassRoomTest()
        {
            Console.WriteLine("\n--- KIỂM THỬ KẾT TẬP CLASSROOM ---");
            string roomId = ReadString("Nhập mã phòng học: ", "P501");
            string roomName = ReadString("Nhập tên phòng: ", "Giảng đường đa năng 501");
            int capacity = ReadInt("Nhập sức chứa: ", 120);

            var room = new ClassRoom(roomId, roomName, capacity);

            var d1 = new AirConditionerRemote("DH501", "Điều khiển điều hòa", 2018, 500000m);
            var d2 = new Microphone("MIC501", "Micro trợ giảng", 2024, 1200000m, 1, DeviceStatus.UnderMaintenance);
            var d3 = new ProjectorRemote("MC501", "Điều khiển máy chiếu", 2023, 350000m);

            room.AddDevice(d1);
            room.AddDevice(d2);
            room.AddDevice(d3);

            Console.WriteLine($"\nThông tin phòng: {room}");
            Console.WriteLine("Danh sách thiết bị trong phòng:");
            foreach (var d in room.Devices)
            {
                Console.WriteLine($"  + {d}");
            }

            Console.WriteLine($"\nTổng chi phí bảo trì dự kiến 1 năm: {room.CalculateAnnualMaintenanceCost():N0} VNĐ");

            var maintenanceList = room.GetDevicesRequiringMaintenance();
            Console.WriteLine($"\nDanh sách thiết bị cần bảo trì: {maintenanceList.Count} thiết bị");
            foreach (var d in maintenanceList)
            {
                int age = DateTime.Now.Year - d.YearOfUsage;
                Console.WriteLine($"  ! [{d.DeviceId}] {d.DeviceName} | Trạng thái: {d.Status} | Thời gian SD: {age} năm");
            }
        }

        // Chạy tự động lần lượt 14 trường hợp kiểm thử
        public void RunAll14Cases()
        {
            Console.WriteLine("\n========================================================");
            Console.WriteLine("BẮT ĐẦU CHẠY TỰ ĐỘNG TOÀN BỘ 14 TRƯỜNG HỢP KIỂM THỬ");
            Console.WriteLine("========================================================");

            RunTestCase1();
            RunTestCase2();
            RunTestCase3();
            RunTestCase4();
            RunTestCase5();
            RunTestCase6();
            RunTestCase7();

            RunTestCase8();
            RunTestCase9();
            RunTestCase10();
            RunTestCase11();
            RunTestCase12();
            RunTestCase13();
            RunTestCase14();

            Console.WriteLine("\n========================================================");
            Console.WriteLine("HOÀN THÀNH CHẠY 14 TRƯỜNG HỢP KIỂM THỬ!");
            Console.WriteLine("========================================================");
        }
    }
}
