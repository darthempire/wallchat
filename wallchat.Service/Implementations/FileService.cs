using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Model.App.DTO;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Repository.App.File;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class FileService : IFileService
    {
        private readonly Logger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _fileRepository;

        public FileService(
            IUnitOfWork unitOfWork,
            IFileRepository fileRepository)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
        }
        public void Create(RegisterFileDTO FileDto)
        {
            var file = new File();

            if (FileDto != null)
            {
                file.Name = FileDto.Name;
                file.Type = FileDto.Type;
                file.PublishDate = DateTime.Now;
                file.IsDelete = false;
                file.Size = FileDto.Size;
                file.Description = FileDto.Description;
                file.UserId = 1;
                _fileRepository.Add(file);
            }
        }

        public FileDTO Find(long id)
        {
            throw new NotImplementedException();
        }

        public List<FileDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(FileDTO fileDto)
        {
            throw new NotImplementedException();
        }
    }
}
