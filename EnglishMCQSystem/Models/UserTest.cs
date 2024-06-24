using System;
using System.Collections.Generic;

namespace EnglishMCQSystem.Models;

public partial class UserTest
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? TestId { get; set; }

    public double? Score { get; set; }

    public DateTime? TestDate { get; set; }

    public virtual Test? Test { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<UserTestAnswer> UserTestAnswers { get; set; } = new List<UserTestAnswer>();
}
