using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface ICentralService
    {
        Result<bool> ImportProject(string projectName);

        Result<bool> ImportUsers(string fileName);
    }
}
