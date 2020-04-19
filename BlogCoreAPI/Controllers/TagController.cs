using BlogBLL;
using BlogModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BlogCoreAPI.Controllers
{
    [Route("api/[controller]")]

    public class TagController : Controller
    {
        private readonly IBlogLogic _blogLogic;
        public TagController(IBlogLogic blogLogic)
        {
            _blogLogic = blogLogic;
        }
        [HttpGet]
        public ActionResult<TagList> GetTagList()
        {
            return _blogLogic.GetTagList();
        }
    }
}
