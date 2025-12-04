global using Microsoft.EntityFrameworkCore;
global using Serilog;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.ComponentModel.DataAnnotations;

global using MovieReservation.API.Middleware;
global using MovieReservation.Data.Context;
global using MovieReservation.API.Extensions;
global using MovieReservation.Business.DependencyInjection;
global using MovieReservation.Business.Extensions;
global using MovieReservation.Data.DependencyInjection;
global using MovieReservation.API.Constants;
global using MovieReservation.API.DTO.Request;
global using MovieReservation.Business.Model;
global using MovieReservation.Data.Enum;
