using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationCore.DTOs;

namespace ApplicationCore.Services
{
    public interface IService<Entity, Dto, SaveDto>
    {
        Dto toDto(Entity entity);
        IEnumerable<Dto> toDtoRange(IEnumerable<Entity> entities);

        Entity toEntity(SaveDto save);
        IEnumerable<Entity> toEnityRange(IEnumerable<SaveDto> entites);
        void convertEntityToDto(Entity entity, Dto dto);
        void convertDtoToEntity(SaveDto dto, Entity entity);

        Task<IEnumerable<Dto>> SortAsync(IEnumerable<Dto> entities, ORDER_ENUM col, ORDER_ENUM order);

        Task<int> CompleteAsync();
        Task addFlightAsync(FlightDTO flightDto, IEnumerable<FlightDetailDTO> details);
    }
}