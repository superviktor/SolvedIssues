﻿namespace RoleBasedAuth
{
    public static class ExtensionMethods
    {
        public static User WithoutPassword(this User user)
        {
            if (user == null)
                return null;

            user.Password = null;
            return user;
        }
    }
}