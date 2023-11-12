using dataTrip.DTOS.Accounts;
using dataTrip.DTOS.Images;
using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;

        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetImages()
        {
            var result = await _imagesService.GetAllAsync();
            return Ok(new { data = result.Select(ImagesResponse.FromImage) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagesByLocation(int id)
        {
  
            var result = await _imagesService.GetImageLocation(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { data = result.Select(ImagesResponse.FromImage) });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Images>> DeleteImages([FromQuery] int id)
        {
            var result = await _imagesService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _imagesService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Images>> AddImage([FromForm] ImagesRequest imagesRequest)
        {
            (string erorrMesage, List<string> imageName) = await _imagesService.UploadImage(imagesRequest.ImageSum);
            //if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            var images = imagesRequest.Adapt<Images>();
            await _imagesService.CreactAsync(images,imageName);
            return Ok(new { msg = "OK", data = imagesRequest });

        }
    }
}
