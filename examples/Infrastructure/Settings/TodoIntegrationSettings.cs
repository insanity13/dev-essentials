using NetOptions.Core;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Settings
{
    [Options("TodoSettings:Integration")]
    internal class TodoIntegrationSettings
    {
        [Required(ErrorMessage = $"{nameof(Url)} required")]
        public string Url { get; set; }
    }
}
