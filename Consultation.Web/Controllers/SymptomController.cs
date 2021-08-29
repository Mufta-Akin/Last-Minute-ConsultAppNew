using Consultation.Data.Models;
using Consultation.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Web.Controllers
{
    public class SymptomController : BaseController
    {
        private PracticeService _svc;

        public SymptomController(PracticeService svc)
        {
            _svc = svc;
        }

        // GET: Symptoms
        public IActionResult SymptomIndex()
        {
            var symptoms = _svc.GetSymptoms();

            return View(symptoms);
        }

        // GET: Symptoms/Details/5
        public IActionResult Details(int id)
        {
            // retrieve the symptom with specified id from the service
            var sym = _svc.GetSymptom(id);
            if (sym == null)
            {
                Alert("Symptom Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            return View(sym);
        }

        // GET: Symptoms/Create
        public IActionResult Create()
        {
            var sym = new Symptom();
            return View(sym);
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Symptom sym)
        {
            // check email is unique for this doctor
            if (_svc.IsDuplicateName(sym.Name))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(sym.Name), "The name already exists");
            }

            // validate doctor
            if (ModelState.IsValid)
            {
                // pass data to service to update
                var added = _svc.Addsymptom(sym.Name);
                //_svc.UpdateSymptom(sym);
                Alert("Symptom details saved", AlertType.info);

                return RedirectToAction(nameof(Index));
            }
            // redisplay the form for editing as validation errors
            return View(sym);
        }

        // GET: Symptoms/Edit/5
        public IActionResult Edit(int id)
        {
            // load the symptom using the service
            var symptom = _svc.GetSymptom(id);

            // check if symptom is null and return NotFound()
            if (symptom == null)
            {
                Alert($"No such symptom {id}", AlertType.warning);
                return RedirectToAction(nameof(SymptomIndex));
            }

            // pass symptom to view for editing
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name")] Symptom symptom)
        {
            // check email is unique for this doctor
            if (_svc.IsDuplicateName(symptom.Name))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(symptom.Name), "The name already exists");
            }

            // validate doctor
            if (ModelState.IsValid)
            {
                // pass data to service to update
                _svc.UpdateSymptom(symptom);
                Alert("Symptom details saved", AlertType.info);

                return RedirectToAction(nameof(SymptomIndex));
            }
            // redisplay the form for editing as validation errors
            return View(symptom);
        }

        // GET: Symptom/Delete
        public IActionResult Delete(int id)
        {
            // load the symptom using the service
            var symptom = _svc.GetSymptom(id);
            // check the returned symptom is not null and if so return NotFound()
            if (symptom == null)
            {
                Alert("Symptom Not Found", AlertType.warning);
                return RedirectToAction(nameof(SymptomIndex));
            }

            // pass symptom to view for deletion confirmation
            return View(symptom);
        }

        // POST: Symptoms/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // delete symptom via service
            _svc.DeleteSymptom(id);

            Alert($"Symptom {id} deleted successfully", AlertType.success);
            // redirect to the index view
            return RedirectToAction(nameof(SymptomIndex));
        }

    }
}
