﻿using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class StarEntity : BaseEntity
{
    public string? Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhotographyURL { get; set; }

    public virtual ICollection<CastEntity> StarMovies { get; set; } = new List<CastEntity>();
}
