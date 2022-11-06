﻿using System.ComponentModel.DataAnnotations;

namespace Favourites.API.Models;

public record BookmarkDto(string WebLink, string Name)
{
    public DateTime? ModificationDate { get; init; } 
    
    public string? Description { get; init; } = null;

    public string[] Tags { get; init; } = Array.Empty<string>();
    
}