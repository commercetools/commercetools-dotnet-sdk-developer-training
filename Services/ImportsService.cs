using CsvHelper;
using commercetools.Sdk.ImportApi.Client;
using commercetools.Sdk.ImportApi.Models.Importrequests;
using commercetools.Sdk.ImportApi.Models.Importsummaries;
using System.Globalization;
using commercetools.Sdk.ImportApi.Models.Productdrafts;
using commercetools.Sdk.ImportApi.Models.Common;
using Microsoft.Extensions.Options;
using Training.Options;

namespace Training.Services
{
    public interface IImportsService
    {
        Task<IImportSummary> GetSummaryByContainerAsync();
        Task<IImportResponse> ImportProductDraftsAsync(Stream stream);
    }

    public class ImportsService : IImportsService
    {
        private readonly ImportApiRoot _api;
        private const string Prefix = "CT";
        private const string ImportContainerKey = "my-import-container";
        private readonly string _projectKey;

        public ImportsService(ImportApiRoot api, IOptions<Settings> options)
        {
            _api = api;
            _projectKey = options.Value.ProjectKey;
        }

        public async Task<IImportSummary> GetSummaryByContainerAsync()
        {
            return await _api
                .WithProjectKeyValue(_projectKey)
                .ImportContainers()
                .WithImportContainerKeyValue(ImportContainerKey)
                .ImportSummaries()
                .Get()
                .ExecuteAsync();

        }
        public async Task<IImportResponse> ImportProductDraftsAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<ProductCsvRow>().ToList();

            var drafts = records.Select(MapCsvRowToProductDraft).ToList();

            // TODO: Import product drafts

            throw new NotImplementedException("This method is not yet implemented.");

        }

        private IProductDraftImport MapCsvRowToProductDraft(ProductCsvRow csvRow)
        {
            return new ProductDraftImport
            {
                ProductType = new ProductTypeKeyReference { Key = csvRow.ProductType },
                Key = (Prefix + "-" + csvRow.ProductName).ToLower(),
                Name = new LocalizedString { { "en-US", Prefix + "-" + csvRow.ProductName } },
                Slug = new LocalizedString { { "en-US", Prefix + "-" + csvRow.ProductName } },
                Description = new LocalizedString { { "en-US", csvRow.ProductDescription}},
                MasterVariant = new ProductVariantDraftImport
                {
                    Key = (Prefix + "-" + csvRow.ProductName).ToLower(),
                    Sku = Prefix + "-" + csvRow.InventoryId,
                    Images = new List<IImage>
                    {
                        new Image {Url = csvRow.ImageUrl, Dimensions = new AssetDimensions {W = 177, H = 237}}
                    },
                    Prices = new List<IPriceDraftImport>
                    {
                        new PriceDraftImport
                        {
                            Key = (Prefix + "-price-" + csvRow.InventoryId).ToLower(),
                            Value = new Money
                            {
                                CurrencyCode = csvRow.CurrencyCode,
                                CentAmount = csvRow.BasePrice
                            }
                        }
                    }
                }
            };
        }
    }
    public class ProductCsvRow
    {
        public string ProductType { get; set; }
        public string InventoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public long BasePrice { get; set; }
        public string CurrencyCode { get; set; }
        public string ImageUrl { get; set; }
    }
}
