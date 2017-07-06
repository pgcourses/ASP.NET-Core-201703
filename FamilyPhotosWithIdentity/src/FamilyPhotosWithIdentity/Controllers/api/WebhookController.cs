using FamilyPhotosWithIdentity.Models.Github;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyPhotosWithIdentity.Controllers.api
{
    //+-----------------------------------+             +----------------------------------+
    //|   GitHub repository               |             |  WebAlkalmazás                   |
    //|-----------------------------------|             |----------------------------------|
    //|                                   |             |WebHook Controller                |
    //|     Esemény1+-------------------------------------> Action1                        |
    //|     Esemény2+-------------------------------------> Action2                        |
    //|     Esemény3+-------------------------------------> Action3                        |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //|                                   |             |                                  |
    //+-----------------------------------+             |                                  |
    //                                                  |                                  |
    //                                                  +----------------------------------+

    // https://requestb.in/1ao3pcb1?inspect

    //+------------------+             +--------------+                                     +--------------------+
    //| Github           |             | NGROK        |                                     | Developer PC       |
    //|------------------|             |--------------|                                     |--------------------|
    //|                  |             |              |                                     |                    |
    //|      +           |             |              |                                     |                    |
    //|      +------------------>  ngrok_url +---------------------------> localhost:59167  |  ngrok http 59167  |
    //|                  |             |              |                                     |      +             |
    //|                  |             |              |                                     |      |             |
    //|                  |             |              |                                     |      |             |
    //|                  |             |              |                                     |      v             |
    //|                  |             |              |                                     |  api/Github        |
    //|                  |             |              |                                     |                    |
    //|                  |             |              |                                     |                    |
    //|                  |             |              |                                     |                    |
    //+------------------+             +--------------+                                     +--------------------+

    [Route("api/Github")]
    [AllowAnonymous]
    public class WebhookController : Controller
    {
        [HttpPost]
        public IActionResult Post()
        {
            var length = HttpContext.Request.ContentLength;
            if (int.MaxValue<length)
            {
                throw new ArgumentOutOfRangeException($"Túl hosszú kérés: {length}");
            }

            var buffer = new byte[(int)HttpContext.Request.ContentLength];

            var count = HttpContext.Request.Body.Read(buffer, 0, (int)length);

            if (count!=(int)length)
            {
                throw new Exception($"valami hiba az olvasásnál: {count} != {length}");
            }

            var payload = Encoding.UTF8.GetString(buffer);

            if (payload != null)
            {
                var request = JsonConvert.DeserializeObject<GithubRequest>(payload);
            }
            return Ok();
        }

    }
}
