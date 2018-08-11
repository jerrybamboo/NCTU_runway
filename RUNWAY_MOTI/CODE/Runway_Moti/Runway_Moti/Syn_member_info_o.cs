using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runway_Moti
{
    class Syn_member_info_o
    {
        public string member_id { get; set; }
        public string member_fname { get; set; }
        public string member_lname { get; set; }
        public string fb_email { get; set; }
        public string twitter_email { get; set; }
        public string gender { get; set; }   // 0:female,1:male
        public string height { get; set; }
        public string weight { get; set; }
        public string numeric_code { get; set; }
        public string is_coach { get; set; }   // 0:not coach,1:coach
        public string years_of_exp { get; set; }
        public string personal_intro { get; set; }
        public string profile_photo { get; set; }
        public string unit_length { get; set; }  // 0:cm,1:inch
        public string unit_weight { get; set; }  // 0:kg,1:pound
        public string unit_distance { get; set; }   // 0:km,1:mile
        public string social_connect_fb { get; set; }   // 0: not connect,1: connected
        public string social_connect_twitter { get; set; }  // 0: not connect,1: connected
        public string app_version { get; set; }
        public string stride { get; set; }
        public string fitness_load_setting { get; set; }
        public int group_id { get; set; }
        public string dateofbirth { get; set; }
        public string default_time_offset { get; set; }
    }
}
