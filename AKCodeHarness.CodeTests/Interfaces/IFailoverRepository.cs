using System.Collections.Generic;

namespace AKCodeHarness.CodeTests
{
    public interface IFailoverRepository
    {
        List<FailoverEntry> GetFailOverEntries();
    }
}