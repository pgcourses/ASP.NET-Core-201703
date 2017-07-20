using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using FamilyPhotosWithIdentity.Models;
using AspNet.Security.OpenIdConnect.Extensions;

namespace FamilyPhotosWithIdentity.Helpers
{
    public static class OpenIdConnectHelpers
    {

        /// <summary>
        /// A kérések vavlidálását végzi el, csak akkor 
        /// megy tovább a valósi tennivalóra, ha a kérés valid.
        /// Mivel nagyon egyszerű workflow-t akarunk legyártani,
        /// ezért a beépített validációt le kell butítanunk,
        /// és kétféle kérést elfogadnunk, a többit pedig nem ellenőrizni.
        /// </summary>
        /// <param name="options"></param>
        public static void UseMyValidateTokenRequest(this OpenIdConnectServerOptions options)
        {
            //Delegate helyett függvénnyel is megadhatjuk a tennivalót:
            //options.Provider.OnValidateTokenRequest = OnValidateTokenRequest;

            options.Provider.OnValidateTokenRequest = context =>
            {
                if (!context.Request.IsPasswordGrantType()
                    && !context.Request.IsRefreshTokenGrantType())
                {
                    context.Reject(
                        error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                        description: "Nem megfelelő kérés"
                        );
                    return Task.FromResult(0);
                }

                //a további validálást kikapcsoljuk
                context.Skip();
                return Task.FromResult(0);
            };
        }

        //Delegate nélkül így lehetne definiálni ezt
        ////////////////////////////////////////////
        //private static Task OnValidateTokenRequest(ValidateTokenRequestContext context)
        //{
        //    if (!context.Request.IsPasswordGrantType()
        //        && !context.Request.IsRefreshTokenGrantType())
        //    {
        //        context.Reject(
        //            error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
        //            description: "Nem megfelelő kérés"
        //            );
        //        return Task.FromResult(0);
        //    }

        //    //a további validálást kikapcsoljuk
        //    context.Skip();
        //    return Task.FromResult(0);
        //}


        
        public static void UseMyHandleTokenRequest(this OpenIdConnectServerOptions options)
        {
            options.Provider.OnHandleTokenRequest = async context => //async, mert az identity async felületet ad
            {
                if (context.Request.IsPasswordGrantType())
                {
                    //csak ezt kell implementálni, a token refresh-t megoldja a szerver
                    //beépített működése

                    //Ellenőriznünk kell, hogy a felhasználó neve és jelszava rendben van-e?

                    //Ehhez szükségünk van a UserManager-re
                    //figyelem: ne mi hozzunk létre példányt hanem 
                    //bízzuk rá az ASP.NET Core Dependency Service-ére!!!
                    var manager = context.HttpContext
                                         .RequestServices
                                         .GetRequiredService<UserManager<ApplicationUser>>();

                    //1. lépés: ellenőrizzük a felhasználót
                    var user = await manager.FindByNameAsync(context.Request.Username);

                    if (user==null)
                    {
                        context.Reject(
                            error: OpenIdConnectConstants.Errors.InvalidGrant,
                            description: "nem megfelelő bejelentkezési adatok"
                            );
                        return;
                    }

                    //2. ellenőrizzük a jelszavát, ehhez a bejelentkezési szolgáltatás kell.
                    var signInManager = context.HttpContext
                                               .RequestServices
                                               .GetRequiredService<SignInManager<ApplicationUser>>();

                    //először is ellenőrizzük, hogy nincs-e letilva
                    var canSignIn = await signInManager.CanSignInAsync(user);
                    if (!canSignIn)
                    {
                        context.Reject(
                            error: OpenIdConnectConstants.Errors.InvalidGrant,
                            description: "a felhasználó le van tiltva"
                            );
                        return;
                    }

                    //ide jön még egy csomó minden

                    //2 faktoros authentikáció
                    //túl sok próbálkozás miatti kitiltás kezelése
                    //stb.

                    //végül ellenőrizzük, hogy a név és jelszó megfelelő-e?
                    var isPasswordOk = await manager.CheckPasswordAsync(user, context.Request.Password);
                    if (!isPasswordOk)
                    {
                        context.Reject(
                            error: OpenIdConnectConstants.Errors.InvalidGrant,
                            description: "nem megfelelő bejelentkezési adatok"
                            );
                        return;
                    }

                    var identity = new System.Security.Claims.ClaimsIdentity(context.Options.AuthenticationScheme);
                    identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.UrlCode);
                    identity.AddClaim("username", context.Request.Username, 
                        OpenIdConnectConstants.Destinations.AccessToken
                        , OpenIdConnectConstants.Destinations.IdentityToken); //ha a jwt-ben ezt szeretnénk látni

                    //tegyük rá a tokenre a felhasználó csoporttagságát is:
                    var roles = await manager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        identity.AddClaim("role", role, 
                            OpenIdConnectConstants.Destinations.AccessToken
                            , OpenIdConnectConstants.Destinations.IdentityToken); //ha a jwt-ben ezt szeretnénk látni
                    }

                    var ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(
                        new System.Security.Claims.ClaimsPrincipal(identity)
                        ,new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties()
                        ,context.Options.AuthenticationScheme);

                    ticket.SetScopes(
                        OpenIdConnectConstants.Scopes.OpenId //ha openId jwt tokent is akarunk gyártani
                        ,OpenIdConnectConstants.Scopes.OfflineAccess //a refresh token készítéshez ezt be kell állítani
                        );



                    context.Validate(ticket);
                }

                return;
            };
        }

    }
}
