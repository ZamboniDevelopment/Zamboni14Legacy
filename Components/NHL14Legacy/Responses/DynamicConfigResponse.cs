using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses
{
    [TdfStruct]
    public struct DynamicConfigResponse
    {
        [TdfMember("CDRD")]
        public ushort mDataRequestDelaySeconds;
        
        [TdfMember("CERD")]
        public ushort mErrorRetryDelaySeconds;
        
        [TdfMember("CMDI")]
        public ushort mMessageDelayIntervalSeconds;
        
        [TdfMember("CMMC")]
        public ushort mMaximumMessageCount;
    }
}