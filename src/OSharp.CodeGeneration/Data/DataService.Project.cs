using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSharp.CodeGeneration.Entities;
using OSharp.Data;

namespace OSharp.CodeGeneration.Data
{
    public partial class DataService
    {
        public async Task<OperationResult> CreateProject(CodeProject project)
        {
            
            int count = await this.ProjectRepository.InsertAsync(project);
            if (count > 0)
            {
                
            }
            throw new NotImplementedException();
        }

        public Task<OperationResult> DeleteProject(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateProject(CodeProject project)
        {
            throw new NotImplementedException();
        }
    }
}
