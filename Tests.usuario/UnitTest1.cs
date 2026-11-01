//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using ApparelApi.Controllers;
//using CapaEntidad.DTOs;

//namespace Tests.usuarioControllers
//{
   // public class UnitTest1
   // {
    //    [Fact]
     //   public async Task Login_ConCredencialesCorrectas_RetornaOk()
      //  {
            // ======================================================
            // 1. ARRANGE
            // ======================================================

            // Mock IUserStore
           // var userStoreMock = new Mock<IUserStore<IdentityUser>>();

            // Mock UserManager
           // var userManagerMock = new Mock<UserManager<IdentityUser>>(
            //    userStoreMock.Object,
              //  null,
                //null,
           //     null,
             //   null,
               // null,
          //      null,
           //     null,
             //   null
           // );

            // Mock IHttpContextAccessor
            //var contextAccessorMock = new Mock<IHttpContextAccessor>();

            // Mock IUserClaimsPrincipalFactory
      //      var claimsFactoryMock =
        //        new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();

            // Mock SignInManager
          //  var signInManagerMock = new Mock<SignInManager<IdentityUser>>(
            //    userManagerMock.Object,
              //  contextAccessorMock.Object,
                //claimsFactoryMock.Object,
             //   null,
               // null,
        //        null,
          //      null
            //);

            // ======================================================
            // Mock IConfiguration (JWT)
            // ======================================================

          //  var configMock = new Mock<IConfiguration>();

           // configMock.Setup(c => c["Jwt:Key"])
             //   .Returns("Pruebas123456789");


//            configMock.Setup(c => c["Jwt:Issuer"])
  //             .Returns("ApparelApi");

    //        configMock.Setup(c => c["Jwt:Audience"])
      //          .Returns("ApparelApiUsers");

            // ======================================================
            // Usuario simulado
            // ======================================================

   //         var usuarioSimulado = new IdentityUser
     //       {
       //         Email = "Masiel@gmail.com",
         //       UserName = "Masiel@gmail.com"
         //   };

            // ======================================================
            // Configuración de métodos mockeados
            // ======================================================

            // Buscar usuario
     //       userManagerMock
       //         .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
         //       .ReturnsAsync(usuarioSimulado);

            // Login correcto
           // signInManagerMock
             //   .Setup(x => x.PasswordSignInAsync(
               //     It.IsAny<string>(),
                 //   It.IsAny<string>(),
                   // false,
              //      false))
             //   .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Roles
    //        userManagerMock
      //          .Setup(x => x.GetRolesAsync(usuarioSimulado))
        //        .ReturnsAsync(new List<string> { "Admin" });

            // ======================================================
            // Instancia del controlador
            // ======================================================

  //          var controller = new UsuarioController(
    //            userManagerMock.Object,
      //          signInManagerMock.Object,
        //        configMock.Object
          //  );

            // ======================================================
            // DTO de entrada
            // ======================================================

    //        var userDTO = new UserDTO
      //      {
        //        Email = "Masiel@gmail.com",
          //      Password = "M@siel123"
            //};

            // ======================================================
            // 2. ACT
            // ======================================================

       //     var result = await controller.Login(userDTO);

            // ======================================================
            // 3. ASSERT
            // ======================================================

 //           Assert.NotNull(result);

  //          var okResult = result as OkObjectResult;

//            Assert.NotNull(okResult);

      //      Assert.Equal(200, okResult.StatusCode);
    //    }
  //  }
//}