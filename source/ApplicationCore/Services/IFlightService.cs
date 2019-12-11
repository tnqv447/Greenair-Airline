using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using ApplicationCore.Entities;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
namespace ApplicationCore.Services
{
    public interface IFlightService : IService<Flight, FlightDTO, FlightDTO>
    {
        Task<FlightDTO> getFlightAsync(string flight_id);
        Task<IEnumerable<FlightDTO>> getAllFlightAsync();
        Task<IEnumerable<FlightDTO>> getAllAvailableFlightAsync();
        Task<IEnumerable<FlightDTO>> getAllDisabledFlightAsync();
        Task<IEnumerable<FlightDTO>> searchFlightAsync(string origin_id, string destination_id, DateTime dep_date,
                    int adults_num, int childs_num);
        Task<IEnumerable<FlightDTO>> getLimitFlightAsync(IEnumerable<FlightDTO> flights, DateTime arr_date);
        //Task generateId(Flight flight);
        Task<string> generateFlightId();
        // Task addFlightAsync(FlightDTO flightDto);
        //Task addFlightAsync(FlightDTO flightDto);
        Task addFlightAsync(FlightDTO flightDto, IEnumerable<FlightDetailDTO> details);

        Task updateFlightAsync(FlightDTO flightDto);
        Task removeFlightAsync(string flight_id);
        Task removeAllFlightAsync();
        Task disableFlightAsync(string flight_id);
        Task activateFlightAsync(string flight_id);

        Task<string> getFirstRouteId(string flight_id);
        Task<string> getLastRouteId(string flight_id);
        Task<RouteDTO> getFirstRoute(string flight_id);
        Task<RouteDTO> getLastRoute(string flight_id);
        Task<string> getOriginId(string flight_id);
        Task<string> getDestinationId(string flight_id);
        Task<AirportDTO> getOrigin(string flight_id);
        Task<AirportDTO> getDestination(string flight_id);
        Task<DateTime> getArrDate(string flight_id);
        Task<DateTime> getDepDate(string flight_id);
        Task<FlightTimeDTO> getTotalFlightTime(string flight_id);

        //Thao tac voi chi tiet =======================================================================================
        Task<FlightDetailDTO> getFllightdetailAsync(string flight_id, int part);
        Task<FlightDetailDTO> getFllightdetailAsync(string flight_id, string flightdetail_id);
        Task<IEnumerable<FlightDetailDTO>> getAllFlightDetailAsync(string flight_id);
        Task<DateTime> calArrDate(DateTime depDate, FlightTime time);
        Task addFlightDetailAsync(FlightDetailDTO det_dto);
        Task addFlightDetailRangeAsync(IEnumerable<FlightDetailDTO> dets_dto);
        Task removeFlightDetailAsync(string flight_id); //xoa detail dung cuoi
        Task removeAllFlightDetailAsync(string flight_id);

        //Thao tac voi ve =======================================================================================
        Task generateTicket(string flight_id);
        Task<TicketDTO> getTicketAsync(string flight_id, string ticket_id);
        Task<IEnumerable<TicketDTO>> getAllTicketAsync();
        Task<IEnumerable<TicketDTO>> getAllTicketByFlightIdAsync(string flight_id);
        Task<IEnumerable<TicketDTO>> getAvailableTicketAsync(string flight_id);
        Task<IEnumerable<TicketDTO>> getOrderedTicketAsync(string flight_id);
        Task<IEnumerable<TicketDTO>> getPaidTicketAsync(string flight_id);
        Task<decimal> calTicketPrice(string flight_id, string ticket_type_id);

        Task updateTicket(TicketDTO ticketDto);
        Task cancelTicket(string flight_id, string ticket_id);



    }
}