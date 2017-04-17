using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wallchat.Model.App.DTO;

namespace wallchat.Service.Contracts
{
    public interface IFileService
    {
        FileDTO Find ( long id );
        void Remove ( long id );
        void Update ( FileDTO fileDto );
        List<FileDTO> GetAll ();
        void Create ( RegisterFileDTO registerFileDto );
    }
}
