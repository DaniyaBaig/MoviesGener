using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.Common
{
    public class AppSettings
    {
        public string ArrangementGroupId { get; set; }
        public string MaxMindDbPath { get; set; }
        public Dictionary<string, int> BannerAreaIds { get; set; }
        public string DefaultCategoryId { get; set; }
        public decimal ServerTimeZone { get; set; }
        public int EtgBulkCouponId { get; set; }
        public int PickupStoreLookupDistanceEnd { get; set; }
        public int EmailSubscriptionPageBulkCouponId { get; set; }
        public DateTime EmailPopupSchedule1StartDate { get; set; }
        public DateTime EmailPopupSchedule1EndDate { get; set; }
        public string MGageBaseUrl { get; set; }
        public string MGageUserName { get; set; }
        public string MGagePassword { get; set; }
        public string MGageShortCode { get; set; }
        public string MGageAcceptableDigitLength { get; set; }
        public string MGageDialingCode { get; set; }
        public int DefaultCountryId { get; set; }
        public string AreaIdSharingStores { get; set; }
        public decimal SecondIncrementPickupStoreLookupDistance { get; set; }
        public decimal DistanceFactor { get; set; }
        public string TestAccountIds { get; set; }
        public string Pickup7MinutesCutOffTime { get; set; }
        public decimal PickupStoreLookupDistance { get; set; }
        public string CordialApiKey { get; set; }
        public string CordialTemplateUrl { get; set; }
        public string CordialContactListUrl { get; set; }
        public bool EnableCordialEmailLogging { get; set; }
        public Dictionary<string, int> AddOnsConfig { get; set; }
        public Dictionary<string, string> Fedex { get; set; }
    }
}
