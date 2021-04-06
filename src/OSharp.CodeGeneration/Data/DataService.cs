using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OSharp.CodeGeneration.Entities;
using OSharp.Data;
using OSharp.Entity;

namespace OSharp.CodeGeneration.Data
{
    public partial class DataService : IDataContract
    {
        private readonly IServiceProvider _serviceProvider;

        public DataService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IUnitOfWork UnitOfWork => _serviceProvider.GetUnitOfWork();

        protected IRepository<CodeProject, Guid> ProjectRepository => _serviceProvider.GetService<IRepository<CodeProject, Guid>>();

        protected IRepository<CodeModule, Guid> ModuleRepository => _serviceProvider.GetService<IRepository<CodeModule, Guid>>();

        protected IRepository<CodeEntity, Guid> EntityRepository => _serviceProvider.GetService<IRepository<CodeEntity, Guid>>();

        protected IRepository<CodeProperty, Guid> CodePropertyRepository => _serviceProvider.GetService<IRepository<CodeProperty, Guid>>();

    }
}
