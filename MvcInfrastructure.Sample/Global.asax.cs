using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MvcInfrastructure.Core;
using System.IO;
using System.Threading;

namespace MvcInfrastructure.Sample
{
    public class Global : MvcApplication
    {
        protected override bool IsPreLoading
        {
            get
            {
                return false;
            }
        }

        protected override void OnStartApplication()
        {

        }
    }
}