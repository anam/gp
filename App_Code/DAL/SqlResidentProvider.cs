using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class SqlResidentProvider:DataAccessObject
{
	public SqlResidentProvider()
    {
    }


    public bool DeleteResident(int residentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_DeleteResident", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (result == 1);
        }
    }

    public List<Resident> GetAllResidents()
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetAllResidents", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetResidentsFromReader(reader);
        }
    }
    public List<Resident> SearchResidents(string SearchString)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_SearchResidents", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@SearchString", SqlDbType.NVarChar).Value = SearchString;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.Default);

            return GetResidentsFromReader(reader);
        }
    }

    public List<Resident> GetResidentsFromReader(IDataReader reader)
    {
        List<Resident> residents = new List<Resident>();

        while (reader.Read())
        {
            residents.Add(GetResidentFromReader(reader));
        }
        return residents;
    }

    public Resident GetResidentFromReader(IDataReader reader)
    {
        try
        {
            Resident resident = new Resident
                (
                    (int)reader["ResidentID"],
                    reader["Name"].ToString(),
                    (DateTime)reader["AdmissionDate"],
                    reader["AdmissionFrom"].ToString(),
                    reader["UsualOccupation"].ToString(),
                    reader["PlaceOfBirth"].ToString(),
                    reader["UsualAddress"].ToString(),
                    reader["Telephone"].ToString(),
                    reader["Race"].ToString(),
                    (int)reader["Age"],
                    (DateTime)reader["DateOfBirth"],
                    reader["Sex"].ToString(),
                    reader["MaritalStatus"].ToString(),
                    reader["Height"].ToString(),
                    reader["Weight"].ToString(),
                    reader["Religion"].ToString(),
                    reader["Clergyman"].ToString(),
                    reader["ChurchSynagogue"].ToString(),
                    reader["ChurchSynagogueTelephone"].ToString(),
                    reader["Address"].ToString(),
                    reader["AddressTelephone"].ToString(),
                    reader["SocialSecurity"].ToString(),
                    reader["Medicare"].ToString(),
                    reader["Medicaid"].ToString(),
                    reader["Insurance"].ToString(),
                    reader["InsuranceAddress"].ToString(),
                    reader["InsuranceAddressTelephone"].ToString(),
                    reader["Policy"].ToString(),
                    reader["InsuranceGroup"].ToString(),
                    reader["InsuranceGroupNo"].ToString(),
                    reader["ResponsibleParty"].ToString(),
                    reader["ResponsiblePartyRelationship"].ToString(),
                    reader["ResponsiblePartyAddress"].ToString(),
                    reader["ResponsiblePartyTelephone"].ToString(),
                    reader["PowerOfAttorney"].ToString(),
                    reader["PowerOfAttorneyAddress"].ToString(),
                    reader["PowerOfAttorneyRelationship"].ToString(),
                    reader["PowerOfAttorneyTelephone"].ToString(),
                    reader["NearestRelativeOrGuardian"].ToString(),
                    reader["NearestRelativeOrGuardianRelationship"].ToString(),
                    reader["NearestRelativeOrGuardianAddress"].ToString(),
                    reader["NearestRelativeOrGuardianTelephone"].ToString(),
                    reader["NotifyInCaseOfEmergency"].ToString(),
                    reader["NotifyInCaseOfEmergencyRelationship"].ToString(),
                    reader["NotifyInCaseOfEmergencyAddress"].ToString(),
                    reader["NotifyInCaseOfEmergencyTelephone"].ToString(),
                    reader["HospitalPreference"].ToString(),
                    reader["HospitalPreferenceTelephone"].ToString(),
                    reader["HospitalPreferenceEmail"].ToString(),
                    reader["FuneralHomePreference"].ToString(),
                    reader["FuneralHomePreferenceTelephone"].ToString(),
                    reader["PharmacyPreference"].ToString(),
                    reader["PharmacyPreferenceTelephone"].ToString(),
                    reader["Dentist"].ToString(),
                    reader["DentistTelephone"].ToString(),
                    reader["AttendingPhysician"].ToString(),
                    reader["AttendingPhysicianAddress"].ToString(),
                    reader["AttendingPhysicianTelephone"].ToString(),
                    reader["AlternatePhysician"].ToString(),
                    reader["AlternatePhysicianAddress"].ToString(),
                    reader["AlternatePhysicianTelephone"].ToString(),
                    (DateTime)reader["DateofLastPhysicalExam"],
                    reader["YearlyPhysicalDue"].ToString(),
                    reader["Diagnosis"].ToString(),
                    reader["Allergies"].ToString(),
                    (DateTime)reader["DischargedOrExpiredDate"],
                    reader["DischargedOrExpiredReason"].ToString(),
                    (bool)reader["IsWithoutMDApproval"],
                    reader["ReleasedTo"].ToString(),
                    reader["NewAddress"].ToString(),
                    reader["PlaceOfDeathAddressnCitynCountynState"].ToString(),
                    reader["PrecinctNo"].ToString(),
                    reader["MorticianName"].ToString(),
                    reader["Signature"].ToString()
                );
            try { resident.ExtraField1 = reader["ExtraField1"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField2 = reader["ExtraField2"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField3 = reader["ExtraField3"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField4 = reader["ExtraField4"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField5 = reader["ExtraField5"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField6 = reader["PropertyName"].ToString(); }
            catch (Exception ex) { resident.ExtraField6 = ""; }
            try { resident.ExtraField7 = reader["PropertyAddress"].ToString(); }
            catch (Exception ex) { resident.ExtraField7 = ""; }
            try { resident.ExtraField8 = reader["ExtraField8"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField9 = reader["CompanyName"].ToString(); }
            catch (Exception ex) { }
            try { resident.ExtraField10 = reader["ExtraField10"].ToString(); }
            catch (Exception ex) { }

            try { resident.ExtraField11 = reader["Allergy"].ToString(); }
            catch (Exception ex) { }


             return resident;
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    public Resident GetResidentByID(int residentID)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand command = new SqlCommand("AL_GetResidentByID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResidentID", SqlDbType.Int).Value = residentID;
            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.Read())
            {
                return GetResidentFromReader(reader);
            }
            else
            {
                return null;
            }
        }
    }

    public int InsertResident(Resident resident)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_InsertResident", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = resident.Name;
            cmd.Parameters.Add("@AdmissionDate", SqlDbType.DateTime).Value = resident.AdmissionDate;
            cmd.Parameters.Add("@AdmissionFrom", SqlDbType.NVarChar).Value = resident.AdmissionFrom;
            cmd.Parameters.Add("@UsualOccupation", SqlDbType.NVarChar).Value = resident.UsualOccupation;
            cmd.Parameters.Add("@PlaceOfBirth", SqlDbType.NVarChar).Value = resident.PlaceOfBirth;
            cmd.Parameters.Add("@UsualAddress", SqlDbType.NVarChar).Value = resident.UsualAddress;
            cmd.Parameters.Add("@Telephone", SqlDbType.NVarChar).Value = resident.Telephone;
            cmd.Parameters.Add("@Race", SqlDbType.NVarChar).Value = resident.Race;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = resident.Age;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = resident.DateOfBirth;
            cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = resident.Sex;
            cmd.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar).Value = resident.MaritalStatus;
            cmd.Parameters.Add("@Height", SqlDbType.NChar).Value = resident.Height;
            cmd.Parameters.Add("@Weight", SqlDbType.NChar).Value = resident.Weight;
            cmd.Parameters.Add("@Religion", SqlDbType.NVarChar).Value = resident.Religion;
            cmd.Parameters.Add("@Clergyman", SqlDbType.NVarChar).Value = resident.Clergyman;
            cmd.Parameters.Add("@ChurchSynagogue", SqlDbType.NVarChar).Value = resident.ChurchSynagogue;
            cmd.Parameters.Add("@ChurchSynagogueTelephone", SqlDbType.NVarChar).Value = resident.ChurchSynagogueTelephone;
            cmd.Parameters.Add("@Address", SqlDbType.NText).Value = resident.Address;
            cmd.Parameters.Add("@AddressTelephone", SqlDbType.NVarChar).Value = resident.AddressTelephone;
            cmd.Parameters.Add("@SocialSecurity", SqlDbType.NVarChar).Value = resident.SocialSecurity;
            cmd.Parameters.Add("@Medicare", SqlDbType.NVarChar).Value = resident.Medicare;
            cmd.Parameters.Add("@Medicaid", SqlDbType.NVarChar).Value = resident.Medicaid;
            cmd.Parameters.Add("@Insurance", SqlDbType.NVarChar).Value = resident.Insurance;
            cmd.Parameters.Add("@InsuranceAddress", SqlDbType.NText).Value = resident.InsuranceAddress;
            cmd.Parameters.Add("@InsuranceAddressTelephone", SqlDbType.NVarChar).Value = resident.InsuranceAddressTelephone;
            cmd.Parameters.Add("@Policy", SqlDbType.NVarChar).Value = resident.Policy;
            cmd.Parameters.Add("@InsuranceGroup", SqlDbType.NVarChar).Value = resident.InsuranceGroup;
            cmd.Parameters.Add("@InsuranceGroupNo", SqlDbType.NVarChar).Value = resident.InsuranceGroupNo;
            cmd.Parameters.Add("@ResponsibleParty", SqlDbType.NVarChar).Value = resident.ResponsibleParty;
            cmd.Parameters.Add("@ResponsiblePartyRelationship", SqlDbType.NVarChar).Value = resident.ResponsiblePartyRelationship;
            cmd.Parameters.Add("@ResponsiblePartyAddress", SqlDbType.NText).Value = resident.ResponsiblePartyAddress;
            cmd.Parameters.Add("@ResponsiblePartyTelephone", SqlDbType.NVarChar).Value = resident.ResponsiblePartyTelephone;
            cmd.Parameters.Add("@PowerOfAttorney", SqlDbType.NVarChar).Value = resident.PowerOfAttorney;
            cmd.Parameters.Add("@PowerOfAttorneyAddress", SqlDbType.NText).Value = resident.PowerOfAttorneyAddress;
            cmd.Parameters.Add("@PowerOfAttorneyRelationship", SqlDbType.NVarChar).Value = resident.PowerOfAttorneyRelationship;
            cmd.Parameters.Add("@PowerOfAttorneyTelephone", SqlDbType.NVarChar).Value = resident.PowerOfAttorneyTelephone;
            cmd.Parameters.Add("@NearestRelativeOrGuardian", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardian;
            cmd.Parameters.Add("@NearestRelativeOrGuardianRelationship", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardianRelationship;
            cmd.Parameters.Add("@NearestRelativeOrGuardianAddress", SqlDbType.NText).Value = resident.NearestRelativeOrGuardianAddress;
            cmd.Parameters.Add("@NearestRelativeOrGuardianTelephone", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardianTelephone;
            cmd.Parameters.Add("@NotifyInCaseOfEmergency", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergency;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyRelationship", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyRelationship;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyAddress", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyAddress;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyTelephone", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyTelephone;
            cmd.Parameters.Add("@HospitalPreference", SqlDbType.NVarChar).Value = resident.HospitalPreference;
            cmd.Parameters.Add("@HospitalPreferenceTelephone", SqlDbType.NVarChar).Value = resident.HospitalPreferenceTelephone;
            cmd.Parameters.Add("@HospitalPreferenceEmail", SqlDbType.NVarChar).Value = resident.HospitalPreferenceEmail;
            cmd.Parameters.Add("@FuneralHomePreference", SqlDbType.NVarChar).Value = resident.FuneralHomePreference;
            cmd.Parameters.Add("@FuneralHomePreferenceTelephone", SqlDbType.NVarChar).Value = resident.FuneralHomePreferenceTelephone;
            cmd.Parameters.Add("@PharmacyPreference", SqlDbType.NVarChar).Value = resident.PharmacyPreference;
            cmd.Parameters.Add("@PharmacyPreferenceTelephone", SqlDbType.NVarChar).Value = resident.PharmacyPreferenceTelephone;
            cmd.Parameters.Add("@Dentist", SqlDbType.NVarChar).Value = resident.Dentist;
            cmd.Parameters.Add("@DentistTelephone", SqlDbType.NVarChar).Value = resident.DentistTelephone;
            cmd.Parameters.Add("@AttendingPhysician", SqlDbType.NVarChar).Value = resident.AttendingPhysician;
            cmd.Parameters.Add("@AttendingPhysicianAddress", SqlDbType.NText).Value = resident.AttendingPhysicianAddress;
            cmd.Parameters.Add("@AttendingPhysicianTelephone", SqlDbType.NVarChar).Value = resident.AttendingPhysicianTelephone;
            cmd.Parameters.Add("@AlternatePhysician", SqlDbType.NVarChar).Value = resident.AlternatePhysician;
            cmd.Parameters.Add("@AlternatePhysicianAddress", SqlDbType.NText).Value = resident.AlternatePhysicianAddress;
            cmd.Parameters.Add("@AlternatePhysicianTelephone", SqlDbType.NVarChar).Value = resident.AlternatePhysicianTelephone;
            cmd.Parameters.Add("@DateofLastPhysicalExam", SqlDbType.DateTime).Value = resident.DateofLastPhysicalExam;
            cmd.Parameters.Add("@YearlyPhysicalDue", SqlDbType.NVarChar).Value = resident.YearlyPhysicalDue;
            cmd.Parameters.Add("@Diagnosis", SqlDbType.NText).Value = resident.Diagnosis;
            cmd.Parameters.Add("@Allergies", SqlDbType.NText).Value = resident.Allergies;
            cmd.Parameters.Add("@DischargedOrExpiredDate", SqlDbType.DateTime).Value = resident.DischargedOrExpiredDate;
            cmd.Parameters.Add("@DischargedOrExpiredReason", SqlDbType.NText).Value = resident.DischargedOrExpiredReason;
            cmd.Parameters.Add("@IsWithoutMDApproval", SqlDbType.Bit).Value = resident.IsWithoutMDApproval;
            cmd.Parameters.Add("@ReleasedTo", SqlDbType.NVarChar).Value = resident.ReleasedTo;
            cmd.Parameters.Add("@NewAddress", SqlDbType.NText).Value = resident.NewAddress;
            cmd.Parameters.Add("@PlaceOfDeathAddressnCitynCountynState", SqlDbType.NText).Value = resident.PlaceOfDeathAddressnCitynCountynState;
            cmd.Parameters.Add("@PrecinctNo", SqlDbType.NVarChar).Value = resident.PrecinctNo;
            cmd.Parameters.Add("@MorticianName", SqlDbType.NVarChar).Value = resident.MorticianName;
            cmd.Parameters.Add("@Signature", SqlDbType.NVarChar).Value = resident.Signature;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = resident.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = resident.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = resident.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = resident.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = resident.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = resident.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = resident.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = resident.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = resident.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = resident.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return (int)cmd.Parameters["@ResidentID"].Value;
        }
    }

    public bool UpdateResident(Resident resident)
    {
        using (SqlConnection connection = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("AL_UpdateResident", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ResidentID", SqlDbType.Int).Value = resident.ResidentID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = resident.Name;
            cmd.Parameters.Add("@AdmissionDate", SqlDbType.DateTime).Value = resident.AdmissionDate;
            cmd.Parameters.Add("@AdmissionFrom", SqlDbType.NVarChar).Value = resident.AdmissionFrom;
            cmd.Parameters.Add("@UsualOccupation", SqlDbType.NVarChar).Value = resident.UsualOccupation;
            cmd.Parameters.Add("@PlaceOfBirth", SqlDbType.NVarChar).Value = resident.PlaceOfBirth;
            cmd.Parameters.Add("@UsualAddress", SqlDbType.NVarChar).Value = resident.UsualAddress;
            cmd.Parameters.Add("@Telephone", SqlDbType.NVarChar).Value = resident.Telephone;
            cmd.Parameters.Add("@Race", SqlDbType.NVarChar).Value = resident.Race;
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = resident.Age;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = resident.DateOfBirth;
            cmd.Parameters.Add("@Sex", SqlDbType.NVarChar).Value = resident.Sex;
            cmd.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar).Value = resident.MaritalStatus;
            cmd.Parameters.Add("@Height", SqlDbType.NChar).Value = resident.Height;
            cmd.Parameters.Add("@Weight", SqlDbType.NChar).Value = resident.Weight;
            cmd.Parameters.Add("@Religion", SqlDbType.NVarChar).Value = resident.Religion;
            cmd.Parameters.Add("@Clergyman", SqlDbType.NVarChar).Value = resident.Clergyman;
            cmd.Parameters.Add("@ChurchSynagogue", SqlDbType.NVarChar).Value = resident.ChurchSynagogue;
            cmd.Parameters.Add("@ChurchSynagogueTelephone", SqlDbType.NVarChar).Value = resident.ChurchSynagogueTelephone;
            cmd.Parameters.Add("@Address", SqlDbType.NText).Value = resident.Address;
            cmd.Parameters.Add("@AddressTelephone", SqlDbType.NVarChar).Value = resident.AddressTelephone;
            cmd.Parameters.Add("@SocialSecurity", SqlDbType.NVarChar).Value = resident.SocialSecurity;
            cmd.Parameters.Add("@Medicare", SqlDbType.NVarChar).Value = resident.Medicare;
            cmd.Parameters.Add("@Medicaid", SqlDbType.NVarChar).Value = resident.Medicaid;
            cmd.Parameters.Add("@Insurance", SqlDbType.NVarChar).Value = resident.Insurance;
            cmd.Parameters.Add("@InsuranceAddress", SqlDbType.NText).Value = resident.InsuranceAddress;
            cmd.Parameters.Add("@InsuranceAddressTelephone", SqlDbType.NVarChar).Value = resident.InsuranceAddressTelephone;
            cmd.Parameters.Add("@Policy", SqlDbType.NVarChar).Value = resident.Policy;
            cmd.Parameters.Add("@InsuranceGroup", SqlDbType.NVarChar).Value = resident.InsuranceGroup;
            cmd.Parameters.Add("@InsuranceGroupNo", SqlDbType.NVarChar).Value = resident.InsuranceGroupNo;
            cmd.Parameters.Add("@ResponsibleParty", SqlDbType.NVarChar).Value = resident.ResponsibleParty;
            cmd.Parameters.Add("@ResponsiblePartyRelationship", SqlDbType.NVarChar).Value = resident.ResponsiblePartyRelationship;
            cmd.Parameters.Add("@ResponsiblePartyAddress", SqlDbType.NText).Value = resident.ResponsiblePartyAddress;
            cmd.Parameters.Add("@ResponsiblePartyTelephone", SqlDbType.NVarChar).Value = resident.ResponsiblePartyTelephone;
            cmd.Parameters.Add("@PowerOfAttorney", SqlDbType.NVarChar).Value = resident.PowerOfAttorney;
            cmd.Parameters.Add("@PowerOfAttorneyAddress", SqlDbType.NText).Value = resident.PowerOfAttorneyAddress;
            cmd.Parameters.Add("@PowerOfAttorneyRelationship", SqlDbType.NVarChar).Value = resident.PowerOfAttorneyRelationship;
            cmd.Parameters.Add("@PowerOfAttorneyTelephone", SqlDbType.NVarChar).Value = resident.PowerOfAttorneyTelephone;
            cmd.Parameters.Add("@NearestRelativeOrGuardian", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardian;
            cmd.Parameters.Add("@NearestRelativeOrGuardianRelationship", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardianRelationship;
            cmd.Parameters.Add("@NearestRelativeOrGuardianAddress", SqlDbType.NText).Value = resident.NearestRelativeOrGuardianAddress;
            cmd.Parameters.Add("@NearestRelativeOrGuardianTelephone", SqlDbType.NVarChar).Value = resident.NearestRelativeOrGuardianTelephone;
            cmd.Parameters.Add("@NotifyInCaseOfEmergency", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergency;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyRelationship", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyRelationship;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyAddress", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyAddress;
            cmd.Parameters.Add("@NotifyInCaseOfEmergencyTelephone", SqlDbType.NVarChar).Value = resident.NotifyInCaseOfEmergencyTelephone;
            cmd.Parameters.Add("@HospitalPreference", SqlDbType.NVarChar).Value = resident.HospitalPreference;
            cmd.Parameters.Add("@HospitalPreferenceTelephone", SqlDbType.NVarChar).Value = resident.HospitalPreferenceTelephone;
            cmd.Parameters.Add("@HospitalPreferenceEmail", SqlDbType.NVarChar).Value = resident.HospitalPreferenceEmail;
            cmd.Parameters.Add("@FuneralHomePreference", SqlDbType.NVarChar).Value = resident.FuneralHomePreference;
            cmd.Parameters.Add("@FuneralHomePreferenceTelephone", SqlDbType.NVarChar).Value = resident.FuneralHomePreferenceTelephone;
            cmd.Parameters.Add("@PharmacyPreference", SqlDbType.NVarChar).Value = resident.PharmacyPreference;
            cmd.Parameters.Add("@PharmacyPreferenceTelephone", SqlDbType.NVarChar).Value = resident.PharmacyPreferenceTelephone;
            cmd.Parameters.Add("@Dentist", SqlDbType.NVarChar).Value = resident.Dentist;
            cmd.Parameters.Add("@DentistTelephone", SqlDbType.NVarChar).Value = resident.DentistTelephone;
            cmd.Parameters.Add("@AttendingPhysician", SqlDbType.NVarChar).Value = resident.AttendingPhysician;
            cmd.Parameters.Add("@AttendingPhysicianAddress", SqlDbType.NText).Value = resident.AttendingPhysicianAddress;
            cmd.Parameters.Add("@AttendingPhysicianTelephone", SqlDbType.NVarChar).Value = resident.AttendingPhysicianTelephone;
            cmd.Parameters.Add("@AlternatePhysician", SqlDbType.NVarChar).Value = resident.AlternatePhysician;
            cmd.Parameters.Add("@AlternatePhysicianAddress", SqlDbType.NText).Value = resident.AlternatePhysicianAddress;
            cmd.Parameters.Add("@AlternatePhysicianTelephone", SqlDbType.NVarChar).Value = resident.AlternatePhysicianTelephone;
            cmd.Parameters.Add("@DateofLastPhysicalExam", SqlDbType.DateTime).Value = resident.DateofLastPhysicalExam;
            cmd.Parameters.Add("@YearlyPhysicalDue", SqlDbType.NVarChar).Value = resident.YearlyPhysicalDue;
            cmd.Parameters.Add("@Diagnosis", SqlDbType.NText).Value = resident.Diagnosis;
            cmd.Parameters.Add("@Allergies", SqlDbType.NText).Value = resident.Allergies;
            cmd.Parameters.Add("@DischargedOrExpiredDate", SqlDbType.DateTime).Value = resident.DischargedOrExpiredDate;
            cmd.Parameters.Add("@DischargedOrExpiredReason", SqlDbType.NText).Value = resident.DischargedOrExpiredReason;
            cmd.Parameters.Add("@IsWithoutMDApproval", SqlDbType.Bit).Value = resident.IsWithoutMDApproval;
            cmd.Parameters.Add("@ReleasedTo", SqlDbType.NVarChar).Value = resident.ReleasedTo;
            cmd.Parameters.Add("@NewAddress", SqlDbType.NText).Value = resident.NewAddress;
            cmd.Parameters.Add("@PlaceOfDeathAddressnCitynCountynState", SqlDbType.NText).Value = resident.PlaceOfDeathAddressnCitynCountynState;
            cmd.Parameters.Add("@PrecinctNo", SqlDbType.NVarChar).Value = resident.PrecinctNo;
            cmd.Parameters.Add("@MorticianName", SqlDbType.NVarChar).Value = resident.MorticianName;
            cmd.Parameters.Add("@Signature", SqlDbType.NVarChar).Value = resident.Signature;
            cmd.Parameters.Add("@ExtraField1", SqlDbType.NVarChar).Value = resident.ExtraField1;
            cmd.Parameters.Add("@ExtraField2", SqlDbType.NVarChar).Value = resident.ExtraField2;
            cmd.Parameters.Add("@ExtraField3", SqlDbType.NVarChar).Value = resident.ExtraField3;
            cmd.Parameters.Add("@ExtraField4", SqlDbType.NVarChar).Value = resident.ExtraField4;
            cmd.Parameters.Add("@ExtraField5", SqlDbType.NVarChar).Value = resident.ExtraField5;
            cmd.Parameters.Add("@ExtraField6", SqlDbType.NVarChar).Value = resident.ExtraField6;
            cmd.Parameters.Add("@ExtraField7", SqlDbType.NVarChar).Value = resident.ExtraField7;
            cmd.Parameters.Add("@ExtraField8", SqlDbType.NVarChar).Value = resident.ExtraField8;
            cmd.Parameters.Add("@ExtraField9", SqlDbType.NVarChar).Value = resident.ExtraField9;
            cmd.Parameters.Add("@ExtraField10", SqlDbType.NVarChar).Value = resident.ExtraField10;
            connection.Open();

            int result = cmd.ExecuteNonQuery();
            return result == 1;
        }
    }
}
