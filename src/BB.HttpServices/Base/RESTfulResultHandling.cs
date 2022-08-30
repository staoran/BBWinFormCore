using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.UnifyResult;

namespace BB.HttpServices.Base;

public class RESTfulResultControl<T> : RESTfulResult<T>
{
    public T Handling()
    {
        if (Succeeded)
        {
            return Data;
        }
        
        throw Oops.Bah(JSON.Serialize(Errors), Data, Extras, Errors);
    }
}