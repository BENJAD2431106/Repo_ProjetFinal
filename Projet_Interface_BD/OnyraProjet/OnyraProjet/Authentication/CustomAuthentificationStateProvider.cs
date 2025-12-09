using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IO.Pipes;
using System.Security.Claims;

namespace OnyraProjet.Authentication
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthentificationStateProvider(ProtectedSessionStorage protectedSessionStorage)
        {
            _sessionStorage = protectedSessionStorage;
        }

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var claimsPrincipal = _anonymous;
        //    try
        //    {
        //        var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
        //        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

        //        if (userSession != null)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Role, userSession.Role),
        //                new Claim(ClaimTypes.Name, userSession.NomUtilisateur),
        //                new Claim(ClaimTypes.Surname, userSession.PrenomUtilisateur),
        //                new Claim(ClaimTypes.NameIdentifier, userSession.NoUtilisateur.ToString()),
        //                new Claim(ClaimTypes.UserData, userSession.Medecin.ToString())
        //            };
        //            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
        //        }
        //    }
        //    catch
        //    {
        //        claimsPrincipal = _anonymous;
        //    }
        //    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        //}

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = _anonymous;
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession2>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

                if (userSession != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, userSession.Role),
                        new Claim(ClaimTypes.Name, userSession.Name),
                        new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString())
                    };
                    claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
                }
            }
            catch
            {
                claimsPrincipal = _anonymous;
            }
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        //public async Task UpDateAuthenticationState(UserSession userSession)
        //{
        //    ClaimsPrincipal claimsPrincipal;
        //    if (userSession != null)
        //    {
        //        await _sessionStorage.SetAsync("UserSession", userSession);

        //        var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Role, userSession.Role),
        //                new Claim(ClaimTypes.Name, userSession.NomUtilisateur),
        //                new Claim(ClaimTypes.Surname, userSession.PrenomUtilisateur),
        //                new Claim(ClaimTypes.NameIdentifier, userSession.NoUtilisateur.ToString()),
        //                new Claim(ClaimTypes.UserData, userSession.Medecin.ToString())
        //            };
        //        claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
        //    }
        //    else
        //    {
        //        await _sessionStorage.DeleteAsync("UserSession");
        //        claimsPrincipal = _anonymous;
        //    }

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        //}

        public async Task UpDateAuthenticationState(UserSession2 userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, userSession.Role),
                        new Claim(ClaimTypes.Name, userSession.Name),
                        new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString())
                    };
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<UserSession2?> GetUserSessionAsync()
        {
            try
            {
                var result = await _sessionStorage.GetAsync<UserSession2>("UserSession");
                return result.Success ? result.Value : null;
            }
            catch
            {
                return null;
            }
        }

    }
}
