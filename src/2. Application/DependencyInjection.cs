namespace Application;

using System.Reflection;
using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    var assembly = Assembly.GetExecutingAssembly();
    services.AddMediatR(cfg => {
      cfg.
