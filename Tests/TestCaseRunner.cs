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

        // Danh sách lưu lịch sử các phiếu mượn và nhật ký hoạt động
        public List<BorrowingSlip> SlipHistory { get; } = new();
        public List<string> ActivityLogs { get; } = new();

        private static string ReadString(string prompt, string defaultValue)
        {
            Console.Write(prompt);
            string? val = Console.ReadLine();
            val = val?.Trim('\0', '\uFEFF', '\r', '\n', ' ');
            if (string.IsNullOrWhiteSpace(val))
            {
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

            Console.WriteLine("\n1. Khởi tạo Điều khiển máy chiếu:");
            string mcId = ReadString("Nhập mã điều khiển máy chiếu: ", "MC01");
            string mcName = ReadString("Nhập tên điều khiển máy chiếu: ", "Điều khiển máy chiếu Sony");
            int mcYear = ReadInt("Nhập năm sử dụng: ", 2023);
            decimal mcPrice = ReadDecimal("Nhập giá mua: ", 350000m);
            int mcMaxDays = ReadInt("Nhập số ngày mượn tối đa: ", 3);
            CurrentProjectorRemote = new ProjectorRemote(mcId, mcName, mcYear, mcPrice, mcMaxDays);

            Console.WriteLine("\n2. Khởi tạo Điều khiển điều hòa:");
            string dhId = ReadString("Nhập mã điều khiển điều hòa: ", "DH01");
            string dhName = ReadString("Nhập tên điều khiển điều hòa: ", "Điều khiển điều hòa Daikin");
            int dhYear = ReadInt("Nhập năm sử dụng: ", 2022);
            decimal dhPrice = ReadDecimal("Nhập giá mua: ", 400000m);
            int dhMaxDays = ReadInt("Nhập số ngày mượn tối đa: ", 3);
            CurrentAcRemote = new AirConditionerRemote(dhId, dhName, dhYear, dhPrice, dhMaxDays);

            Console.WriteLine("\n3. Khởi tạo Micro không dây:");
            string micId = ReadString("Nhập mã micro: ", "MIC01");
            string micName = ReadString("Nhập tên micro: ", "Micro không dây Shure");
            int micYear = ReadInt("Nhập năm sử dụng: ", 2024);
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
                Console.WriteLine("Chưa có thông tin người mượn, vui lòng nhập:");
                string gvId = ReadString("Nhập mã người mượn: ", "GV101");
                string gvName = ReadString("Nhập họ tên người mượn: ", "Nguyễn Văn An");
                string dept = ReadString("Nhập khoa: ", "Công nghệ thông tin");
                CurrentLecturer = new Lecturer(gvId, gvName, dept);
            }

            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM2026-001");
            string room = ReadString("Nhập phòng dạy: ", "Phòng A205");
            DateTime borrowDate = ReadDateTime("Nhập ngày mượn: ", DateTime.Today);

            CurrentSlip = new BorrowingSlip(slipId, CurrentLecturer, room, borrowDate);
            if (!SlipHistory.Contains(CurrentSlip)) SlipHistory.Add(CurrentSlip);
            ActivityLogs.Add($"Tạo phiếu mượn {CurrentSlip.SlipId} cho {CurrentSlip.Borrower.FullName}");
            Console.WriteLine("Kết quả: Khởi tạo phiếu mượn thành công!");
            Console.WriteLine(CurrentSlip);
        }

        // Trường hợp 4: Thêm điều khiển máy chiếu và micro vào phiếu
        public void RunTestCase4()
        {
            Console.WriteLine("\n--- KIỂM THỬ 4: THÊM ĐIỀU KHIỂN MÁY CHIẾU VÀ MICRO VÀO PHIẾU ---");
            if (CurrentSlip == null)
            {
                RunTestCase3();
            }

            Console.WriteLine("\nNhập thiết bị 1 - Điều khiển máy chiếu:");
            string mcId = ReadString("Nhập mã điều khiển máy chiếu: ", CurrentProjectorRemote?.DeviceId ?? "MC01");
            string mcName = ReadString("Nhập tên điều khiển máy chiếu: ", CurrentProjectorRemote?.DeviceName ?? "Điều khiển máy chiếu Sony");
            int mcDays = ReadInt("Nhập số ngày mượn: ", 2);
            CurrentProjectorRemote = new ProjectorRemote(mcId, mcName, 2023, 350000m, Math.Max(mcDays, 3));

            Console.WriteLine("\nNhập thiết bị 2 - Micro không dây:");
            string micId = ReadString("Nhập mã micro: ", CurrentMicrophone?.DeviceId ?? "MIC01");
            string micName = ReadString("Nhập tên micro: ", CurrentMicrophone?.DeviceName ?? "Micro không dây Shure");
            int micDays = ReadInt("Nhập số ngày mượn: ", 1);
            CurrentMicrophone = new Microphone(micId, micName, 2024, 1200000m, Math.Max(micDays, 1));

            try
            {
                CurrentSlip!.AddDevice(CurrentProjectorRemote, mcDays);
                Console.WriteLine($"-> Đã thêm: {CurrentProjectorRemote.DeviceName}");

                CurrentSlip.AddDevice(CurrentMicrophone, micDays);
                Console.WriteLine($"-> Đã thêm: {CurrentMicrophone.DeviceName}");

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
            Console.WriteLine("\n--- KIỂM THỬ 7: LỊCH SỬ CÁC PHIẾU MƯỢN VÀ TÌNH TRẠNG TRẢ THIẾT BỊ ---");
            var allSlips = new List<BorrowingSlip>(SlipHistory);
            if (CurrentSlip != null && !allSlips.Contains(CurrentSlip))
            {
                allSlips.Add(CurrentSlip);
            }

            if (allSlips.Count == 0)
            {
                Console.WriteLine("Chưa có phiếu mượn nào trong lịch sử hệ thống!");
                return;
            }

            Console.WriteLine($"Tổng số phiếu mượn đã thực hiện: {allSlips.Count}");
            int slipIdx = 1;
            foreach (var slip in allSlips)
            {
                Console.WriteLine($"\n[PHIẾU {slipIdx++}] Mã phiếu: {slip.SlipId}");
                Console.WriteLine($"- Người mượn: {slip.Borrower.FullName} - {slip.Borrower.BorrowerId}");
                Console.WriteLine($"- Phòng học: {slip.TeachingRoom}");
                Console.WriteLine($"- Ngày mượn: {slip.BorrowedDate:yyyy-MM-dd}");
                Console.WriteLine("Danh sách thiết bị:");

                if (slip.Items.Count == 0)
                {
                    Console.WriteLine("  * (Chưa có thiết bị nào trong phiếu)");
                }

                foreach (var item in slip.Items)
                {
                    string statusDesc;
                    if (!item.IsReturned)
                    {
                        statusDesc = $"CHƯA TRẢ - Hạn trả: {item.DueDate:yyyy-MM-dd}";
                    }
                    else
                    {
                        int overdue = (item.ReturnedDate!.Value.Date - item.DueDate.Date).Days;
                        statusDesc = overdue > 0 
                            ? $"ĐÃ TRẢ MUỘN {overdue} NGÀY - Ngày trả: {item.ReturnedDate:yyyy-MM-dd}" 
                            : $"ĐÃ TRẢ ĐÚNG HẠN - Ngày trả: {item.ReturnedDate:yyyy-MM-dd}";
                    }
                    Console.WriteLine($"  * [{item.Device.DeviceId}] {item.Device.DeviceName}: {statusDesc}");
                }

                decimal totalLateFee = slip.CalculateTotalLateFee();
                Console.WriteLine($"-> Tiền phạt trễ hạn của phiếu: {totalLateFee:N0} VNĐ");
            }

            if (ActivityLogs.Count > 0)
            {
                Console.WriteLine("\n--- NHẬT KÝ HOẠT ĐỘNG HỆ THỐNG ---");
                foreach (var log in ActivityLogs)
                {
                    Console.WriteLine($"  > {log}");
                }
            }
        }

        // Ngoại lệ 1: Mượn điều khiển điều hòa đang bảo trì
        public void RunTestCase8()
        {
            Console.WriteLine("\n--- NGOẠI LỆ 1: MƯỢN THIẾT BỊ ĐANG BẢO TRÌ ---");
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX01");
            string room = ReadString("Nhập phòng dạy: ", "Phòng B101");
            string gvId = ReadString("Nhập mã người mượn: ", "GV99");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Trần Văn B");
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 1 - Mượn thiết bị bảo trì: {slip.SlipId}");

            string devId = ReadString("Nhập mã điều khiển điều hòa: ", "DH-BAOTRI");
            string devName = ReadString("Nhập tên điều khiển điều hòa: ", "Điều khiển Panasonic");
            int year = ReadInt("Nhập năm sử dụng: ", 2022);
            decimal price = ReadDecimal("Nhập giá mua: ", 450000m);
            int borrowDays = ReadInt("Nhập số ngày mượn: ", 2);

            var ac = new AirConditionerRemote(devId, devName, year, price, 3, DeviceStatus.UnderMaintenance);
            Console.WriteLine($"Trạng thái ban đầu của thiết bị: {ac.Status}");

            try
            {
                slip.AddDevice(ac, borrowDays);
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

            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX02");
            string room = ReadString("Nhập phòng dạy: ", "Phòng Lab 1");
            var slip = new BorrowingSlip(slipId, ta, room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 2 - Trợ giảng mượn vượt giới hạn: {slip.SlipId}");

            Console.WriteLine("\nNhập thông tin thiết bị 1:");
            string id1 = ReadString("Nhập mã thiết bị 1: ", "MC10");
            string name1 = ReadString("Nhập tên thiết bị 1: ", "Điều khiển máy chiếu 10");
            int days1 = ReadInt("Nhập số ngày mượn thiết bị 1: ", 2);
            var dev1 = new ProjectorRemote(id1, name1, 2023, 300000m, maxBorrowDays: Math.Max(days1, 3));

            Console.WriteLine("\nNhập thông tin thiết bị 2:");
            string id2 = ReadString("Nhập mã thiết bị 2: ", "MIC10");
            string name2 = ReadString("Nhập tên thiết bị 2: ", "Micro 10");
            int days2 = ReadInt("Nhập số ngày mượn thiết bị 2: ", 1);
            var dev2 = new Microphone(id2, name2, 2023, 800000m, maxBorrowDays: Math.Max(days2, 1));

            Console.WriteLine("\nNhập thông tin thiết bị 3:");
            string id3 = ReadString("Nhập mã thiết bị 3: ", "DH10");
            string name3 = ReadString("Nhập tên thiết bị 3: ", "Điều khiển điều hòa 10");
            int days3 = ReadInt("Nhập số ngày mượn thiết bị 3: ", 2);
            var dev3 = new AirConditionerRemote(id3, name3, 2023, 350000m, maxBorrowDays: Math.Max(days3, 3));

            try
            {
                Console.WriteLine("\nThêm thiết bị 1...");
                slip.AddDevice(dev1, days1);
                Console.WriteLine($"-> Đã thêm: {dev1.DeviceName}");

                Console.WriteLine("Thêm thiết bị 2...");
                slip.AddDevice(dev2, days2);
                Console.WriteLine($"-> Đã thêm: {dev2.DeviceName}");

                Console.WriteLine("Thêm thiết bị 3...");
                slip.AddDevice(dev3, days3);
                Console.WriteLine($"-> Đã thêm: {dev3.DeviceName}");

                Console.WriteLine($"Số thiết bị hiện tại trong phiếu: {slip.Items.Count}");

                Console.WriteLine("\nNhập thông tin thiết bị 4:");
                string id4 = ReadString("Nhập mã thiết bị 4: ", "MC11");
                string name4 = ReadString("Nhập tên thiết bị 4: ", "Điều khiển máy chiếu 11");
                int days4 = ReadInt("Nhập số ngày mượn thiết bị 4: ", 1);
                var dev4 = new ProjectorRemote(id4, name4, 2023, 300000m, maxBorrowDays: Math.Max(days4, 3));

                Console.WriteLine("Thử thêm thiết bị 4...");
                try
                {
                    slip.AddDevice(dev4, days4);
                }
                catch (BorrowLimitExceededException)
                {
                    Console.WriteLine("-> Thêm thiết bị 4 bị từ chối do vượt quá giới hạn.");
                    Console.WriteLine($"-> Trạng thái của thiết bị 4: {dev4.Status}");
                    
                    Console.WriteLine("Thử cho người khác mượn thiết bị 4...");
                    var slipOther = new BorrowingSlip("PM-TEST", new Lecturer("GV02", "Trần D"), "Phòng B2", DateTime.Today);
                    try
                    {
                        slipOther.AddDevice(dev4, 1);
                    }
                    catch (DeviceUnavailableException)
                    {
                        Console.WriteLine("-> Thất bại: Không thể mượn vì thiết bị 4 đã bị kẹt ở trạng thái Borrowed!");
                    }
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
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX03");
            string room = ReadString("Nhập phòng dạy: ", "Phòng Hội thảo");
            string gvId = ReadString("Nhập mã người mượn: ", "GV01");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Phạm Văn D");
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 3 - Thêm trùng thiết bị: {slip.SlipId}");

            string micId = ReadString("Nhập mã micro: ", "MIC99");
            string micName = ReadString("Nhập tên micro: ", "Micro hội trường");
            int days = ReadInt("Nhập số ngày mượn: ", 1);
            var mic = new Microphone(micId, micName, 2024, 1500000m, maxBorrowDays: Math.Max(days, 1));

            try
            {
                Console.WriteLine("\nThêm micro lần 1...");
                slip.AddDevice(mic, days);
                Console.WriteLine($"-> Đã thêm lần 1: {mic.DeviceName}");

                Console.WriteLine("Thêm lại chính micro đó lần 2...");
                slip.AddDevice(mic, days);
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
            Console.WriteLine("\n--- NGOẠI LỆ 4: MƯỢN MICRO 3 NGÀY DÙ GIỚI HẠN LÀ 1 NGÀY ---");
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX04");
            string room = ReadString("Nhập phòng dạy: ", "Phòng C301");
            string gvId = ReadString("Nhập mã người mượn: ", "GV01");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Phạm Văn D");
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 4 - Mượn quá ngày tối đa: {slip.SlipId}");

            string micId = ReadString("Nhập mã micro: ", "MIC02");
            string micName = ReadString("Nhập tên micro: ", "Micro cầm tay");
            int maxDays = ReadInt("Nhập số ngày mượn tối đa của thiết bị: ", 1);
            int requestedDays = ReadInt("Nhập số ngày muốn mượn: ", 3);

            var mic = new Microphone(micId, micName, 2024, 1000000m, maxBorrowDays: maxDays);
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
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX05");
            string room = ReadString("Nhập phòng dạy: ", "Phòng D102");
            string gvId = ReadString("Nhập mã người mượn: ", "GV01");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Phạm Văn D");
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 5 - Trả thiết bị lạ: {slip.SlipId}");

            string devId = ReadString("Nhập mã thiết bị mượn: ", "MC01");
            string devName = ReadString("Nhập tên thiết bị mượn: ", "Điều khiển phòng D102");
            int days = ReadInt("Nhập số ngày mượn: ", 2);
            var dev = new ProjectorRemote(devId, devName, 2023, 300000m, maxBorrowDays: Math.Max(days, 3));
            slip.AddDevice(dev, days);
            Console.WriteLine($"-> Đã thêm thiết bị vào phiếu: {dev.DeviceName}");

            string invalidId = ReadString("Nhập mã thiết bị lạ muốn trả: ", "MC999");
            DateTime returnDate = ReadDateTime("Nhập ngày trả: ", DateTime.Today.AddDays(1));

            try
            {
                slip.ReturnDevice(invalidId, returnDate);
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
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX06");
            string room = ReadString("Nhập phòng dạy: ", "Phòng E201");
            string gvId = ReadString("Nhập mã người mượn: ", "GV01");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Phạm Văn D");
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, DateTime.Today);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 6 - Trả thiết bị 2 lần: {slip.SlipId}");

            string devId = ReadString("Nhập mã thiết bị: ", "MC50");
            string devName = ReadString("Nhập tên thiết bị: ", "Điều khiển phòng E201");
            int days = ReadInt("Nhập số ngày mượn: ", 2);
            var dev = new ProjectorRemote(devId, devName, 2023, 300000m, maxBorrowDays: Math.Max(days, 3));
            slip.AddDevice(dev, days);
            Console.WriteLine($"-> Đã thêm thiết bị vào phiếu: {dev.DeviceName}");

            DateTime returnDate1 = ReadDateTime("Nhập ngày trả lần 1: ", DateTime.Today.AddDays(2));
            DateTime returnDate2 = ReadDateTime("Nhập ngày trả lần 2: ", DateTime.Today.AddDays(2));

            try
            {
                Console.WriteLine("\nTrả thiết bị lần 1...");
                slip.ReturnDevice(devId, returnDate1);
                Console.WriteLine("-> Trả lần 1 thành công.");

                Console.WriteLine("Thử trả lại thiết bị lần 2...");
                slip.ReturnDevice(devId, returnDate2);
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
            string slipId = ReadString("Nhập mã phiếu mượn: ", "PM-EX07");
            string room = ReadString("Nhập phòng dạy: ", "Phòng F301");
            string gvId = ReadString("Nhập mã người mượn: ", "GV01");
            string gvName = ReadString("Nhập họ tên người mượn: ", "Phạm Văn D");
            DateTime borrowDate = ReadDateTime("Nhập ngày mượn: ", DateTime.Today);
            var slip = new BorrowingSlip(slipId, new Lecturer(gvId, gvName), room, borrowDate);
            SlipHistory.Add(slip);
            ActivityLogs.Add($"Kiểm thử ngoại lệ 7 - Ngày trả trước ngày mượn: {slip.SlipId}");

            string devId = ReadString("Nhập mã thiết bị: ", "MC60");
            string devName = ReadString("Nhập tên thiết bị: ", "Điều khiển phòng F301");
            int days = ReadInt("Nhập số ngày mượn: ", 2);
            var dev = new ProjectorRemote(devId, devName, 2023, 300000m, maxBorrowDays: Math.Max(days, 2));
            slip.AddDevice(dev, days);
            Console.WriteLine($"-> Đã thêm thiết bị vào phiếu: {dev.DeviceName}");

            DateTime invalidDate = ReadDateTime("Nhập ngày trả: ", borrowDate.AddDays(-2));
            Console.WriteLine($"Ngày mượn: {borrowDate:yyyy-MM-dd} | Ngày trả: {invalidDate:yyyy-MM-dd}");

            try
            {
                slip.ReturnDevice(devId, invalidDate);
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

            Console.WriteLine("\nNhập thiết bị 1 - Điều khiển điều hòa:");
            string d1Id = ReadString("Nhập mã thiết bị 1: ", "DH501");
            string d1Name = ReadString("Nhập tên thiết bị 1: ", "Điều khiển điều hòa");
            int d1Year = ReadInt("Nhập năm sử dụng: ", 2018);
            decimal d1Price = ReadDecimal("Nhập giá mua: ", 500000m);
            var d1 = new AirConditionerRemote(d1Id, d1Name, d1Year, d1Price);

            Console.WriteLine("\nNhập thiết bị 2 - Micro đang bảo trì:");
            string d2Id = ReadString("Nhập mã thiết bị 2: ", "MIC501");
            string d2Name = ReadString("Nhập tên thiết bị 2: ", "Micro trợ giảng");
            int d2Year = ReadInt("Nhập năm sử dụng: ", 2024);
            decimal d2Price = ReadDecimal("Nhập giá mua: ", 1200000m);
            var d2 = new Microphone(d2Id, d2Name, d2Year, d2Price, 1, DeviceStatus.UnderMaintenance);

            Console.WriteLine("\nNhập thiết bị 3 - Điều khiển máy chiếu:");
            string d3Id = ReadString("Nhập mã thiết bị 3: ", "MC501");
            string d3Name = ReadString("Nhập tên thiết bị 3: ", "Điều khiển máy chiếu");
            int d3Year = ReadInt("Nhập năm sử dụng: ", 2023);
            decimal d3Price = ReadDecimal("Nhập giá mua: ", 350000m);
            var d3 = new ProjectorRemote(d3Id, d3Name, d3Year, d3Price);

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
