using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface IBookRepository
    {
        PagedData<List<BookModel>> GetBooks(string userId,int page = 0, int pageSize = 0, List<string> Tags = null,string searchText=null);
        BookModel Find(long id);
        PagedData<List<BookProposalModel>>  GetBookProposals(int page, int pageSize);
        List<Author> GetAuthors();
        long MakeNewBookProposal(string userId, string title, string description, List<Author> authors, List<string> tags,int? datePublished);


        void AddToFavourites(string userId, long bookId);
        
        void AddToRead(string userId, long bookId);
        
        void AddToLikeToRead(string userId, long bookId);
     
        void RemoveFromFavourites(string userId, long bookId);
      

        void RemoveFromLikeToRead(string userId, long bookId);
        
        void RemoveFromRead(string userId, long bookId);
       


    }
}
