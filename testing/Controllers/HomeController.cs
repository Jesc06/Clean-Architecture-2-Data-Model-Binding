using Microsoft.AspNetCore.Mvc;
using testing.ViewModels;
using testing.Application.DTO;
using testing.Application.Services.Information;
using testing.Application.Services.Hobby;

namespace testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddInfoAsyncServices _addAsyncServices;
        private readonly GetInfoServices _getInfoServices;

        private readonly HobbyServices _addHobbyServices;
        private readonly GetAllHobbyServices _getAllHobbyServices;

        public HomeController(HobbyServices addHobbyServices, GetAllHobbyServices getAllHobbyServices, GetInfoServices getInfoServices, AddInfoAsyncServices addAsyncServices)
        {
            //add info injection
            _addAsyncServices = addAsyncServices;
            _getInfoServices = getInfoServices;

            //add hobby injection
            _addHobbyServices = addHobbyServices;
            _getAllHobbyServices = getAllHobbyServices;
        }


        public async Task<IActionResult> Home()
        {
            GeneralAllModel model = new GeneralAllModel();
            model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();
            model.HobbyAllRecords = await _getAllHobbyServices.AllHobbyRecordsAsync();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddInfo(GeneralAllModel model)
        {

            InformationDTO dto = new InformationDTO
            {
               name = model.information.name,
               lastname = model.information.lastname
            };
            model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();
            model.HobbyAllRecords = await _getAllHobbyServices.AllHobbyRecordsAsync();
            var success = await _addAsyncServices.AddAsync(dto);
            if (success)
            {
                return RedirectToAction("Home", model);
            }
            else
            {
                ModelState.AddModelError("", "Failed to add information. Please try again.");
                return View("Home", model);
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddHobby(GeneralAllModel model)
        {
            HobbyDTO hobbyDTO = new HobbyDTO
            {
                hobbyname = model.hobby.HobbyName,
                secondhobbyname = model.hobby.SecondHobbyName
            };
            model.HobbyAllRecords = await _getAllHobbyServices.AllHobbyRecordsAsync();
            model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();
            var success = await _addHobbyServices.AddHobbyAsync(hobbyDTO);
            if (success)
            {
                return RedirectToAction("Home", model);
            }
            else
            {
                ModelState.AddModelError("", "Failed to add hobby. Please try again.");
                return View("Home", model);
            }

        }



    }
}
