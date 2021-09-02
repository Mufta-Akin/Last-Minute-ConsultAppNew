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
            var pat2 = _svc.CreatePatient("Marty Bluffy", "7 Gainsville Avenue, Senate House", "marty@mail.com", "password", "+241 2467326493", new DateTime(1945,4,11));
            var pat3 = _svc.CreatePatient("Gracie Tempper", "900 Briton Lane, Birminghan", "G_Temper@gomail.eu", "password", "+44 4905342773", new DateTime(1972,1,1));
            var pat4 = _svc.CreatePatient("Drew Horne", "Westin House, apt 23, Luton", "flipper22@mail.uk", "password", "+44 3425239403", new DateTime(1991,1,1));
            var pat5 = _svc.CreatePatient("Kong Leonne", "Unit 7 Ash Road Apartment, Derry", "Kong@goodmail.com", "password", "+23 8063788273", new DateTime(2010,1,1));
            var pat6 = _svc.CreatePatient("Henrt Claimmer", "8804 Flaneggan Road, apt 2, London", "Claimmer.H@mail.net", "password", "03564495748", new DateTime(1964,1,1));
            var pat7 = _svc.CreatePatient("Daniel Leopez", "Central Parkway, Apt 80, Oxford", "Dan@mail.com", "password", "06375465577", new DateTime(1983,1,1));

            // add doctors
            var doc = _svc.AddDoctor("Dr Peggy Smitler", Speciality.Cardiology, "doc@mail.com", "password", "+24364589034");
            var doc1 = _svc.AddDoctor("Dr Ray Fann", Speciality.Dermatology, "doc@email.com", "password", "+1354895768");

            //var ailment = _svc.AddAilment(pat1.Id, "Im not feelin well");
            //var ailment1 = _svc.AddAilment(pat1.Id, "I'm sick");
            //var ailment2 = _svc.AddAilment(pat1.Id, "I feel tired");
            //var ailment3 = _svc.AddAilment(pat1.Id, "Abdominal pain");

            // add symptoms
            var sym0 = _svc.Addsymptom("Runny or stuffy nose");
            var sym1 = _svc.Addsymptom("Cough");
            var sym2 = _svc.Addsymptom("Vomiting");
            var sym3 = _svc.Addsymptom("Diarrhea");
            var sym4 = _svc.Addsymptom("Loss of appetite");
            var sym5 = _svc.Addsymptom("Repeated thrush");
            var sym6 = _svc.Addsymptom("Loss of sense of taste or smell");
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
            var sym19 = _svc.Addsymptom("Breathing difficulty");
            var sym20 = _svc.Addsymptom("Thirst");
            var sym21 = _svc.Addsymptom("Dehydration");
            var sym22 = _svc.Addsymptom("Hesistancy");
            var sym23 = _svc.Addsymptom("Weak urine flow");
            var sym24 = _svc.Addsymptom("Blood in urine");
            var sym25 = _svc.Addsymptom("Feeling of unempty bladder");
            var sym26 = _svc.Addsymptom("Sweating and chivering");
            var sym27 = _svc.Addsymptom("Rapid heartbeat");
            var sym28 = _svc.Addsymptom("Chest pain");
            var sym29 = _svc.Addsymptom("Chills");
            var sym30 = _svc.Addsymptom("Upset stomach");
            var sym31 = _svc.Addsymptom("Straining to wee");


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
                    new ConditionSymptom { Condition = con2, Symptom = sym19 },//Breathing difficulty
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
            _svc.AddConditionSymptoms(con6.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con6, Symptom = sym18 },//muscle or body aches
                    new ConditionSymptom { Condition = con6, Symptom = sym14 },//tired (fatigue)
                    new ConditionSymptom { Condition = con6, Symptom = sym9 },//high temperature                    
                    new ConditionSymptom { Condition = con6, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con6, Symptom = sym15},//sore throat
                    new ConditionSymptom { Condition = con6, Symptom = sym10 },//new continous cough
                    new ConditionSymptom { Condition = con6, Symptom = sym6 },//loss of sense of taste/smell
             });

            var con7 = _svc.AddCondition("Cholera", "Cholera");
            _svc.AddConditionSymptoms(con7.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con7, Symptom = sym3 },// diarrhea
                    new ConditionSymptom { Condition = con7, Symptom = sym2 },//vomiting
                    new ConditionSymptom { Condition = con7, Symptom = sym14 },//tired (fatigue)
                    new ConditionSymptom { Condition = con7, Symptom = sym20 },//thirst
                    new ConditionSymptom { Condition = con7, Symptom = sym21 },//dehydration
             });

            var con8 = _svc.AddCondition("Prostate Cancer", "Prostate Cancer");
            _svc.AddConditionSymptoms(con8.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con8, Symptom = sym13 },//frequent urination
                    new ConditionSymptom { Condition = con8, Symptom = sym23 },//Weak urine flow
                    new ConditionSymptom { Condition = con8, Symptom = sym31 },//straining to wee
                    new ConditionSymptom { Condition = con8, Symptom = sym24 },//blood in urine
                    new ConditionSymptom { Condition = con8, Symptom = sym25 },//feeling of unempty bladder
             });

            var con9 = _svc.AddCondition("Pneumonia", "Pneumonia");
            _svc.AddConditionSymptoms(con9.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con9, Symptom = sym1 },//cough
                    new ConditionSymptom { Condition = con9, Symptom = sym26},//sweating and shivering
                    new ConditionSymptom { Condition = con9, Symptom = sym9 },//high temperature
                    new ConditionSymptom { Condition = con9, Symptom = sym4 },//loss of appetite
                    new ConditionSymptom { Condition = con9, Symptom = sym28 },//chest pain
             });

            var con10 = _svc.AddCondition("Bronchitis", "Bronchitis");
            _svc.AddConditionSymptoms(con10.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con10, Symptom = sym15},//sore throat
                    new ConditionSymptom { Condition = con10, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con10, Symptom = sym18 },//muscle or body aches
                    new ConditionSymptom { Condition = con10, Symptom = sym0 },//("Runny or stuffy nose
                    new ConditionSymptom { Condition = con10, Symptom = sym1 },//cough
             });

            var con11 = _svc.AddCondition("Shingles", "Shingles");
            _svc.AddConditionSymptoms(con11.Id, new List<ConditionSymptom> {
                    new ConditionSymptom { Condition = con11, Symptom = sym16 },//Body rash or hive
                    new ConditionSymptom { Condition = con11, Symptom = sym17},//fever
                    new ConditionSymptom { Condition = con11, Symptom = sym8 },//headache
                    new ConditionSymptom { Condition = con11, Symptom = sym29 },//chills
                    new ConditionSymptom { Condition = con11, Symptom = sym30 },//upset stomach
             });
        }
    }
}
