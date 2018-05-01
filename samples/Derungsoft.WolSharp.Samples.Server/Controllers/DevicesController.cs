using System;
using System.Collections.Generic;
using System.Linq;
using Derungsoft.WolSharp.Samples.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Derungsoft.WolSharp.Samples.Server.Controllers
{
    public class DevicesResponse
    {
        public DevicesResponse()
        {
        }

        public DevicesResponse(string error)
        {
            Error = error;
        }

        [JsonProperty("success")]
        public bool Success => string.IsNullOrEmpty(Error);

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty("deviceId", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeviceId { get; set; }
    }

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly WolSharpDbContext _wolSharpDbContext;
        private readonly IPhysicalAddressParser _physicalAddressParser;

        public DevicesController(WolSharpDbContext wolSharpDbContext, IPhysicalAddressParser physicalAddressParser)
        {
            _wolSharpDbContext = wolSharpDbContext;
            _physicalAddressParser = physicalAddressParser;
        }

        // GET: api/Device
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            return _wolSharpDbContext.Devices;
        }

        // GET: api/Device/5
        [HttpGet("{id}", Name = "Get")]
        public Device Get(int id)
        {
            return _wolSharpDbContext.Devices.FirstOrDefault(d => d.Id == id);
        }

        // POST: api/Device
        [HttpPost]
        public void Post([FromBody]Device device)
        {
            if (device != null && device.Id > 0)
            {
                _wolSharpDbContext.Devices.Update(device);
                _wolSharpDbContext.SaveChanges();
            }
        }

        // PUT: api/Device/5
        [HttpPut]
        public DevicesResponse Put([FromBody]Device device)
        {
            if (device == null)
            {
                return new DevicesResponse($"{nameof(device)} cannot be null");
            }

            if (device.Id != 0)
            {
                return new DevicesResponse($"{nameof(device.Id)} must be 0");
            }

            try
            {
                _physicalAddressParser.Parse(device.MacAddress);
                var insertedDevice = _wolSharpDbContext.Devices.Add(device);
                _wolSharpDbContext.SaveChanges();
                return new DevicesResponse { DeviceId = insertedDevice.Entity.Id };
            }
            catch (Exception e)
            {
                return new DevicesResponse(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var device = _wolSharpDbContext.Devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                _wolSharpDbContext.Devices.Remove(device);
                _wolSharpDbContext.SaveChanges();
            }
        }
    }
}
