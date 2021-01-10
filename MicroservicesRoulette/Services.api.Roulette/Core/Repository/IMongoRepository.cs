using MongoDB.Driver;
using Services.api.Roulette.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette.Core.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();

        Task<TDocument> GetById(string id);

        Task<IEnumerable<TDocument>> GetByFilter(FilterDefinition<TDocument> filter);

        Task InsertDocument(TDocument document);

        Task UpdateDocument(TDocument document);

        Task DeleteById(string id);
    }
}
