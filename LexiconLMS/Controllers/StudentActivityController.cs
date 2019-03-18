//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using LexiconLMS.Data;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LexiconLMS.Controllers
//{
//    [Authorize(Roles = "Student")]
//    public class StudentActivityController : Controller
//    {


//            private readonly LexiconLMSContext _context;
//            private readonly IMapper _mapper;

//            public StudentActivityController(LexiconLMSContext context, IMapper mapper)
//            {
//                _context = context;
//                _mapper = mapper;

//            }

//       public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var activity = await _context.Activities
//                .Include(v => v.Module)
//                .Include(v => v.ActivityType)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (activity == null)
//            {
//                return NotFound();
//            }

//            return View(activity);
//        }
//    }
//}
