using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace CursoAngular.API.Bindings
{
    public class CustomModelBinder<T> : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(propertyName);

            if (value == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(value.FirstValue);

                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(propertyName, $"The type {typeof(T)} is not valid for {propertyName} property.");
            }

            return Task.CompletedTask;
        }
    }
}
