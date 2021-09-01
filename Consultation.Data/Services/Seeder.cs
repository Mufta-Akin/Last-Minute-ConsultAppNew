using System;
using System.Text;
using System.Collections.Generic;

using Consultation.Data.Models;

namespace Consultation.Data.Services
{
    public static class Seeder
    {
        // use this class to seed the database with dummy 
        // test data using an IUserService and IPracticeService
        public static void Seed(IUserService _user, IPracticeService _svc)
        {
            _svc.Initialise();

            // create practice adminstrator
            var admin = _user.AddUser("Practice Manager", "admin@admin.com", "admin", Role.Practice);

            // create practice admin Staff
            var staff = _svc.AddStaff("Fred Langley", "Admin Assistant", "staff@mail.com", "password", "+13645860385");
            var staff1 = _svc.AddStaff("West Trafford", "Medical Data Associate", "weste@mail.com", "password", "0743884905569");
            var staff2 = _svc.AddStaff("Jene Tate-Prior", "Medical Data Associate", "jane@email.com", "password", "076384930274");
            var staff3 = _svc.AddStaff("Peggy Jo Flanigan", "Admin Assistant", "peggy22@email.com", "password", "+24948375104");
            var staff4 = _svc.AddStaff("Ann-Hill Willows", "Medical Data Associate", "willows@email.com", "password", "0892347813");

            // add patients
            var pat = _svc.CreatePatient("Joanna Salome", "3454 Highway 22nd Bypass, Antrim", "pat@mail.com", "password", "0383458734650", new DateTime(2010, 1, 1));
            var pat1 = _svc.CreatePatient("Fay Connor", "27 Blammer Road, Georgetown", "fay@mail.com", "password", "+1 2304564789", new DateTime(2000,1,1));
            var pat2 = _svc.CreatePatient("Marty Bluffy", "7 Gainsville Avenue, Senate House", "marty@mail.com", "password", "+241 2467326493", new DateTime(1945, 4, 11));
            var pat3 = _svc.CreatePatient("Gracie Tempper", "900 Briton Lane, Birminghan", "G_Temper@gomail.eu", "password", "+44 4905342773", new DateTime(1972, 1, 1));
            var pat4 = _svc.CreatePatient("Drew Horne", "Westin House, apt 23, Luton", "flipper22@mail.uk", "password", "+44 3425239403", new DateTime(1991, 1, 1));
            var pat5 = _svc.CreatePatient("Kong Leonne", "Unit 7 Ash Road Apartment, Derry", "Kong@goodmail.com", "password", "+23 8063788273", new DateTime(2010, 1, 1));
            var pat6 = _svc.CreatePatient("Henrt Claimmer", "8804 Flaneggan Road, apt 2, London", "Claimmer.H@mail.net", "password", "03564495748", new DateTime(1964, 1, 1));
            var pat7 = _svc.CreatePatient("Daniel Leopez", "Central Parkway, Apt 80, Oxford", "Dan@mail.com", "password", "06375465577", new DateTime(1983, 1, 1));


            // add doctors
            var doc = _svc.AddDoctor("Dr Peggy Smitler", Speciality.Cardiology, "doc@mail.com", "password", "+24364589034");
            var doc1 = _svc.AddDoctor("Dr Ray Fann", Speciality.Dermatology, "doc@email.com", "password", "+1354895768");


            // add symptoms
            var sym0 = _svc.Addsymptom("Runny or stuffy nose");
            var sym1 = _svc.Addsymptom("Cough");
            var sym2 = _svc.Addsymptom("Vomiting");
            var sym3 = _svc.Addsymptom("Diarrhea");
            var sym4 = _svc.Addsymptom("Loss of appetite");
            var sym5 = _svc.Addsymptom("Repeated thrush");
            var sym6 = _svc.Addsymptom("loss of sense of taste or smell");
            var sym7 = _svc.Addsymptom("Coughing up thick mucus");
            var sym8 = _svc.Addsymptom("Headache");
            var sym9 = _svc.Addsymptom("High temperature");
            var sym10 = _svc.Addsymptom("New continuous cough");
            var sym11 = _svc.Addsymptom("Blured vision");
            var sym12 = _svc.Addsymptom("Frequent thirst");
            var sym13 = _svc.Addsymptom("Frequent urination");
            var sym14 = _svc.Addsymptom("Tired (Fatigue)");
            var sym15 = _svc.Addsymptom("Sore throat");
            var sym16 = _svc.Addsymptom("Body rash (Hive)");
            var sym17 = _svc.Addsymptom("Fever");
            var sym18 = _svc.Addsymptom("Muscle or body aches");
            var sym19 = _svc.Addsymptom("Breathlessness");
            var sym20 = _svc.Addsymptom("Tight chest");



            //https://www.nhs.uk/
            //https://www.cdc.gov/DiseasesConditions/az/a.html
            var con1 = _svc.AddCondition("Flu", "Influenza");
            _svc.AddConditionSymptoms(con1.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con1, Symptom = sym17 },//fever
                    new ConditionSymptom { Condition = con1, Symptom = sym1 },//cough
                    new ConditionSymptom { Condition = con1, Symptom = sym0 },//("Runny or stuffy nose
                    new ConditionSymptom { Condition = con1, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con1, Symptom = sym18 },//Muscle or body aches
             });

            var con2 = _svc.AddCondition("Pertussis", "Whooping Cough");
            _svc.AddConditionSymptoms(con2.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con2, Symptom = sym1 },//cough
                    new ConditionSymptom { Condition = con2, Symptom = sym15},//sore throat
                    new ConditionSymptom { Condition = con2, Symptom = sym7 },//coughing up thick mucus
                    new ConditionSymptom { Condition = con2, Symptom = sym7 },//gasping for breadth between coughs
                    new ConditionSymptom { Condition = con2, Symptom = sym2 },//vomitting
             });

            var con3 = _svc.AddCondition("Malaria", "Malaria");
            _svc.AddConditionSymptoms(con3.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con3, Symptom = sym9 },//high temperature
                    new ConditionSymptom { Condition = con3, Symptom = sym8},//headache
                    new ConditionSymptom { Condition = con3, Symptom = sym18 },//muscle or body aches
                    new ConditionSymptom { Condition = con3, Symptom = sym3 },//diarrhoea
                    new ConditionSymptom { Condition = con3, Symptom = sym2 },//vomitting
             });

            var con4 = _svc.AddCondition("Diabetes", "Diabetes");
            _svc.AddConditionSymptoms(con4.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con4, Symptom = sym13 },//frequent urination
                    new ConditionSymptom { Condition = con4, Symptom = sym12},//frequent thirst
                    new ConditionSymptom { Condition = con4, Symptom = sym14 },//tired (fatigue)
                    new ConditionSymptom { Condition = con4, Symptom = sym11 },//blured vision
                    new ConditionSymptom { Condition = con4, Symptom = sym5 },//repeated thrush
             });

            var con5 = _svc.AddCondition("Hepatitis A", "Hepatitis A");
            _svc.AddConditionSymptoms(con5.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con5, Symptom = sym18 },//muscle or body aches
                    new ConditionSymptom { Condition = con5, Symptom = sym9 },//high temperature
                    new ConditionSymptom { Condition = con5, Symptom = sym14 },//tired (fatigue)
                    new ConditionSymptom { Condition = con5, Symptom = sym4 },//Loss of appetite
                    new ConditionSymptom { Condition = con5, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con5, Symptom = sym15},//sore throat
                    new ConditionSymptom { Condition = con5, Symptom = sym3 },//diarrhoea
                    new ConditionSymptom { Condition = con3, Symptom = sym16 },//body rash(hive)
             });

            var con6 = _svc.AddCondition("COVID19", "Corona Virus");
            _svc.AddConditionSymptoms(con4.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con6, Symptom = sym18 },//muscle or body aches
                    new ConditionSymptom { Condition = con6, Symptom = sym9 },//high temperature
                    new ConditionSymptom { Condition = con6, Symptom = sym14 },//tired (fatigue)
                    new ConditionSymptom { Condition = con6, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con6, Symptom = sym15},//sore throat
                    new ConditionSymptom { Condition = con6, Symptom = sym10 },//new continous cough
                    new ConditionSymptom { Condition = con6, Symptom = sym6 },//loss of sense of taste/smell
             });

        }
    }
}
