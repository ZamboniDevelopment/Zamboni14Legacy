using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Report
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