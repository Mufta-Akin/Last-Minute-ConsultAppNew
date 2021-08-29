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
    public class ConditionController : BaseController
    {
        private readonly PracticeService _svc;
        private object conditionId;

        public ConditionController(PracticeService svc)
        {
            _svc = svc;
        }

        public IActionResult Index()
        {
            // dashboard for Ailment   
            return View();
        }

        // GET: Condition
        public IActionResult ConditionIndex()
        {
            var ailments = _svc.GetAllAilments();
            return View(ailments);
        }

        // GET: Condition/Details/5
        public IActionResult ConditionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = _svc.GetCondition(conditionId);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        private bool ConditionExists(int id)
        {
            var condition = _svc.GetCondition(conditionId);            
            return ((bool)condition);
            Console.WriteLine("Your Condition is " + condition);
        }
    }
}
