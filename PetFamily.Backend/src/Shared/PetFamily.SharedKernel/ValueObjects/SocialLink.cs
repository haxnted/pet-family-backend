﻿using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.ValueObjects;

public record SocialLink
{
    public string Name { get; }
    public string Url { get; }
    
    [JsonConstructor]
    private SocialLink(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public static Result<SocialLink, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.MAX_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid($"Name");

        if (string.IsNullOrWhiteSpace(url) || url.Length > Constants.EXTRA_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid($"Path");

        return new SocialLink(name, url);
    }
}