﻿using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculums.Commanding.ProfileCommandImages.UpdateImage;
public class UpdateImageCommand : ICommand
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public byte[] ImageContent { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}