using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Derungsoft.WolSharp.WebApi.Controllers
{
    public class WakeOnLanResponse
    {
        public WakeOnLanResponse()
        {

        }

        public WakeOnLanResponse(string error)
        {
            Error = error;
        }

        [JsonProperty("success")]
        public bool Success => string.IsNullOrEmpty(Error);

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
    }

    [Route("api/[controller]")]
    public class WakeController : Controller
    {
        private readonly IAwakener _awakener;

        public WakeController(IAwakener awakener)
        {
            _awakener = awakener;
        }

        [HttpGet("{macAddress}")]
        public async Task<WakeOnLanResponse> Get(string macAddress)
        {
            try
            {
                await _awakener.WakeAsync(macAddress);
            }
            catch (Exception e)
            {
                return new WakeOnLanResponse(e.Message);
            }

            return new WakeOnLanResponse();
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<WakeOnLanResponse> Post([FromForm]string macAddress)
        {
            try
            {
                await _awakener.WakeAsync(macAddress);
            }
            catch (Exception e)
            {
                return new WakeOnLanResponse(e.Message);
            }

            return new WakeOnLanResponse();
        }
    }
}
