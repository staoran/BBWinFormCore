using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BB.Core.Filter;

public class ObjectModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var val = bindingContext.ValueProvider.GetValue(
            bindingContext.ModelName
        );
        bindingContext.Model = val.FirstValue;
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
    }
}