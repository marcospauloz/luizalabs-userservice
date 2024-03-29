﻿namespace luizalabs.UserService.Domain.Entities;

using Core;

public class User : Entity
{
    public string? Name { get; set; }

    public string? Email { get; set; } 

    public string? PasswordHash { get; set; }
}