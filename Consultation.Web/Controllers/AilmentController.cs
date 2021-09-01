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
using Consultation.Web.ViewModels;

namespace Consultation.Web.Controllers
{
    public class AilmentController : BaseController
    {

        private readonly PracticeService _svc;

        public AilmentController(PracticeService svc)
        {
            _svc = svc;
        }

 
        // GET: /ailment/AilmentIndex
        public IActionResult Index()
        {
            // display blank form to create a doctor
            var ailments = _svc.GetAllAilments();
            return View(ailments);
        }

        public IActionResult Details(int id)
        {
            // retrieve the symptom with specified id from the service
            var ailment = _svc.GetAilment(id);
            if (ailment == null)
            {
                Alert("Ailment Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
           
            var vm = new AilmentConditionViewModel {
                Id = ailment.Id,
                Issue = ailment.Issue,
                Resolution = ailment.Resolution,
                CreatedOn = ailment.CreatedOn,
                ResolvedOn= ailment.ResolvedOn,
                Active = ailment.Active,
                PatientId = ailment.PatientId,
                PatientName = ailment.Patient.User.Name,
                Symptoms = ailment.Symptoms,
                PossibleConditions = _svc.DiagnoseConditions(ailment)
            };

            return View(vm);
        }
     
         // GET /patient/createailment
        public IActionResult Create(int id)
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

            return View("Create", ailment);
        }
       

        // POST /patient/createailment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AilmentViewModel m)
        {
            var pat = _svc.GetPatientById(m.PatientId);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert($"No such patient {m.PatientId}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // create the ailment view model and populate the PatientId property
            var ailment = _svc.AddAilment(m.PatientId, m.Issue);
            if (ailment == null)
            {
                Alert($"You must add an ailment", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            var ailmentSymptoms = m.SelectedSymptomIds.Select(i => new AilmentSymptom { AilmentId = ailment.Id, SymptomId = i }).ToList();
            if (ailmentSymptoms == null)
            {
                Alert($"Please select ailment symptoms", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            _svc.AddAilmentSymptoms(ailment.Id, ailmentSymptoms);

            Alert($"Ailment created successfully", AlertType.success);

            return RedirectToAction("Details", "Patient", new { Id = m.PatientId });
        }

        

        public IActionResult Edit(int id)
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

            return View("Edit", ailment);
        }

        // POST /patient/createailment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AilmentViewModel m)
        {
            var pat = _svc.GetPatientById(m.PatientId);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert($"No such patient {m.PatientId}", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // create the ailment view model and populate the PatientId property
            var ailment = _svc.AddAilment(m.PatientId, m.Issue);
            var ailmentSymptoms = m.SelectedSymptomIds.Select(i => new AilmentSymptom { AilmentId = ailment.Id, SymptomId = i }).ToList();
            _svc.AddAilmentSymptoms(ailment.Id, ailmentSymptoms);

            Alert($"Ailment edited successfully", AlertType.success);

            return RedirectToAction("Details", "Patient", new { Id = m.PatientId });
        }

        // GET: Ailment/Delete
        public IActionResult Delete(int id)
        {
            // load the ailment using the service
            var ailment = _svc.GetAilment(id);
            // check the returned symptom is not null and if so return NotFound()
            if (ailment == null)
            {
                Alert("Symptom Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // pass symptom to view for deletion confirmation
            return View(ailment);
        }

        // POST: Symptoms/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // delete symptom via service
            _svc.DeleteAilment(id);

            Alert($"Symptom {id} deleted successfully", AlertType.success);
            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }

    }
}
