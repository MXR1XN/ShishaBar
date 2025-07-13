using UnityEngine;

public static class User
{
    public static int Id;
    public static string UserName;
    public static string Email;

    public static void Initialize(int IdP, string UserNameP, string EmailP)
    {
        Id = IdP;
        UserName = UserNameP;
        Email = EmailP;
    }
}
