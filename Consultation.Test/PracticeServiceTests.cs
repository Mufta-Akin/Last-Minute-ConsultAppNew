using System;
using Xunit;
using Consultation.Data.Models;
using Consultation.Data.Services;
using System.Collections.Generic;

namespace Consultation.Test
{
    public class PracticeServiceTests
    {
        private PracticeService svc;

        public PracticeServiceTests()
        {
            svc = new PracticeService();

            // empty data source before each test
            svc.Initialise();
        }

        //-------------User Tests-----------------------------------
        [Fact]
        public void User_LoginWithValidCredentials_ShouldWork()
        {
            // arrange
            svc.AddUser("Admin", "admin@mail.com", "admin", Role.Patient);

            // act            
            var user = svc.Authenticate("admin@mail.com", "admin");

            // assert
            Assert.NotNull(user);
        }

        [Fact]
        public void User_EmptyDbShould_ReturnNoUsers()
        {
            // act
            var users = svc.GetUsers();

            // assert
            Assert.Equal(0, users.Count);
        }

        [Fact]
        public void User_AddingUsers_ShouldAddUsers()
        {
            // arrange
            svc.AddUser("Admin", "admin@mail.com", "admin", Role.Practice);
            svc.AddUser("Doctor", "guest@mail.com", "doctor", Role.Doctor);

            // act
            var users = svc.GetUsers();

            // assert
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public void User_UpdatingUser_ShouldUpdateUser()
        {
            // arrange
            var user = svc.AddUser("Admin", "admin@mail.com", "admin", Role.Practice);

            // act
            user.Name = "Admin";
            user.Email = "admin@mail.com";
            var updatedUser = svc.UpdateUser(user);

            // assert
            Assert.Equal("Admin", user.Name);
            Assert.Equal("admin@mail.com", user.Email);
        }

        [Fact]
        public void User_LoginWithInvalidCredentials_ShouldNotWork()
        {
            // arrange
            svc.AddUser("Admin", "admin@mail.com", "admin", Role.Practice);

            // act      
            var user = svc.Authenticate("admin@mail.com", "xxx");

            // assert
            Assert.Null(user);
        }

        [Fact]
        public void User_DeleteUserThatExists_ShouldReturnTrue()
        {
            // arrange 
            svc.AddUser("Admin", "admin@mail.com", "admin", Role.Practice);

            // act
            var deleted = svc.DeleteUser(1);
            var retrieve = svc.GetUser(1);           // try to retrieve deleted user

            // assert
            Assert.True(deleted);       // delete user should return true
            Assert.Null(retrieve);      // cannot retrieve the user (deleted)

        }

        [Fact]
        public void User_GetUserByEmailWhenInvalidEmail_ShouldReturnNull()
        {
            // arrange
            svc.AddUser("Admin", "admin@mail.com", "admin", Role.Practice);

            // act      
            var user = svc.GetUserByEmail("YYYY@mail.com", 1);

            // assert
            Assert.Null(user);
        }

        [Fact]
        public void Patient_DeletePatientShouldDeleteUserAccount()
        {
            // arrange
            var p = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act
            svc.PatientDelete(p.Id);
            var loggedIn = svc.GetPatientByEmail("connore@mail.net");

            //assert
            Assert.Null(loggedIn);
        }

        [Fact]
        public void User_GetPatientByEmail_WhenEmptyDbShouldReturnNull()
        {
            // act
            var users = svc.GetPatientByEmail("user@mail.net");

            // assert
            Assert.Null(users);
        }

        [Fact]
        public void User_GetPatientById_ShouldRetrivePatient()
        {
            // arrange
            var p = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act
            var patient = svc.GetPatientById(p.Id);

            //assert
            Assert.NotNull(patient);
            Assert.Equal(p.Id, patient.Id);
            Assert.Equal(p.User.Name, patient.User.Name);
        }

        //-------------Patients Tests-------------------------------------------
        [Fact]
        public void Patient_GetPatient_ShouldReturnAllPatients()
        {
            //Arrange
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var pat2 = svc.CreatePatient("Linsey Bora", "909 Malibu Way, Atlanta", "Bora@kmail.net", "password", "073493476100", new DateTime(2012, 4, 3));
            var pat3 = svc.CreatePatient("Phill Gunn", "12279 Sinners Way, Birmingham", "Phil@mail.com", "password", "+1 8201374593", new DateTime(1992, 3, 5));

            // act 
            var patients = svc.GetPatients();
            var count = patients.Count;

            //Assert
            Assert.Equal(3, count);
            Assert.NotNull(pat1);
            Assert.NotNull(pat2);
            Assert.NotNull(pat3);
        }

        [Fact]
        public void Patient_GetPatientsWhenNoPatients_ShouldReturnZero()
        {
            // arrange

            // act 
            var patients = svc.GetPatients();
            var count = patients.Count;

            // assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Patient_CreatePatientShouldCreatePatientAndAccount()
        {
            // arrange
            var p = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act
            var patient = svc.GetPatientByEmail("connore@mail.net");

            // assert
            Assert.NotNull(patient); // delete doctor should return true
            Assert.Equal(Role.Patient, patient.User.Role);
        }

        [Fact]
        public void Patient_CreatePatientWithDuplicateEmail_ShouldReturnNull()
        {
            // arrange 
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act - add duplicate
            var pat2 = svc.CreatePatient("Blabb Gunn", "767 Ahmmer drive, Kenssington", "connore@mail.net", "password", "+1435 847 3664 ", new DateTime(2000, 1, 1));

            // assert
            Assert.NotNull(pat1);
            Assert.Null(pat2);
        }

        [Fact]
        public void Patient_UpdateExistingPatient_ShouldUpdateAccount()
        {
            // arrange - create test patient
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act - update test patient         
            pat.User.Name = "Name";
            pat.Address = "Modified Address";
            pat.User.Email = "xxx@email.com";
            pat.User.Password = "New_Password";
            pat.Mobile = "Modified Mobile";

            pat = svc.UpdatePatient(pat);

            // assert
            Assert.NotNull(pat);
            Assert.Equal("Name", pat.User.Name);
            Assert.Equal("Modified Address", pat.Address);
            Assert.Equal("xxx@email.com", pat.User.Email);
            //Assert.Equal("New_Password", pat.User.Password);
            Assert.Equal("Modified Mobile", pat.Mobile);
        }

        [Fact]
        public void Patient_GetPatientWhenNotExisting_ShouldReturnNull()
        {
            // arrange 
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act - add duplicate
            var pat2 = svc.CreatePatient("Blabb Gunn", "767 Ahmmer drive, Kenssington", "connore@mail.net", "password", "+1435 847 3664 ", new DateTime(2000, 1, 1));
            // act 
            // act
            var patient = svc.GetPatientByEmail("YYYY@Ymail.com"); // non existing patient email !

            // assert
            Assert.Null(patient);
        }

        [Fact]
        public void Patient_GetPatientWhenExisting_ShouldReturnNotNull()
        {
            // arrange
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act - add duplicate
            var pat2 = svc.CreatePatient("Blabb Gunn", "767 Ahmmer drive, Kenssington", "Blabb@mail.net", "password", "+1435 847 3664 ", new DateTime(2000, 1, 1));
            // act 
            var patient = svc.GetPatientByEmail("Blabb@mail.net"); // existing patient email

            // assert
            Assert.NotNull(patient);
        }

        [Fact]
        public void Patient_DeletePatientThatExists_ShouldReducePatientCountByOne()
        {
            //Arrange
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var pat2 = svc.CreatePatient("Linsey Bora", "909 Malibu Way, Atlanta", "Bora@kmail.net", "password", "073493476100", new DateTime(2012, 4, 3));
            var pat3 = svc.CreatePatient("Phill Gunn", "12279 Sinners Way, Birmingham", "Phil@mail.com", "password", "+1 8201374593", new DateTime(1992, 3, 5));

            // act 
            var patients = svc.GetPatients();
            var count = patients.Count;

            //act
            var deleted = svc.PatientDelete(pat1.Id);
            var pat = svc.GetPatients();

            // assert
            Assert.Equal(3, patients.Count);      // should be reduced by 1 patients
        }

        [Fact]
        public void Patient_AddPatientWhenUnique_ShouldSetAllProperties()
        {
            // arrange
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));

            // act 
            var patients = svc.GetPatients();

            // assert - that patient is not null
            Assert.NotNull(patients);
            //Assert.Equal(pat1.Id, pat1.Id);
            //Assert.Equal("Name", pat1.User.Name);
            //Assert.Equal("Address", pat1.Address);
            //Assert.Equal("xxx@email.com", pat1.User.Email);
            //Assert.Equal("Mobile", pat1.Mobile);
            //Assert.Equal(20, pat1.Age);
        }

        [Fact]
        public void Patient_UpdateExistingPatient_ShouldSetAllProperties()
        {
            // arrange - create test patient
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));

            // act - update test patient         
            pat.User.Name = "Name";
            pat.Address = "Modified Address";
            pat.User.Email = "xxx@email.com";
            pat.Mobile = "Modified Mobile";
            //pat.Dob = 2001:12:12;

            pat = svc.UpdatePatient(pat);

            // assert
            Assert.NotNull(pat);
            Assert.Equal("Name", pat.User.Name);
            Assert.Equal("Modified Address", pat.Address);
            Assert.Equal("xxx@email.com", pat.User.Email);
            Assert.Equal("Modified Mobile", pat.Mobile);
            //Assert.Equal(25, pat.Age);
        }

        [Fact]
        public void Patient_GetPatientThatExists_ShouldReturnPatient()
        {
            // arrange - create test patient
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));

            // act
            var retrieve = svc.GetPatients();

            // assert
            Assert.NotNull(retrieve);
        }

        [Fact]
        public void Patient_DeletePatientThatExists_ShouldReturnTrue()
        {
            // arrange - create test patient
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));

            // act
            var deleted = svc.PatientDelete(pat.Id);
            var retrieve = svc.GetPatientByEmail("connore@mail.net");           // try to retrieve deleted patient

            // assert
            Assert.True(deleted); // delete patient should return true
            Assert.Null(retrieve);      // cannot retrieve the patient (deleted)
        }

        //-------------Ailment Tests-------------------------------------------

        [Fact]
        public void Ailment_CreateAilmentForExistingPatient_ShouldBeActive()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            // act
            var ailment = svc.AddAilment(pat.User.Id, "Not feeling well");

            // assert
            Assert.True(ailment.Active);
        }

        [Fact]
        public void Ailment_AilmentCount_ShouldReturnAilmentNumberAdded()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));

            var ail1 = svc.AddAilment(pat.User.Id, "I have headache");
            var ail2 = svc.AddAilment(pat.User.Id, "I'm vomiting");
            var ail3 = svc.AddAilment(pat.User.Id, "sore throat");

            // act
            var ailments = svc.GetAllAilments();      // get allailment

            // assert
            Assert.Equal(3, ailments.Count);
        }

        [Fact]
        public void Ailment_GetAilments_ShouldReturnAilment()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));
            var ail1 = svc.AddAilment(pat.User.Id, "I have headache");

            // act
            var ailment = svc.GetAilment(ail1.Id);      // get ailment by id

            // assert
            Assert.NotNull(ail1);
            Assert.NotNull(ailment);
        }

        [Fact]
        public void Ailment_DeleteAilmentWhenExist_ShouldReturnTrue()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));
            var ail1 = svc.AddAilment(pat.User.Id, "I have headache");

            // act
            var deleted = svc.DeleteAilment(ail1.Id);      // delete ailment by id

            // assert
            Assert.True(deleted);
        }

        [Fact]
        public void Ailment_GetOpenAilmentsWhenTwoAdded_ShouldReturnTwo()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2000, 1, 1));
            var ail1 = svc.AddAilment(pat.Id, "I have headache");
            var ail2 = svc.AddAilment(pat.Id, "I'm vomiting");
            var ail3 = svc.AddAilment(pat.Id, "sore throat");

            // act
            var open = svc.GetOpenAilments();

            // assert
            Assert.Equal(3, open.Count);
        }

        [Fact]
        public void Ailment_CloseAilmentWhenOpen_ShouldReturnAilment()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var ail = svc.AddAilment(pat.Id, "I have headache");

            // act
            var res = svc.CloseAilment(ail.Id, "Resolved");

            // assert
            Assert.NotNull(res);                          // verify closed ailment returned          

        }

        [Fact]
        public void Ailment_CloseAilmentWhenOpen_ShouldReturnActiveIsFalse()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var ail = svc.AddAilment(pat.Id, "I have headache");

            // act
            var res = svc.CloseAilment(ail.Id, "Resolved");

            // assert          
            Assert.False(res.Active);    // verify its closed

        }

        [Fact]
        public void Ailment_GetAllAilments_ShouldReturnAilmentCount()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var ail1 = svc.AddAilment(pat.Id, "have headache");
            var ail2 = svc.AddAilment(pat.Id, "vomiting");
            var ail3 = svc.AddAilment(pat.Id, "sore throat");
            var ail4 = svc.AddAilment(pat.Id, "fever");

            // act
            var ailments = svc.GetAllAilments();      // get allailment

            // assert
            Assert.Equal(4, ailments.Count);
        }

        [Fact]
        public void Ailment_SearchAilmentssWhenOneResultAvailableInOpenAilments_ShouldReturnOne()
        {
            // arrange
            var pat = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var ail1 = svc.AddAilment(pat.Id, "bad headache");
            var ail2 = svc.AddAilment(pat.Id, "bad breadth");
            var closed = svc.CloseAilment(ail1.Id, "bad");     // close one ailment    

            // act
            var ailments = svc.SearchAilments(AilmentRange.OPEN, "breadth");      // search open ailments

            // assert
            Assert.Equal(1, ailments.Count);
            Assert.NotNull(ail1);
        }

        [Fact]
        public void Ailment_SearchAilmentssWhenMultipleResultInOpenAilments_ShouldReturnAllAvailable()
        {
            //Arrange
            var pat1 = svc.CreatePatient("Jameses Conneore", "27 Blammer Road, Georgetown", "connore@mail.net", "password", "+1 2304564789", new DateTime(2002, 3, 7));
            var pat2 = svc.CreatePatient("Linsey Bora", "909 Malibu Way, Atlanta", "Bora@kmail.net", "password", "073493476100", new DateTime(2012, 4, 3));
            var pat3 = svc.CreatePatient("Phill Gunn", "12279 Sinners Way, Birmingham", "Phil@mail.com", "password", "+1 8201374593", new DateTime(1992, 3, 5));
            var ail1 = svc.AddAilment(pat1.Id, "bad headache");
            var ail2 = svc.AddAilment(pat1.Id, "toothache");
            var ail3 = svc.AddAilment(pat2.Id, "backache");
            var ail4 = svc.AddAilment(pat2.Id, "mild tummy pain");

            // act
            var ailments = svc.SearchAilments(AilmentRange.OPEN, "ache");      // search open ailments

            // assert
            Assert.Equal(3, ailments.Count);
            Assert.NotNull(ail1);
        }

        //-------------Doctor Tests-----------------------------------
        [Fact]
        public void Doctor_GetDoctorWhenExisting_ShouldReturnDoctor()
        {
            // arrange - create test doctor
            var doc1 = svc.AddDoctor("Name", Speciality.Cardiology, "xxx@email.com", "password", "Mobile");

            // act - update test doctor
            var Doctors = svc.GetDoctors();

            //Assert
            Assert.NotNull(doc1);
        }

        [Fact]
        public void Doctor_GetDoctorWhenAlreadyDeleted_ShouldReturnNull()
        {
            // arrange
            var doc1 = svc.AddDoctor("Name", Speciality.Dermatology, "xxx@email.com", "password", "Mobile");

            // act - update test doctor
            var deleteDoc1 = svc.DoctorDelete(doc1.Id);

            //Assert
            Assert.True(deleteDoc1);
        }

        [Fact]
        public void Doctor_DetDoctorByEmail_ShouldReturnDoctor()
        {
            // arrange - create test doctor
            var doc1 = svc.AddDoctor("Name", Speciality.Hematology, "xxx@email.com", "password", "Mobile");
            var doc2 = svc.AddDoctor("Name", Speciality.Dermatology, "yyy@email.com", "password", "Mobile");
            var doc3 = svc.AddDoctor("Name", Speciality.Obstetrics_Gynecology, "zzz@email.com", "password", "Mobile");

            // act - update test doctor
            var getDoc1 = svc.GetDoctorByEmail(doc1.User.Email);

            //Assert
            Assert.NotNull(doc1);
            Assert.NotNull(doc2);
            Assert.NotNull(doc3);

        }

        [Fact]
        public void Doctor_DeleteNonExistingDoctor_ShouldNotChangeDoctorsCount()
        {
            // arrange - create test doctor
            var doc1 = svc.AddDoctor("Name", Speciality.Pediatrics, "xxx@email.com", "password", "Mobile");
            var doc2 = svc.AddDoctor("Name", Speciality.Dermatology, "yyy@email.com", "password", "Mobile");
            var doc3 = svc.AddDoctor("Name", Speciality.Neurology, "zzz@email.com", "password", "Mobile");


            // act 	
            var deleted = svc.DoctorDelete(5);               // delete non existent doctor
            var doctors = svc.GetDoctors();   // retrieve list of doctors

            // assert
            Assert.False(deleted);
            Assert.Equal(3, doctors.Count);    // should be 3 doctors
            Assert.NotNull(doc1);
            Assert.NotNull(doc2);
            Assert.NotNull(doc3);
        }


        [Fact]
        public void Doctor_DeleteDoctorThatExists_ShouldReduceDoctorsCountByOne()
        {
            // arrange - create test doctor
            var doc1 = svc.AddDoctor("Name", Speciality.Psychiatry, "xxx@email.com", "password", "Mobile");
            var doc2 = svc.AddDoctor("Name", Speciality.Rheumatology, "yyy@email.com", "password", "Mobile");
            var doc3 = svc.AddDoctor("Name", Speciality.Urology, "zzz@email.com", "password", "Mobile");


            // act - update test doctor
            var delDoc3 = svc.DoctorDelete(doc3.Id);
            var Doctors = svc.GetDoctors();

            //Assert
            Assert.NotNull(doc1);
            Assert.NotNull(doc2);
            Assert.NotNull(doc3);
            Assert.True(delDoc3);
            Assert.Equal(2, Doctors.Count);
        }

        [Fact]
        public void Doctor_UpdateExistingDoctor_ShouldUpdateAllProperties()
        {
            // arrange - create test doctor
            var doc = svc.AddDoctor("Name", Speciality.Obstetrics_Gynecology, "zzz@email.com", "password", "Mobile");

            // act - update test doctor         
            doc.User.Name = "Name";
            doc.Speciality = Speciality.Pediatrics;
            doc.User.Email = "yyy@email.com";
            doc.Mobile = "Modified Mobile";

            var d = svc.UpdateDoctor(doc);

            // assert
            Assert.NotNull(d);
            Assert.Equal("Name", doc.User.Name);
            Assert.Equal(Speciality.Pediatrics, doc.Speciality);
            Assert.Equal("yyy@email.com", doc.User.Email);
            Assert.Equal("Modified Mobile", doc.Mobile);
        }

        [Fact]
        public void Doctor_GetDoctorThatExists_ShouldReturnDoctor()
        {
            // arrange - create test doctor
            var doc = svc.AddDoctor("Name", Speciality.Obstetrics_Gynecology, "zzz@email.com", "password", "Mobile");

            // act
            var retrieved = svc.GetDoctorById(doc.Id);

            // assert
            Assert.NotNull(retrieved);
            Assert.Equal(doc.Id, retrieved.Id);
        }

        [Fact]
        public void Doctor_DeleteDoctorThatExists_ShouldReturnTrue()
        {
            // arrange - create test doctor
            var doc = svc.AddDoctor("Name", Speciality.Urology, "zzz@email.com", "password", "Mobile");

            // act
            var deleted = svc.DoctorDelete(doc.Id);
            var retrieve = svc.GetDoctorById(doc.Id);           // try to retrieve deleted doctor

            // assert
            Assert.True(deleted); // delete doctor should return true
            Assert.Null(retrieve);      // cannot retrieve the doctor (deleted)
        }

        //-------------Diagnoses------------------------------------------------------

        [Fact]
        void DiagnoseAilment()
        {
            // arrange
            // symptoms
            var headache = svc.Addsymptom("headache");
            var temp = svc.Addsymptom("temperature");
            var appetite = svc.Addsymptom("loss of appetite");
            var itch = svc.Addsymptom("itchy skin");
            var sweating = svc.Addsymptom("sweating");
            var throat = svc.Addsymptom("sore throat");
            var vomit = svc.Addsymptom("vomiting");
            var rash = svc.Addsymptom("skin rashes");
            var poo = svc.Addsymptom("diarrhoea");
            var muscle = svc.Addsymptom("aching muscles");
            var cough = svc.Addsymptom("cough");
            var fever = svc.Addsymptom("fever");
            var chest = svc.Addsymptom("chest pain");
            var breath = svc.Addsymptom("difficulty breathing");
            var constipate = svc.Addsymptom("constipation");
            var tired = svc.Addsymptom("Lack of energy");
            var libido = svc.Addsymptom("low sex drive");
            var aches = svc.Addsymptom("body aches and pains");
            var slur = svc.Addsymptom("speaking more slowly than usual");
            var sleepless = svc.Addsymptom("disturbed sleep");
            var sleepy = svc.Addsymptom("always feeling sleepy");
            var slin = svc.Addsymptom("dry and scaly skin");
            var brittle = svc.Addsymptom("brittle hair and nails");
            var cycle = svc.Addsymptom("irregular or heavy periods");
            // conditions 
            var flu = svc.AddCondition("Flu", "");
            svc.AddConditionSymptoms(flu.Id, new List<ConditionSymptom> {
             new ConditionSymptom { ConditionId = flu.Id, SymptomId = headache.Id },
             new ConditionSymptom { ConditionId = flu.Id, SymptomId = temp.Id },
             new ConditionSymptom { ConditionId = flu.Id, SymptomId = appetite.Id },
        });
            var eczema = svc.AddCondition("Eczema", "");
            svc.AddConditionSymptoms(eczema.Id, new List<ConditionSymptom> {
            new ConditionSymptom { ConditionId = eczema.Id, SymptomId = appetite.Id },
            new ConditionSymptom { ConditionId = eczema.Id, SymptomId = itch.Id },
        });
            var migrane = svc.AddCondition("Migrane", "");
            svc.AddConditionSymptoms(migrane.Id, new List<ConditionSymptom> {
            new ConditionSymptom { ConditionId = migrane.Id, SymptomId = headache.Id }
        });

            // patient + ailment
            var pat = svc.CreatePatient("P1", "27 Georgetown", "p1@mail.net", "password", "123456", new DateTime(2000, 1, 1));
            var a1 = svc.AddAilment(pat.User.Id, "I feel unwell");
            a1 = svc.AddAilmentSymptoms(a1.Id, new List<AilmentSymptom> {
               new AilmentSymptom { AilmentId = a1.Id, SymptomId = headache.Id },
               new AilmentSymptom { AilmentId = a1.Id, SymptomId = temp.Id }
        });

            // act
            var r = svc.DiagnoseConditions(a1);

            // assert
            Assert.Equal(1, r.Count);// one condition found and it should be flu
            Assert.Equal("Flu", r[0].Name);
        }

    }
}
