using Microsoft.AspNetCore.Mvc;

namespace AStar.Organisation.Infrastructure.API.Controllers.Interfaces
{
    public interface ICrudableController<T>
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update(T dto);
        Task<IActionResult> Create(T dto);
        Task<IActionResult> Delete(int id);
    }
}