using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace ASP.API.Controllers;

[OpenApiTag("news - Новости")]
[Route("api/v1/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    // GET api/news
    [OpenApiOperation("Получить список новостей", "Получить список новостей за последние 3 года")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> Get()
        => await Task.FromResult<ActionResult<IEnumerable<News>>>(
            newsItems.OrderByDescending(n => n.DateCreated).ToArray()
        );

    // GET api/news/3
    
    [HttpGet("{id}")]
    public async Task<ActionResult<News>> Get(int id)
    {
        var news = newsItems.FirstOrDefault(n => n.Id == id);
        return news == null 
            ? await Task.FromResult<ActionResult<News>>(NotFound())
            : await Task.FromResult<ActionResult<News>>(news);
    }

    // POST api/news
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] News value)
        => await Task.FromResult<ActionResult>(BadRequest());

    // PUT api/news/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] News value)
    {
    }

    // DELETE api/news/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    private readonly List<News> newsItems = new List<News> {
            new News { 
                Id = 1,
                Title = "Новость №1",
                DateCreated = DateTime.Today.AddDays(-3)
            },
            new News { 
                Id = 2,
                Title = "Новость №2",
                DateCreated = DateTime.Today.AddDays(-2)
            },
            new News { 
                Id = 3,
                Title = "Новость №3",
                DateCreated = DateTime.Today.AddDays(-1)
            },
            new News { 
                Id = 4,
                Title = "Новость №4",
                DateCreated = DateTime.Today
            }
    };
}