
using Microsoft.AspNetCore.Mvc.Filters;

namespace OGCP.Curriculum.API.Filters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class SkipModelValidationFilter : Attribute, IFilterMetadata
{
}
