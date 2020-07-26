using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OLS.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstnName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    PartyID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.PartyID);
                });

            migrationBuilder.CreateTable(
                name: "zAddressType",
                columns: table => new
                {
                    AddressTypeID = table.Column<Guid>(nullable: false),
                    AddressTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    AddressTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.AddressTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zApplicationStatusType",
                columns: table => new
                {
                    ApplicationStatusTypeID = table.Column<Guid>(nullable: false),
                    StatusTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zApplicationStatusType", x => x.ApplicationStatusTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zContactMechanismType",
                columns: table => new
                {
                    ContactMechanismTypeID = table.Column<Guid>(nullable: false),
                    ContactMechanismTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    ContactMechanismTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMechanismType", x => x.ContactMechanismTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zDocCategory",
                columns: table => new
                {
                    DocCategoryID = table.Column<Guid>(nullable: false),
                    CatagoryName = table.Column<string>(maxLength: 50, nullable: true),
                    CatagoryNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocCategory", x => x.DocCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "zEducationLevel",
                columns: table => new
                {
                    EducationLevelID = table.Column<Guid>(nullable: false),
                    EducationLevelName = table.Column<string>(maxLength: 50, nullable: true),
                    EducationLevelNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.EducationLevelID);
                });

            migrationBuilder.CreateTable(
                name: "zFacultyType",
                columns: table => new
                {
                    FacultyTypeID = table.Column<Guid>(nullable: false),
                    FacultypeName = table.Column<string>(maxLength: 50, nullable: true),
                    FacultypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyType", x => x.FacultyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zGenderType",
                columns: table => new
                {
                    GenderTypeID = table.Column<Guid>(nullable: false),
                    GenderTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    GenderTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderType", x => x.GenderTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zLaboratoryMaterialType",
                columns: table => new
                {
                    LaboratoryMaterialTypeID = table.Column<Guid>(nullable: false),
                    LaboratoryMaterialTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    LaboratoryMaterialTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zLaboratoryTypeID", x => x.LaboratoryMaterialTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zOtherExpenseType",
                columns: table => new
                {
                    OtherExpenseTypeID = table.Column<Guid>(nullable: false),
                    OtherExpenseTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    OtherExpenseTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zOtherExpenseType", x => x.OtherExpenseTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zPartyRoleType",
                columns: table => new
                {
                    PartyRoleTypeID = table.Column<Guid>(nullable: false),
                    PartyRoleTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    PartyRoleTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatyRoleType", x => x.PartyRoleTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zProvince",
                columns: table => new
                {
                    ProvinceID = table.Column<Guid>(nullable: false),
                    PROV_NA_ENG = table.Column<string>(maxLength: 255, nullable: true),
                    PROV_NA_DAR = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zProvinces", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolBussinessType",
                columns: table => new
                {
                    SchoolBussinessTypeID = table.Column<Guid>(nullable: false),
                    BussinessTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    BussinessTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolBussinessType", x => x.SchoolBussinessTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolGenderType",
                columns: table => new
                {
                    SchoolGenderTypeID = table.Column<Guid>(nullable: false),
                    SchoolGenderTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    SchoolGenderTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zSchoolGenderType", x => x.SchoolGenderTypeID);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolIndicator",
                columns: table => new
                {
                    SchoolIndicatorID = table.Column<Guid>(nullable: false),
                    IndicatorName = table.Column<string>(maxLength: 250, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zSchoolIndicator", x => x.SchoolIndicatorID);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolLevel",
                columns: table => new
                {
                    SchoolLevelID = table.Column<Guid>(nullable: false),
                    SchoolLevelName = table.Column<string>(maxLength: 50, nullable: true),
                    SchoolLevelNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolLevel", x => x.SchoolLevelID);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolQualityLevel",
                columns: table => new
                {
                    SchoolQualityLevelID = table.Column<Guid>(nullable: false),
                    LevelName = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: true),
                    LevelNameDari = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolQualityLevel", x => x.SchoolQualityLevelID);
                });

            migrationBuilder.CreateTable(
                name: "zStatusType",
                columns: table => new
                {
                    StatusTypeID = table.Column<Guid>(nullable: false),
                    StatusTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    StatusTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zStatusType", x => x.StatusTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    ContactDetailID = table.Column<Guid>(nullable: false),
                    PartyID = table.Column<Guid>(nullable: false),
                    ContactMechanismTypeID = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails_1", x => x.ContactDetailID);
                    table.ForeignKey(
                        name: "FK_ContactDetails_ContactMechanismType",
                        column: x => x.ContactMechanismTypeID,
                        principalTable: "zContactMechanismType",
                        principalColumn: "ContactMechanismTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactDetails_Party",
                        column: x => x.PartyID,
                        principalTable: "Party",
                        principalColumn: "PartyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zDocType",
                columns: table => new
                {
                    DocTypeID = table.Column<Guid>(nullable: false),
                    DocCategoryID = table.Column<Guid>(nullable: true),
                    DocTypeName = table.Column<string>(maxLength: 50, nullable: true),
                    DocTypeNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocType", x => x.DocTypeID);
                    table.ForeignKey(
                        name: "FK_DocType_DocCategory",
                        column: x => x.DocCategoryID,
                        principalTable: "zDocCategory",
                        principalColumn: "DocCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zEducationalRole",
                columns: table => new
                {
                    PartyRoleTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalRoles", x => x.PartyRoleTypeID);
                    table.ForeignKey(
                        name: "FK_EducationalRoles_PatyRoleType",
                        column: x => x.PartyRoleTypeID,
                        principalTable: "zPartyRoleType",
                        principalColumn: "PartyRoleTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zDistrict",
                columns: table => new
                {
                    DistrictID = table.Column<Guid>(nullable: false),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    DIST_NA_ENG = table.Column<string>(maxLength: 255, nullable: true),
                    DIST_NA_DAR = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zDistricts", x => x.DistrictID);
                    table.ForeignKey(
                        name: "FK_zDistrict_zProvince",
                        column: x => x.ProvinceID,
                        principalTable: "zProvince",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    SchoolLevelID = table.Column<Guid>(nullable: true),
                    SchoolName = table.Column<string>(maxLength: 50, nullable: true),
                    IsAcceptingCommitment = table.Column<byte>(nullable: true),
                    SchoolGenderTypeID = table.Column<Guid>(nullable: true),
                    NRooms = table.Column<int>(nullable: true),
                    DistancefromPuSchool = table.Column<int>(nullable: true),
                    DistanceFromPrSchool = table.Column<int>(nullable: true),
                    HasTeachingBooks = table.Column<byte>(nullable: true),
                    HasTeachingAids = table.Column<byte>(nullable: true),
                    NTeachDeskChair = table.Column<int>(nullable: true),
                    NStudentDeskChair = table.Column<int>(nullable: true),
                    HasLibrary = table.Column<byte>(nullable: true),
                    Nbooks = table.Column<int>(nullable: true),
                    NBoards = table.Column<int>(nullable: true),
                    LaboratoryMaterialTypeID = table.Column<Guid>(nullable: true),
                    HasComputerLab = table.Column<byte>(nullable: true),
                    NComputers = table.Column<int>(nullable: true),
                    AdmissionFee = table.Column<decimal>(type: "money", nullable: true),
                    HasDrinkingWater = table.Column<byte>(nullable: true),
                    NWashRooms = table.Column<int>(nullable: true),
                    HasFirstAid = table.Column<byte>(nullable: true),
                    HasFireDistinguisher = table.Column<byte>(nullable: true),
                    HasTransportation = table.Column<byte>(nullable: true),
                    HasSportFacilities = table.Column<byte>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_School_zLaboratoryTypeID",
                        column: x => x.LaboratoryMaterialTypeID,
                        principalTable: "zLaboratoryMaterialType",
                        principalColumn: "LaboratoryMaterialTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_zSchoolGenderType",
                        column: x => x.SchoolGenderTypeID,
                        principalTable: "zSchoolGenderType",
                        principalColumn: "SchoolGenderTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_Party",
                        column: x => x.SchoolID,
                        principalTable: "Party",
                        principalColumn: "PartyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_zSchoolLevel",
                        column: x => x.SchoolLevelID,
                        principalTable: "zSchoolLevel",
                        principalColumn: "SchoolLevelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zSchoolSubLevel",
                columns: table => new
                {
                    SchoolSubLevelID = table.Column<Guid>(nullable: false),
                    SchoolLevelID = table.Column<Guid>(nullable: true),
                    SubLevelName = table.Column<string>(maxLength: 50, nullable: true),
                    SubLevelNameDari = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSubLevel", x => x.SchoolSubLevelID);
                    table.ForeignKey(
                        name: "FK_SchoolSubLevel_SchoolLevel",
                        column: x => x.SchoolLevelID,
                        principalTable: "zSchoolLevel",
                        principalColumn: "SchoolLevelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zPayment",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(nullable: false),
                    AmountInNumbers = table.Column<decimal>(type: "money", nullable: true),
                    AmountInLetters = table.Column<string>(maxLength: 50, nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusTypeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_zAmountPayable_zStatusType",
                        column: x => x.StatusTypeID,
                        principalTable: "zStatusType",
                        principalColumn: "StatusTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyDocument",
                columns: table => new
                {
                    PartyID = table.Column<Guid>(nullable: false),
                    DocTypeID = table.Column<Guid>(nullable: true),
                    DocUrl = table.Column<string>(maxLength: 50, nullable: true),
                    DocCategoryID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyDocuments", x => x.PartyID);
                    table.ForeignKey(
                        name: "FK_PartyDocuments_DocCategory",
                        column: x => x.DocCategoryID,
                        principalTable: "zDocCategory",
                        principalColumn: "DocCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyDocuments_DocType",
                        column: x => x.DocTypeID,
                        principalTable: "zDocType",
                        principalColumn: "DocTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyDocuments_Party",
                        column: x => x.PartyID,
                        principalTable: "Party",
                        principalColumn: "PartyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "zVillageNahia",
                columns: table => new
                {
                    VillageNahiaID = table.Column<Guid>(nullable: false),
                    DistrictID = table.Column<Guid>(nullable: true),
                    VILLAGE_NAME_ENG = table.Column<string>(maxLength: 255, nullable: true),
                    MISTI_PROV_CODE = table.Column<string>(maxLength: 255, nullable: true),
                    MISTI_DIST_CODE = table.Column<string>(maxLength: 255, nullable: true),
                    VIL_UID = table.Column<string>(maxLength: 255, nullable: true),
                    CENTER = table.Column<string>(maxLength: 255, nullable: true),
                    CNTR_CODE = table.Column<double>(nullable: true),
                    AFG_UID = table.Column<string>(maxLength: 255, nullable: true),
                    POPULATION = table.Column<double>(nullable: true),
                    LANGUAGE_ = table.Column<string>(maxLength: 255, nullable: true),
                    LANG_CODE = table.Column<double>(nullable: true),
                    ELEVATION = table.Column<double>(nullable: true),
                    LAT_Y = table.Column<double>(nullable: true),
                    LON_X = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zVillagesNahias", x => x.VillageNahiaID);
                    table.ForeignKey(
                        name: "FK_zVillageNahia_zDistrict",
                        column: x => x.DistrictID,
                        principalTable: "zDistrict",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    ApplicationStatusID = table.Column<Guid>(nullable: false),
                    ApplicationStatusTypeID = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    SchoolID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.ApplicationStatusID);
                    table.ForeignKey(
                        name: "FK_ApplicationStatus_zApplicationStatusType",
                        column: x => x.ApplicationStatusTypeID,
                        principalTable: "zApplicationStatusType",
                        principalColumn: "ApplicationStatusTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationStatus_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicensePayment",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    RecieptNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePayment", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_LicensePayment_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(maxLength: 50, nullable: true),
                    GrandFatherName = table.Column<string>(maxLength: 50, nullable: true),
                    NIDNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Age = table.Column<int>(nullable: true),
                    EDUService = table.Column<int>(nullable: true),
                    Photo = table.Column<string>(maxLength: 250, nullable: true),
                    GenderTypeID = table.Column<Guid>(nullable: true),
                    PartyRoleTypeID = table.Column<Guid>(nullable: true),
                    SchoolID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_zGenderType",
                        column: x => x.GenderTypeID,
                        principalTable: "zGenderType",
                        principalColumn: "GenderTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_PatyRoleType",
                        column: x => x.PartyRoleTypeID,
                        principalTable: "zPartyRoleType",
                        principalColumn: "PartyRoleTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Party",
                        column: x => x.PersonID,
                        principalTable: "Party",
                        principalColumn: "PartyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolCheckList",
                columns: table => new
                {
                    SchoolCheckListID = table.Column<Guid>(nullable: false),
                    SchoolID = table.Column<Guid>(nullable: true),
                    SchoolIndicatorID = table.Column<Guid>(nullable: true),
                    SchoolQualityLevelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCheckList", x => x.SchoolCheckListID);
                    table.ForeignKey(
                        name: "FK_SchoolCheckList_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolCheckList_zSchoolIndicator",
                        column: x => x.SchoolIndicatorID,
                        principalTable: "zSchoolIndicator",
                        principalColumn: "SchoolIndicatorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolCheckList_SchoolQualityLevel",
                        column: x => x.SchoolQualityLevelID,
                        principalTable: "zSchoolQualityLevel",
                        principalColumn: "SchoolQualityLevelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolFinancialResource",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    SchoolBussinessTypeID = table.Column<Guid>(nullable: true),
                    FundingSourceName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyFinancialResources", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_SchoolFinancialResource_SchoolBussinessType",
                        column: x => x.SchoolBussinessTypeID,
                        principalTable: "zSchoolBussinessType",
                        principalColumn: "SchoolBussinessTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolFinancialResources_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolLicense",
                columns: table => new
                {
                    SchoolLicenseID = table.Column<Guid>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SchoolID = table.Column<Guid>(nullable: true),
                    ExpirateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolLicense", x => x.SchoolLicenseID);
                    table.ForeignKey(
                        name: "FK_SchoolLicense_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolOtherExpenses",
                columns: table => new
                {
                    OtherExpenseID = table.Column<Guid>(nullable: false),
                    SchoolID = table.Column<Guid>(nullable: true),
                    OtherExpenseTypeID = table.Column<Guid>(nullable: true),
                    ExpensePerMonth = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherExpenses", x => x.OtherExpenseID);
                    table.ForeignKey(
                        name: "FK_OtherExpenses_zOtherExpenseType",
                        column: x => x.OtherExpenseTypeID,
                        principalTable: "zOtherExpenseType",
                        principalColumn: "OtherExpenseTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OtherExpenses_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaffExpenses",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    SchoolSubLevelID = table.Column<Guid>(nullable: true),
                    NFreeStudents = table.Column<int>(nullable: true),
                    NPaidStudents = table.Column<int>(nullable: true),
                    FeeAmount = table.Column<int>(nullable: true),
                    Year = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolFinancialPlan", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_SchoolFinancialPlan_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffFinancialPlan",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    PartyRoleTypeID = table.Column<Guid>(nullable: true),
                    Salary = table.Column<decimal>(type: "money", nullable: true),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true),
                    Amount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffFinancialPlan", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_StaffFinancialPlan_zPatyRoleType",
                        column: x => x.PartyRoleTypeID,
                        principalTable: "zPartyRoleType",
                        principalColumn: "PartyRoleTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffFinancialPlan_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollmentPlan",
                columns: table => new
                {
                    SchoolID = table.Column<Guid>(nullable: false),
                    Year = table.Column<DateTime>(type: "datetime", nullable: true),
                    GenderTypeID = table.Column<Guid>(nullable: true),
                    SchoolSubLevelID = table.Column<Guid>(nullable: true),
                    NumberOfStudents = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollmentPlan", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_StudentEnrollmentPlan_GenderType",
                        column: x => x.GenderTypeID,
                        principalTable: "zGenderType",
                        principalColumn: "GenderTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrollmentPlan_School",
                        column: x => x.SchoolID,
                        principalTable: "School",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentEnrollmentPlan_SchoolSubLevel",
                        column: x => x.SchoolSubLevelID,
                        principalTable: "zSchoolSubLevel",
                        principalColumn: "SchoolSubLevelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyAddress",
                columns: table => new
                {
                    PartyAddressID = table.Column<Guid>(nullable: false),
                    PartyID = table.Column<Guid>(nullable: false),
                    AddressTypeID = table.Column<Guid>(nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    DistrictID = table.Column<Guid>(nullable: true),
                    VillageNahiaID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAddress", x => x.PartyAddressID);
                    table.ForeignKey(
                        name: "FK_PartyAddress_AddressType",
                        column: x => x.AddressTypeID,
                        principalTable: "zAddressType",
                        principalColumn: "AddressTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyAddress_zDistrict",
                        column: x => x.DistrictID,
                        principalTable: "zDistrict",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyAddress_Party",
                        column: x => x.PartyID,
                        principalTable: "Party",
                        principalColumn: "PartyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyAddress_zProvince",
                        column: x => x.ProvinceID,
                        principalTable: "zProvince",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyAddress_zVillageNahia",
                        column: x => x.VillageNahiaID,
                        principalTable: "zVillageNahia",
                        principalColumn: "VillageNahiaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonEducation",
                columns: table => new
                {
                    PersonEducationID = table.Column<Guid>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: false),
                    EducationLevelID = table.Column<Guid>(nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FacultyTypeID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEducation", x => x.PersonEducationID);
                    table.ForeignKey(
                        name: "FK_PartyEducation_EducationLevel",
                        column: x => x.EducationLevelID,
                        principalTable: "zEducationLevel",
                        principalColumn: "EducationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonEducation_FacultyType",
                        column: x => x.FacultyTypeID,
                        principalTable: "zFacultyType",
                        principalColumn: "FacultyTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonEducation_Person",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatus_ApplicationStatusTypeID",
                table: "ApplicationStatus",
                column: "ApplicationStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationStatus_SchoolID",
                table: "ApplicationStatus",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ContactMechanismTypeID",
                table: "ContactDetails",
                column: "ContactMechanismTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_PartyID",
                table: "ContactDetails",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "UniqueContactDetail",
                table: "ContactDetails",
                column: "Value",
                unique: true,
                filter: "[Value] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_AddressTypeID",
                table: "PartyAddress",
                column: "AddressTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_DistrictID",
                table: "PartyAddress",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_PartyID",
                table: "PartyAddress",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_ProvinceID",
                table: "PartyAddress",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_VillageNahiaID",
                table: "PartyAddress",
                column: "VillageNahiaID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyDocument_DocCategoryID",
                table: "PartyDocument",
                column: "DocCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyDocument_DocTypeID",
                table: "PartyDocument",
                column: "DocTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderTypeID",
                table: "Person",
                column: "GenderTypeID");

            migrationBuilder.CreateIndex(
                name: "UniquieNIDNumber",
                table: "Person",
                column: "NIDNumber",
                unique: true,
                filter: "[NIDNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PartyRoleTypeID",
                table: "Person",
                column: "PartyRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_SchoolID",
                table: "Person",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEducation_EducationLevelID",
                table: "PersonEducation",
                column: "EducationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEducation_FacultyTypeID",
                table: "PersonEducation",
                column: "FacultyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEducation_PersonID",
                table: "PersonEducation",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_School_LaboratoryMaterialTypeID",
                table: "School",
                column: "LaboratoryMaterialTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolGenderTypeID",
                table: "School",
                column: "SchoolGenderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolLevelID",
                table: "School",
                column: "SchoolLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCheckList_SchoolID",
                table: "SchoolCheckList",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCheckList_SchoolIndicatorID",
                table: "SchoolCheckList",
                column: "SchoolIndicatorID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolCheckList_SchoolQualityLevelID",
                table: "SchoolCheckList",
                column: "SchoolQualityLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolFinancialResource_SchoolBussinessTypeID",
                table: "SchoolFinancialResource",
                column: "SchoolBussinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolLicense_SchoolID",
                table: "SchoolLicense",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolOtherExpenses_OtherExpenseTypeID",
                table: "SchoolOtherExpenses",
                column: "OtherExpenseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolOtherExpenses_SchoolID",
                table: "SchoolOtherExpenses",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffFinancialPlan_PartyRoleTypeID",
                table: "StaffFinancialPlan",
                column: "PartyRoleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollmentPlan_GenderTypeID",
                table: "StudentEnrollmentPlan",
                column: "GenderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollmentPlan_SchoolSubLevelID",
                table: "StudentEnrollmentPlan",
                column: "SchoolSubLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_zDistrict_ProvinceID",
                table: "zDistrict",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_zDocType_DocCategoryID",
                table: "zDocType",
                column: "DocCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_zPayment_StatusTypeID",
                table: "zPayment",
                column: "StatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_zSchoolSubLevel_SchoolLevelID",
                table: "zSchoolSubLevel",
                column: "SchoolLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_zVillageNahia_DistrictID",
                table: "zVillageNahia",
                column: "DistrictID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationStatus");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "LicensePayment");

            migrationBuilder.DropTable(
                name: "PartyAddress");

            migrationBuilder.DropTable(
                name: "PartyDocument");

            migrationBuilder.DropTable(
                name: "PersonEducation");

            migrationBuilder.DropTable(
                name: "SchoolCheckList");

            migrationBuilder.DropTable(
                name: "SchoolFinancialResource");

            migrationBuilder.DropTable(
                name: "SchoolLicense");

            migrationBuilder.DropTable(
                name: "SchoolOtherExpenses");

            migrationBuilder.DropTable(
                name: "SchoolStaffExpenses");

            migrationBuilder.DropTable(
                name: "StaffFinancialPlan");

            migrationBuilder.DropTable(
                name: "StudentEnrollmentPlan");

            migrationBuilder.DropTable(
                name: "zEducationalRole");

            migrationBuilder.DropTable(
                name: "zPayment");

            migrationBuilder.DropTable(
                name: "zApplicationStatusType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "zContactMechanismType");

            migrationBuilder.DropTable(
                name: "zAddressType");

            migrationBuilder.DropTable(
                name: "zVillageNahia");

            migrationBuilder.DropTable(
                name: "zDocType");

            migrationBuilder.DropTable(
                name: "zEducationLevel");

            migrationBuilder.DropTable(
                name: "zFacultyType");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "zSchoolIndicator");

            migrationBuilder.DropTable(
                name: "zSchoolQualityLevel");

            migrationBuilder.DropTable(
                name: "zSchoolBussinessType");

            migrationBuilder.DropTable(
                name: "zOtherExpenseType");

            migrationBuilder.DropTable(
                name: "zSchoolSubLevel");

            migrationBuilder.DropTable(
                name: "zStatusType");

            migrationBuilder.DropTable(
                name: "zDistrict");

            migrationBuilder.DropTable(
                name: "zDocCategory");

            migrationBuilder.DropTable(
                name: "zGenderType");

            migrationBuilder.DropTable(
                name: "zPartyRoleType");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "zProvince");

            migrationBuilder.DropTable(
                name: "zLaboratoryMaterialType");

            migrationBuilder.DropTable(
                name: "zSchoolGenderType");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "zSchoolLevel");
        }
    }
}
