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
        FileDTO Find (int id );
        void Remove (int id );
        void Update ( FileDTO fileDto );
        List<FileDTO> GetAll ();
        int Create ( RegisterFileDTO registerFileDto );
    }
}
