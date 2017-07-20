using FamilyPhotosWithIdentity.Data;
using FamilyPhotosWithIdentity.Models.Github;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


    // Osztályhierarchia

    //     +--------------+              +---------+
    //     |    Request   |+------------>|  Hook   |
    //     |--------------|              +---------+
    //     |              |
    //     |              |              +---------+                UUUU
    //     |              |              |         |               +--------+
    //     |              |+------------>| Issue   |+------------> |User    |
    //     |              |              |         |               +--------+
    //     +-+------------+              |         |
    //       |    +  +                   |         |               +---------------+
    //       |    |  |                   +---------+               | Assignee      |
    //       |    |  |                                             |               |
    //       |    |  |                   +---------+               +---------------+
    //       |    |  |                   |         |
    //       |    |  |                   | Repository
    //       |    |  +------------------>|         |               +------------------+      UUUU
    //       |    |                      |         |   UUUU        |                  |     +----------+
    //       |    |                      |         |  +---------+  |  Milestone       |+--->| Creator  |
    //       |    |                      |         |+>| Owner   |  |                  |     +----------+
    //       |    |                      +---------+  |         |  |                  |
    //  UUUU v    |                                   +---------+  +------------------+
    // +-------+  |                      +---------+
    // |User   |  |                      |         |
    // |       |  +--------------------->|Organization
    // +-------+                         |         |
    //                                   |         |
    //                                   +---------+

    [Route("api/Github")]
    [AllowAnonymous]
    public class WebhookController : Controller
    {
        private readonly ApplicationDbContext db;

        public WebhookController(ApplicationDbContext db)
        {
            this.db = db;
        }

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
                if (request!=null)
                {
                    db.AddOrUpdate(request.sender);
                    db.AddOrUpdate(request.issue.user);
                    db.AddOrUpdate(request.issue.assignee);

                    foreach (var assignee in request.issue.assignees)
                    {
                        db.AddOrUpdate(assignee);
                    }

                    db.AddOrUpdate(request.repository.owner);
                    db.AddOrUpdate(request.repository);
                    db.AddOrUpdate(request.organization);
                    db.AddOrUpdate(request.issue.assignee);
                    db.AddOrUpdate(request.issue.milestone);
                    db.AddOrUpdate(request.issue.milestone?.creator);
                    db.SaveChanges();

                    db.AddOrUpdate(request.issue);

                    db.SaveChanges();

                    db.AddOrUpdate(request);
                    db.SaveChanges();
                }
            }
            return Ok();
        }

    }
}
