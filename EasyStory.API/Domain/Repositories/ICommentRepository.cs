﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;

namespace EasyStory.API.Domain.Repositories
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> ListAsync();
        public Task AddAsync(Comment comment);
        public Task<Comment> FindById(long id);
        void Update(Comment comment);
        void Remove(Comment comment);
    }
}