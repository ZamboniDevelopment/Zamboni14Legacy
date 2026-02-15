using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Report
{
    [TdfStruct]
    public struct GameInfoReport
    {

        [TdfMember("ARID")]
        public ulong mARID;
        
        [TdfMember("CGRT")]
        public object? mClubReport;
        
        [TdfMember("CTID")]
        public uint mCTID;
        
        [TdfMember("GRID")]
        public ulong mGRID;
        
        [TdfMember("GTIM")]
        public uint mGTIM;
        
        [TdfMember("ISIM")]
        public bool mISIM;
        
        [TdfMember("LGID")]
        public uint mLGID;
        
        [TdfMember("NPER")]
        public ushort mNPER;
        
        [TdfMember("OVRT")]
        public uint mOVRT;
        
        [TdfMember("PLEN")]
        public ushort mPLEN;
        
        [TdfMember("RANK")]
        public bool mRANK;
        
        [TdfMember("ROID")]
        public uint mROID;
        
        [TdfMember("SEID")]
        public uint mSEID;
        
        [TdfMember("SHOO")]
        public uint mSHOO;
        
        [TdfMember("SKIL")]
        public ushort mSKIL;
        
        [TdfMember("SKU")]
        public ushort mSKU;
        
        [TdfMember("STUS")]
        public uint mSTUS;
        
        [TdfMember("TYPE")]
        public string mTYPE;
        
        [TdfMember("VENU")]
        public ushort mVENU;

    }
}