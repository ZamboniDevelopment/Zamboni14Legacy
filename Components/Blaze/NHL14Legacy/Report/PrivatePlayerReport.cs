using Tdf;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy.Report
{
    [TdfStruct]
    public struct PrivatePlayerReport
    {

        [TdfMember("PIAM")]
        public SortedDictionary<string, ulong> mPrivateIntAttributeMap;
        
        [TdfMember("PPAM")]
        public SortedDictionary<string, string> mPrivateAttributeMap;
        
    }
}