using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.UseCases.Configurations
{
    using System.Threading.Tasks;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    
    public class DeleteConfigurationUseCase : IUseCase<int, bool>
    {
        private readonly IConfigurationRepository _configurationRepository;
        
        public DeleteConfigurationUseCase(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        
        public async Task<bool> ExecuteAsync(int id)
        {
            var configuration = await _configurationRepository.GetByIdAsync(id);
            if (configuration == null)
                return false;
                
            await _configurationRepository.DeleteAsync(configuration);
            await _configurationRepository.SaveChangesAsync();
            
            return true;
        }
    }
} 