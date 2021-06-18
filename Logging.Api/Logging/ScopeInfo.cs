using System;
using System.Collections.Generic;
using System.Reflection;

namespace Logging.Api.Logging
{
    public interface IScopeInfo
    {
        Dictionary<string, string> HostScopeInfo { get; }
    }
    public class ScopeInfo : IScopeInfo
    {
        public ScopeInfo()
        {
            HostScopeInfo = new Dictionary<string, string>
            {
                {"MachineName", Environment.MachineName},
                {"EntryPoint", Assembly.GetEntryAssembly().GetName().Name},
            };
        }
        public Dictionary<string, string> HostScopeInfo { get; }
    }
}