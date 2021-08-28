using Consultation.Data.Models;
using System;
using System.Collections.Generic;


namespace Consultation.Data.Services
{
    public interface IPracticeService
    {
        void Initialise();

        // ---------------- Practice Management --------------
        IList<Practice> GetPractice();
        Practice CreatePractice(string name, string Address, string email, string password, string mobile);
        Practice GetPracticeById(int practiceId);
        bool IsDuplicatePracticeEmail(string email, int practiceId);
        Practice GetPracticeByEmail(string email);
        Practice UpdatePractice(Practice updated);
        bool PracticeDelete(int id);
        Practice GetPracticeByUserId(int userId);
        Practice AddPractice(string name, string address, string email, string password, string mobile);

        //-------------------Patients Management--------------------
        IList<Patient> GetPatients();
        Patient GetPatientById(int patientId);
        Patient CreatePatient(string name, string address, string email, string password, string mobile, DateTime dob);
        bool IsDuplicatePatientEmail(string email, int patientId);
        Patient GetPatientByEmail(string email);
        Patient UpdatePatient(Patient updated);
        bool PatientDelete(int id);
        Patient GetPatientByUserId(int userId);

        // -----------Ailment Management--------------------------------------------------

        IList<Ailment> GetOpenAilments();
        Ailment AddAilment(int patientId, string issue);
        Ailment AddAilmentSymptoms(int ailmentId, List<AilmentSymptom> symptoms);
        IList<Ailment> GetAllAilments();
        Ailment GetAilment(int id);
        bool DeleteAilment(int id);
        Ailment CloseAilment(int id, string resolution);
        IList<Ailment> SearchAilments(AilmentRange range, string query);
        IList<Ailment> SearchAilmentsLongWinded(string range, string query);

        // ------------------ Symptom Management------------------- --------
        IList<Symptom> GetSymptoms();
        Symptom GetSymptom(int id);
        Symptom GetSymptomByName(string name);
        bool IsDuplicateName(string name);
        Symptom Addsymptom(string name);
        bool DeleteSymptom(int id);
        Symptom UpdateSymptom(Symptom updated);


        //----------Condition Management------------------------------------

        Condition AddConditionSymptoms(int conditionId, IList<ConditionSymptom> symptoms);
        Condition AddCondition(string name, string description);
        Condition GetCondition(int conditionId);
        IList<Condition> GetConditions();
        IList<Condition> DiagnoseConditions(Ailment ailment);

        //----------Doactor Management-----------------------
        IList<Doctor> GetDoctors();
        Doctor DoctorCreate(string name, Speciality Speciality, string email, string password, string mobile);
        Doctor GetDoctorById(int doctorId);
        bool IsDuplicateDrEmail(string email, int doctorId);
        Doctor GetDoctorByEmail(string email);
        Doctor UpdateDoctor(Doctor updated);
        bool DoctorDelete(int id);
        Doctor GetDoctorByUserId(int userId);
        Doctor AddDoctor(string name, Speciality Speciality, string email, string password, string mobile);

        //----------Staff Management-----------------------
        IList<Staff> GetStaffs();
        Staff StaffCreate(string name, string position, string email, string password, string mobile);
        Staff GetStaffById(int staffId);
        bool IsDuplicateStaffEmail(string email, int staffId);
        Staff GetStaffByEmail(string email);
        Staff UpdateStaff(Staff updated);
        bool StaffDelete(int id);
        Staff GetStaffByUserId(int userId);
        Staff AddStaff(string name, string position, string email, string password, string mobile);

        //----------Diagnosis Management-----------------------
        public IList<Diagnosis> GetDiagnoses(IList<ConditionSymptom> conditionSymptoms);
        Diagnosis GetDiagnosisById(int DiagnosisId);
    }
}
