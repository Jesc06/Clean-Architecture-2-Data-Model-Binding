# Clean-Architecture-2-Data-Model-Binding
Two Data model binding using clean architecture

<br>
<br>



### 1. Create Application Interfaces

Add data interface
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;

namespace testing.Application.Interfaces.Hobby
{
    public interface IHobby
    {
        Task<bool> AddHobby(HobbyDTO dto);
    }
}

```

<br>

Get all data interface
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;

namespace testing.Application.Interfaces.Hobby
{
    public interface IHobbyGetAllRecords
    {
        Task<List<HobbyDTO>> GetAllHobbyRecords();
    }
}


```


<br>
<br>



### 2. Create Application DTO

HobbyDTO
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing.Application.DTO
{
    public class HobbyDTO
    {
        public string hobbyname { get; set; }
        public string secondhobbyname { get; set; }
    }
}
```

Information DTO
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing.Application.DTO
{
    public class InformationDTO
    {
        public string name { get; set; }
        public string lastname { get; set; } 

    }
}

```

<br>
<br>

### 3. Create Infrastructure Repository

Add data repository
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Domain.Entities;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;


namespace testing.Infrastructure.Repository.Hobby
{
    public class AddHobbyRepository : IHobby
    {
        private readonly ApplicationDbContext _context; 
        public AddHobbyRepository(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task<bool> AddHobby(HobbyDTO dto)
        {
            HobbyDomain Domain = new HobbyDomain
            {
                HobbyName = dto.hobbyname,
                SecondHobbyName = dto.secondhobbyname
            };
            await _context.Hobby.AddAsync(Domain);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}


```
<br>

Get all data repository
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Infrastructure.Data;
using testing.Application.DTO;
using Microsoft.EntityFrameworkCore;
using testing.Application.Interfaces.Hobby;

namespace testing.Infrastructure.Repository.Hobby
{
    public class GellAllHobbyRepository : IHobbyGetAllRecords
    {
        private readonly ApplicationDbContext _context; 
        public GellAllHobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<HobbyDTO>> GetAllHobbyRecords()
        {
            return await _context.Hobby.Select(hobbyDomain => new HobbyDTO
            {
                hobbyname = hobbyDomain.HobbyName,
                secondhobbyname = hobbyDomain.SecondHobbyName
            }).ToListAsync();
        }


    }
}

```

<br>
<br>

### 4. Create Application Services

Add data services
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;

namespace testing.Application.Services.Hobby
{
    public class HobbyServices
    {
        private readonly IHobby _Ihobby;
        public HobbyServices(IHobby Ihobby)
        {
            _Ihobby = Ihobby;
        }

        public async Task<bool> AddHobbyAsync(HobbyDTO dto)
        {
            if(dto.hobbyname == null || dto.secondhobbyname == null)
            {
                return false;
                throw new NullReferenceException("Hobby names cannot be null");
            }
            else
            {
                await _Ihobby.AddHobby(dto);
                return true;
            }
        }


    }
}

```
<br>

Get all data services
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Application.DTO;
using testing.Application.Interfaces.Hobby;

namespace testing.Application.Services.Hobby
{
    public class GetAllHobbyServices
    {
        private readonly IHobbyGetAllRecords _IHobbyGetAllRecords;
        public GetAllHobbyServices(IHobbyGetAllRecords IHobbyGetAllRecords)
        {
            _IHobbyGetAllRecords = IHobbyGetAllRecords;
        }

        public async Task<List<HobbyDTO>> AllHobbyRecordsAsync()
        {
           return await _IHobbyGetAllRecords.GetAllHobbyRecords();
        }


    }
}

```



<br>
<br>

### 5. Register interfaces, services, and repositories in Program.cs

```csharp
builder.Services.AddScoped<IAddInfo, AddInfoRepository>();
builder.Services.AddScoped<AddInfoAsyncServices>();

builder.Services.AddScoped<IGetInfo, GetInfoRepository>();
builder.Services.AddScoped<GetInfoServices>();


builder.Services.AddScoped<IHobby, AddHobbyRepository>();
builder.Services.AddScoped<HobbyServices>();

builder.Services.AddScoped<IHobbyGetAllRecords, GellAllHobbyRepository>();
builder.Services.AddScoped<GetAllHobbyServices>();
```

<br>
<br>

### 6. Create ViewModels for adding and displaying data

#### Combined ViewModels
```csharp
using testing.Application.DTO;
using testing.ViewModels;

namespace testing.ViewModels
{
    public class GeneralAllModel
    {
        public InformationViewModel information { get; set; }
        public List<InformationDTO> infoAllRecords { get; set; }


        public HobbyViewModel hobby { get; set; }
        public List<HobbyDTO> HobbyAllRecords { get; set; }
    }
}

```

<br>

HobbyViewModel
```csharp
using System.ComponentModel.DataAnnotations;

namespace testing.ViewModels
{
    public class HobbyViewModel
    {
        [Required(ErrorMessage = "Hobby name is required")]
        public string HobbyName { get; set; }

        [Required(ErrorMessage = "Hobby second name is required")]
        public string SecondHobbyName { get; set; }
       
    }
}

```

InformationViewModel
```csharp
using System.ComponentModel.DataAnnotations;

namespace testing.ViewModels
{
    public class InformationViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string lastname { get; set; }
    }
}

```


<br>
<br>

### 7. Method of how to connect Combined ViewModel in Razor View

```cshtml
@model GeneralAllModel

<h1>Add Information</h1><br>

<form asp-controller="Home" asp-action="AddInfo" method="post">
    <div asp-validation-summary="All"></div>
    <input asp-for="information.name" /><br>
    <span asp-validation-for="information.name"></span><br>
    <input asp-for="information.lastname" /><br>
    <span asp-validation-for="information.lastname"></span><br>
    <button type="submit">Add</button>
</form>

<table>
    <thead>
        <tr>
            <th>Name</th>
            <th>Lastname</th>
        </tr>
    </thead>
    <tbody>
        @foreach(var records in Model.infoAllRecords)
        {
            <tr>
                <td>@records.name</td>
                <td>@records.lastname</td>
            </tr>
        }
    </tbody>
</table>



<h1>Hobby works</h1>

<form asp-controller="Home" asp-action="AddHobby" method="post">
    <div asp-validation-summary="All"></div>
    <input asp-for="hobby.HobbyName" /><br>
    <span asp-validation-for="hobby.HobbyName"></span><br>
    <input asp-for="hobby.SecondHobbyName" /><br>
    <span asp-validation-for="hobby.SecondHobbyName"></span><br>
    <button type="submit">Add hobby</button>
</form>

<table>
    <thead>
        <tr>
            <th>Hobby</th>
            <th>Second Hobby</th>
        </tr>
    </thead>
    <tbody>
        @foreach(var records in Model.HobbyAllRecords)
        {
            <tr>
                <td>@records.hobbyname</td>
                <td>@records.secondhobbyname</td>
            </tr>
        }
    </tbody>
</table>


```

<br>
<br>

### 8. Controller logic

```csharp
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

```

<br>
<br>

### Why I re-declare the list data in the POST method?

HTTP is stateless – every request is fresh.
After form submit ([HttpPost]), only form fields are sent.
The other data like lists (infoAllRecords, HobbyAllRecords) are not included, so we re-fetch them.

```csharp
[HttpPost]
public async Task<IActionResult> AddInfo(GeneralAllModel model)
{
    // Only this data is posted from the form
    InformationDTO dto = new InformationDTO
    {
        name = model.information.name,
        lastname = model.information.lastname
    };

    // Re-fetch these lists because HTTP doesn't keep them
    model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();
    model.HobbyAllRecords = await _getAllHobbyServices.AllHobbyRecordsAsync();

    var success = await _addAsyncServices.AddAsync(dto);
    if (success)
    {
        return RedirectToAction("Home");
    }
    else
    {
        ModelState.AddModelError("", "Failed to add information.");
        return View("Home", model); // Needs full model with lists
    }
}

```

#### Summary

Form only sends form fields. Lists are not included, so we re-declare them to avoid null errors and show complete data in the View again.

In a GET request (like when you open the page), we load all the data including lists. But in a POST request (like when submitting a form), only the form data is sent. The other data, like lists, are not included that's why we need to reload them manually.

#### Why we reload the list in POST even if it's already loaded in GET (Home)?

Because HTTP is stateless, meaning...

The data loaded in Home() (GET) does not carry over automatically to the AddInfo() (POST) request.

Each request (GET or POST) is separate. So kahit na-load mo na ang list sa Home(), once you do a POST (submit form), only the form fields are sent not the entire model.

##### Example:

Even if you did this in GET:

```csharp
public async Task<IActionResult> Home()
{
    model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();
    return View(model);
}

```

In POST, you still need to do:

```csharp
model.infoAllRecords = await _getInfoServices.GetAllRecordsFromInfo();

```


##### Because:

The model in POST is created fresh from the form input. The list (infoAllRecords) was not part of the form, so it's empty/null when you receive it.



