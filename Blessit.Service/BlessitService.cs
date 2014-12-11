using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Blessit.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class BlessitService : IBlessitService
    {
        public string GetResult()
        {
            return "sharad";
        }
    }
}
