using System.Text.Json;
using Furion.FriendlyException;
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

        throw Errors switch
        {
            JsonElement errors => Oops.Bah(errors.GetRawText()).WithData(Extras).WithData(Errors),
            string => Oops.Bah(Errors).WithData(Extras).WithData(Errors),
            _ => Oops.Bah(Errors.ToString()).WithData(Extras).WithData(Errors)
        };
    }
}