<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace Recipe.API.Repository.Common
{
    public interface IRepository<T>
    {
        List<T> Get();
        T GetById(Guid id);
        bool Insert(T entity);
        bool Update(Guid id, T entity);
        bool Delete(Guid id);
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Recipe.API.Repository.Common
{
    public interface IRepository<T>
    {
        List<T> Get();
        T GetById(Guid id);
        bool Insert(T entity);
        bool Update(Guid id, T entity);
        bool Delete(Guid id);
    }
>>>>>>> ee6675f (Nadograđen program i dodate nove funkcionalnosti)
}