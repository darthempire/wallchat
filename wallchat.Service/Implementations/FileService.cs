using System;
using System.Collections.Generic;
using AutoMapper;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.File;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly Logger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public FileService (
            IUnitOfWork unitOfWork,
            IFileRepository fileRepository )
        {
            _logger = LogManager.GetCurrentClassLogger ();
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
        }

        public int Create ( RegisterFileDTO FileDto )
        {
            try
            {
                var file = new File ();

                if( FileDto == null ) return 0;
                file.Name = FileDto.Name;
                file.Type = FileDto.Type;
                file.PublishDate = DateTime.Now;
                file.IsDelete = false;
                file.Size = Convert.ToDouble ( FileDto.Size );
                file.Description = FileDto.Description;
                file.UserId = FileDto.UserId;
                _fileRepository.Add ( file );

                return file.Id;
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ( "Repository ex: " + re.Message );
            }
            catch( Exception ex )
            {
                throw new ServiceException ( ex.Message );
            }
        }

        public FileDTO Find ( int id )
        {
            try
            {
                var file = _fileRepository.GetById ( id );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<File, FileDTO> () );
                var fileDto = Mapper.Map<File, FileDTO> ( file );
                _logger.Info ( "Get File: id = " + id );
                return fileDto;
            }
            catch( RepositoryException rep )
            {
                _logger.Error ( "Method: Fine File ( long id )" );
                _logger.Error ( rep.Message );
                throw new ServiceException ( "Service exception: from repository ", rep );
            }
            catch( Exception ex )
            {
                _logger.Error ( $"Method: Fine File ( long id )", ex );
                throw new ServiceException ( "Method: FindUser ( long id )", ex );
            }
        }

        public List<FileDTO> GetAll ()
        {
            try
            {
                _logger.Info ( "Start getting all files" );
                var files = _fileRepository.GetAll ();
                Mapper.Initialize (
                    cfg => cfg.CreateMap<File, FileDTO> () );
                var filesDto = Mapper.Map<IEnumerable<File>, List<FileDTO>> ( files );
                _logger.Info ( "Get all files" );
                return filesDto;
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ( "Repository ex: " + re.Message );
            }
            catch( Exception ex )
            {
                throw new ServiceException ( ex.Message );
            }
        }

        public void Remove ( int id )
        {
            throw new NotImplementedException ();
        }

        public void Update ( FileDTO fileDto )
        {
            throw new NotImplementedException ();
        }
    }
}