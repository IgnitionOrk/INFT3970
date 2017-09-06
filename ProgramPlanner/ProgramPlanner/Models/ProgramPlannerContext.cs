
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
/// <summary>
/// Author: Ryan Cunneen
/// Date created: 05-Aug-2017
/// Date Modified: 31-Aug-2017
/// </summary>
namespace ProgramPlanner.Models

{
    public class ProgramPlannerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ProgramPlannerContext() : base("name=ProgramPlannerContext")
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<YearDegree> YearDegrees { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<MajorCore> MajorCores { get; set; }
        public DbSet<DegreeCore> DegreeCores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProgramStructure> ProgramStructures { get; set; }
        public DbSet<ProgramMajor> ProgramMajors { get; set; }
        public DbSet<ProgramElective> ProgramElectives { get; set; }
        public DbSet<ProgramDirected> ProgramDirecteds { get; set; }
        public DbSet<ProgramOptionalCoreCourse> ProgramOptionalCoreCourses { get; set; }
        public DbSet<OptionalCoreCourse> OptionalCoreCourses { get; set; }
        public DbSet<DegreeCoreSlot> DegreeCoreSlots { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Trimester> Trimesters { get; set; }
        public DbSet<SemesterCourse> SemesterCourses { get; set; }
        public DbSet<TrimesterCourse> TrimesterCourses { get; set; }
        public DbSet<Replacement> Replacements { get; set; }
        public DbSet<Abbreviation> Abbreviations { get; set; }
        public DbSet<StudyArea> StudyAreas { get; set; }
        public DbSet<DirectedSlot> DirectedSlots { get; set; }
        /// <summary>
        /// Constructs the model constraints, and their associations between other entities. 
        /// </summary>
        /// <param name="modelbuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            Models(modelbuilder);
            Relationships(modelbuilder);
        }
        /// <summary>
        /// Constructs the models and their constraints.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void Models(DbModelBuilder modelbuilder)
        {
            ModelCourse(modelbuilder);
            ModelYearDegree(modelbuilder);
            ModelReplacement(modelbuilder);
            ModelDegreeCore(modelbuilder);
            ModelMajors(modelbuilder);
            ModelMajorCore(modelbuilder);
            ModelSemester(modelbuilder);
            ModelTrimester(modelbuilder);
            ModelSemesterCourse(modelbuilder);
            ModelTrimesterCourse(modelbuilder);
            ModelReplacement(modelbuilder);
            ModelDirecteds(modelbuilder);
            ModelProgramMajor(modelbuilder);
            ModelProgramElective(modelbuilder);
            ModelProgramDirected(modelbuilder);
            ModelUniversity(modelbuilder);
            ModelUser(modelbuilder);
            ModelProgramStructure(modelbuilder);
            ModelProgramOptionalCoreCourse(modelbuilder);
            ModelAbbreviation(modelbuilder);
            ModelStudyArea(modelbuilder);
            ModelDirectedSlot(modelbuilder);
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Course.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Course>()
                .Ignore(y => y.CourseCode);

            modelbuilder.Entity<Course>()
                .Property(y => y.CourseName).IsRequired();

            modelbuilder.Entity<Course>()
                .Property(y => y.UniversityID)
                .IsRequired();

            modelbuilder.Entity<Course>()
                .Property(y => y.Units)
                .IsRequired();

            modelbuilder.Entity<Course>()
                .Property(y => y.AbbreviationID)
                .IsRequired();

            modelbuilder.Entity<Course>()
                .Property(y => y.Code)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity ProgramElective.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelProgramElective(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramElective>()
                .HasKey(y => new { y.CourseID, y.ProgramStructureID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity YearDegrees
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelYearDegree(DbModelBuilder modelbuilder) { }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Major.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelMajors(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Major>()
                .Property(y => y.MajorName)
                .IsRequired();

            modelbuilder.Entity<Major>()
                .Property(y => y.YearDegreeID)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Replacement.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelReplacement(DbModelBuilder modelbuilder)
        {
            // Creating a new composite Primary key for entity Replacement;
            modelbuilder.Entity<Replacement>()
                .HasKey(y => new { y.ReplacementID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Degree.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelDegree(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Degree>()
                .Property(y => y.DegreeName)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity DegreeCore.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelDegreeCore(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DegreeCore>()
                .HasKey(y => new { y.CourseID, y.YearDegreeID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity MajorCore.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelMajorCore(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<MajorCore>()
                .HasKey(y => new { y.CourseID, y.MajorID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Semester.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelSemester(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Semester>()
                .HasKey(y => new { y.SemesterID });

            modelbuilder.Entity<Semester>()
                .Property(y => y.SemesterValue)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Trimester.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelTrimester(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Trimester>()
                .HasKey(y => new { y.TrimesterID });

            modelbuilder.Entity<Trimester>()
                .Property(y => y.TrimesterValue)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity SemesterCourse.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelSemesterCourse(DbModelBuilder modelbuilder)
        {
            // Creating a new composite Primary key. 
            modelbuilder.Entity<SemesterCourse>()
                .HasKey(y => new { y.SemesterID, y.CourseID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity TrimesterCourse.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelTrimesterCourse(DbModelBuilder modelbuilder)
        {
            // Creating a new composite Primary key. 
            modelbuilder.Entity<TrimesterCourse>()
                .HasKey(y => new { y.TrimesterID, y.CourseID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity OptionalDirected.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelDirecteds(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DirectedSlot>().HasKey(y => new { y.DirectedSlotID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity ProgramMajor.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelProgramMajor(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramMajor>()
                .HasKey(y => new { y.ProgramStructureID, y.MajorID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity ProgramDirected.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelProgramDirected(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramDirected>()
                .HasKey(y => new { y.ProgramStructureID, y.DirectedSlotID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity University.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelUniversity(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<University>()
                .Property(y => y.UniName)
                .IsRequired();

            modelbuilder.Entity<University>()
                .Property(y => y.UniName)
                .HasMaxLength(250);
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity User.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelUser(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>()
                .Property(y => y.Email)
                .IsRequired();

            modelbuilder.Entity<User>()
                .Property(y => y.Password)
                .IsRequired();

            modelbuilder.Entity<User>()
                .HasKey(y => y.Email);
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity ProgramStructure.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelProgramStructure(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramStructure>()
                .Property(y => y.DateCreated)
                .IsRequired();

            modelbuilder.Entity<ProgramStructure>()
                .Property(y => y.DateModified)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity ProgramOptionalCoreCourse.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelProgramOptionalCoreCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramOptionalCoreCourse>()
                .HasKey(y => new { y.ProgramStructureID, y.OptionalCoreID });
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity Abbreviation.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelAbbreviation(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Abbreviation>()
                .Property(y => y.AbbrevName)
                .IsRequired();
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity StudyArea.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelStudyArea(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<StudyArea>()
                .Property(y => y.StudyAreaName)
                .IsRequired();
        }

        private void ModelMajorSlot(DbModelBuilder modelbuilder) {
            modelbuilder.Entity<MajorSlot>()
                .HasKey(y => y.MajorSlotID);
        }
        /// <summary>
        /// Constructs all constraints (not relationships) associcated with the entity DirectedSlot.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void ModelDirectedSlot(DbModelBuilder modelbuilder) { }
        /// <summary>
        /// Defines associations between entities for database context.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void Relationships(DbModelBuilder modelbuilder)
        {
            RelationshipsForCourse(modelbuilder);
            RelationshipsForYearDegree(modelbuilder);
            RelationshipsForDegree(modelbuilder);
            RelationshipsForDegreeCore(modelbuilder);
            RelationshipsForReplacement(modelbuilder);
            RelationshipsForProgramMajor(modelbuilder);
            RelationshipsForProgramElective(modelbuilder);
            RelationshipsForProgramDirected(modelbuilder);
            RelationshipsForProgramStructures(modelbuilder);
            RelationshipsForUser(modelbuilder);
            RelationshipsForMajors(modelbuilder);
            RelationshipsForMajorCores(modelbuilder);
            RelationshipsForOptionalCoreCourses(modelbuilder);
            RelationshipsForTrimester(modelbuilder);
            RelationshipsForSemester(modelbuilder);
            RelationshipsForSemesterCourse(modelbuilder);
            RelationshipsForTrimesterCourse(modelbuilder);
            RelationshipsForProgramOptionalCoreCourse(modelbuilder);
            RelationshipsForAbbreviation(modelbuilder);
            RelationshipsForStudyArea(modelbuilder);
            RelationshipsForUniversity(modelbuilder);
            RelationshipsForMajorSlots(modelbuilder);
            RelationshipsForDegreeCoreSlot(modelbuilder);
        }
        /// <summary>
        /// Defines the associations for the entity Course.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Course>()
                .HasRequired(y => y.Abbreviation)
                .WithMany(y => y.Courses)
                .HasForeignKey(y => y.AbbreviationID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Course>()
                .HasRequired(y => y.University)
                .WithMany(y => y.Courses)
                .HasForeignKey(y => y.UniversityID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Course>().HasMany(y => y.MandatoryPrerequisites).WithMany().Map(
                Mandatory => {
                    Mandatory.MapLeftKey("CourseID");
                    Mandatory.MapRightKey("AssumedKnowledgeID");
                    Mandatory.ToTable("AssumedKnowledge");
                });

            modelbuilder.Entity<Course>().HasMany(y => y.OptionalPrerequisites).WithMany().Map(
                Mandatory => {
                    Mandatory.MapLeftKey("CourseID");
                    Mandatory.MapRightKey("AlternativeAKID");
                    Mandatory.ToTable("AlternativeAssumedKnowledge");
                });

            modelbuilder.Entity<Course>()
                .HasOptional(y => y.Replacement)
                .WithRequired(y => y.ReplacementCourse);

            modelbuilder.Entity<Course>()
                .HasMany(y => y.Directeds)
                .WithRequired(y => y.Course)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Course>()
                .HasMany(y => y.MajorCores);

            modelbuilder.Entity<Course>()
                .HasMany(y => y.DegreeCores)
                .WithRequired(y => y.Course)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity MajorCores.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForMajorCores(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<MajorCore>()
                .HasRequired(y => y.Major)
                .WithMany(y => y.MajorCores)
                .HasForeignKey(y => y.MajorID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<MajorCore>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.MajorCores)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity YearDegree.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForYearDegree(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<YearDegree>()
                .HasRequired(y => y.Degree)
                .WithMany(y => y.YearDegrees)
                .HasForeignKey(y => y.DegreeID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<YearDegree>()
                .HasMany(y => y.DegreeCores)
                .WithRequired(y => y.YearDegree)
                .HasForeignKey(y => y.YearDegreeID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<YearDegree>()
                .HasMany(y => y.Replacements);

            modelbuilder.Entity<YearDegree>()
                .HasMany(y => y.DegreeCoreSlots)
                .WithRequired(y => y.YearDegree)
                .HasForeignKey(y => y.YearDegreeID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity Degree.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForDegree(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Degree>()
                .HasRequired(y => y.University)
                .WithMany(y => y.Degrees)
                .HasForeignKey(y => y.UniversityID).WillCascadeOnDelete(false);
        }

        /// <summary>
        /// Defines the associations for the entity Replacement.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForReplacement(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Replacement>()
             .HasRequired(y => y.ReplacementCourse)
             .WithOptional(y => y.Replacement);
        }
        /// <summary>
        /// Defines the associations for the entity DegreeCore.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForDegreeCore(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DegreeCore>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.DegreeCores)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<DegreeCore>()
                .HasRequired(y => y.YearDegree)
                .WithMany(y => y.DegreeCores)
                .HasForeignKey(y => y.YearDegreeID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity ProgramDirected.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForProgramDirected(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramDirected>()
                .HasRequired(y => y.DirectedSlot)
                .WithMany(y => y.ProgramDirecteds)
                .HasForeignKey(y => y.DirectedSlotID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramDirected>()
                .HasRequired(y => y.ProgramStructure)
                .WithMany(y => y.ProgramDirecteds)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity ProgramElective.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForProgramElective(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramElective>()
                .HasRequired(y => y.ProgramStructure)
                .WithMany(y => y.ProgramElectives)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramElective>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.ProgramElectives)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity ProgramMajor.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForProgramMajor(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramMajor>()
                .HasRequired(y => y.ProgramStructure)
                .WithMany(y => y.ProgramMajors)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramMajor>()
                .HasRequired(y => y.Major)
                .WithMany(y => y.ProgramMajors)
                .HasForeignKey(y => y.MajorID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity ProgramStructures.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForProgramStructures(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramStructure>()
                .HasMany(y => y.ProgramDirecteds)
                .WithRequired(y => y.ProgramStructure)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramStructure>()
                .HasMany(y => y.ProgramElectives)
                .WithRequired(y => y.ProgramStructure)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramStructure>()
               .HasMany(y => y.ProgramMajors)
               .WithRequired(y => y.ProgramStructure)
               .HasForeignKey(y => y.ProgramStructureID)
               .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramStructure>()
               .HasMany(y => y.ProgramOptionalCores)
               .WithRequired(y => y.ProgramStructure)
               .HasForeignKey(y => y.ProgramStructureID)
               .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramStructure>()
               .HasRequired(y => y.User)
               .WithMany(y => y.ProgramStructures)
               .HasForeignKey(y => y.Email)
               .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity User.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForUser(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>()
                .HasMany(y => y.ProgramStructures)
                .WithRequired(y => y.User)
                .HasForeignKey(y => y.Email)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity Major.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForMajors(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Major>()
                .HasRequired(y => y.YearDegree)
                .WithMany(y => y.Majors)
                .HasForeignKey(y => y.YearDegreeID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Major>()
                .HasMany(y => y.MajorCores)
                .WithRequired(y => y.Major)
                .HasForeignKey(y => y.MajorID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Major>()
                .HasMany(y => y.MajorSlots)
                .WithRequired(y => y.Major)
                .HasForeignKey(y => y.MajorID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity DirectedSlot.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForDirectedSlots(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DirectedSlot>()
                .HasRequired(y => y.MajorSlot)
                .WithMany(y => y.Directeds)
                .HasForeignKey(y => y.MajorSlotID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<DirectedSlot>()
                .HasMany(y => y.ProgramDirecteds)
                .WithRequired(y => y.DirectedSlot)
                .HasForeignKey(y => y.DirectedSlotID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<DirectedSlot>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.Directeds)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity Trimester.
        /// </summary>
        /// <param name="builder"></param>
        private void RelationshipsForTrimester(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Trimester>()
                .HasMany(y => y.TrimesterCourses)
                .WithRequired(y => y.Trimester)
                .HasForeignKey(y => y.TrimesterID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity Semester.
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForSemester(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Semester>()
                .HasMany(y => y.SemesterCourses)
                .WithRequired(y => y.Semester)
                .HasForeignKey(y => y.SemesterID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity SemesterCourse.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForSemesterCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<SemesterCourse>()
                .HasRequired(y => y.Semester)
                .WithMany(y => y.SemesterCourses)
                .HasForeignKey(y => y.SemesterID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<SemesterCourse>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.SemesterCourses)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity TrimesterCourse.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForTrimesterCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<TrimesterCourse>()
                .HasRequired(y => y.Trimester)
                .WithMany(y => y.TrimesterCourses)
                .HasForeignKey(y => y.TrimesterID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<TrimesterCourse>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.TrimesterCourses)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity OptionalCoreCourse.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForOptionalCoreCourses(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<OptionalCoreCourse>()
                .HasRequired(y => y.DegreeCoreSlot)
                .WithMany(y => y.OptionalCoreCourses)
                .HasForeignKey(y => y.DegreeCoreSlotID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<OptionalCoreCourse>()
                .HasRequired(y => y.Course)
                .WithMany(y => y.OptionalCoreCourses)
                .HasForeignKey(y => y.CourseID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<OptionalCoreCourse>()
               .HasRequired(y => y.DegreeCoreSlot)
               .WithMany(y => y.OptionalCoreCourses)
               .HasForeignKey(y => y.DegreeCoreSlotID)
               .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity ProgramOptionalCoreCourse.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForProgramOptionalCoreCourse(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProgramOptionalCoreCourse>()
                .HasRequired(y => y.ProgramStructure)
                .WithMany(y => y.ProgramOptionalCores)
                .HasForeignKey(y => y.ProgramStructureID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<ProgramOptionalCoreCourse>()
                .HasRequired(y => y.OptionalCoreCourse)
                .WithMany(y => y.ProgramOptionalCoreCourse)
                .HasForeignKey(y => y.OptionalCoreID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity Abbreviation.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForAbbreviation(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Abbreviation>()
                .HasRequired(y => y.StudyArea)
                .WithMany(y => y.Abbrevations)
                .HasForeignKey(y => y.StudyAreaID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<Abbreviation>()
                .HasMany(y => y.Courses)
                .WithRequired(y => y.Abbreviation)
                .HasForeignKey(y => y.AbbreviationID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity StudyArea.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForStudyArea(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<StudyArea>()
                .HasMany(y => y.Abbrevations)
                .WithRequired(y => y.StudyArea)
                .HasForeignKey(y => y.StudyAreaID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<StudyArea>()
                .HasRequired(y => y.University)
                .WithMany(y => y.StudyAreas)
                .HasForeignKey(y => y.UniversityID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity University.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForUniversity(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<University>()
                .HasMany(y => y.StudyAreas)
                .WithRequired(y => y.University)
                .HasForeignKey(y => y.UniversityID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<University>()
                .HasMany(y => y.Courses)
                .WithRequired(y => y.University)
                .HasForeignKey(y => y.UniversityID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<University>()
                .HasMany(y => y.Degrees)
                .WithRequired(y => y.University)
                .HasForeignKey(y => y.UniversityID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForMajorSlots(DbModelBuilder modelbuilder) {
            modelbuilder.Entity<MajorSlot>()
                .HasRequired(y => y.Major)
                .WithMany(y => y.MajorSlots)
                .HasForeignKey(y => y.MajorID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<MajorSlot>()
                .HasMany(y => y.Directeds)
                .WithRequired(y => y.MajorSlot)
                .HasForeignKey(y => y.MajorSlotID)
                .WillCascadeOnDelete(false);
        }
        /// <summary>
        /// Defines the associations for the entity DegreeCoreSlot.  
        /// </summary>
        /// <param name="modelbuilder"></param>
        private void RelationshipsForDegreeCoreSlot(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DegreeCoreSlot>()
                .HasMany(y => y.OptionalCoreCourses)
                .WithRequired(y => y.DegreeCoreSlot)
                .HasForeignKey(y => y.DegreeCoreSlotID)
                .WillCascadeOnDelete(false);

            modelbuilder.Entity<DegreeCoreSlot>()
               .HasRequired(y => y.YearDegree)
               .WithMany(y => y.DegreeCoreSlots)
               .HasForeignKey(y => y.YearDegreeID)
               .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<ProgramPlanner.Models.MajorSlot> MajorSlots { get; set; }
    }   
}
