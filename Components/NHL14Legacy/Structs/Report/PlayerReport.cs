using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Structs.Report
{
    [TdfStruct]
    public struct PlayerReport
    {

        [TdfMember("CDNF")]
        public uint mCDNF;
        
        [TdfMember("CHT")]
        public ushort mCHT;
        
        [TdfMember("CPRT")]
        public object? mCustomPlayerReport;
        
        [TdfMember("CSCO")]
        public uint mCSCO;
        
        [TdfMember("CTRY")]
        public ushort mCTRY;
        
        [TdfMember("DISC")]
        public ushort mDISC;
        
        [TdfMember("FHRN")]
        public uint mFHRN;
        
        [TdfMember("GRLT")]
        public uint mGRLT;
        
        [TdfMember("GTAG")]
        public string mGTAG;
        
        [TdfMember("HOME")]
        public bool mHOME;
        
        [TdfMember("LOSS")]
        public uint mLOSS;
        
        [TdfMember("NAME")]
        public string mNAME;
        
        [TdfMember("OPCT")]
        public uint mOPCT;
        
        [TdfMember("OPPR")]
        public PrivatePlayerReport mPrivatePlayerReport;
        
        [TdfMember("OTL")]
        public uint mOTL;
        
        [TdfMember("PEID")]
        public ulong mPEID;
        
        [TdfMember("PNID")]
        public ulong mPNID;
        
        [TdfMember("PPNA")]
        public string mPPNA;
        
        [TdfMember("PTAG")]
        public uint mPTAG;
        
        [TdfMember("QUIT")]
        public ushort mQUIT;
        
        [TdfMember("RELT")]
        public uint mRELT;
        
        [TdfMember("SCOR")]
        public uint mSCOR;
        
        [TdfMember("SERG")]
        public ushort mSERG;
        
        [TdfMember("SKIL")]
        public uint mSKIL;
        
        [TdfMember("SKPT")]
        public uint mSKPT;
        
        [TdfMember("TEAM")]
        public uint mTEAM;
        
        [TdfMember("TIES")]
        public uint mTIES;
        
        [TdfMember("TNAM")]
        public string mTNAM;
        
        [TdfMember("WDNF")]
        public uint mWDNF;
        
        [TdfMember("WGHT")]
        public uint mWGHT;
        
        [TdfMember("WINS")]
        public uint mWINS;

    }
}