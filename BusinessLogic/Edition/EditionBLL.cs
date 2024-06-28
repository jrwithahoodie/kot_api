using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.AppContext;

namespace BusinessLogic.Edition
{
    public class EditionBLL : IEditionBll
    {
        #region Fields
        private readonly Context _context;
        #endregion

        #region Builder
        public EditionBLL()
        {
            _context = new Context();
        }
        #endregion
        public IEnumerable<Entities.Entities.Edition> Get()
        {
            try
            {
                var editionList = _context.Editions.ToList();

                return editionList;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                throw;
            }
        }

        public Entities.Entities.Edition Post(Entities.Entities.Edition newEditionData)
        {
            try
            {
                var existingEdition = _context.Editions
                    .Where(e => e.Name == newEditionData.Name)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("Esta edición ya existe");
                
                var edition = _context.Editions.Add(newEditionData);
                _context.SaveChanges();

                return edition.Entity;
            }
            catch (Exception ex)
            {
                var m  = ex.Message;
                throw;
            }
        }

        public Entities.Entities.Edition Update(string editionName)
        {
            try
            {
                if(string.IsNullOrEmpty(editionName))
                    throw new Exception("El nombre de la edición no puede ser nulo/vacío");

                var edition = _context.Editions
                    .Where(e => e.Name == editionName)
                    .ToList()
                    .FirstOrDefault()
                        ?? throw new Exception("No existe ninguna edición con el nombre indicado");

                edition.IsActive = !edition.IsActive;

                var result =_context.Update(edition);
                _context.SaveChanges();

                return result.Entity;
            }
            catch (Exception ex)
            {
                var m  = ex.Message;
                throw;
            }
        }
    }
}