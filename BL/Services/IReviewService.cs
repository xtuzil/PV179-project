﻿using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDto>> GetReviewsOnUser(int usedId);
    }
}
