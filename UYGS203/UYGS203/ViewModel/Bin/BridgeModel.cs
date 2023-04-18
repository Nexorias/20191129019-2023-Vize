using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UYGS203.ViewModel
{
    public class BridgeModel
    {
        public string BridgeId { get; set; }
        public string BridgeSubjectId { get; set; }
        public string BridgeStudentId { get; set; }

        public SubjectModel SubjectInfo { get; set; }
        public StudentModel StudentInfo { get; set; }

    }
}