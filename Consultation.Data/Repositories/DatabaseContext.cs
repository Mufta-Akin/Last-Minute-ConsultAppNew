using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// import the Models (representing structure of tables in database)
using Consultation.Data.Models; 

namespace Consultation.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class DatabaseContext :DbContext
    {
         // authentication store
        public DbSet<User> Users { get; set; }
        public DbSet<Practice> Practice { get; set; }
        public DbSet<Ailment> Ailments { get; set; }
        public DbSet<AilmentSymptom> AilmentSymptom { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ConditionSymptom> ConditionSymptoms { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Staff> Staffs { get; set; }


        // Configure the context to use Specified database. We are using 
        // Sqlite database as it does not require any additional installations.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Filename=data.db")
                //.LogTo(Console.WriteLine, LogLevel.Information) // remove in production
                //.EnableSensitiveDataLogging()                   // remove in production
                ;
        }

        // Convenience method to recreate the database thus ensuring  the new database takes 
        // account of any changes to the Models or DatabaseContext
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
