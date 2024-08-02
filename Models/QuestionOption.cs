using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECF.Models;

public partial class QuestionOption
{
    public int OptionId { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    [JsonIgnore]
    public virtual Question? Question { get; set; }
}
