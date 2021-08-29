using Consultation.Data.Services;
using Consultation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Consultation.Web.Controllers
{
    //[Authorize(Roles = "Doctor, Practice")]
    public class DoctorController : BaseController
    {
        private readonly PracticeService _svc;

        public DoctorController(PracticeService svc)
        {
            _svc = svc;
        }

        // GET: /doctor/dindex
        public IActionResult DIndex()
        {
            // display blank form to create a doctor
            var doctors = _svc.GetDoctors();
            return View(doctors);
        }

        public IActionResult DoctorIndex()
        {
            // dashboard for patient   
            return View();
        }

        public IActionResult DoctorDetails()
        {
            // obtain id from currently logged in user (doctor)
            var id = GetSignedInUserId(); // method in base controller

            // retrieve the doctor with specified id from the service
            var doc = _svc.GetDoctorByUserId(id);
            if (doc == null)
            {
                Alert("Doctor Not Found", AlertType.warning);
                return RedirectToAction(nameof(DoctorIndex));
            }
            return View(doc);
        }

        // GET /doctor/edit
        public IActionResult DoctorEdit()
        {
            //obtain id from current logged in user (doctor)
            var id = GetSignedInUserId(); //method in base controller

            // retrieve the doctor with specified id from the service
            var doc = _svc.GetDoctorByUserId(id);
            if (doc == null)
            {
                Alert($"No such doctor {id}", AlertType.warning);
                return RedirectToAction(nameof(DoctorIndex));
            }

            // pass doctor to view for editing
            return View(DoctorViewModel.FromDoctor(doc));
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorEdit(int id, DoctorViewModel doc)
        {            
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

        //---------Patient Actions in Doctor Controller--------------------------

        // GET: /doctor/patientindex
        public IActionResult PatientIndex()
        {
            // display blank form to create a patient
            var patients = _svc.GetPatients();
            return View(patients);
        }

        // GET /doctor/Patientdetails/{id}
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

        // GET: /doctor/edit
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

        // GET /dactor/createailment
        public IActionResult CreateAilment(int id)
        {
            var pat = _svc.GetPatientById(id);
            // check the returned patient is not null and if so alert
            if (pat == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }
            // create the AilmentViewModel and populate the PatientId property
            var ailment = new AilmentViewModel
            {
                PatientId = id
            };

            return View("CreateAilment", ailment);
        }

        // POST /doctor/createailment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAilment([Bind("PatientId, Issue")] AilmentViewModel m)
        {
            var pat = _svc.GetPatientById(m.PatientId);
            // check the returned patient is not null and if so return NotFound()
            if (pat == null)
            {
                Alert($"No such patient {m.PatientId}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }

            // create the ailment view model and populate the PatientId property
            _svc.AddAilment(m.PatientId, m.Issue);
            Alert($"Ailment created successfully", AlertType.success);

            return RedirectToAction("PatientDetails", new { Id = m.PatientId });
        }

        //-----------Diagnosis Actions in Doctor Controller------------
      
        //// GET: Diagnoses
        //public IActionResult DiagnosisIndex()
        //{
        //    var diagnoses = _svc.GetDiagnoses();
        //    return View(diagnoses);
        //}

        // GET: Diagnoses/Details/5
        public IActionResult DiagnosisDetails(int DiagnosisId)
        {
            var diagnosis = _svc.GetDiagnosisById(DiagnosisId); ;
            if (diagnosis == null)
            {
                return NotFound();
            }
                        
            return View(diagnosis);
        }


        // GET: Diagnoses/Create
        public IActionResult Create(int id)
        {
            var pat = _svc.GetPatientById(id);
            // check the returned patient is not null and if so alert
            if (pat == null)
            {
                Alert($"No such patient {id}", AlertType.warning);
                return RedirectToAction(nameof(PatientIndex));
            }
            // create the AilmentViewModel and populate the PatientId property
            var diagnosis = new DiagnosisViewModel
            {
                PatientId = id
            };

            return View("CreateDiagnosis", diagnosis);
        }

        //// POST: Diagnoses/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("Id,DiagnosedOn,ConfirmedOn,DoctorId,PatientId,Description")] DiagnosisViewModel d)
        //{

        //    var pat = _svc.GetPatientById(d.PatientId);
        //    // check the returned patient is not null and if so return NotFound()
        //    if (pat == null)
        //    {
        //        Alert($"No such patient {d.PatientId}", AlertType.warning);
        //        return RedirectToAction(nameof(PatientIndex));
        //    }

        //    // create the ailment view model and populate the PatientId property
        //    _svc.AddDiagnosis(d.PatientId);
        //    Alert($"Ailment created successfully", AlertType.success);

        //    return RedirectToAction("PatientDetails", new { Id = d.PatientId });
        //}

    }
}