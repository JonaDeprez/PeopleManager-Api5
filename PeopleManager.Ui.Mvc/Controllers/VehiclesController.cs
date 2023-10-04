using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Ui.Mvc.ApiServices;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleApiService _vehicleApiService;
        private readonly PersonApiService _personApiService;

        public VehiclesController(VehicleApiService vehicleApiService, PersonApiService personApiService)
        {
            _vehicleApiService = vehicleApiService;
            _personApiService = personApiService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleApiService.Find();

            return View(vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return await CreateEditView("Create", vehicle);
            }

            await _vehicleApiService.Create(vehicle);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleApiService.Get(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return await CreateEditView("Edit", vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return await CreateEditView("Edit", vehicle);
            }

            await _vehicleApiService.Update(id, vehicle);

            return RedirectToAction("Index");
        }
        
        private async Task<IActionResult> CreateEditView([AspMvcView]string viewName, Vehicle? vehicle = null)
        {
            var people = await _personApiService.Find();

            ViewBag.People = people;

            return View(viewName, vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleApiService.Get(id);

            if (vehicle is null)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpPost("Vehicles/Delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleApiService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
