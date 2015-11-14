using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdDinner.Models
{
    public interface IDinnerRepository
    {
        IQueryable<Dinner> FindAllDinners();
        IQueryable<Dinner> FindUpcomingDinners();
        IQueryable<Dinner> FindByLocation(float latitude, float longitude);
        Dinner GetDinner(int id);
        void AddDinner(Dinner dinner);
        void Delete(Dinner dinner);
        void Save();
    }
}
