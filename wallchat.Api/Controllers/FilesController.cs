using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using wallchat.Api.App.Filters;
using wallchat.Api.Models;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Model.App.DTO.Users;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IFileService _fileService;
        public FilesController (IFileService fileService)
        {
            _fileService = fileService;
        }

        private long CurrentUserId
        {
            get
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;
                var userId = principal?.Claims.FirstOrDefault(c => c.Type == "userId");
                return userId != null ? Convert.ToInt64(userId?.Value) : 0;
            }
        }

        [Role("*")]
        // POST api/<controller>
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Ok("Файлы не загружены");
            }
            var provider = new MultipartMemoryStreamProvider();
            // путь к папке на сервере
            string root = System.Web.HttpContext.Current.Server.MapPath("~/uploadedFiles/");
            await Request.Content.ReadAsMultipartAsync(provider);

            var fileLength = 0;
            var filename = "";
            foreach (var file in provider.Contents)
            {
                filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                //var buffer = await file.ReadAsByteArrayAsync();
                byte[] fileArray = await file.ReadAsByteArrayAsync();
                fileLength = fileArray.Length;
                using (System.IO.FileStream fs = new System.IO.FileStream(root + filename, System.IO.FileMode.Create))
                {
                    await fs.WriteAsync(fileArray, 0, fileArray.Length);
                }
            }

            var fileDto = new RegisterFileDTO
            {
                Name = filename,
                UserId = CurrentUserId,
                Description = "Description",
                Size = fileLength,
                Type = "jpg"
            };
            _fileService.Create (fileDto);

            return Ok("файлы загружены");
        }

        //все подписки
        // GET api/<controller>
        [Role("*")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Json(GetAllFiles());
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message,
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                };
                return Json(error);
            }

        }

        // GET api/<controller>/5
        [Role("*")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var file = _fileService.Find(id);
                Mapper.Initialize(
                    cfg => cfg.CreateMap<FileDTO, FileModel>());
                var viewFile = Mapper.Map<FileDTO, FileModel>(file);
                return Json(viewFile);
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message
                };
                return Json(error);
            }
        }

        private List<FileModel> GetAllFiles ()
        {
            var files = _fileService.GetAll();
            Mapper.Initialize(
                cfg => cfg.CreateMap<FileDTO, FileModel>());
            return Mapper.Map<List<FileDTO>, List<FileModel>>(files);
        }

    }
}