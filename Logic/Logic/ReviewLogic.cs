using Data;
using Entities.Dtos.Review;
using Entities.Models;
using Logic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class ReviewLogic
    {
        Repository<Review> repo;
        DtoProvider dtoProvider;

        public ReviewLogic(Repository<Review> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public void AddReview(ReviewCreateDto dto)
        {
            var model = dtoProvider.Mapper.Map<Review>(dto);
            repo.Create(model);
        }

        public void DeleteReview(string id)
        {
            repo.DeleteById(id);
        }

    }
}
