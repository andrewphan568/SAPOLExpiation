//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expiation
    {
        public string noticeStatusDesc { get; set; }
        public string driversLicenseStateDescExpiationSubject { get; set; }
        public string regStateDescExpiationVehicle { get; set; }
        public string expiationVehicleDescriptionExpiationVehicle { get; set; }
        public string localServiceAreaDesc { get; set; }
        public System.DateTime incidentStartDate { get; set; }
        public System.TimeSpan time24HourIncidentStart { get; set; }
        public System.DateTime issueDate { get; set; }
        public string expiationOffenceCode { get; set; }
        public int offencePenaltyAmt { get; set; }
        public int offenceLevyAmt { get; set; }
        public int corporateFeeAmount { get; set; }
        public string offenceStatus { get; set; }
        public int penaltyWrittenOnNoticeAmount { get; set; }
        public int vehicleSpeed { get; set; }
        public string expiationZoneSpeedLimit { get; set; }
        public string bloodAlcoholContentExp { get; set; }
        public string speedCameraCategory { get; set; }
        public string photoRejectedReasonCode { get; set; }
        public string photoRejectedReasonDesc { get; set; }
        public string withdrawnReasonDesc { get; set; }
        public string noticeTypeDesc { get; set; }
        public string enforcementWarningNoticeFeeAmount { get; set; }
        public string fixedCameraLocnCode { get; set; }
        public string locationCodeMobileSpeedCamera { get; set; }
        public string offenceStatusDescription { get; set; }
    
        public virtual ExpiationCode ExpiationCode { get; set; }
    }
}
