using System.Threading;
using ApplicationCore.Entities;
using System.Linq;
using System;
using ApplicationCore;
using ApplicationCore.Interfaces;
namespace Infrastructure.Persistence
{
    public class DataSeed
    {
        public static void Initialize(GreenairContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Jobs.Any())
            {
                context.Jobs.AddRange(
                    new Job("000", "Administrator"),
                    new Job("001", "Customer Manager"),
                    new Job("002", "Employee Manager"),
                    new Job("003", "Flight Manager")
                );
                context.SaveChanges();
            }
            if (!context.Makers.Any())
            {
                var address = new Address("123", "ABC", "Hogwart", "Gotham", "Newyork", "USA");
                var address2 = new Address("123", "ABC", "Hogwart", "Gotham", "Newyork", "USA");
                context.Makers.AddRange(
                                new Maker("000", "BOEING", new Address("123", "ABC", "Hahaha", "Gotham", "Newyork", "USA")),
                                new Maker("001", "BAMBOO", address2),
                                new Maker("002", "Mc.King", new Address("457", "AN Duong Vuong", "10", "5", "", "Viet Nam"))
                            );
                context.SaveChanges();
            }
            if (!context.Airports.Any())
            {
                Address add = new Address("472", "Mac Cua", "An Lan", "Cai Chien", "Ha Noi", "Viet Nam");
                Address add2 = new Address("472", "Cong Hoa", "5", "Tan Binh", "Ho Chi Minh", "Viet Nam");
                context.Airports.AddRange(
                    new Airport("HAN", "Noi Bai", add, STATUS.AVAILABLE),
                    new Airport("SGN", "Tan Son Nhat", add2, STATUS.AVAILABLE),
                    new Airport("HPH", "Hai Phong", new Address("25/78", "HeHe", "TuTu", "MeMe", "California", "USA"), STATUS.AVAILABLE),
                    new Airport("VDO", "Van Don", new Address("2547", "TonTon", "ChiChi", "Goku", "Texas", "USA"), STATUS.AVAILABLE),
                    new Airport("THD", "Thanh Hoa", new Address("2547", "TonTon", "ChiChi", "Goku", "California", "USA"), STATUS.AVAILABLE),
                    new Airport("VII", "Vinh", new Address("2547", "TonTon", "ChiChi", "Goku", "Tokyo", "Japan"), STATUS.AVAILABLE),
                    new Airport("VDH", "Dong Hoi", new Address("2547", "TonTon", "ChiChi", "Goku", "Osaka", "Japan"), STATUS.AVAILABLE),
                    new Airport("HUI", "Hue", new Address("2547", "TonTon", "ChiChi", "Goku", "Liverpool", "UK"), STATUS.AVAILABLE),
                    new Airport("DAD", "Da Nang", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("VCL", "Tam Ky", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("UIH", "Quy Nhon", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("TBB", "Tuy Hoa", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("CXR", "Nha Trang", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("PQC", "Phu Quoc", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("PXU", "Pleiku", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("BMV", "Buon Me Thuot", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("DLI", "Da Lat", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("SIN", "Singapore", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("BKK", "Bangkok", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("JKT", "Jakarta jkt", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("HKG", "Hong Kong", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("CAN", "Guangzhou", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("KIX", "Osaka Kansai", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("SYD", "Sydney", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE),
                    new Airport("MEl", "Melbourne", new Address("2547", "TonTon", "ChiChi", "Goku", "London", "UK"), STATUS.AVAILABLE)

                );
                context.SaveChanges();
            }
            if (!context.Planes.Any())
            {
                context.Planes.AddRange(
                    new Plane("00000", 40, "000"),
                    new Plane("00001", 50, "000"),
                    new Plane("00002", 30, "001"),
                    new Plane("00003", 40, "001"),
                    new Plane("00004", 35, "002")
                );
                context.SaveChanges();
            }
            if (!context.Airports.Any())
            {

                context.SaveChanges();
            }
            if (!context.TicketTypes.Any())
            {
                context.TicketTypes.AddRange(
                    new TicketType("000", "Người lớn", 100000),
                    new TicketType("001", "Trẻ em", 50000)
                );
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {

                context.Employees.AddRange(
                    new Employee("00001", "Trieu Ng Quoc", "Viet", new DateTime(1999, 12, 7), "0904897191", new Address("123", "ABC", "hahahaha", "Gotham", "Newyork", "USA"), new Account("admin1", "12345"), 15000, "001"),
                    new Employee("00002", "Vu Tuong", "Giang", new DateTime(1999, 12, 8), "0904891597", new Address("123", "gfd", "fsd", "sdf", "fs", "fsd"), new Account("admin2", "12345"), 18000, "002"),
                    new Employee("00003", "Do Trung", "Hieu", new DateTime(1999, 7, 10), "0903774110", new Address("456", "gfd", "fsd", "sdf", "fs", "fsd"), new Account("admin3", "12345"), 19000, "003"),
                    new Employee("00004", "Trieu Trung", "Gia", new DateTime(2000, 5, 5), "0903775781", new Address("456", "Tran Phu", "Phu Tho", "Tan Phu", "Ho Chi Minh", "Viet Nam"), new Account("admin4", "12345"), 20000, "000")
                );
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer("000", "", "Unknown", DateTime.MinValue, "0000000000", new Address("", "", "", "", "", ""), new Account("cus0", "12345"), "unknow@gmail.com"),
                    new Customer("00005", "Phung Quoc", "Hai", new DateTime(1999, 12, 5), "0802774147", new Address("142", "An Duong Vuong", "10", "5", "Ho Chi Minh", "Viet Nam"), new Account("cus1", "12345"), "abrrc@gmail.com"),
                    new Customer("00006", "Luu Minh", "Hoang", new DateTime(1999, 4, 30), "0801774147", new Address("472", "Mac Cua", "An Lan", "Cai Chien", "Ha Noi", "Viet Nam"), new Account("cus2", "12345"), "abrrfsc@gmail.com"),
                    new Customer("00007", "Tran Van", "Hoang", new DateTime(1999, 6, 1), "0908774147", new Address("789", "gfd", "fsd", "sdf", "fs", "fsd"), new Account("cus3", "12345"), "abrrfsc@gmail.com"),
                    new Customer("00008", "Tran Van", "An", new DateTime(1999, 6, 1), "0908774147", new Address("789", "gfd", "fsd", "sdf", "fs", "fsd"), new Account("cus4", "12345"), "abrrfsc@gmail.com")
                );
                context.SaveChanges();
            }

            // Flight 
            if (!context.Routes.Any())
            {
                context.Routes.AddRange(
                    new Route("00000", "HAN", "SGN", new FlightTime(1, 45)),
                    new Route("00001", "SGN", "HAN", new FlightTime(1, 45)),
                    new Route("00002", "HAN", "THD", new FlightTime(4, 0)),
                    new Route("00003", "THD", "HAN", new FlightTime(4, 0)),
                    new Route("00004", "DLI", "BMV", new FlightTime(4, 30)),
                    new Route("00005", "BMV", "DLI", new FlightTime(4, 30)),
                    new Route("00006", "SGN", "DLI", new FlightTime(10, 30)),
                    new Route("00007", "DLI", "SGN", new FlightTime(10, 30)),
                    new Route("00008", "BMV", "HAN", new FlightTime(10, 0)),
                    new Route("00009", "HAN", "BMV", new FlightTime(10, 0))
                );
                context.SaveChanges();
            }

            if (!context.Flights.Any())
            {
                context.Flights.AddRange(
                    new Flight("00000", "00000", STATUS.AVAILABLE),
                    new Flight("00001", "00001", STATUS.AVAILABLE),
                    new Flight("00002", "00003", STATUS.AVAILABLE),
                    new Flight("00003", "00004", STATUS.AVAILABLE),
                    new Flight("00004", "00002", STATUS.AVAILABLE),
                    new Flight("00005", "00000", STATUS.AVAILABLE)
                );
                context.SaveChanges();
            }

            if (!context.FlightDetails.Any())
            {
                context.FlightDetails.AddRange(
                    new FlightDetail("00000", "00000", "00000", new DateTime(2019, 12, 1, 10, 0, 0), new DateTime(2019, 12, 1, 11, 45, 0)),
                    new FlightDetail("00001", "00001", "00001", new DateTime(2019, 12, 1, 12, 0, 0), new DateTime(2019, 12, 1, 13, 45, 0)),
                    new FlightDetail("00002", "00002", "00000", new DateTime(2019, 12, 2, 12, 0, 0), new DateTime(2019, 12, 2, 13, 45, 0)),
                    new FlightDetail("00003", "00002", "00002", new DateTime(2019, 12, 2, 15, 0, 0), new DateTime(2019, 12, 2, 19, 30, 0)),
                    new FlightDetail("00004", "00003", "00006", new DateTime(2019, 12, 3, 6, 0, 0), new DateTime(2019, 12, 3, 16, 30, 0))
                );
                context.SaveChanges();
            }
        }

    }
}