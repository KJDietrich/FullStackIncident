using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business
{
    public class Address
    {
        public string address_id { get; set; }
        public string address_line1 { get; set; }
        public string city { get; set; }
        public string common_place_name { get; set; }
        public string cross_street1 { get; set; }
        public string cross_street2 { get; set; }
        public string first_due { get; set; }
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string postal_code { get; set; }
        public string prefix_direction { get; set; }
        public string response_zone { get; set; }
        public string state { get; set; }
        public string suffix_direction { get; set; }
        public string type { get; set; }
    }

    public class ExtendedData
    {
        public int event_duration { get; set; }
        public int response_duration { get; set; }
        public int travel_duration { get; set; }
        public int turnout_duration { get; set; }
    }

    public class Acknowledged
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Arrived
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Available
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Cleared
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Dispatched
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Enroute
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }
    public class Tilde
    {
        public string geohash { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
    }
    /// <summary>
    /// Had to add DataContract/DataMember attributes to handle the invalid member name of ~
    /// </summary>
    [DataContract]
    public class UnitStatus
    {
        [DataMember]
        public Acknowledged acknowledged { get; set; }
        [DataMember]
        public Arrived arrived { get; set; }
        [DataMember]
        public Available available { get; set; }
        [DataMember]
        public Cleared cleared { get; set; }
        [DataMember]
        public Dispatched dispatched { get; set; }
        [DataMember]
        public Enroute enroute { get; set; }
        [DataMember(Name ="~")]
        public Tilde tilde { get; set; }
    }
    public class Apparatus
    {
        public string car_id { get; set; }
        public ExtendedData extended_data { get; set; }
        public string geohash { get; set; }
        public List<object> personnel { get; set; }
        public string shift { get; set; }
        public string station { get; set; }
        public string unit_id { get; set; }
        public UnitStatus unit_status { get; set; }
        public string unit_type { get; set; }
    }

    public class ExtendedData2
    {
        public int dispatch_duration { get; set; }
        public int event_duration { get; set; }
        public int response_time { get; set; }
    }

    public class Description
    {
        public string comments { get; set; }
        public string day_of_week { get; set; }
        public DateTime event_closed { get; set; }
        public string event_id { get; set; }
        public DateTime event_opened { get; set; }
        public ExtendedData2 extended_data { get; set; }
        public DateTime first_unit_arrived { get; set; }
        public DateTime first_unit_dispatched { get; set; }
        public DateTime first_unit_enroute { get; set; }
        public int hour_of_day { get; set; }
        public string incident_number { get; set; }
        public DateTime loi_search_complete { get; set; }
        public string subtype { get; set; }
        public string type { get; set; }
    }

    public class FireDepartment
    {
        public string fd_id { get; set; }
        public string firecares_id { get; set; }
        public string name { get; set; }
        public string shift { get; set; }
        public string state { get; set; }
        public string timezone { get; set; }
    }
    public partial class IncidentData
    {
        public Address address { get; set; }
        public List<Apparatus> apparatus { get; set; }
        public Description description { get; set; }
        public FireDepartment fire_department { get; set; }
        public string version { get; set; }
    }
}
