using System.Text.Json;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Models.Common;
using commercetools.Sdk.Api.Models.CustomObjects;
using commercetools.Sdk.Api.Models.Types;
using Training.ViewModels;

namespace Training.Services
{
    public interface IExtensionsService
    {
        Task<ICustomObject> GetCustomObjectByContainerAndKeyAsync(string container, string key);
        Task<ICustomObject> CreateCustomObjectAsync(CustomObjectCreateRequest customObjectCreateRequest);
        Task<IType> CreateTypeAsync();
    }

    public class ExtensionsService : IExtensionsService
    {
        private readonly ProjectApiRoot _api;

        public ExtensionsService(ProjectApiRoot api)
        {
            _api = api;
        }

        public async Task<ICustomObject> GetCustomObjectByContainerAndKeyAsync(string container, string key) {
            return await _api
                .CustomObjects()
                .WithContainerAndKey(container, key)
                .Get()
                .ExecuteAsync();

        }
        public async Task<ICustomObject> CreateCustomObjectAsync(CustomObjectCreateRequest request)
        {
            List<Subscriber> subscribers;

            try
            {
                var existing = await GetCustomObjectByContainerAndKeyAsync(request.Container, request.Key);

                var jsonElement = (JsonElement)existing.Value;
                var json = jsonElement.GetRawText();

                subscribers = JsonSerializer.Deserialize<List<Subscriber>>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }) ?? new List<Subscriber>();
            }
            catch (commercetools.Base.Client.ApiHttpException ex) when (ex.StatusCode == 404)
            {
                subscribers = new List<Subscriber>();
            }

            subscribers.Add(request.Subscriber);

            return await _api
                .CustomObjects()
                .Post(new CustomObjectDraft
                {
                    Container = request.Container,
                    Key = request.Key,
                    Value = subscribers
                })
                .ExecuteAsync();
        }
        public async Task<IType> CreateTypeAsync()
        {
            return await _api
                .Types()
                .Post(new TypeDraft
                {
                    Key = "delivery-instructions",
                    Name = new LocalizedString{
                        { "en-US", "Delivery Instructions" },
                        { "de-DE", "Delivery Instructions" }
                    },
                    ResourceTypeIds = new List<IResourceTypeId> { IResourceTypeId.Customer, IResourceTypeId.Customer, IResourceTypeId.Order },
                    FieldDefinitions = new List<IFieldDefinition> { new FieldDefinition{
                        Name = "instructions",
                        Label = new LocalizedString{
                            { "en-US", "Instructions"},
                            { "de-DE", "Instructions"}
                        },
                        Required = true,
                        Type = new CustomFieldStringType()
                    },
                     new FieldDefinition{
                        Name = "time",
                        Label = new LocalizedString{
                            { "en-US", "Time" },
                            { "de-DE", "Time"   }
                        },
                        Required = true,
                        Type = new CustomFieldStringType()
                    }}
                })
                .ExecuteAsync();
        }
    }
}
