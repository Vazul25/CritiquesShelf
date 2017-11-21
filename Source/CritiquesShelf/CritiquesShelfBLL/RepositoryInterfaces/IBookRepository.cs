using CritiquesShelfBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface IBookRepository
    {
    PagedData<List<BookModel>> GetBooks(int page = 0, int pageSize = 0);
        BookModel Find(long id);
        PagedData<List<BookProposalModel>>  GetBookProposals(int page, int pageSize);
    }
}
