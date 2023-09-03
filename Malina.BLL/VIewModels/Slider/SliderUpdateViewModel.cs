using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malina.BLL.VIewModels.Slider
{
    public class SliderUpdateViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public string SliderName { get; set; }
        public string Description { get; set; }
    }
}
