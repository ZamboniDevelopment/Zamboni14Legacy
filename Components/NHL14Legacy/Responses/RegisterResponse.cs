using Tdf;

namespace Zamboni14Legacy.Components.NHL14Legacy.Responses;

[TdfStruct]
public struct RegisterResponse
{
    
    [TdfMember("MSGS")] 
    public uint mNumMessages;
    
}