using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Resident
{
    public Resident()
    {
    }

    public Resident
        (
        int residentID, 
        string name, 
        DateTime admissionDate, 
        string admissionFrom, 
        string usualOccupation, 
        string placeOfBirth, 
        string usualAddress, 
        string telephone, 
        string race, 
        int age, 
        DateTime dateOfBirth, 
        string sex, 
        string maritalStatus, 
        string height, 
        string weight, 
        string religion, 
        string clergyman, 
        string churchSynagogue, 
        string churchSynagogueTelephone, 
        string address, 
        string addressTelephone, 
        string socialSecurity, 
        string medicare, 
        string medicaid, 
        string insurance, 
        string insuranceAddress, 
        string insuranceAddressTelephone, 
        string policy, 
        string insuranceGroup, 
        string insuranceGroupNo, 
        string responsibleParty, 
        string responsiblePartyRelationship, 
        string responsiblePartyAddress, 
        string responsiblePartyTelephone, 
        string powerOfAttorney, 
        string powerOfAttorneyAddress, 
        string powerOfAttorneyRelationship, 
        string powerOfAttorneyTelephone, 
        string nearestRelativeOrGuardian, 
        string nearestRelativeOrGuardianRelationship, 
        string nearestRelativeOrGuardianAddress, 
        string nearestRelativeOrGuardianTelephone, 
        string notifyInCaseOfEmergency, 
        string notifyInCaseOfEmergencyRelationship, 
        string notifyInCaseOfEmergencyAddress, 
        string notifyInCaseOfEmergencyTelephone, 
        string hospitalPreference, 
        string hospitalPreferenceTelephone, 
        string hospitalPreferenceEmail, 
        string funeralHomePreference, 
        string funeralHomePreferenceTelephone, 
        string pharmacyPreference, 
        string pharmacyPreferenceTelephone, 
        string dentist, 
        string dentistTelephone, 
        string attendingPhysician, 
        string attendingPhysicianAddress, 
        string attendingPhysicianTelephone, 
        string alternatePhysician, 
        string alternatePhysicianAddress, 
        string alternatePhysicianTelephone, 
        DateTime dateofLastPhysicalExam, 
        string yearlyPhysicalDue, 
        string diagnosis, 
        string allergies, 
        DateTime dischargedOrExpiredDate, 
        string dischargedOrExpiredReason, 
        bool isWithoutMDApproval, 
        string releasedTo, 
        string newAddress, 
        string placeOfDeathAddressnCitynCountynState, 
        string precinctNo, 
        string morticianName, 
        string signature
        )
    {
        this.ResidentID = residentID;
        this.Name = name;
        this.AdmissionDate = admissionDate;
        this.AdmissionFrom = admissionFrom;
        this.UsualOccupation = usualOccupation;
        this.PlaceOfBirth = placeOfBirth;
        this.UsualAddress = usualAddress;
        this.Telephone = telephone;
        this.Race = race;
        this.Age = age;
        this.DateOfBirth = dateOfBirth;
        this.Sex = sex;
        this.MaritalStatus = maritalStatus;
        this.Height = height;
        this.Weight = weight;
        this.Religion = religion;
        this.Clergyman = clergyman;
        this.ChurchSynagogue = churchSynagogue;
        this.ChurchSynagogueTelephone = churchSynagogueTelephone;
        this.Address = address;
        this.AddressTelephone = addressTelephone;
        this.SocialSecurity = socialSecurity;
        this.Medicare = medicare;
        this.Medicaid = medicaid;
        this.Insurance = insurance;
        this.InsuranceAddress = insuranceAddress;
        this.InsuranceAddressTelephone = insuranceAddressTelephone;
        this.Policy = policy;
        this.InsuranceGroup = insuranceGroup;
        this.InsuranceGroupNo = insuranceGroupNo;
        this.ResponsibleParty = responsibleParty;
        this.ResponsiblePartyRelationship = responsiblePartyRelationship;
        this.ResponsiblePartyAddress = responsiblePartyAddress;
        this.ResponsiblePartyTelephone = responsiblePartyTelephone;
        this.PowerOfAttorney = powerOfAttorney;
        this.PowerOfAttorneyAddress = powerOfAttorneyAddress;
        this.PowerOfAttorneyRelationship = powerOfAttorneyRelationship;
        this.PowerOfAttorneyTelephone = powerOfAttorneyTelephone;
        this.NearestRelativeOrGuardian = nearestRelativeOrGuardian;
        this.NearestRelativeOrGuardianRelationship = nearestRelativeOrGuardianRelationship;
        this.NearestRelativeOrGuardianAddress = nearestRelativeOrGuardianAddress;
        this.NearestRelativeOrGuardianTelephone = nearestRelativeOrGuardianTelephone;
        this.NotifyInCaseOfEmergency = notifyInCaseOfEmergency;
        this.NotifyInCaseOfEmergencyRelationship = notifyInCaseOfEmergencyRelationship;
        this.NotifyInCaseOfEmergencyAddress = notifyInCaseOfEmergencyAddress;
        this.NotifyInCaseOfEmergencyTelephone = notifyInCaseOfEmergencyTelephone;
        this.HospitalPreference = hospitalPreference;
        this.HospitalPreferenceTelephone = hospitalPreferenceTelephone;
        this.HospitalPreferenceEmail = hospitalPreferenceEmail;
        this.FuneralHomePreference = funeralHomePreference;
        this.FuneralHomePreferenceTelephone = funeralHomePreferenceTelephone;
        this.PharmacyPreference = pharmacyPreference;
        this.PharmacyPreferenceTelephone = pharmacyPreferenceTelephone;
        this.Dentist = dentist;
        this.DentistTelephone = dentistTelephone;
        this.AttendingPhysician = attendingPhysician;
        this.AttendingPhysicianAddress = attendingPhysicianAddress;
        this.AttendingPhysicianTelephone = attendingPhysicianTelephone;
        this.AlternatePhysician = alternatePhysician;
        this.AlternatePhysicianAddress = alternatePhysicianAddress;
        this.AlternatePhysicianTelephone = alternatePhysicianTelephone;
        this.DateofLastPhysicalExam = dateofLastPhysicalExam;
        this.YearlyPhysicalDue = yearlyPhysicalDue;
        this.Diagnosis = diagnosis;
        this.Allergies = allergies;
        this.DischargedOrExpiredDate = dischargedOrExpiredDate;
        this.DischargedOrExpiredReason = dischargedOrExpiredReason;
        this.IsWithoutMDApproval = isWithoutMDApproval;
        this.ReleasedTo = releasedTo;
        this.NewAddress = newAddress;
        this.PlaceOfDeathAddressnCitynCountynState = placeOfDeathAddressnCitynCountynState;
        this.PrecinctNo = precinctNo;
        this.MorticianName = morticianName;
        this.Signature = signature;
    }


    private int _residentID;
    public int ResidentID
    {
        get { return _residentID; }
        set { _residentID = value; }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private DateTime _admissionDate;
    public DateTime AdmissionDate
    {
        get { return _admissionDate; }
        set { _admissionDate = value; }
    }

    private string _admissionFrom;
    public string AdmissionFrom
    {
        get { return _admissionFrom; }
        set { _admissionFrom = value; }
    }

    private string _usualOccupation;
    public string UsualOccupation
    {
        get { return _usualOccupation; }
        set { _usualOccupation = value; }
    }

    private string _placeOfBirth;
    public string PlaceOfBirth
    {
        get { return _placeOfBirth; }
        set { _placeOfBirth = value; }
    }

    private string _usualAddress;
    public string UsualAddress
    {
        get { return _usualAddress; }
        set { _usualAddress = value; }
    }

    private string _telephone;
    public string Telephone
    {
        get { return _telephone; }
        set { _telephone = value; }
    }

    private string _race;
    public string Race
    {
        get { return _race; }
        set { _race = value; }
    }

    private int _age;
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set { _dateOfBirth = value; }
    }

    private string _sex;
    public string Sex
    {
        get { return _sex; }
        set { _sex = value; }
    }

    private string _maritalStatus;
    public string MaritalStatus
    {
        get { return _maritalStatus; }
        set { _maritalStatus = value; }
    }

    private string _height;
    public string Height
    {
        get { return _height; }
        set { _height = value; }
    }

    private string _weight;
    public string Weight
    {
        get { return _weight; }
        set { _weight = value; }
    }

    private string _religion;
    public string Religion
    {
        get { return _religion; }
        set { _religion = value; }
    }

    private string _clergyman;
    public string Clergyman
    {
        get { return _clergyman; }
        set { _clergyman = value; }
    }

    private string _churchSynagogue;
    public string ChurchSynagogue
    {
        get { return _churchSynagogue; }
        set { _churchSynagogue = value; }
    }

    private string _churchSynagogueTelephone;
    public string ChurchSynagogueTelephone
    {
        get { return _churchSynagogueTelephone; }
        set { _churchSynagogueTelephone = value; }
    }

    private string _address;
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    private string _addressTelephone;
    public string AddressTelephone
    {
        get { return _addressTelephone; }
        set { _addressTelephone = value; }
    }

    private string _socialSecurity;
    public string SocialSecurity
    {
        get { return _socialSecurity; }
        set { _socialSecurity = value; }
    }

    private string _medicare;
    public string Medicare
    {
        get { return _medicare; }
        set { _medicare = value; }
    }

    private string _medicaid;
    public string Medicaid
    {
        get { return _medicaid; }
        set { _medicaid = value; }
    }

    private string _insurance;
    public string Insurance
    {
        get { return _insurance; }
        set { _insurance = value; }
    }

    private string _insuranceAddress;
    public string InsuranceAddress
    {
        get { return _insuranceAddress; }
        set { _insuranceAddress = value; }
    }

    private string _insuranceAddressTelephone;
    public string InsuranceAddressTelephone
    {
        get { return _insuranceAddressTelephone; }
        set { _insuranceAddressTelephone = value; }
    }

    private string _policy;
    public string Policy
    {
        get { return _policy; }
        set { _policy = value; }
    }

    private string _insuranceGroup;
    public string InsuranceGroup
    {
        get { return _insuranceGroup; }
        set { _insuranceGroup = value; }
    }

    private string _insuranceGroupNo;
    public string InsuranceGroupNo
    {
        get { return _insuranceGroupNo; }
        set { _insuranceGroupNo = value; }
    }

    private string _responsibleParty;
    public string ResponsibleParty
    {
        get { return _responsibleParty; }
        set { _responsibleParty = value; }
    }

    private string _responsiblePartyRelationship;
    public string ResponsiblePartyRelationship
    {
        get { return _responsiblePartyRelationship; }
        set { _responsiblePartyRelationship = value; }
    }

    private string _responsiblePartyAddress;
    public string ResponsiblePartyAddress
    {
        get { return _responsiblePartyAddress; }
        set { _responsiblePartyAddress = value; }
    }

    private string _responsiblePartyTelephone;
    public string ResponsiblePartyTelephone
    {
        get { return _responsiblePartyTelephone; }
        set { _responsiblePartyTelephone = value; }
    }

    private string _powerOfAttorney;
    public string PowerOfAttorney
    {
        get { return _powerOfAttorney; }
        set { _powerOfAttorney = value; }
    }

    private string _powerOfAttorneyAddress;
    public string PowerOfAttorneyAddress
    {
        get { return _powerOfAttorneyAddress; }
        set { _powerOfAttorneyAddress = value; }
    }

    private string _powerOfAttorneyRelationship;
    public string PowerOfAttorneyRelationship
    {
        get { return _powerOfAttorneyRelationship; }
        set { _powerOfAttorneyRelationship = value; }
    }

    private string _powerOfAttorneyTelephone;
    public string PowerOfAttorneyTelephone
    {
        get { return _powerOfAttorneyTelephone; }
        set { _powerOfAttorneyTelephone = value; }
    }

    private string _nearestRelativeOrGuardian;
    public string NearestRelativeOrGuardian
    {
        get { return _nearestRelativeOrGuardian; }
        set { _nearestRelativeOrGuardian = value; }
    }

    private string _nearestRelativeOrGuardianRelationship;
    public string NearestRelativeOrGuardianRelationship
    {
        get { return _nearestRelativeOrGuardianRelationship; }
        set { _nearestRelativeOrGuardianRelationship = value; }
    }

    private string _nearestRelativeOrGuardianAddress;
    public string NearestRelativeOrGuardianAddress
    {
        get { return _nearestRelativeOrGuardianAddress; }
        set { _nearestRelativeOrGuardianAddress = value; }
    }

    private string _nearestRelativeOrGuardianTelephone;
    public string NearestRelativeOrGuardianTelephone
    {
        get { return _nearestRelativeOrGuardianTelephone; }
        set { _nearestRelativeOrGuardianTelephone = value; }
    }

    private string _notifyInCaseOfEmergency;
    public string NotifyInCaseOfEmergency
    {
        get { return _notifyInCaseOfEmergency; }
        set { _notifyInCaseOfEmergency = value; }
    }

    private string _notifyInCaseOfEmergencyRelationship;
    public string NotifyInCaseOfEmergencyRelationship
    {
        get { return _notifyInCaseOfEmergencyRelationship; }
        set { _notifyInCaseOfEmergencyRelationship = value; }
    }

    private string _notifyInCaseOfEmergencyAddress;
    public string NotifyInCaseOfEmergencyAddress
    {
        get { return _notifyInCaseOfEmergencyAddress; }
        set { _notifyInCaseOfEmergencyAddress = value; }
    }

    private string _notifyInCaseOfEmergencyTelephone;
    public string NotifyInCaseOfEmergencyTelephone
    {
        get { return _notifyInCaseOfEmergencyTelephone; }
        set { _notifyInCaseOfEmergencyTelephone = value; }
    }

    private string _hospitalPreference;
    public string HospitalPreference
    {
        get { return _hospitalPreference; }
        set { _hospitalPreference = value; }
    }

    private string _hospitalPreferenceTelephone;
    public string HospitalPreferenceTelephone
    {
        get { return _hospitalPreferenceTelephone; }
        set { _hospitalPreferenceTelephone = value; }
    }

    private string _hospitalPreferenceEmail;
    public string HospitalPreferenceEmail
    {
        get { return _hospitalPreferenceEmail; }
        set { _hospitalPreferenceEmail = value; }
    }

    private string _funeralHomePreference;
    public string FuneralHomePreference
    {
        get { return _funeralHomePreference; }
        set { _funeralHomePreference = value; }
    }

    private string _funeralHomePreferenceTelephone;
    public string FuneralHomePreferenceTelephone
    {
        get { return _funeralHomePreferenceTelephone; }
        set { _funeralHomePreferenceTelephone = value; }
    }

    private string _pharmacyPreference;
    public string PharmacyPreference
    {
        get { return _pharmacyPreference; }
        set { _pharmacyPreference = value; }
    }

    private string _pharmacyPreferenceTelephone;
    public string PharmacyPreferenceTelephone
    {
        get { return _pharmacyPreferenceTelephone; }
        set { _pharmacyPreferenceTelephone = value; }
    }

    private string _dentist;
    public string Dentist
    {
        get { return _dentist; }
        set { _dentist = value; }
    }

    private string _dentistTelephone;
    public string DentistTelephone
    {
        get { return _dentistTelephone; }
        set { _dentistTelephone = value; }
    }

    private string _attendingPhysician;
    public string AttendingPhysician
    {
        get { return _attendingPhysician; }
        set { _attendingPhysician = value; }
    }

    private string _attendingPhysicianAddress;
    public string AttendingPhysicianAddress
    {
        get { return _attendingPhysicianAddress; }
        set { _attendingPhysicianAddress = value; }
    }

    private string _attendingPhysicianTelephone;
    public string AttendingPhysicianTelephone
    {
        get { return _attendingPhysicianTelephone; }
        set { _attendingPhysicianTelephone = value; }
    }

    private string _alternatePhysician;
    public string AlternatePhysician
    {
        get { return _alternatePhysician; }
        set { _alternatePhysician = value; }
    }

    private string _alternatePhysicianAddress;
    public string AlternatePhysicianAddress
    {
        get { return _alternatePhysicianAddress; }
        set { _alternatePhysicianAddress = value; }
    }

    private string _alternatePhysicianTelephone;
    public string AlternatePhysicianTelephone
    {
        get { return _alternatePhysicianTelephone; }
        set { _alternatePhysicianTelephone = value; }
    }

    private DateTime _dateofLastPhysicalExam;
    public DateTime DateofLastPhysicalExam
    {
        get { return _dateofLastPhysicalExam; }
        set { _dateofLastPhysicalExam = value; }
    }

    private string _yearlyPhysicalDue;
    public string YearlyPhysicalDue
    {
        get { return _yearlyPhysicalDue; }
        set { _yearlyPhysicalDue = value; }
    }

    private string _diagnosis;
    public string Diagnosis
    {
        get { return _diagnosis; }
        set { _diagnosis = value; }
    }

    private string _allergies;
    public string Allergies
    {
        get { return _allergies; }
        set { _allergies = value; }
    }

    private DateTime _dischargedOrExpiredDate;
    public DateTime DischargedOrExpiredDate
    {
        get { return _dischargedOrExpiredDate; }
        set { _dischargedOrExpiredDate = value; }
    }

    private string _dischargedOrExpiredReason;
    public string DischargedOrExpiredReason
    {
        get { return _dischargedOrExpiredReason; }
        set { _dischargedOrExpiredReason = value; }
    }

    private bool _isWithoutMDApproval;
    public bool IsWithoutMDApproval
    {
        get { return _isWithoutMDApproval; }
        set { _isWithoutMDApproval = value; }
    }

    private string _releasedTo;
    public string ReleasedTo
    {
        get { return _releasedTo; }
        set { _releasedTo = value; }
    }

    private string _newAddress;
    public string NewAddress
    {
        get { return _newAddress; }
        set { _newAddress = value; }
    }

    private string _placeOfDeathAddressnCitynCountynState;
    public string PlaceOfDeathAddressnCitynCountynState
    {
        get { return _placeOfDeathAddressnCitynCountynState; }
        set { _placeOfDeathAddressnCitynCountynState = value; }
    }

    private string _precinctNo;
    public string PrecinctNo
    {
        get { return _precinctNo; }
        set { _precinctNo = value; }
    }

    private string _morticianName;
    public string MorticianName
    {
        get { return _morticianName; }
        set { _morticianName = value; }
    }

    private string _signature;
    public string Signature
    {
        get { return _signature; }
        set { _signature = value; }
    }

    /// <summary>
    /// PropertyID
    /// </summary>
    public string ExtraField1 { set; get; }

    /// <summary>
    /// Added Date
    /// </summary>
    public string ExtraField2 { set; get; }

    /// <summary>
    /// Added By
    /// </summary>
    public string ExtraField3 { set; get; }

    /// <summary>
    /// updated Date
    /// </summary>
    public string ExtraField4 { set; get; }

    /// <summary>
    /// updated By
    /// </summary>
    public string ExtraField5 { set; get; }

    /// <summary>
    /// At Display time the value will be Property Name
    /// </summary>
    public string ExtraField6 { set; get; }

    /// <summary>
    /// At Display time the value will be Property Address
    /// </summary>
    public string ExtraField7 { set; get; }

    /// <summary>
    /// PrimaryLanguage got from the Asst. & Care
    /// </summary>
    public string ExtraField8 { set; get; }
    /// <summary>
    /// Compnay name
    /// </summary>
    public string ExtraField9 { set; get; }

    /// <summary>
    /// Status (Active/InActive)
    /// </summary>
    public string ExtraField10 { set; get; }

    /// <summary>
    /// Allergy
    /// </summary>
    public string ExtraField11 { set; get; }

    /// <summary>
    /// li_ADLRecord_Visible
    /// </summary>
    public bool li_ADLRecord { set; get; }

    /// <summary>
    /// li_ComprehensiveAssessment
    /// </summary>
    public bool li_ComprehensiveAssessment { set; get; }

    /// <summary>
    /// li_ServiceCareAssessment
    /// </summary>
    public bool li_ServiceCareAssessment { set; get; }

    /// <summary>
    /// li_Medicaiton
    /// </summary>
    public bool li_Medicaiton { set; get; }

    /// <summary>
    /// li_ObservationLog
    /// </summary>
    public bool li_ObservationLog { set; get; }

    /// <summary>
    /// li_DoctorsOrder
    /// </summary>
    public bool li_DoctorsOrder { set; get; }
}
