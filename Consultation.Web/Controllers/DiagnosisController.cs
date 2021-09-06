using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consultation.Data.Models;
using Consultation.Data.Repositories;
using Consultation.Data.Services;

namespace Consultation.Web.Controllers
{
    public class DiagnosisController : BaseController
    {
        private readonly PracticeService _svc;

        public DiagnosisController(PracticeService svc)
        {
            _svc = svc;
        }

        public IActionResult DiagnosisIndex()
        {
            // dashboard for Ailment   
            return View();
        }

    }
}
