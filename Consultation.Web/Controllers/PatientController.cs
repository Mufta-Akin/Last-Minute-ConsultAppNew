using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Consultation.Data.Models;
using Consultation.Data.Services;
using Consultation.Web.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Consultation.Web.Controllers
{
    //[Authorize(Roles = "Patient")]
    public class PatientController : BaseController
    {
        private PracticeService _svc;

        public PatientController(PracticeService svc)
        {
            _svc = svc;
        }

        // GET: Current patient Dashboard
        public IActionResult PatientIndex()
        {
            // dashboard for patient   
            return View();
        }

        // GET: Patients/Details
        [Authorize]
        public IActionResult PatientDetails()
        {
            // obtain id from currently logged in user (patient)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the patient with specified id from the service
            var pat = _svc.GetPatientByUserId(id);
            if (pat == null)
            {
                Alert("Patient Not Found", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }
            return View(pat);
        }

        // GET /patient/edit
        [Authorize]
        public IActionResult PatientEdit()
        {
            // obtain id from currently logged in user (patient)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the patient with specified id from the service
            var pat = _svc.GetPatientByUserId(id);
            if (pat == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // pass patient to view for editing
            return View(pat);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Practice, Doctor, Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PatientEdit(int id, [Bind("Id,Name,Address,Email,Mobile,Age,Password")] Patient pat)
        {
            // validate patient
            if (ModelState.IsValid)
            {
                // pass data to service to update

                _svc.UpdatePatient(pat);
                Alert("Patient details saved", AlertType.info);

                return RedirectToAction(nameof(PatientIndex));
            }

            // redisplay the form for editing as validation errors
            return View(pat);
        }


        // GET /patient/createailment
        public IActionResult CreateAilment(int id)
        {
            var pat = _svc.GetPatientById(id);
            // check the returned patient is not null and if so alert
            if (pat == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            // create the AilmentViewModel and populate the PatientId property
            var ailment = new AilmentViewModel
            {
                PatientId = id,
                Symptoms = new MultiSelectList(_svc.GetSymptoms(), "Id", "Name")
            };

            return View("CreateAilment", ailment);
        }

        // POST /patient/createailment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAilment(AilmentViewModel m)
        {
            var pat = _svc.GetPatientById(m.PatientId);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert($"No such patient {m.PatientId}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // create the ailment view model and populate the PatientId property
            var ailment = _svc.AddAilment(m.PatientId, m.Issue);
            var ailmentSymptoms = m.SelectedSymptomIds.Select(i => new AilmentSymptom { AilmentId = ailment.Id, SymptomId = i }).ToList();
            _svc.AddAilmentSymptoms(ailment.Id, ailmentSymptoms);

            Alert($"Ailment created successfully", AlertType.success);

            return RedirectToAction("PatientDetails", new { Id = m.PatientId });
        }


        // GET: Patients/condition details
        [Authorize]
        public IActionResult PatientConditionDetails(ConditionViewModel condition)
        {
            // obtain id from currently logged in user (patient)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the patient with specified id from the service
            var pat = _svc.GetPatientByUserId(id);
            if (pat == null)
            {
                Alert("Patient Not Found", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }
            var conditionDetails = _svc.GetDiagnoses(condition.ConditionSymptoms);

            return View(conditionDetails);
        }


        // GET /patient/createailment
        public IActionResult AilmentEdit(int id)
        {
            var pat = _svc.GetPatientById(id);
            // check the returned patient is not null and if so alert
            if (pat == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            // create the AilmentViewModel and populate the PatientId property
            var ailment = new AilmentViewModel
            {
                PatientId = id,
                Symptoms = new MultiSelectList(_svc.GetSymptoms(), "Id", "Name")
            };

            return View("CreateAilment", ailment);
        }

        // POST /patient/createailment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AilmentEdit(AilmentViewModel m)
        {
            var pat = _svc.GetPatientById(m.PatientId);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert($"No such patient {m.PatientId}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // create the ailment view model and populate the PatientId property
            var ailment = _svc.AddAilment(m.PatientId, m.Issue);
            var ailmentSymptoms = m.SelectedSymptomIds.Select(i => new AilmentSymptom { AilmentId = ailment.Id, SymptomId = i }).ToList();
            _svc.AddAilmentSymptoms(ailment.Id, ailmentSymptoms);

            Alert($"Ailment edited successfully", AlertType.success);

            return RedirectToAction("PatientDetails", new { Id = m.PatientId });
        }

    }
}
