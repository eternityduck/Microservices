﻿using UsersService.Data.Interfaces.Repositories;

namespace UsersService.Data.Interfaces;
public interface IUnitOfWork
{
    #region Repositories
    IUserRepository UserRepository { get; }
    #endregion

    Task RollbackAsync();
    Task SaveChangesAsync();
}
