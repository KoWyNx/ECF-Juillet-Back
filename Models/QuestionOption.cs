using System;
using System.Collections.Generic;

namespace ECF.Models;

public partial class QuestionOption
{
    public int OptionId { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public virtual Question? Question { get; set; }
}
