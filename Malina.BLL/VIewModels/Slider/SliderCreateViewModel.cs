using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malina.BLL.VIewModels.Slider
{
    public class SliderCreateViewModel
    {
        public IFormFile Image { get; set; }
        public string SliderName { get; set; }
        public string Description { get; set; }
    }
}
