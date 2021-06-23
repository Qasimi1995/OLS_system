using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace OLS.Models
{
    public class ApplicationContext:IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public virtual DbQuery<DashProcessNumbers> DashProcessNumbers { get; set; }
        public virtual DbQuery<DashboardStatuses> Status { get; set; }
        public virtual DbQuery<SchoolListAll> SchoolListAll { get; set; }
        public virtual DbQuery<FigureReport> FigureReport { get; set; }
        public virtual DbSet<ContactDetails> ContactDetails { get; set; }
        public virtual DbSet<LicensePayment> LicensePayment { get; set; }
        public virtual DbSet<Party> Party { get; set; }
        public virtual DbSet<PartyAddress> PartyAddress { get; set; }
        public virtual DbSet<PartyDocument> PartyDocument { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonEducation> PersonEducation { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<ProcessHistory> ProcessHistory { get; set; }
        public virtual DbSet<ProcessProgress> ProcessProgress { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolCheckList> SchoolCheckList { get; set; }
        public virtual DbSet<SchoolFinancialResource> SchoolFinancialResource { get; set; }
        public virtual DbSet<SchoolLicense> SchoolLicense { get; set; }
        public virtual DbSet<SchoolOtherExpenses> SchoolOtherExpenses { get; set; }
        public virtual DbSet<SchoolStaffExpenses> SchoolStaffExpenses { get; set; }
        public virtual DbSet<SchoolFinancialPlan> SchoolFinancialPlan { get; set; }
        public virtual DbSet<StudentEnrollmentPlan> StudentEnrollmentPlan { get; set; }
        public virtual DbSet<SubProcess> SubProcess { get; set; }
        public virtual DbSet<SubProcessStatus> SubProcessStatus { get; set; }
        public virtual DbSet<ZAddressType> ZAddressType { get; set; }
        public virtual DbSet<ZContactMechanismType> ZContactMechanismType { get; set; }
        public virtual DbSet<ZDistrict> ZDistrict { get; set; }
        public virtual DbSet<ZDocCategory> ZDocCategory { get; set; }
        public virtual DbSet<ZDocType> ZDocType { get; set; }
        public virtual DbSet<ZEducationLevel> ZEducationLevel { get; set; }
        public virtual DbSet<ZEducationalRole> ZEducationalRole { get; set; }
        public virtual DbSet<ZFacultyType> ZFacultyType { get; set; }
        public virtual DbSet<ZGenderType> ZGenderType { get; set; }
        public virtual DbSet<ZLaboratoryMaterialType> ZLaboratoryMaterialType { get; set; }
        public virtual DbSet<ZOtherExpenseType> ZOtherExpenseType { get; set; }
        public virtual DbSet<ZPartyRoleType> ZPartyRoleType { get; set; }
        public virtual DbSet<ZPayment> ZPayment { get; set; }
        public virtual DbSet<ZProcessStatus> ZProcessStatus { get; set; }
        public virtual DbSet<ZProvince> ZProvince { get; set; }
        public virtual DbSet<ZSchoolBussinessType> ZSchoolBussinessType { get; set; }
        public virtual DbSet<ZSchoolGenderType> ZSchoolGenderType { get; set; }
        public virtual DbSet<ZSchoolIndicator> ZSchoolIndicator { get; set; }
        public virtual DbSet<ZSchoolLevel> ZSchoolLevel { get; set; }
        public virtual DbSet<ZSchoolLevelSubLevel> ZSchoolLevelSubLevel { get; set; }
        public virtual DbSet<ZSchoolQualityLevel> ZSchoolQualityLevel { get; set; }
        public virtual DbSet<ZSchoolSubLevel> ZSchoolSubLevel { get; set; }
        public virtual DbSet<ZStatusType> ZStatusType { get; set; }
        public virtual DbSet<ZVillageNahia> ZVillageNahia { get; set; }

        public virtual DbSet<LicensePrintingLog> LicensePrintingLog { get; set; } 
       


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=OLS; User Id=sa; Password=admin.123");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

      

            modelBuilder.Entity<ContactDetails>(entity =>
            {
                entity.HasKey(e => e.ContactDetailId)
                    .HasName("PK_ContactDetails_1");

                entity.HasIndex(e => e.Value)
                    .HasName("UniqueContactDetail")
                    .IsUnique();

                entity.Property(e => e.ContactDetailId)
                    .HasColumnName("ContactDetailID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContactMechanismTypeId).HasColumnName("ContactMechanismTypeID");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.ContactMechanismType)
                    .WithMany(p => p.ContactDetails)
                    .HasForeignKey(d => d.ContactMechanismTypeId)
                    .HasConstraintName("FK_ContactDetails_ContactMechanismType");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.ContactDetails)
                    .HasForeignKey(d => d.PartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactDetails_Party");
            });


            modelBuilder.Entity<LicensePayment>(entity =>
            {
                entity.HasKey(e => e.SchoolId);

                entity.Property(e => e.SchoolId)
                    .HasColumnName("SchoolID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.RecieptNumber).HasMaxLength(50);

                entity.HasOne(d => d.School)
                    .WithOne(p => p.LicensePayment)
                    .HasForeignKey<LicensePayment>(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LicensePayment_School");
            });

            modelBuilder.Entity<Party>(entity =>
            {
                entity.Property(e => e.PartyId)
                    .HasColumnName("PartyID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<PartyAddress>(entity =>
            {
                entity.Property(e => e.PartyAddressId)
                    .HasColumnName("PartyAddressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.VillageNahiaId).HasColumnName("VillageNahiaID");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.PartyAddress)
                    .HasForeignKey(d => d.AddressTypeId)
                    .HasConstraintName("FK_PartyAddress_AddressType");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.PartyAddress)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_PartyAddress_zDistrict");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.PartyAddress)
                    .HasForeignKey(d => d.PartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartyAddress_Party");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.PartyAddress)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_PartyAddress_zProvince");

                entity.HasOne(d => d.VillageNahia)
                    .WithMany(p => p.PartyAddress)
                    .HasForeignKey(d => d.VillageNahiaId)
                    .HasConstraintName("FK_PartyAddress_zVillageNahia");
            });

            modelBuilder.Entity<PartyDocument>(entity =>
            {
                entity.Property(e => e.PartyDocumentId)
                    .HasColumnName("PartyDocumentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DocCategoryId).HasColumnName("DocCategoryID");

                entity.Property(e => e.DocPath).HasMaxLength(50);

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.HasOne(d => d.DocCategory)
                    .WithMany(p => p.PartyDocument)
                    .HasForeignKey(d => d.DocCategoryId)
                    .HasConstraintName("FK_PartyDocuments_DocCategory");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.PartyDocument)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_PartyDocuments_DocType");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.PartyDocument)
                    .HasForeignKey(d => d.PartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartyDocuments_Party");
            });


            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Nidnumber)
                    .HasName("UniquieNIDNumber")
                    .IsUnique();

                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Eduservice).HasColumnName("EDUService");

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.GenderTypeId).HasColumnName("GenderTypeID");

                entity.Property(e => e.GrandFatherName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nidnumber)
                    .HasColumnName("NIDNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.PartyRoleTypeId).HasColumnName("PartyRoleTypeID");

                entity.Property(e => e.Photo).HasMaxLength(250);

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.GenderType)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.GenderTypeId)
                    .HasConstraintName("FK_Person_zGenderType");

                entity.HasOne(d => d.PartyRoleType)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.PartyRoleTypeId)
                    .HasConstraintName("FK_Person_PatyRoleType");

                entity.HasOne(d => d.PersonNavigation)
                    .WithOne(p => p.Person)
                    .HasForeignKey<Person>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Party");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Person_School");
            });
            modelBuilder.Entity<PersonEducation>(entity =>
            {
                entity.Property(e => e.PersonEducationId)
                    .HasColumnName("PersonEducationID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EducationLevelId).HasColumnName("EducationLevelID");

                entity.Property(e => e.FacultyTypeId).HasColumnName("FacultyTypeID");

                entity.Property(e => e.GraduationDate).HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.PersonEducation)
                    .HasForeignKey(d => d.EducationLevelId)
                    .HasConstraintName("FK_PartyEducation_EducationLevel");

                entity.HasOne(d => d.FacultyType)
                    .WithMany(p => p.PersonEducation)
                    .HasForeignKey(d => d.FacultyTypeId)
                    .HasConstraintName("FK_PersonEducation_FacultyType");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonEducation)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonEducation_Person");
            });


            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.ProcessId)
                    .HasColumnName("ProcessID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessName).HasMaxLength(450);
            });


            modelBuilder.Entity<ProcessHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.HistoryId)
                    .HasColumnName("HistoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.Remarks).HasColumnType("text");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.Property(e => e.SubProcessId).HasColumnName("SubProcessID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);
            });


            modelBuilder.Entity<ProcessProgress>(entity =>
            {
                entity.Property(e => e.ProcessProgressId)
                    .HasColumnName("ProcessProgressID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.ProcessStatusId).HasColumnName("ProcessStatusID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.Property(e => e.SubProcessId).HasColumnName("SubProcessID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.ProcessProgress)
                    .HasForeignKey(d => d.ProcessId)
                    .HasConstraintName("FK_ProcessProgress_Process");

                entity.HasOne(d => d.ProcessStatus)
                    .WithMany(p => p.ProcessProgress)
                    .HasForeignKey(d => d.ProcessStatusId)
                    .HasConstraintName("FK_ProcessProgress_zProcessStatus");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.ProcessProgress)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_ProcessProgress_School");

                entity.HasOne(d => d.SubProcess)
                    .WithMany(p => p.ProcessProgress)
                    .HasForeignKey(d => d.SubProcessId)
                    .HasConstraintName("FK_ProcessProgress_SubProcess");
            });


            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.SchoolId)
                    .HasColumnName("SchoolID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.LaboratoryMaterialTypeId).HasColumnName("LaboratoryMaterialTypeID");

                entity.Property(e => e.Nboards).HasColumnName("NBoards");

                entity.Property(e => e.Ncomputers).HasColumnName("NComputers");

                entity.Property(e => e.Nrooms).HasColumnName("NRooms");

                entity.Property(e => e.NstudentDeskChair).HasColumnName("NStudentDeskChair");

                entity.Property(e => e.NteachDeskChair).HasColumnName("NTeachDeskChair");

                entity.Property(e => e.NwashRooms).HasColumnName("NWashRooms");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.SchoolGenderTypeId).HasColumnName("SchoolGenderTypeID");

                entity.Property(e => e.SchoolLevelId).HasColumnName("SchoolLevelID");

                entity.Property(e => e.SchoolName).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

          

                entity.HasOne(d => d.LaboratoryMaterialType)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.LaboratoryMaterialTypeId)
                    .HasConstraintName("FK_School_zLaboratoryTypeID");

                entity.HasOne(d => d.SchoolGenderType)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.SchoolGenderTypeId)
                    .HasConstraintName("FK_School_zSchoolGenderType");

                entity.HasOne(d => d.SchoolNavigation)
                    .WithOne(p => p.School)
                    .HasForeignKey<School>(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_School_Party");

                entity.HasOne(d => d.SchoolLevel)
                    .WithMany(p => p.School)
                    .HasForeignKey(d => d.SchoolLevelId)
                    .HasConstraintName("FK_School_zSchoolLevel");
            });


            modelBuilder.Entity<SchoolCheckList>(entity =>
            {
                entity.Property(e => e.SchoolCheckListId)
                    .HasColumnName("SchoolCheckListID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.SchoolIndicatorId).HasColumnName("SchoolIndicatorID");

                entity.Property(e => e.SchoolQualityLevelId).HasColumnName("SchoolQualityLevelID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolCheckList)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolCheckList_School");

                entity.HasOne(d => d.SchoolIndicator)
                    .WithMany(p => p.SchoolCheckList)
                    .HasForeignKey(d => d.SchoolIndicatorId)
                    .HasConstraintName("FK_SchoolCheckList_zSchoolIndicator");

                entity.HasOne(d => d.SchoolQualityLevel)
                    .WithMany(p => p.SchoolCheckList)
                    .HasForeignKey(d => d.SchoolQualityLevelId)
                    .HasConstraintName("FK_SchoolCheckList_SchoolQualityLevel");
            });

            modelBuilder.Entity<SchoolFinancialResource>(entity =>
            {
                entity.HasKey(e => e.SchoolId)
                    .HasName("PK_PartyFinancialResources");

                entity.Property(e => e.SchoolId)
                    .HasColumnName("SchoolID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FundingSourceName).HasMaxLength(250);

                entity.Property(e => e.SchoolBussinessTypeId).HasColumnName("SchoolBussinessTypeID");

                entity.HasOne(d => d.SchoolBussinessType)
                    .WithMany(p => p.SchoolFinancialResource)
                    .HasForeignKey(d => d.SchoolBussinessTypeId)
                    .HasConstraintName("FK_SchoolFinancialResource_SchoolBussinessType");

                entity.HasOne(d => d.School)
                    .WithOne(p => p.SchoolFinancialResource)
                    .HasForeignKey<SchoolFinancialResource>(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolFinancialResources_School");
            });


            modelBuilder.Entity<SchoolLicense>(entity =>
            {
                entity.Property(e => e.SchoolLicenseId)
                    .HasColumnName("SchoolLicenseID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpirateDate).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseNumber).HasMaxLength(50);

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolLicense)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolLicense_School");
            });

            modelBuilder.Entity<SchoolOtherExpenses>(entity =>
            {
                entity.HasKey(e => e.OtherExpenseId)
                    .HasName("PK_OtherExpenses");

                entity.Property(e => e.OtherExpenseId)
                    .HasColumnName("OtherExpenseID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpensePerMonth).HasColumnType("money");

                entity.Property(e => e.OtherExpenseTypeId).HasColumnName("OtherExpenseTypeID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.OtherExpenseType)
                    .WithMany(p => p.SchoolOtherExpenses)
                    .HasForeignKey(d => d.OtherExpenseTypeId)
                    .HasConstraintName("FK_OtherExpenses_zOtherExpenseType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolOtherExpenses)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_OtherExpenses_School");
            });

            modelBuilder.Entity<SchoolStaffExpenses>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PartyRoleTypeId).HasColumnName("PartyRoleTypeID");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.HasOne(d => d.PartyRoleType)
                    .WithMany(p => p.SchoolStaffExpenses)
                    .HasForeignKey(d => d.PartyRoleTypeId)
                    .HasConstraintName("FK_StaffFinancialPlan_zPatyRoleType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolStaffExpenses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolStaffExpenses_School");
            });

            modelBuilder.Entity<SchoolFinancialPlan>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdmissionFee).HasColumnType("money");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.FeeAmount).HasColumnType("money");

                entity.Property(e => e.NfreeStudents).HasColumnName("NFreeStudents");

                entity.Property(e => e.NpaidStudents).HasColumnName("NPaidStudents");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.SchoolSubLevelId).HasColumnName("SchoolSubLevelID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.Year).HasMaxLength(50);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolFinancialPlan)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolFinancialPlan_School1");

                entity.HasOne(d => d.SchoolSubLevel)
                    .WithMany(p => p.SchoolFinancialPlan)
                    .HasForeignKey(d => d.SchoolSubLevelId)
                    .HasConstraintName("FK_SchoolFinancialPlan_zSchoolSubLevel");
            });



            modelBuilder.Entity<StudentEnrollmentPlan>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenderTypeId).HasColumnName("GenderTypeID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.SchoolSubLevelId).HasColumnName("SchoolSubLevelID");

                entity.Property(e => e.Year).HasMaxLength(50);

                entity.HasOne(d => d.GenderType)
                    .WithMany(p => p.StudentEnrollmentPlan)
                    .HasForeignKey(d => d.GenderTypeId)
                    .HasConstraintName("FK_StudentEnrollmentPlan_GenderType");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.StudentEnrollmentPlan)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentEnrollmentPlan_School");

                entity.HasOne(d => d.SchoolSubLevel)
                    .WithMany(p => p.StudentEnrollmentPlan)
                    .HasForeignKey(d => d.SchoolSubLevelId)
                    .HasConstraintName("FK_StudentEnrollmentPlan_SchoolSubLevel");
            });

            modelBuilder.Entity<SubProcess>(entity =>
            {
                entity.Property(e => e.SubProcessId)
                    .HasColumnName("SubProcessID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(450);

                entity.Property(e => e.SubProcesName).HasMaxLength(450);

                entity.Property(e => e.SubProcesNameDari).HasMaxLength(450);

                entity.HasOne(d => d.Process)
                    .WithMany(p => p.SubProcess)
                    .HasForeignKey(d => d.ProcessId)
                    .HasConstraintName("FK_SubProcess_Process");
            });


            modelBuilder.Entity<ZAddressType>(entity =>
            {
                entity.HasKey(e => e.AddressTypeId)
                    .HasName("PK_AddressType");

                entity.ToTable("zAddressType");

                entity.Property(e => e.AddressTypeId)
                    .HasColumnName("AddressTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressTypeName).HasMaxLength(50);

                entity.Property(e => e.AddressTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZContactMechanismType>(entity =>
            {
                entity.HasKey(e => e.ContactMechanismTypeId)
                    .HasName("PK_ContactMechanismType");

                entity.ToTable("zContactMechanismType");

                entity.Property(e => e.ContactMechanismTypeId)
                    .HasColumnName("ContactMechanismTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContactMechanismTypeName).HasMaxLength(50);

                entity.Property(e => e.ContactMechanismTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZDistrict>(entity =>
            {
                entity.HasKey(e => e.DistrictId)
                    .HasName("PK_zDistricts");

                entity.ToTable("zDistrict");

                entity.Property(e => e.DistrictId)
                    .HasColumnName("DistrictID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DistNaDar)
                    .HasColumnName("DIST_NA_DAR")
                    .HasMaxLength(255);

                entity.Property(e => e.DistNaEng)
                    .HasColumnName("DIST_NA_ENG")
                    .HasMaxLength(255);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.ZDistrict)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_zDistrict_zProvince");
            });

            modelBuilder.Entity<ZDocCategory>(entity =>
            {
                entity.HasKey(e => e.DocCategoryId)
                    .HasName("PK_DocCategory");

                entity.ToTable("zDocCategory");

                entity.Property(e => e.DocCategoryId)
                    .HasColumnName("DocCategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CatagoryName).HasMaxLength(50);

                entity.Property(e => e.CatagoryNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZDocType>(entity =>
            {
                entity.HasKey(e => e.DocTypeId)
                    .HasName("PK_DocType");

                entity.ToTable("zDocType");

                entity.Property(e => e.DocTypeId)
                    .HasColumnName("DocTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DocCategoryId).HasColumnName("DocCategoryID");

                entity.Property(e => e.DocTypeName).HasMaxLength(50);

                entity.Property(e => e.DocTypeNameDari).HasMaxLength(50);

                entity.Property(e => e.OrderNumber).HasMaxLength(50);

                entity.HasOne(d => d.DocCategory)
                    .WithMany(p => p.ZDocType)
                    .HasForeignKey(d => d.DocCategoryId)
                    .HasConstraintName("FK_DocType_DocCategory");
            });

            modelBuilder.Entity<ZEducationLevel>(entity =>
            {
                entity.HasKey(e => e.EducationLevelId)
                    .HasName("PK_EducationLevel");

                entity.ToTable("zEducationLevel");

                entity.Property(e => e.EducationLevelId)
                    .HasColumnName("EducationLevelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EducationLevelName).HasMaxLength(50);

                entity.Property(e => e.EducationLevelNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZEducationalRole>(entity =>
            {
                entity.HasKey(e => e.PartyRoleTypeId)
                    .HasName("PK_EducationalRoles");

                entity.ToTable("zEducationalRole");

                entity.Property(e => e.PartyRoleTypeId)
                    .HasColumnName("PartyRoleTypeID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.PartyRoleType)
                    .WithOne(p => p.ZEducationalRole)
                    .HasForeignKey<ZEducationalRole>(d => d.PartyRoleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EducationalRoles_PatyRoleType");
            });

            modelBuilder.Entity<ZFacultyType>(entity =>
            {
                entity.HasKey(e => e.FacultyTypeId)
                    .HasName("PK_FacultyType");

                entity.ToTable("zFacultyType");

                entity.Property(e => e.FacultyTypeId)
                    .HasColumnName("FacultyTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FacultypeName).HasMaxLength(50);

                entity.Property(e => e.FacultypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZGenderType>(entity =>
            {
                entity.HasKey(e => e.GenderTypeId)
                    .HasName("PK_GenderType");

                entity.ToTable("zGenderType");

                entity.Property(e => e.GenderTypeId)
                    .HasColumnName("GenderTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GenderTypeName).HasMaxLength(50);

                entity.Property(e => e.GenderTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZLaboratoryMaterialType>(entity =>
            {
                entity.HasKey(e => e.LaboratoryMaterialTypeId)
                    .HasName("PK_zLaboratoryTypeID");

                entity.ToTable("zLaboratoryMaterialType");

                entity.Property(e => e.LaboratoryMaterialTypeId)
                    .HasColumnName("LaboratoryMaterialTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LaboratoryMaterialTypeName).HasMaxLength(50);

                entity.Property(e => e.LaboratoryMaterialTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZOtherExpenseType>(entity =>
            {
                entity.HasKey(e => e.OtherExpenseTypeId);

                entity.ToTable("zOtherExpenseType");

                entity.Property(e => e.OtherExpenseTypeId)
                    .HasColumnName("OtherExpenseTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OtherExpenseTypeName).HasMaxLength(50);

                entity.Property(e => e.OtherExpenseTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZPartyRoleType>(entity =>
            {
                entity.HasKey(e => e.PartyRoleTypeId)
                    .HasName("PK_PatyRoleType");

                entity.ToTable("zPartyRoleType");

                entity.Property(e => e.PartyRoleTypeId)
                    .HasColumnName("PartyRoleTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PartyRoleTypeName).HasMaxLength(50);

                entity.Property(e => e.PartyRoleTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId)
                    .HasName("PK_Table_1");

                entity.ToTable("zPayment");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("PaymentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AmountInLetters).HasMaxLength(50);

                entity.Property(e => e.AmountInNumbers).HasColumnType("money");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.StatusType)
                    .WithMany(p => p.ZPayment)
                    .HasForeignKey(d => d.StatusTypeId)
                    .HasConstraintName("FK_zAmountPayable_zStatusType");
            });

            modelBuilder.Entity<ZProcessStatus>(entity =>
            {
                entity.HasKey(e => e.ProcessStatusId)
                    .HasName("PK_zApplicationStatusType");

                entity.ToTable("zProcessStatus");

                entity.Property(e => e.ProcessStatusId)
                    .HasColumnName("ProcessStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusName).HasMaxLength(250);

                entity.Property(e => e.StatusNameDari).HasMaxLength(250);

                entity.Property(e => e.StatusNameDariPast).HasMaxLength(250);

                entity.Property(e => e.StatusNameDash).HasMaxLength(250);

                entity.Property(e => e.StatusNameDashDari).HasMaxLength(250);

                entity.Property(e => e.StatusNamePast).HasMaxLength(250);
            });


            modelBuilder.Entity<ZProvince>(entity =>
            {
                entity.HasKey(e => e.ProvinceId)
                    .HasName("PK_zProvinces");

                entity.ToTable("zProvince");

                entity.Property(e => e.ProvinceId)
                    .HasColumnName("ProvinceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvNaDar)
                    .HasColumnName("PROV_NA_DAR")
                    .HasMaxLength(255);

                entity.Property(e => e.ProvNaEng)
                    .HasColumnName("PROV_NA_ENG")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ZSchoolBussinessType>(entity =>
            {
                entity.HasKey(e => e.SchoolBussinessTypeId)
                    .HasName("PK_SchoolBussinessType");

                entity.ToTable("zSchoolBussinessType");

                entity.Property(e => e.SchoolBussinessTypeId)
                    .HasColumnName("SchoolBussinessTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BussinessTypeName).HasMaxLength(50);

                entity.Property(e => e.BussinessTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZSchoolGenderType>(entity =>
            {
                entity.HasKey(e => e.SchoolGenderTypeId);

                entity.ToTable("zSchoolGenderType");

                entity.Property(e => e.SchoolGenderTypeId)
                    .HasColumnName("SchoolGenderTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderNumber).HasMaxLength(50);

                entity.Property(e => e.SchoolGenderTypeName).HasMaxLength(50);

                entity.Property(e => e.SchoolGenderTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZSchoolIndicator>(entity =>
            {
                entity.HasKey(e => e.SchoolIndicatorId);

                entity.ToTable("zSchoolIndicator");

                entity.Property(e => e.SchoolIndicatorId)
                    .HasColumnName("SchoolIndicatorID")
                    .ValueGeneratedNever();

                entity.Property(e => e.IndicatorName).HasMaxLength(250);
            });
            modelBuilder.Entity<ZSchoolLevel>(entity =>
            {
                entity.HasKey(e => e.SchoolLevelId)
                    .HasName("PK_SchoolLevel");

                entity.ToTable("zSchoolLevel");

                entity.Property(e => e.SchoolLevelId)
                    .HasColumnName("SchoolLevelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SchoolLevelName).HasMaxLength(50);

                entity.Property(e => e.SchoolLevelNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZSchoolLevelSubLevel>(entity =>
            {
                entity.ToTable("zSchoolLevelSubLevel");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SchoolLevelId).HasColumnName("SchoolLevelID");

                entity.Property(e => e.SchoolSubLevelId).HasColumnName("SchoolSubLevelID");

                entity.HasOne(d => d.SchoolLevel)
                    .WithMany(p => p.ZSchoolLevelSubLevel)
                    .HasForeignKey(d => d.SchoolLevelId)
                    .HasConstraintName("FK_zSchoolLevelSubLevel_zSchoolLevel");

                entity.HasOne(d => d.SchoolSubLevel)
                    .WithMany(p => p.ZSchoolLevelSubLevel)
                    .HasForeignKey(d => d.SchoolSubLevelId)
                    .HasConstraintName("FK_zSchoolLevelSubLevel_zSchoolSubLevel");
            });



            modelBuilder.Entity<ZSchoolQualityLevel>(entity =>
            {
                entity.HasKey(e => e.SchoolQualityLevelId)
                    .HasName("PK_SchoolQualityLevel");

                entity.ToTable("zSchoolQualityLevel");

                entity.Property(e => e.SchoolQualityLevelId)
                    .HasColumnName("SchoolQualityLevelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LevelName).HasMaxLength(50);

                entity.Property(e => e.LevelNameDari).HasMaxLength(50);

                entity.Property(e => e.OrderNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<ZSchoolSubLevel>(entity =>
            {
                entity.HasKey(e => e.SchoolSubLevelId)
                    .HasName("PK_SchoolSubLevel");

                entity.ToTable("zSchoolSubLevel");

                entity.Property(e => e.SchoolSubLevelId)
                    .HasColumnName("SchoolSubLevelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SubLevelName).HasMaxLength(50);

                entity.Property(e => e.SubLevelNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZStatusType>(entity =>
            {
                entity.HasKey(e => e.StatusTypeId);

                entity.ToTable("zStatusType");

                entity.Property(e => e.StatusTypeId)
                    .HasColumnName("StatusTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderNumber).HasMaxLength(50);

                entity.Property(e => e.StatusTypeName).HasMaxLength(50);

                entity.Property(e => e.StatusTypeNameDari).HasMaxLength(50);
            });

            modelBuilder.Entity<ZVillageNahia>(entity =>
            {
                entity.HasKey(e => e.VillageNahiaId)
                    .HasName("PK_zVillagesNahias");

                entity.ToTable("zVillageNahia");

                entity.Property(e => e.VillageNahiaId)
                    .HasColumnName("VillageNahiaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AfgUid)
                    .HasColumnName("AFG_UID")
                    .HasMaxLength(255);

                entity.Property(e => e.Center)
                    .HasColumnName("CENTER")
                    .HasMaxLength(255);

                entity.Property(e => e.CntrCode).HasColumnName("CNTR_CODE");

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Elevation).HasColumnName("ELEVATION");

                entity.Property(e => e.LangCode).HasColumnName("LANG_CODE");

                entity.Property(e => e.Language)
                    .HasColumnName("LANGUAGE_")
                    .HasMaxLength(255);

                entity.Property(e => e.LatY).HasColumnName("LAT_Y");

                entity.Property(e => e.LonX).HasColumnName("LON_X");

                entity.Property(e => e.MistiDistCode)
                    .HasColumnName("MISTI_DIST_CODE")
                    .HasMaxLength(255);

                entity.Property(e => e.MistiProvCode)
                    .HasColumnName("MISTI_PROV_CODE")
                    .HasMaxLength(255);

                entity.Property(e => e.Population).HasColumnName("POPULATION");

                entity.Property(e => e.VilUid)
                    .HasColumnName("VIL_UID")
                    .HasMaxLength(255);

                entity.Property(e => e.VillageNameEng)
                    .HasColumnName("VILLAGE_NAME_ENG")
                    .HasMaxLength(255);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.ZVillageNahia)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_zVillageNahia_zDistrict");
            });

        }

    }
}
