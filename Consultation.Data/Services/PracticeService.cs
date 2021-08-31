using System;
using System.Linq;
using System.Collections.Generic;

using Consultation.Data.Models;
using Consultation.Data.Security;
using Consultation.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Consultation.Data.Services
{
    public class PracticeService : IPracticeService, IUserService
    {
        private readonly DatabaseContext ctx;

        public PracticeService()
        {
            ctx = new DatabaseContext();
        }

        public void Initialise()
        {
            ctx.Initialise();
        }

        // ------------------ Symptom Related Operations ------------------------

        public IList<Symptom> GetSymptoms()
        {
            return ctx.Symptoms.ToList();
        }
    
        public Symptom GetSymptom(int id)
        {
            return ctx.Symptoms.FirstOrDefault(sym => sym.Id == id);
        }

        public Symptom GetSymptomByName(string name)
        {
            return ctx.Symptoms.FirstOrDefault(sym => sym.Name == name);
        }

        public bool IsDuplicateName(string name)
        {

            var existing = GetSymptomByName(name);
            // if a symptom with name exists, then they cannot use the name
            return existing != null;
        }

        public object GetCondition(object conditionId)
        {
            throw new NotImplementedException();
        }

        public Symptom Addsymptom(string name)
        {
            var existing = GetSymptomByName(name);
            if (existing != null)
            {
                return null;
            }
            var sym = new Symptom
            {
                Name = name,
            };
            ctx.Symptoms.Add(sym);
            ctx.SaveChanges();
            return sym; // return newly added symptom
        }

        public bool DeleteSymptom(int id)
        {
            var sym = GetSymptom(id);
            if (sym == null)
            {
                return false;
            }
            ctx.Symptoms.Remove(sym);
            ctx.SaveChanges(); // write to database
            return true;
        }

        public Symptom UpdateSymptom(Symptom updated)
        {
            // verify the Symptom exists
            var Symptom = GetSymptom(updated.Id);
            if (Symptom == null)
            {
                return null;
            }
            // update symptom details and save
            Symptom.Name = updated.Name;

            ctx.SaveChanges();
            return Symptom;
        }

        //-----------------Practice Related---------------------------

        // get all practice
        public IList<Practice> GetPractice()
        {
            return ctx.Practice.Include(p => p.User).ToList();
        }


        public Practice CreatePractice(string name, string address, string email, string password, string mobile)
        {
            // check if email is already in use by another practice
            var existing = GetPracticeByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create staff
            }
            // proceed to create new practicestaff, email is unique.
            var practice = new Practice()
            {
                //  Id is set automatically by the database
                Address = address,
                Mobile = mobile,

                // add staff user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Practice }
            };
            ctx.Practice.Add(practice);
            ctx.SaveChanges(); // write to database
            return practice; // return newly added practice staff
        }

        // get the staff with the specified staff id
        public Practice GetPracticeById(int practiceId)
        {
            return ctx.Practice
                     .Include(sta => sta.User)
                     .FirstOrDefault(sta => sta.Id == practiceId);
        }
        public bool IsDuplicatePracticeEmail(string email, int practiceId)
        {
            var existing = GetPracticeByEmail(email);
            // if a practice with email exists and the Id does not match the practiceId, email cannot be used
            return existing != null && practiceId != existing.Id;
        }

        public Practice GetPracticeByEmail(string email)
        {
            return ctx.Practice.FirstOrDefault(pr => pr.User.Email == email);
        }

        public Practice UpdatePractice(Practice updated)
        {
            // verify that the staff exists
            var Practice = GetPracticeById(updated.Id);
            if (Practice == null)
            {
                return null;
            }
            // update the details of the staff retrieved and save
            Practice.Address = updated.Address;
            Practice.Mobile = updated.Mobile;

            Practice.User.Name = updated.User.Name;
            Practice.User.Email = updated.User.Email;
            Practice.User.Password = Hasher.CalculateHash(updated.User.Password);

            ctx.SaveChanges(); // write to database
            return Practice;
        }

        // delete the specified staff and related user
        public bool PracticeDelete(int id)
        {
            var pr = GetPracticeById(id);
            if (pr == null)
            {
                return false;
            }
            ctx.Users.Remove(pr.User);
            ctx.Practice.Remove(pr);
            ctx.SaveChanges(); // write to database
            return true;
        }

        // Get the staff with the specified user id
        public Practice GetPracticeByUserId(int userId)
        {
            return ctx.Practice
                      .Include(pr => pr.User)
                      .FirstOrDefault(pr => pr.User.Id == userId);
        }

        public Practice AddPractice(string name, string address, string email, string password, string mobile)
        {
            // check if email is already in use by another staff
            var existing = GetPracticeByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create staff
            }
            // proceed to create new staff, email is unique.
            var practice = new Practice()
            {
                //  Id is set automatically by the database
                Address = address,
                Mobile = mobile,

                // add staff user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Practice }
            };
            ctx.Practice.Add(practice);
            ctx.SaveChanges(); // write to database
            return practice; // return newly added staff
        }

        //-------------------Patients Related--------------------------------------

        // get all patients
        public IList<Patient> GetPatients()
        {
            return ctx.Patients.Include(p => p.User).ToList();
        }

        // get the patient with the specified patient id
        public Patient GetPatientById(int patientId)
        {
            return ctx.Patients
                     .Include(pat => pat.Ailments)
                     .Include(pat => pat.User)
                     .FirstOrDefault(pat => pat.Id == patientId);
        }

        // add a new patient and associated user account 
        public Patient CreatePatient(string name, string address, string email, string password, string mobile, DateTime dob)
        {
            // check if email is already in use by another patient
            var existing = GetPatientByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create patient
            }
            // proceed to create new patient, email is unique.
            var pat = new Patient()
            {
                //  Id is set automatically by the database
                Address = address,
                Mobile = mobile,
                Dob = dob,

                // add patient user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Patient }
            };
            ctx.Patients.Add(pat);
            ctx.SaveChanges(); // write to database
            return pat; // return newly added patient
        }
        public bool IsDuplicatePatientEmail(string email, int patientId)
        {
            var existing = GetPatientByEmail(email);
            // if a patient with email exists and the Id does not match the patientId, email cannot be used
            return existing != null && patientId != existing.Id;
        }

        public Patient GetPatientByEmail(string email)
        {
            return ctx.Patients.FirstOrDefault(pat => pat.User.Email == email);
        }

        //update patient
        public Patient UpdatePatient(Patient updated)
        {
            // verify that the patient exists
            var patient = GetPatientById(updated.Id);
            if (patient == null)
            {
                return null;
            }
            // update the details of the patient retrieved and save - can limit what the patient can update
            patient.Address = updated.Address;
            patient.Mobile = updated.Mobile;
            patient.Dob = updated.Dob;

            patient.User.Name = updated.User.Name;
            patient.User.Email = updated.User.Email;
            if (!Hasher.IsHashed(updated.User.Password))
            {
                patient.User.Password = Hasher.CalculateHash(updated.User.Password);
            }
            ctx.SaveChanges(); // write to database
            return patient;
        }


        // delete the specified patient and related user
        public bool PatientDelete(int id)
        {
            var pat = GetPatientById(id);
            if (pat == null)
            {
                return false;
            }
            ctx.Users.Remove(pat.User);
            ctx.Patients.Remove(pat);
            ctx.SaveChanges(); // write to database
            return true;
        }

        // Get the patient with the specified user id
        public Patient GetPatientByUserId(int userId)
        {
            return ctx.Patients
                     .Include(pat => pat.Ailments)
                     .Include(pat => pat.User)
                     .FirstOrDefault(pat => pat.User.Id == userId);
        }



        //-------------------Ailment Related-----------------

        public IList<Ailment> GetOpenAilments()
        {
            return ctx.Ailments
                      .Include(a => a.Patient)
                      .Where(a => a.Active)
                      .ToList();
        }

        public Ailment AddAilment(int patientId, string issue)
        {
            var patient = GetPatientById(patientId);
            if (patient == null)
            {
                return null;
            }
            // create ailment and add to patient
            var ailment = new Ailment { PatientId = patientId, Issue = issue };
            patient.Ailments.Add(ailment);
            ctx.SaveChanges();
            return ailment;
        }

        public Ailment AddAilmentSymptoms(int ailmentId, List<AilmentSymptom> symptoms)
        {
            var ailment = ctx.Ailments.FirstOrDefault(a => a.Id == ailmentId);
            if (ailment == null)
            {
                return null;
            }
            ailment.Symptoms = symptoms;
            ctx.SaveChanges();
            return ailment;
        }


        public IList<Ailment> GetAllAilments()
        {
            var ailments = ctx.Ailments
                             .Include(a => a.Patient)
                             .ToList();
            return ailments;
        }

        public Ailment GetAilment(int id)
        {
            return ctx.Ailments
                      .Include(a => a.Patient)   
                      .ThenInclude(p => p.User)                   
                      .Include(sa => sa.Symptoms)
                      .ThenInclude(s => s.Symptom)                     
                      .FirstOrDefault(a => a.Id == id);
        }

        public bool DeleteAilment(int id)
        {
            // find ailment
            var ailment = GetAilment(id);
            if (ailment == null) return false;

            // remove sickness 
            var result = ailment.Patient.Ailments.Remove(ailment);

            ctx.SaveChanges();
            return result;
        }

        public Ailment CloseAilment(int id, string resolution)
        {
            var ailment = GetAilment(id);
            // if ailment does not exist or is already closed return null
            if (ailment == null || ailment.Active == false) return null;

            // ailment exists and is active so close
            ailment.Active = false;
            //ailment.Resolution = resolution;
            ailment.ResolvedOn = DateTime.Now;

            ctx.SaveChanges(); // write to database
            return ailment;  // return closed ailment
        }

        public IList<Ailment> SearchAilments(AilmentRange range, string query)
        {
            // ensure query is not null    
            query ??= "";

            // search patient name
            var r1 = ctx.Ailments
                       .Include(a => a.Patient)
                       .Where(a => a.Patient.User.Name.ToLower().Contains(query.ToLower()));
            // search ailment issue
            var r2 = ctx.Ailments
                       .Include(a => a.Patient)
                       .Where(a => a.Issue.ToLower().Contains(query.ToLower()));

            // Use Union to join both queries and Where to filter by ailment status 
            // Calling ToList() actually executes the final combined query
            return r1.Union(r2).Where(t =>
                   range == AilmentRange.OPEN && t.Active ||
                   range == AilmentRange.CLOSED && !t.Active ||
                   range == AilmentRange.ALL
            ).ToList();
        }

        public IList<Ailment> SearchAilmentsLongWinded(string range, string query)
        {
            // convert query to lowercase
            query = query == null ? "" : query.ToLower();
            range = range == null ? "ALL" : range.ToUpper();

            // search based on query and range
            if (range == "ALL")
            {
                var r1 = ctx.Ailments
                       .Include(a => a.Patient)
                       .Where(a => a.Patient.User.Name.ToLower().Contains(query));
                var r2 = ctx.Ailments
                       .Include(a => a.Patient)
                       .Where(a => a.Issue.ToLower().Contains(query));

                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }
            else if (range == "CLOSED")
            {
                var r1 = ctx.Ailments
                            .Include(a => a.Patient)
                            .Where(a => !a.Active && a.Patient.User.Name.ToLower().Contains(query));
                var r2 = ctx.Ailments
                            .Include(a => a.Patient)
                            .Where(a => !a.Active && a.Issue.ToLower().Contains(query));

                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }
            else
            {
                var r1 = ctx.Ailments
                            .Include(a => a.Patient)
                            .Where(a => a.Active && a.Patient.User.Name.ToLower().Contains(query));
                var r2 = ctx.Ailments
                            .Include(a => a.Patient)
                            .Where(a => a.Active && a.Issue.ToLower().Contains(query));

                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }
        }

        //----------Condition Related--------------------------------------------
      
        public Condition AddConditionSymptoms(int conditionId, IList<ConditionSymptom> symptoms)
        {
            var condition = ctx.Conditions.FirstOrDefault(c => c.Id == conditionId);
            if (condition == null)
            {
                return null;
            }
            condition.ConditionSymptoms = symptoms;
            ctx.SaveChanges();
            return condition;
        }

        // condition
        public Condition AddCondition(string name, string description)
        {
            var condition = new Condition
            {
                Name = name,
                Description = description
            };
            ctx.Conditions.Add(condition);
            ctx.SaveChanges();
            return condition;
        }

        public Condition GetCondition(int conditionId)
        {
            return ctx.Conditions.Include(c => c.ConditionSymptoms).FirstOrDefault(x => x.Id == conditionId);
        }
        public IList<Condition> GetConditions()
        {
            return ctx.Conditions.Include(c => c.ConditionSymptoms).ToList();
        }

        public IList<Condition> DiagnoseConditions(Ailment ailment)
        {
            // all conditions
            var conditions = GetConditions();

            // list of ailment symptom id's
            var ailmentSymptoms = ailment.Symptoms.Select(a => a.SymptomId).ToList();

            // uses containslist extension method found in Extensions/ListExtensions.cs
            // create a list of condition symptom id's
           
            var results = conditions
                          .Where(
                              c => {
                                  var cs =  c.ConditionSymptoms.Select(cs => cs.SymptomId).ToList();
                                  var r = cs.ContainsList(ailmentSymptoms);
                                  return r;
                                }
                          ) // then check if this contains the list of ailment id's
                          .ToList();

            return results;
        }


        // -----------Diagnosis Related---------------------------
        public Diagnosis AddDiagnosis(int patientId)
        {
            throw new NotImplementedException();
        }

        public Diagnosis GetDiagnosisById(int DiagnosisId)
        {
            return ctx.Diagnoses
                     .Include(patient => patient.Id)
                     .FirstOrDefault(diagnosis => diagnosis.Id == DiagnosisId);
        }

        public IList<Diagnosis> GetDiagnoses(IList<ConditionSymptom> conditionSymptoms)
        {
            return ctx.Diagnoses.Include(patient => patient.Id).ToList();
        }



        //-------------------------Doctor Related-----------------------------

        // get all doctors
        public IList<Doctor> GetDoctors()
        {
            return ctx.Doctors.Include(doc => doc.User).ToList();
        }

        public Doctor DoctorCreate(string name, Speciality Speciality, string email, string password, string mobile)
        {
            // check if email is already in use by another doctor
            var existing = GetDoctorByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create doctor
            }
            // proceed to create new doctor, email is unique.
            var doc = new Doctor()
            {
                //  Id is set automatically by the database
                Speciality = Speciality,
                Mobile = mobile,

                // add doctor user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Doctor }
            };
            ctx.Doctors.Add(doc);
            ctx.SaveChanges(); // write to database
            return doc; // return newly added doctor
        }

        // get the doctor with the specified doctor id
        public Doctor GetDoctorById(int doctorId)
        {
            return ctx.Doctors
                     .Include(doc => doc.User)
                     .FirstOrDefault(doc => doc.Id == doctorId);
        }
        public bool IsDuplicateDrEmail(string email, int doctorId)
        {
            var existing = GetDoctorByEmail(email);
            // if a doctor with email exists and the Id does not match the doctorId, email cannot be used
            return existing != null && doctorId != existing.Id;
        }

        public Doctor GetDoctorByEmail(string email)
        {
            return ctx.Doctors.FirstOrDefault(doc => doc.User.Email == email);
        }

        public Doctor UpdateDoctor(Doctor updated)
        {
            // verify that the doctor exists
            var doctor = GetDoctorById(updated.Id);
            if (doctor == null)
            {
                return null;
            }
            // update the details of the doctor retrieved and save
            doctor.Speciality = updated.Speciality;
            doctor.Mobile = updated.Mobile;

            doctor.User.Name = updated.User.Name;
            doctor.User.Email = updated.User.Email;
            // password update should only be carried out by the signed in user
            doctor.User.Password = Hasher.CalculateHash(updated.User.Password);

            ctx.SaveChanges(); // write to database
            return doctor;
        }

        // delete the specified doctor and related user
        public bool DoctorDelete(int id)
        {
            var doc = GetDoctorById(id);
            if (doc == null)
            {
                return false;
            }
            ctx.Users.Remove(doc.User);
            ctx.Doctors.Remove(doc);
            ctx.SaveChanges(); // write to database
            return true;
        }


        // Get the doctor with the specified user id
        public Doctor GetDoctorByUserId(int userId)
        {
            return ctx.Doctors
                      .Include(doc => doc.User)
                      .FirstOrDefault(doc => doc.User.Id == userId);
        }

        public Doctor AddDoctor(string name, Speciality Speciality, string email, string password, string mobile)
        {
            // check if email is already in use by another doctor
            var existing = GetDoctorByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create patient
            }
            // proceed to create new doctor, email is unique.
            var doc = new Doctor()
            {
                //  Id is set automatically by the database
                Speciality = Speciality,
                Mobile = mobile,

                // add patient user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Doctor }
            };
            ctx.Doctors.Add(doc);
            ctx.SaveChanges(); // write to database
            return doc; // return newly added patient
        }
        // delete the specified staff and related user


        //-------------------------Staff Related-------------------------

        // get all doctors
        public IList<Staff> GetStaffs()
        {
            return ctx.Staffs.Include(staff => staff.User).ToList();
        }

        public Staff StaffCreate(string name, string position, string email, string password, string mobile)
        {
            // check if email is already in use by another staff
            var existing = GetStaffByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create staff
            }
            // proceed to create new staff, email is unique.
            var staff = new Staff()
            {
                //  Id is set automatically by the database
                Position = position,
                Mobile = mobile,

                // add staff user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Doctor }
            };
            ctx.Staffs.Add(staff);
            ctx.SaveChanges(); // write to database
            return staff; // return newly added staff
        }

        // get the staff with the specified staff id
        public Staff GetStaffById(int staffId)
        {
            return ctx.Staffs
                     .Include(staff => staff.User)
                     .FirstOrDefault(staff => staff.Id == staffId);
        }
        public bool IsDuplicateStaffEmail(string email, int staffId)
        {
            var existing = GetStaffByEmail(email);
            // if a staff with email exists and the Id does not match the staffId, email cannot be used
            return existing != null && staffId != existing.Id;
        }

        public Staff GetStaffByEmail(string email)
        {
            return ctx.Staffs.FirstOrDefault(staff => staff.User.Email == email);
        }

        public Staff UpdateStaff(Staff updated)
        {
            // verify that the staff exists
            var staff = GetStaffById(updated.Id);
            if (staff == null)
            {
                return null;
            }
            // update the details of the staff retrieved and save
            staff.Position = updated.Position;
            staff.Mobile = updated.Mobile;

            staff.User.Name = updated.User.Name;
            staff.User.Email = updated.User.Email;
            staff.User.Password = Hasher.CalculateHash(updated.User.Password);

            ctx.SaveChanges(); // write to database
            return staff;
        }

        // delete the specified staff and related user
        public bool StaffDelete(int id)
        {
            var staff = GetStaffById(id);
            if (staff == null)
            {
                return false;
            }
            ctx.Users.Remove(staff.User);
            ctx.Staffs.Remove(staff);
            ctx.SaveChanges(); // write to database
            return true;
        }

        // Get the staff with the specified user id
        public Staff GetStaffByUserId(int userId)
        {
            return ctx.Staffs
                      .Include(staff => staff.User)
                      .FirstOrDefault(staff => staff.User.Id == userId);
        }

        public Staff AddStaff(string name, string position, string email, string password, string mobile)
        {
            // check if email is already in use by another staff
            var existing = GetStaffByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create staff
            }
            // proceed to create new staff, email is unique.
            var staff = new Staff()
            {
                //  Id is set automatically by the database
                Position = position,
                Mobile = mobile,

                // add staff user account
                User = new User { Name = name, Email = email, Password = Hasher.CalculateHash(password), Role = Role.Practice }
            };
            ctx.Staffs.Add(staff);
            ctx.SaveChanges(); // write to database
            return staff; // return newly added staff
        }

        //--------------- UserService Implementation-------------------------

        // retrieve list of Users
        public IList<User> GetUsers()
        {
            return ctx.Users.ToList();
        }

        // Retrive User by Id 
        public User GetUser(int id)
        {
            return ctx.Users.FirstOrDefault(s => s.Id == id);
        }

        // Add a new User checking a User with same email does not exist
        public User AddUser(string name, string email, string password, Role role)
        {
            var existing = GetUserByEmail(email);
            if (existing != null)
            {
                return null;
            }

            var user = new User
            {
                Name = name,
                Email = email,
                Password = Hasher.CalculateHash(password), // can hash if required 
                Role = role
            };
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user; // return newly added User
        }

        // Delete the User identified by Id returning true if deleted and false if not found
        public bool DeleteUser(int id)
        {
            var s = GetUser(id);
            if (s == null)
            {
                return false;
            }
            ctx.Users.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        // Update the User with the details in updated 
        public User UpdateUser(User updated)
        {
            // verify the User exists
            var User = GetUser(updated.Id);
            if (User == null)
            {
                return null;
            }
            // update the details of the User retrieved and save
            User.Name = updated.Name;
            User.Email = updated.Email;
            User.Password = Hasher.CalculateHash(updated.Password);
            User.Role = updated.Role;

            ctx.SaveChanges();
            return User;
        }

        public User GetUserByEmail(string email, int? id = null)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email && (id == null || u.Id != id));
        }

        public IList<User> GetUsersQuery(Func<User, bool> q)
        {
            return ctx.Users.Where(q).ToList();
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
            //return (user != null && user.Password == password ) ? user: null;
        }

    }
}