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

            var ailment = _svc.AddAilment(pat1.Id, "Blood in urine");
            var ailment1 = _svc.AddAilment(pat1.Id, "Frequent urination");
            var ailment2 = _svc.AddAilment(pat1.Id, "Blood in stool");
            var ailment3 = _svc.AddAilment(pat1.Id, "Abdominal pain");
            var ailment4 = _svc.AddAilment(pat1.Id, "weak flow");
            var ailment5 = _svc.AddAilment(pat1.Id, "Headaches");

            // add symptoms
            var sym = _svc.Addsymptom("Interrupted urine stream");
            var sym1 = _svc.Addsymptom("Cough");
            var sym2 = _svc.Addsymptom("Coughing up blood");
            var sym3 = _svc.Addsymptom("Breathlessness");
            var sym4 = _svc.Addsymptom("Appetite loss");
            var sym5 = _svc.Addsymptom("Weight loss");
            var sym6 = _svc.Addsymptom("Wheezing");
            var sym7 = _svc.Addsymptom("Swelling of face");
            var sym33 = _svc.Addsymptom("Swelling of neck");
            var sym8 = _svc.Addsymptom("Shoulder pain");
            var sym34 = _svc.Addsymptom("Chest pain");
            var sym9 = _svc.Addsymptom("High temperature");
            var sym10 = _svc.Addsymptom("Anxiety");
            var sym11 = _svc.Addsymptom("Lost of smell");
            var sym35 = _svc.Addsymptom("Lost of taste");
            var sym12 = _svc.Addsymptom("Frequent thirst");
            var sym13 = _svc.Addsymptom("Frequent urination");
            var sym14 = _svc.Addsymptom("Tired");
            var sym15 = _svc.Addsymptom("sore throat");
            var sym16 = _svc.Addsymptom("body rash");
            var sym17 = _svc.Addsymptom("Fever");
            var sym18 = _svc.Addsymptom("Joint pain");
            var sym19 = _svc.Addsymptom("Depression");
            var sym20 = _svc.Addsymptom("Swollen glands");
            var sym21 = _svc.Addsymptom("Muscle pain");
            var sym22 = _svc.Addsymptom("Vomiting");
            var sym23 = _svc.Addsymptom("Cold hands");
            var sym36 = _svc.Addsymptom("Cold feet");
            var sym24 = _svc.Addsymptom("Vomiting");
            var sym25 = _svc.Addsymptom("Confusion");
            var sym26 = _svc.Addsymptom("Breathing quickly");
            var sym27 = _svc.Addsymptom("Spots or rashes");
            var sym28 = _svc.Addsymptom("Headache");
            var sym29 = _svc.Addsymptom("Stiff neck");
            var sym30 = _svc.Addsymptom("Sleepiness");
            var sym31 = _svc.Addsymptom("Seizures");
            var sym32 = _svc.Addsymptom("Irritable");
            var sym37 = _svc.Addsymptom("Week urine flow");
            var sym38 = _svc.Addsymptom("Sweatting");
            var sym39 = _svc.Addsymptom("Diarrhea");
            var sym40 = _svc.Addsymptom("Nausea");


        }
    }
}
