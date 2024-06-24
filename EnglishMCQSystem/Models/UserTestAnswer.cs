using System;
using System.Collections.Generic;

namespace EnglishMCQSystem.Models;

public partial class UserTestAnswer
{
    public int Id { get; set; }

    public int? UserTestId { get; set; }

    public int? QuestionId { get; set; }

    public string? UserAnswer { get; set; }

    public virtual Question? Question { get; set; }

    public virtual UserTest? UserTest { get; set; }
}
