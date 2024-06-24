using System;
using System.Collections.Generic;

namespace EnglishMCQSystem.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    public virtual ICollection<UserTestAnswer> UserTestAnswers { get; set; } = new List<UserTestAnswer>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
