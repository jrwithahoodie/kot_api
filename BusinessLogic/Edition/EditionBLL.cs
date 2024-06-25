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
                    .FirstOrDefault();
                
                if (existingEdition != null)
                    throw new Exception("Esta edición ya existe");
                
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
    }
}