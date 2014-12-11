using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;



namespace Blessit.Service
{
    [ServiceContract(Namespace = "Blessit.Services")]
    public interface IBlessitService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        string GetResult();
    }
}
