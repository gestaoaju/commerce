// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Linq;

namespace Gestaoaju.Models.EntityModel.Account.Users
{
    public static class UserQuery
    {
        public static IQueryable<User> WhereEmail(this IQueryable<User> users, string email)
        {
            return users.Where(user => user.Email == email);
        }

        public static IQueryable<User> WhereId(this IQueryable<User> users, int id)
        {
            return users.Where(user => user.Id == id);
        }

        public static IQueryable<User> WhereToken(this IQueryable<User> users, string token)
        {
            return users.Where(user => user.AccessCode == token);
        }
    }
}
