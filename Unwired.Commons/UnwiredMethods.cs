using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.Xml.Serialization;
using Unwired.Commons.Enumarators;
using Unwired.Commons.Extensions;
using Unwired.Models.Enumarators;
using Unwired.Models.ViewModels;

namespace Unwired.Commons
{
    public static class UnwiredMethods
    {
        /// <summary>
        /// Return current datetime with DateTimeKind <br/>
        /// The time zone will be defined by the selected Kind, which can be: <br/>
        /// - UTC for Utc and Unspecified Kinds <br/>
        /// - Location for Kind Local
        /// </summary>
        /// <param name="kind">DateTimeKind. Default: Utc </param>
        /// <returns>DateTime. The current datetime</returns>
        public static DateTime Now(DateTimeKind kind = DateTimeKind.Utc)
        {
            if(kind == DateTimeKind.Utc || kind == DateTimeKind.Unspecified)
                return DateTime.UtcNow.SetKind(kind);

            return DateTime.Now.SetKind(kind);
        }

        /// <summary>
        /// Returns information from an enumerator
        /// </summary>
        /// <param name="fullNameEnum">Fullname of enumarator, include namespace. Example: Unwired.Common.Enumarators.MyEnumarator </param>
        /// <param name="filter">Word or Expression to filter. Filter Rule: The filter is applied to the already translated description property (if using the translation resource) and uses Contains to execute the filter. Default: null </param>
        /// <param name="page">Desired page number. Default: 1</param>
        /// <param name="pageSize">Number of records contained in a page. Default: 25</param>
        /// <param name="sortBy">Order of records. Default: null. </param>
        /// <param name="translationResource">Resources file to be used in description translation. Translation rule: The translation file must have a key with the same name as the enum key, the value of this key will be returned.</param>
        /// <returns>A paginated list of type EnumViewModel with the enum information.</returns>
        public static PaginatedViewModel<EnumViewModel> GetEnumValues(string fullNameEnum, string filter = null, int page = 1, int pageSize = 25, Dictionary<OrderEnum, OrderOrientationEnum> sortBy = null, object translationResource = null)
        {            
            if (fullNameEnum is null)
                return new PaginatedViewModel<EnumViewModel>(null, page, pageSize, 0, 0);

            var enumerator = Type.GetType($"{fullNameEnum}");
            if (enumerator is null)
                return new PaginatedViewModel<EnumViewModel>(null, page, pageSize, 0, 0);


            var values = enumerator.GetEnumValues();
            if (values is null)
                return new PaginatedViewModel<EnumViewModel>(null, page, pageSize, 0, 0);


            var enumResult = new List<EnumViewModel>();
            var total = values.Length;
            var totalPages = (int)Math.Ceiling((total / (decimal)pageSize));
            var description = string.Empty;
            var orderCount = 0;
            foreach (var value in values)
            {                
                if (value is null)
                    continue;

                object currentValue = (object)value;

                description = currentValue.GetEnumDescription();
                //translation of description
                if (translationResource is not null)
                {
                    var localeDescription = GetDescriptionFromResourceFile(value.ToString() ?? "", translationResource.GetType());
                    if (string.IsNullOrEmpty(localeDescription))                    
                        description = localeDescription;                    
                }

                if (!string.IsNullOrEmpty(filter) && !description.ToLower().Contains(filter.ToLower()))
                    continue;

                enumResult.Add(new EnumViewModel()
                {
                    Key = value.ToString(),
                    Value = ((int)value).ToString(),
                    Description = description
                });
            }

            if (sortBy.Count > 0)
            {                
                var firstOrder = true;
                IOrderedEnumerable<EnumViewModel> enumOrdered = null;

                foreach (var currentSort in sortBy)
                {
                    if (orderCount > 2)
                        break;

                    orderCount++;

                    switch (currentSort.Key)
                    {

                        case OrderEnum.Key:
                            if (currentSort.Value == OrderOrientationEnum.Ascending)
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderBy(p => p.Key);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenBy(p => p.Key);

                            }
                            else
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderByDescending(p => p.Key);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenByDescending(p => p.Key);
                            }

                            break;
                        case OrderEnum.Value:
                            if (currentSort.Value == OrderOrientationEnum.Ascending)
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderBy(p => p.Value);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenBy(p => p.Value);

                            }
                            else
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderByDescending(p => p.Value);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenByDescending(p => p.Value);
                            }

                            break;

                        case OrderEnum.Description:
                            if (currentSort.Value == OrderOrientationEnum.Ascending)
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderBy(p => p.Description);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenBy(p => p.Description);
                            }
                            else
                            {
                                if (firstOrder)
                                {
                                    enumOrdered = enumResult.OrderByDescending(p => p.Description);
                                    firstOrder = false;
                                }
                                else
                                    enumOrdered = enumOrdered.ThenByDescending(p => p.Description);
                            }

                            break;

                        default:
                            break;
                    }
                }

                enumResult = enumOrdered.ToList();
            }

            var records = enumResult.Take(pageSize).Skip(pageSize * (page - 1)).ToList();
            return new PaginatedViewModel<EnumViewModel>(records, page, pageSize, total, totalPages);
        }

        /// <summary>
        /// Deserialize a Xml from file into a object.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="filePath">Path of file xml</param>
        /// <returns>An object typed with TResult</returns>
        [RequiresUnreferencedCode("")]
        public static TResult DeserializeXmlFromFile<TResult>(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(TResult));
            string xml = File.ReadAllText(filePath);
            using var textReader = new StringReader(xml);            
            return (TResult)xmlSerializer.Deserialize(textReader);            
        }        

        /// <summary>
        /// Deserialize a Xml from a string into a object.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="xml">String containing xml information</param>
        /// <returns>An object typed with TResult</returns>
        [RequiresUnreferencedCode("")]
        public static TResult DeserializeXmlFromString<TResult>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(TResult));
            using var textReader = new StringReader(xml);
            return (TResult)xmlSerializer.Deserialize(textReader);
        }

        /// <summary>
        /// Get a value of a key in any resource file
        /// </summary>
        /// <param name="key">Key to be returned</param>
        /// <param name="typeResourceFile">Resource File for key search</param>
        /// <returns>String. Value of Key.</returns>
        private static string GetDescriptionFromResourceFile(string key, Type typeResourceFile)
            => (new ResourceManager(typeResourceFile)).GetString(key);        
    }
}