using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OGCP.Curriculum.API.dtos;
using Microsoft.Extensions.Primitives;

namespace OGCP.Curriculum.API.helpers;
public class ProfileModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType != typeof(ProfileRequest))
        {
            return null;
        }

        var subclasses = new[] { typeof(CreateGeneralProfileRequest), typeof(CreateQualifiedProfileRequest), };

        var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
        foreach (var type in subclasses)
        {
            var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
            binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
        }

        return new ProfileModelBinder(binders);
    }
}

public class ProfileModelBinder : IModelBinder
{
    private Dictionary<Type, (ModelMetadata, IModelBinder)> binders;

    public ProfileModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
    {
        this.binders = binders;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelFirstName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, nameof(ProfileRequest.FirstName));
        StringValues modelTypeValue = bindingContext.ValueProvider.GetValue("RequestType").Values;

        foreach (var key in modelTypeValue)
        {
            Console.WriteLine($"{key}");
        }

        IModelBinder modelBinder;
        ModelMetadata modelMetadata;

        if (modelTypeValue == nameof(CreateQualifiedProfileRequest))
        {
            (modelMetadata, modelBinder) = binders[typeof(CreateQualifiedProfileRequest)];
        }
        else if (modelTypeValue == nameof(CreateGeneralProfileRequest))
        {
            (modelMetadata, modelBinder) = binders[typeof(CreateGeneralProfileRequest)];
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }


        var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
            bindingContext.ActionContext,
            bindingContext.ValueProvider,
            modelMetadata,
            bindingInfo: null,
            bindingContext.ModelName);

        await modelBinder.BindModelAsync(newBindingContext);
        bindingContext.Result = newBindingContext.Result;

        if (newBindingContext.Result.IsModelSet)
        {
            // Setting the ValidationState ensures properties on derived types are correctly 
            bindingContext.ValidationState[newBindingContext.Result.Model] = new ValidationStateEntry
            {
                Metadata = modelMetadata,
            };
        }
    }
}