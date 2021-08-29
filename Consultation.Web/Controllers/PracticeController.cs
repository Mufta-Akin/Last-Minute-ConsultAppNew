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

namespace Consultation.Web.Controllers
{
    //[Authorize(Roles = "Practice")]
    public class PracticeController : BaseController
    {
        private PracticeService _svc;

        public PracticeController(PracticeService svc)
        {
            _svc = svc;
        }
        //--------------------PRACTICE ----------------------------------

        // GET: /practice/pindex
        public IActionResult PIndex()
        {
            // display blank form to create a doctor
            var practice = _svc.GetPractice();
            return View(practice);
        }

        // GET: Current practice Dashboard
        public IActionResult PracticeIndex()
        {
            // dashboard for practice          
            return View();
        }

        // GET /Practice/edit
        public IActionResult PracticeEdit()
        {
            // obtain id from currently logged in user (staff)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the staff with specified id from the service
            var pr = _svc.GetPracticeByUserId(id);
            if (pr == null)
            {
                Alert($"No such Practice Staff {id}", AlertType.warning);
                return RedirectToAction(nameof(PracticeIndex));
            }

            // pass staff to view for editing
            return View(pr);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PracticeEdit(int id, [Bind("Id,Name,Address,Email,Mobile,Password,Mobile")] Practice pr)
        {
            // validate staff
            if (ModelState.IsValid)
            {
                // pass data to service to update

                _svc.UpdatePractice(pr);
                Alert("Practice Staff details saved", AlertType.info);

                return RedirectToAction(nameof(PracticeIndex));
            }

            // redisplay the form for editing as validation errors
            return View(pr);
        }

        public IActionResult PracticeDetails()
        {
            // obtain id from currently logged in user (practice)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the staff with specified id from the service
            var prac = _svc.GetPracticeByUserId(id);
            if (prac == null)
            {
                Alert("Practice Staff Not Found", AlertType.warning);
                return RedirectToAction(nameof(PracticeIndex));
            }
            return View(prac);
        }


        // GET: /practice/create
        public IActionResult PracticeCreate()
        {
            // display blank form to create a practice staff
            var pr = new PracticeViewModel();
            return View(pr);
        }

        // POST /practice/create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PracticeCreate([Bind("Name, Address, Email, Password, Mobile, Dob")] PracticeViewModel pr)
        {
            // check email is unique for this practice staff
            if (_svc.IsDuplicatePracticeEmail(pr.Email, pr.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(pr.Email), "The email address is already in use");
            }

            // validate staff
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = _svc.CreatePractice(pr.Name, pr.Address, pr.Email, pr.Password, pr.Mobile);
                Alert("Practice Staff created successfully", AlertType.info);

                return RedirectToAction(nameof(PracticeIndex));
            }

            // redisplay the form for editing as there are validation errors
            return View(pr);
        }

        // GET: Staffs/Delete/5
        [Authorize(Roles = "Practice")]
        public IActionResult PracticeDelete(int id)
        {
            // load the staff using the service
            var pr = _svc.GetPracticeById(id);
            // check the returned staff is not null and if so return NotFound()
            if (pr == null)
            {
                Alert("Practice Staff Not Found", AlertType.warning);
                return RedirectToAction(nameof(PracticeIndex));
            }

            // pass staff to view for deletion confirmation
            return View(pr);
        }

        [Authorize(Roles = "Practice")]
        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult PracticeDeleteConfirmed(int id)
        {
            // delete staff via service
            _svc.GetPracticeById(id);

            Alert($"Practice {id} deleted successfully", AlertType.success);
            // redirect to the index view
            return RedirectToAction(nameof(PracticeIndex));
        }

        //----------------Patient---------------------------------------------

        // GET: /practice/patientindex
        public IActionResult PatientIndex()
        {
            // display blank form to create a patient
            var patients = _svc.GetPatients();
            return View(patients);
        }

        // GET /practice/Patientdetails/{id}
        public IActionResult PatientDetails(int id)
        {
            // retrieve the patient with specified Userid from the service
            var pat = _svc.GetPatientById(id);

            // check if patient is null and return NotFound()
            if (pat == null)
            {
                Alert("Patient Not Found", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // pass patient as parameter to the view
            return View(pat);
        }



        // GET: /practice/patientcreate
        public IActionResult PatientCreate()
        {
            // display blank form to create a patient
            var p = new PatientViewModel();
            return View(p);
        }

        // POST /practice/patientcreate       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PatientCreate([Bind("Name, Address, Email, Password, Mobile, Dob")] PatientViewModel p)
        {
            // check email is unique for this patient
            if (_svc.IsDuplicatePatientEmail(p.Email, p.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(p.Email), "The email address is already in use");
            }

            // validate patient
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = _svc.CreatePatient(p.Name, p.Address, p.Email, p.Password, p.Mobile, p.Dob);
                Alert("Patient created successfully", AlertType.info);

                return RedirectToAction(nameof(PatientIndex));
            }

            // redisplay the form for editing as there are validation errors
            return View(p);
        }

        // GET: /patient/edit
        public IActionResult PatientEdit(int id)
        {
            // load the patient
            var p = _svc.GetPatientById(id);
            // check if p is null and return NotFound()
            if (p == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            return View(PatientViewModel.FromPatient(p));
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PatientEdit(int id, PatientViewModel pat)
        {
            // check email is unique for this patient
            if (_svc.IsDuplicatePatientEmail(pat.Email, pat.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(pat.Email), "The email address is already in use");
            }

            // validate patient
            if (ModelState.IsValid)
            {
                // pass data to service to update
                _svc.UpdatePatient(pat.ToPatient());
                Alert("Patient details saved", AlertType.info);

                return RedirectToAction(nameof(PatientIndex));
            }

            // redisplay the form for editing as validation errors
            return View(pat);
        }

        // GET: Patients/Delete/5
        public IActionResult PatientDelete(int id)
        {
            // load the patient using the service
            var pat = _svc.GetPatientById(id);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert("Patient Not Found", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // pass patient to view for deletion confirmation
            return View(pat);
        }

        // POST: Patients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PatientDeleteConfirmed(int id)
        {
            // delete patient via service
            if (_svc.PatientDelete(id))
            {

                Alert($"Patient {id} deleted successfully", AlertType.success);
            }
            else
            {
                Alert($"Patient {id} could not be deleted", AlertType.warning);
            }
            // redirect to the index view
            return RedirectToAction(nameof(PatientIndex));
        }

        //-----------Doctor Related -------------------------------------------------

        // GET: /practice/doctorindex
        public IActionResult DoctorIndex()
        {
            // display blank form to create a doctor
            var doctors = _svc.GetDoctors();
            return View(doctors);
        }

        // GET /practice/Doctordetails/{id}
        public IActionResult DoctorDetails(int id)
        {
            // retrieve the doctor with specified Userid from the service
            var doc = _svc.GetDoctorById(id);

            // check if doctor is null and return NotFound()
            if (doc == null)
            {
                Alert("Doctor Not Found", AlertType.warning);
                return RedirectToAction(nameof(DoctorIndex));
            }

            // pass doctor as parameter to the view
            return View(doc);
        }

        public IActionResult DoctorCreate()
        {
            // display blank form to create a doctor
            var doc = new DoctorViewModel();
            return View(doc);
        }

        // POST /practice/doctorcreate       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorCreate([Bind("Name, Speciality, Email, Password, Mobile, Dob")] DoctorViewModel doc)
        {
            // check email is unique for this doctor
            if (_svc.IsDuplicateDrEmail(doc.Email, doc.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(doc.Email), "The email address is already in use");
            }

            // validate doctor
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = _svc.DoctorCreate(doc.Name, Speciality.Pediatrics, doc.Email, doc.Password, doc.Mobile);
                Alert("Doctor created successfully", AlertType.info);

                return RedirectToAction(nameof(DoctorIndex));
            }

            // redisplay the form for editing as there are validation errors
            return View(doc);
        }

        // GET: /practice/doctoredit
        public IActionResult DoctorEdit(int id)
        {
            // load the doctor
            var doc = _svc.GetDoctorById(id);
            // check if doc is null and return NotFound()
            if (doc == null)
            {
                Alert($"No such doctor {id}", AlertType.warning);
                return RedirectToAction(nameof(DoctorIndex));
            }

            return View(DoctorViewModel.FromDoctor(doc));
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorEdit(int id, DoctorViewModel doc)
        {
            // check email is unique for this doctor
            if (_svc.IsDuplicateDrEmail(doc.Email, doc.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(doc.Email), "The email address is already in use");
            }

            // validate doctor
            if (ModelState.IsValid)
            {
                // pass data to service to update
                _svc.UpdateDoctor(doc.ToDoctor());
                Alert("Doctor details saved", AlertType.info);

                return RedirectToAction(nameof(DoctorIndex));
            }

            // redisplay the form for editing as validation errors
            return View(doc);
        }

        // GET: Doctors/Delete/5
        public IActionResult DoctorDelete(int id)
        {
            // load the doctor using the service
            var doc = _svc.GetDoctorById(id);
            // check the returned doctor is not null and if so return NotFound()
            if (doc == null)
            {
                Alert("Doctor Not Found", AlertType.warning);
                return RedirectToAction(nameof(DoctorIndex));
            }

            // pass doctor to view for deletion confirmation
            return View(doc);
        }

        // POST: Doctors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorDeleteConfirmed(int id)
        {
            // delete doctor via service
            if (_svc.DoctorDelete(id))
            {
                Alert($"Doctor {id} deleted successfully", AlertType.success);
            }
            else
            {
                Alert($"Doctor {id} could not be deleted", AlertType.warning);
            }
            // redirect to the index view
            return RedirectToAction(nameof(DoctorIndex));
        }


        //-----------Staff Related -------------------------------------------------

        // GET: /practice/staffindex
        public IActionResult StaffIndex()
        {
            // display blank form to create a staff
            var staffs = _svc.GetStaffs();
            return View(staffs);
        }


        // GET /staff/Staffdetails/{id}
        public IActionResult StaffDetails(int id)
        {
            // obtain id from currently logged in user (doctor)
            var staff = _svc.GetStaffById(id); // method in base controller           
            if (staff == null)
            {
                Alert("Staff Not Found", AlertType.warning);
                return RedirectToAction(nameof(StaffIndex));
            }
            return View(staff);
        }

        // GET: /practice/staffcreate
        public IActionResult StaffCreate()
        {
            // display blank form to create a staff
            var staff = new StaffViewModel();
            return View(staff);
        }

        // POST /practice/staffcreate       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StaffCreate([Bind("Name, Position, Email, Password, Mobile, Dob")] StaffViewModel staff)
        {
            // check email is unique for this staff
            if (_svc.IsDuplicateDrEmail(staff.Email, staff.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(staff.Email), "The email address is already in use");
            }

            // validate staff
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = _svc.StaffCreate(staff.Name, staff.Position, staff.Email, staff.Password, staff.Mobile);
                Alert("Staff created successfully", AlertType.info);

                return RedirectToAction(nameof(StaffIndex));
            }

            // redisplay the form for editing as there are validation errors
            return View(staff);
        }

        // GET: /staff/edit
        public IActionResult StaffEdit(int id)
        {
            // load the staff
            var staff = _svc.GetStaffById(id);
            // check if doc is null and return NotFound()
            if (staff == null)
            {
                Alert($"No such staff {id}", AlertType.warning);
                return RedirectToAction(nameof(StaffIndex));
            }
            return View(StaffViewModel.FromStaff(staff));
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StaffEdit(int id, [Bind("Id,Name, Position, Email, Password, Mobile")] StaffViewModel staff)
        {
            // check email is unique for this staff
            if (_svc.IsDuplicateDrEmail(staff.Email, staff.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(staff.Email), "The email address is already in use");
            }

            // validate staff
            if (ModelState.IsValid)
            {
                // pass data to service to update
                _svc.UpdateStaff(staff.ToStaff());
                Alert("Staff details saved", AlertType.info);

                return RedirectToAction(nameof(StaffIndex));
            }

            // redisplay the form for editing as validation errors
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public IActionResult StaffDelete(int id)
        {
            // load the doctor using the service
            var staff = _svc.GetStaffById(id);
            // check the returned staff is not null and if so return NotFound()
            if (staff == null)
            {
                Alert("Staff Not Found", AlertType.warning);
                return RedirectToAction(nameof(StaffIndex));
            }

            // pass staff to view for deletion confirmation
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StaffDeleteConfirmed(int id)
        {
            // delete staff via service
            if (_svc.StaffDelete(id))
            {
                Alert($"Staff {id} deleted successfully", AlertType.success);
            }
            else
            {
                Alert($"Staff {id} could not be deleted", AlertType.warning);
            }
            // redirect to the index view
            return RedirectToAction(nameof(StaffIndex));
        }

    }
}
