using GestionDesUsagers.Domain;
using GestionDesUsagers.Domain.Common;
using GestionDesUsagers.Domain.Entities;
using GestionDesUsagers.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesUsagers.Infrastructure.Repositories
{
    public class UsagerRepository : IUsagerRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Usager> _entityValidationService;
        //private readonly IRecherche<Emprunts> _rechercheEmprunt;
        public UsagerRepository(IUnitOfWork unitOfWork, IEntityValidationService<Usager> entityValidationService)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;                
        }

        public async Task AddAsync(Usager usager)
        {
            await _unitOfWork.Usagers.AddAsync(usager);
        }
        public async Task UpdateAsync(Usager usager)
        {
            await _unitOfWork.Usagers.UpdateAsync(usager);
        }
        public async Task DeleteAsync(Guid idUsager)
        {
            await _unitOfWork.Usagers.DeleteAsync(idUsager);
        }

        
        public async Task<IEnumerable<Usager>> GetAllAsync() 
        {

            return await _unitOfWork.Usagers.GetAllAsync();
        }

        public async Task<Usager> GetByIdAsync(Guid idUsager)
        {
            return await _unitOfWork.Usagers.GetByIdAsync(idUsager);
        }


    }
}
