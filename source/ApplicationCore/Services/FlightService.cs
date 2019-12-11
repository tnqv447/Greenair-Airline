
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using System.Linq;
using LinqKit;
namespace ApplicationCore.Services
{
    public class FlightService : Service<Flight, FlightDTO, FlightDTO>, IFlightService
    {
        // public IEnumerable<Flight> List { get; set; }
        public FlightService(IUnitOfWork _unitOfWork, IMapper _mapper) : base(_unitOfWork, _mapper)
        {
            // List = unitOfWork.Flights.GetAllAsync().GetAwaiter().GetResult();
        }

        public async Task<FlightDTO> getFlightAsync(string flight_id)
        {
            var flight = await unitOfWork.Flights.GetByAsync(flight_id);
            return mapper.Map<Flight, FlightDTO>(flight);
        }

        public async Task<IEnumerable<FlightDTO>> getAllFlightAsync()
        {
            var flights = await unitOfWork.Flights.GetAllAsync();
            return mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDTO>>(flights);
        }
        public async Task<IEnumerable<FlightDTO>> getAllAvailableFlightAsync()
        {
            // var flights = await unitOfWork.Flights.GetAllAsync();
            // flights = flights.Where(m => m.Status == STATUS.AVAILABLE);
            var flights = await unitOfWork.Flights.getAvailableFlights();
            return mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDTO>>(flights);
        }
        public async Task<IEnumerable<FlightDTO>> getAllDisabledFlightAsync()
        {
            // var flights = await unitOfWork.Flights.GetAllAsync();
            // flights = flights.Where(m => m.Status == STATUS.DISABLED);
            var flights = await unitOfWork.Flights.getDisabledFlights();
            return mapper.Map<IEnumerable<Flight>, IEnumerable<FlightDTO>>(flights);
        }

        //Tim kiem ============================================================================================================================
        private async Task<bool> checkOrderNum(string flight_id, int num)
        {
            var tickets = await this.getAvailableTicketAsync(flight_id);
            return num <= tickets.Count();
        }

        public async Task<IEnumerable<FlightDTO>> searchFlightAsync(string origin_id, string destination_id, DateTime dep_date, int adults_num, int childs_num)
        {
            //Expression<Func<Flight, bool>> predicate = m => true;
            //var predicate = PredicateBuilder.New<Flight>();
            if (!String.IsNullOrEmpty(origin_id) && !String.IsNullOrEmpty(destination_id))
            {
                int num = adults_num + childs_num;
                var res = await unitOfWork.Flights.getAvailableFlights();
                res = res.Where(m => Task.Run(() => this.getOriginId(m.FlightId)).GetAwaiter().GetResult().Equals(origin_id));
                res = res.Where(m => Task.Run(() => this.getDestinationId(m.FlightId)).GetAwaiter().GetResult().Equals(destination_id));
                if (dep_date != null)
                    res = res.Where(m => DateTime.Compare(Task.Run(() => this.getDepDate(m.FlightId)).GetAwaiter().GetResult(), dep_date) >= 0);
                res = res.Where(m => Task.Run(() => this.checkOrderNum(m.FlightId, num)).GetAwaiter().GetResult().Equals(origin_id));

                return toDtoRange(res);
            }
            else return null;
        }
        public async Task<IEnumerable<FlightDTO>> getLimitFlightAsync(IEnumerable<FlightDTO> flights, DateTime arr_date)
        {
            await Task.Run(() => true);
            if (arr_date != null)
                flights = flights.Where(m => DateTime.Compare(Task.Run(() => this.getArrDate(m.FlightId)).GetAwaiter().GetResult(), arr_date) <= 0);
            return flights;
        }

        new public async Task<IEnumerable<Flight>> SortAsync(IEnumerable<Flight> entities, ORDER_ENUM col, ORDER_ENUM order)
        {
            IEnumerable<Flight> res = null;
            await Task.Run(() => true);
            if (order == ORDER_ENUM.DESCENDING)
            {
                switch (col)
                {
                    case ORDER_ENUM.ORIGIN_NAME: res = entities.OrderByDescending(m => this.getOrigin(m.FlightId).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DESTINATION_NAME: res = entities.OrderByDescending(m => this.getDestination(m.FlightId).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DEP_DATE: res = entities.OrderByDescending(m => this.getDepDate(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.ARR_DATE: res = entities.OrderByDescending(m => this.getArrDate(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.FLIGHT_TIME: res = entities.OrderByDescending(m => this.getTotalFlightTime(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderByDescending(m => m.Status); break;

                    default: res = entities.OrderByDescending(m => m.FlightId); break;
                }
            }
            else
            {
                switch (col)
                {
                    case ORDER_ENUM.ORIGIN_NAME: res = entities.OrderBy(m => this.getOrigin(m.FlightId).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DESTINATION_NAME: res = entities.OrderBy(m => this.getDestination(m.FlightId).GetAwaiter().GetResult().AirportName); break;
                    case ORDER_ENUM.DEP_DATE: res = entities.OrderBy(m => this.getDepDate(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.ARR_DATE: res = entities.OrderBy(m => this.getArrDate(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.FLIGHT_TIME: res = entities.OrderBy(m => this.getTotalFlightTime(m.FlightId).GetAwaiter().GetResult()); break;
                    case ORDER_ENUM.STATUS: res = entities.OrderBy(m => m.Status); break;

                    default: res = entities.OrderBy(m => m.FlightId); break;
                }

            }
            return res;
        }
        public async Task<string> generateFlightId()
        {
            var res = await unitOfWork.Flights.GetAllAsync();
            res = res.OrderBy(m => m.FlightId);
            string id = null;
            var code = 0;

            if (res != null)
            {
                id = res.Last().FlightId;
            }
            Int32.TryParse(id, out code);
            return String.Format("{0:00000}", code + 1);
        }

        // public async Task generateFlightId(Flight Flight)
        // {
        //     var res = await unitOfWork.Flights.GetAllAsync();
        //     var id = res.LastOrDefault().FlightId;
        //     var code = 0;
        //     Int32.TryParse(id, out code);
        //     Flight.FlightId = String.Format("{0:00000}", code);
        // }
        private async Task addFlightAsync(Flight flight)
        {
            await unitOfWork.Flights.AddAsync(flight);
            await unitOfWork.CompleteAsync();
        }

        public async Task addFlightAsync(FlightDTO flightDto, IEnumerable<FlightDetailDTO> details)
        {
            var flight = this.toEntity(flightDto);
            flight.FlightId = await generateFlightId();
            await this.addFlightAsync(flight);
            foreach (FlightDetailDTO dto in details)
            {
                dto.FlightId = flight.FlightId;
                await this.addFlightDetailAsync(dto);
            }
            await generateTicket(flight.FlightId);
            await unitOfWork.CompleteAsync();

        }

        public async Task updateFlightAsync(FlightDTO flightDto)
        {
            if (await unitOfWork.Flights.GetByAsync(flightDto.FlightId) != null)
            {
                // var Flight = this.toEntity(dto);
                // await unitOfWork.Flights.UpdateAsync(Flight);
                var Flight = await unitOfWork.Flights.GetByAsync(flightDto.FlightId);
                this.convertDtoToEntity(flightDto, Flight);
            }
            // else
            // {
            //     var flight = this.toEntity(flightDto);
            //     await generateFlightId(flight);
            //     await generateTicket(flight.FlightId);
            //     await unitOfWork.Flights.AddAsync(flight);
            // }
            await unitOfWork.CompleteAsync();
        }

        public async Task removeFlightAsync(string flight_id)
        {
            var flight = await unitOfWork.Flights.GetByAsync(flight_id);
            if (flight != null)
            {
                await unitOfWork.Flights.RemoveAsync(flight);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task removeAllFlightAsync()
        {
            await unitOfWork.Flights.RemoveRangeAsync(await unitOfWork.Flights.GetAllAsync());
        }

        public async Task disableFlightAsync(string flight_id)
        {
            await unitOfWork.Flights.disable(flight_id);
        }

        public async Task activateFlightAsync(string flight_id)
        {
            await unitOfWork.Flights.activate(flight_id);
        }

        public async Task<string> getFirstRouteId(string flight_id)
        {
            return await unitOfWork.Flights.getFirstRouteId(flight_id);
        }
        public async Task<string> getLastRouteId(string flight_id)
        {
            return await unitOfWork.Flights.getLastRouteId(flight_id);
        }
        public async Task<RouteDTO> getFirstRoute(string flight_id)
        {
            var route = await unitOfWork.Routes.GetByAsync(await this.getFirstRouteId(flight_id));
            return mapper.Map<Route, RouteDTO>(route);
        }
        public async Task<RouteDTO> getLastRoute(string flight_id)
        {
            var route = await unitOfWork.Routes.GetByAsync(await this.getLastRouteId(flight_id));
            return mapper.Map<Route, RouteDTO>(route);
        }
        public async Task<string> getOriginId(string flight_id)
        {
            var route = await this.getFirstRoute(flight_id);
            if (route == null) return "";
            return route.Origin;
        }
        public async Task<string> getDestinationId(string flight_id)
        {
            var route = await this.getLastRoute(flight_id);
            if (route == null) return "";
            return route.Destination;
        }
        public async Task<AirportDTO> getOrigin(string flight_id)
        {
            var airport = await unitOfWork.Airports.GetByAsync(await this.getOriginId(flight_id));
            return mapper.Map<Airport, AirportDTO>(airport);
        }
        public async Task<AirportDTO> getDestination(string flight_id)
        {
            var airport = await unitOfWork.Airports.GetByAsync(await this.getDestinationId(flight_id));
            return mapper.Map<Airport, AirportDTO>(airport);
        }
        public async Task<DateTime> getArrDate(string flight_id)
        {
            return await unitOfWork.Flights.getArrDate(flight_id);
        }
        public async Task<DateTime> getDepDate(string flight_id)
        {
            return await unitOfWork.Flights.getDepDate(flight_id);
        }
        public async Task<FlightTimeDTO> getTotalFlightTime(string flight_id)
        {
            var dets = await unitOfWork.Flights.getAllFlightDetails(flight_id);
            var time = new FlightTime(0);
            foreach (FlightDetail det in dets)
            {
                var route = await unitOfWork.Routes.GetByAsync(det.RouteId);
                time += route.FlightTime;
            }
            return mapper.Map<FlightTime, FlightTimeDTO>(time);
        }

        //Thao tac voi chi tiet ============================================================================================
        public async Task<FlightDetailDTO> getFllightdetailAsync(string flight_id, int part)
        {
            return mapper.Map<FlightDetail, FlightDetailDTO>(await unitOfWork.Flights.getFlightDetail(flight_id, part));
        }
        public async Task<FlightDetailDTO> getFllightdetailAsync(string flight_id, string flightdetail_id)
        {
            return mapper.Map<FlightDetail, FlightDetailDTO>(await unitOfWork.Flights.getFlightDetail(flight_id, flightdetail_id));
        }
        public async Task<IEnumerable<FlightDetailDTO>> getAllFlightDetailAsync(string flight_id)
        {
            return mapper.Map<IEnumerable<FlightDetail>, IEnumerable<FlightDetailDTO>>(await unitOfWork.Flights.getAllFlightDetails(flight_id));
        }
        private async Task generateDetailId(FlightDetail det)
        {
            if (String.IsNullOrEmpty(det.FlightDetailId))
            {
                var res = await unitOfWork.Flights.getAllFlightDetails(det.FlightId);
                if (res == null || res.Count() == 0) det.FlightDetailId = "000";
                else det.FlightDetailId = String.Format("{0:000}", res.Count());
            }
        }
        public async Task<DateTime> calArrDate(DateTime depDate, FlightTime time)
        {
            await Task.Run(() => true);
            return depDate.AddMinutes(time.toMinutes());
        }
        public async Task addFlightDetailAsync(FlightDetailDTO det_dto)
        {
            var det = mapper.Map<FlightDetailDTO, FlightDetail>(det_dto);
            await generateDetailId(det);
            Console.WriteLine("{0} - {1} - wtf?? {2} wtf??", det.FlightDetailId, det.FlightId, det.RouteId);
            await unitOfWork.Flights.addFlightDetail(det);
            await unitOfWork.CompleteAsync();
        }
        public async Task addFlightDetailRangeAsync(IEnumerable<FlightDetailDTO> dets_dto)
        {
            var dets = mapper.Map<IEnumerable<FlightDetailDTO>, IEnumerable<FlightDetail>>(dets_dto);
            await unitOfWork.Flights.addFlightDetailRange(dets);
            await unitOfWork.CompleteAsync();
        }
        public async Task removeFlightDetailAsync(string flight_id)
        {
            await unitOfWork.Flights.removeFlightDetail(flight_id);
        }
        public async Task removeAllFlightDetailAsync(string flight_id)
        {
            await unitOfWork.Flights.removeAllFlightDetail(flight_id);
        }

        //Thao tac voi ve =======================================================================================

        public async Task<decimal> calTicketPrice(string flight_id, string ticket_type_id)
        {
            var type = await unitOfWork.TicketTypes.GetByAsync(ticket_type_id);
            if (type == null) return 0;
            else
            {
                var time = await this.getTotalFlightTime(flight_id);
                return (time.Hour + (decimal)time.Minute / 60) * type.BasePrice;
            }

        }

        public async Task generateTicket(string flight_id)
        {
            var flight = await unitOfWork.Flights.GetByAsync(flight_id);
            var plane = await unitOfWork.Planes.GetByAsync(flight.PlaneId);
            if (plane.SeatNum > 396)
            {
                Console.WriteLine("GenerateTicket(): Plane has too many seats!!!");
                return;
            }
            var mod = plane.SeatNum / 4;
            var tickets = new List<Ticket>();
            for (int i = 1; i <= plane.SeatNum; i++)
            {
                switch (i / mod)
                {
                    case 0:
                        tickets.Add(new Ticket(String.Format("A{0:00}", i), flight_id, null, "000", "000"));
                        break;
                    case 1:
                        tickets.Add(new Ticket(String.Format("B{0:00}", i), flight_id, null, "000", "000"));
                        break;
                    case 3:
                        tickets.Add(new Ticket(String.Format("C{0:00}", i), flight_id, null, "000", "000"));
                        break;
                    default:
                        tickets.Add(new Ticket(String.Format("D{0:00}", i), flight_id, null, "000", "000"));
                        break;
                }
            }
            await unitOfWork.Flights.addTicketRange(tickets);
        }
        public async Task<TicketDTO> getTicketAsync(string flight_id, string ticket_id)
        {
            var ticket = await unitOfWork.Flights.getTicket(flight_id, ticket_id);
            return mapper.Map<Ticket, TicketDTO>(ticket);
        }
        public async Task<IEnumerable<TicketDTO>> getAllTicketAsync()
        {
            var tickets = await unitOfWork.Flights.getAllTickets();
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }
        public async Task<IEnumerable<TicketDTO>> getAllTicketByFlightIdAsync(string flight_id)
        {
            var tickets = await unitOfWork.Flights.getAllTicketsByFlightId(flight_id);
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }
        public async Task<IEnumerable<TicketDTO>> getAvailableTicketAsync(string flight_id)
        {
            var tickets = await unitOfWork.Flights.getAvailableTickets(flight_id);
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }
        public async Task<IEnumerable<TicketDTO>> getOrderedTicketAsync(string flight_id)
        {
            var tickets = await unitOfWork.Flights.getOrderedTickets(flight_id);
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }
        public async Task<IEnumerable<TicketDTO>> getPaidTicketAsync(string flight_id)
        {
            var tickets = await unitOfWork.Flights.getPaidTickets(flight_id);
            return mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(tickets);
        }

        public async Task updateTicket(TicketDTO ticketDto)
        {
            var ticket = await unitOfWork.Flights.getTicket(ticketDto.FlightId, ticketDto.TicketId);
            if (ticket == null) return;
            mapper.Map<TicketDTO, Ticket>(ticketDto, ticket);
            await unitOfWork.CompleteAsync();
        }
        public async Task cancelTicket(string flight_id, string ticket_id)
        {
            var ticket = await unitOfWork.Flights.getTicket(flight_id, ticket_id);
            if (ticket == null) return;
            ticket.TicketTypeId = "000";
            ticket.CustomerId = null;
            ticket.AssignedCus = null;
            ticket.Status = STATUS.AVAILABLE;
            await unitOfWork.CompleteAsync();
        }

    }
}