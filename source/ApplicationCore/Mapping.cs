using AutoMapper;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
namespace ApplicationCore
{
    // public class AutoMapperConfiguration
    // {
    //     public static void Configure()
    //     {

    //     }
    // }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

            CreateMap<FlightTime, FlightTimeDTO>();
            CreateMap<FlightTimeDTO, FlightTime>();

            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            CreateMap<Flight, FlightDTO>();
            CreateMap<FlightDTO, Flight>();

            CreateMap<FlightDetail, FlightDetailDTO>();
            CreateMap<FlightDetailDTO, FlightDetail>();

            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>();

            CreateMap<TicketType, TicketTypeDTO>();
            CreateMap<TicketTypeDTO, TicketType>();

            CreateMap<Airport, AirportDTO>();
            CreateMap<AirportDTO, Airport>();

            CreateMap<Route, RouteDTO>();
            CreateMap<RouteDTO, Route>();

            CreateMap<Plane, PlaneDTO>();
            CreateMap<PlaneDTO, Plane>();

            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();

            CreateMap<Maker, MakerDTO>();
            CreateMap<MakerDTO, Maker>();
        }
    }
}