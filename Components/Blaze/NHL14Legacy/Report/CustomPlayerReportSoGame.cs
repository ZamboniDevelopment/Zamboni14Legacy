using Tdf;

namespace Zamboni14Legacy.Components.Blaze.NHL14Legacy.Report
{
    [TdfStruct(0x2810F02E)]
    public struct CustomPlayerReportSoGame
    {

        [TdfMember("GA")]
        public uint mGA;
        
        [TdfMember("GF")]
        public uint mGF;
        
        [TdfMember("SHTA")]
        public uint mSHTA;
        
        [TdfMember("SHTS")]
        public uint mSHTS;
        
        [TdfMember("SKLV")]
        public uint mSKLV;
        
    }
}