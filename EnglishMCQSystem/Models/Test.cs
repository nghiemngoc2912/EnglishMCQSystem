using System;
using System.Collections.Generic;

namespace EnglishMCQSystem.Models;

public partial class Test
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? DifficultyLevel { get; set; }

    public int? NumOfQuestions { get; set; }

    public virtual ICollection<UserTest> UserTests { get; set; } = new List<UserTest>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
